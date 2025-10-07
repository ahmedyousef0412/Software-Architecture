
using LayeredArch.Domain.Entities;
using LayeredArch.Infrastructure.Persistence;

namespace LayeredArch.Tests;

public class OrerRepositoryTests
{

    [Fact]
    public async Task GetByIdAsync_ShouldReturnOrder_WhenOrderExists()
    {
        //Arrange
       await using var dbContext = new InMemoryDbContext();
        var orderRepository = new OrderRepository(dbContext);
        var customerRepository = new CustomerRepository(dbContext);



        var customer = new Customer("Moahmed", "Mohamed@gmail.com","0123456789", "Egypt");
        await customerRepository.AddAsync(customer);

        var customerId = customer.Id;


        var order = new Order(customerId);
        order.AddItem("Product1", 2, 10.0m);

        await orderRepository.AddAsync(order);

        var orderId = order.Id;

        //Act
        var result = await orderRepository.GetByIdAsync(orderId);


        //Assert
        //The order exists
        //The items were loaded correctly via Include.

        Assert.NotNull(result);
        Assert.Equal(orderId, result?.Id);
        Assert.Single(result!.Items);
        Assert.Equal("Product1", result.Items[0].Product);
    }
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange
        await using var dbContext = new InMemoryDbContext();
        var orderRepository = new OrderRepository(dbContext);

        var nonExistentOrderId = 999;

        // Act
        var result = await orderRepository.GetByIdAsync(nonExistentOrderId);

        // Assert
        Assert.Null(result); 
    }


    [Fact]
    public async Task AddAsync_ShouldAddOrderSuccessfully()
    {
        // Arrange
       await using var dbContext = new InMemoryDbContext();
        var orderRepository = new OrderRepository(dbContext);
        var customerRepository = new CustomerRepository(dbContext);

        var customer = new Customer("Moahmed", "Mohamed@gmail.com", "0123456789", "Egypt");
        await customerRepository.AddAsync(customer);

        var customerId = customer.Id;

        var order = new Order(customerId);

        order.AddItem("Product1", 2, 10.0m);

        // Act
        await orderRepository.AddAsync(order);

        // Assert
        var addedOrder = await orderRepository.GetByIdAsync(order.Id);
        Assert.NotNull(addedOrder);
        Assert.Equal(order.Id, addedOrder?.Id);
        Assert.Equal(customer.Id, addedOrder?.CustomerId);
        Assert.Single(addedOrder!.Items);
        Assert.Equal("Product1", addedOrder.Items[0].Product);
    }


    [Fact]
    public void Constructor_ShouldThrowException_WhenCustomerIdIsInvalid()
    {
        // Arrange
        var invalidCustomerId = -1;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Order(invalidCustomerId));
        Assert.Equal("customerId", ex.ParamName);
    }

}

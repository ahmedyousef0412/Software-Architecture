
using LayeredArch.Application.Orders;
using LayeredArch.Domain.Entities;
using FakeItEasy;
using LayeredArch.Application.Interfaces;

namespace LayeredArch.Tests.OrderHandlers;

public class GetOrderByIdHandlerTest
{

   
    [Fact]
    public async Task HandleAsync_ShouldReturnOrderDto_WhenOrderIdIsExists()
    {
        //Arrange
        var orderRepository = A.Fake<IOrderRepository>();
        var handler = new GetOrderByIdHandler(orderRepository);

        //Must add a customer first because of the foreign key constraint
        var customer = new Customer("Omer","Omer@gmail.com","0123456789","Egypt");

        typeof(Customer).GetProperty(nameof(Customer.Id))!
            .SetValue(customer, 1);

        //Must add an order first to be able to get it and pass customerId to it
        var order = new Order(customer.Id);

        typeof(Order).GetProperty(nameof(Order.Id))!.SetValue(order, 1);

        typeof(Order).GetProperty(nameof(Order.Customer))!.SetValue(order, customer);

        order.AddItem("Laptop", 1, 1200m);


        var query = new GetOrderByIdQuery(order.Id);

        A.CallTo(() => orderRepository.GetByIdAsync(query.Id, A<CancellationToken>._))
            .Returns(Task.FromResult<Order?>(order));


        //Act
        var result = await handler.HandleAsync(query);

        //Assert
        AssertOrderDtoMatches(result, customer, order);
    }


    [Fact]
    public async Task HandleAsync_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        //Arrange
        var orderRepository = A.Fake<IOrderRepository>();

        var handler = new GetOrderByIdHandler(orderRepository);

        var query = new GetOrderByIdQuery(1);

        A.CallTo(() => orderRepository.GetByIdAsync(query.Id, A<CancellationToken>._))
            .Returns(Task.FromResult<Order?>(null));

        //Act
        var result = await handler.HandleAsync(query);

        //Assert
        Assert.Null(result);
    }



    private static void AssertOrderDtoMatches(OrderDto? result , Customer expectedCustomer, Order expectedOrder)
    {
        Assert.NotNull(result);
        Assert.Equal(expectedOrder.Id, result?.Id);
        Assert.Equal(expectedCustomer.Id, result?.Customer.Id);
        Assert.Equal(expectedCustomer.Name, result?.Customer.Name);
        Assert.Equal(expectedCustomer.Phone, result?.Customer.Phone);
        Assert.Equal(expectedCustomer.Email, result?.Customer.Email);
        Assert.Equal(expectedCustomer.Address, result?.Customer.Address);
        Assert.Equal(expectedOrder.Items.Count, result!.Items.Count);
    }
}

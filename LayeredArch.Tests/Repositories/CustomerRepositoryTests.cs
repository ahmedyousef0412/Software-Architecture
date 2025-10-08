using LayeredArch.Domain.Entities;
using LayeredArch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LayeredArch.Tests.Repositories;

public class CustomerRepositoryTests
{

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCustomers()
    {
        // Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);

        var customer1 = new Customer("Moahmed", "Safwat@gmail.com", "0123456789", "Egypt");
        var customer2 = new Customer("Raid", "Raid@gmail.com", "0123456789", "Mansoura");
        await sut.AddAsync(customer1);
        await sut.AddAsync(customer2);


        //Act

        var result = await sut.GetAllAsync();

        //Assert

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        //Assert.Contains(result, c => c.Email == "Safwat@gmail.com");
        //Assert.Contains(result, c => c.Email == "Raid@gmail.com");


    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoCustomersExist()
    {
        // Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);

        // Act
        var result = await sut.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCustomer_WhenCustomerExists()
    {
        //Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);
        var customer = new Customer("Moahmed", "Safwat@gmail.com", "0123456789", "Egypt");


        await sut.AddAsync(customer);


        //Act
        var result = await sut.GetByIdAsync(customer.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Moahmed", result!.Name);
        Assert.Equal(customer.Email, result.Email);
        Assert.Equal(customer.Phone, result.Phone);
        Assert.Equal(customer.Address, result.Address);

    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenCustomerDoesNotExist()
    {
        //Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);

        //Act
        var result = await sut.GetByIdAsync(999); // Assuming 999 does not exist

        //Assert
        Assert.Null(result);
    }


    [Fact]
    public async Task GetByEmailAsync_ShouldReturnTrue_WhenCustomerExists()
    {
        //Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);
        var customer = new Customer("Omer", "Omer@gmail.com", "0123456789", "Egypt");
        await sut.AddAsync(customer);


        //Act
        var result = await sut.ExistsByEmailAsync("Omer@gmail.com");


        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnFalse_WhenInvalidEmailProvided()
    {
        //Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);
        var customer = new Customer("Omer", "Omer@gmail.com", "0123456789", "Egypt");

        await sut.AddAsync(customer);


        //Act
        var result = await sut.ExistsByEmailAsync("InValid@gmail.com");

        //Assert
        Assert.False(result);

    }


    [Fact]
    public async Task IsExistAsync_ShouldReturnTrue_WhenCustomerExists()
    {
        //Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);
        var customer = new Customer("Yousef", "Yousef@gmail.com", "0123456789", "Egypt");

        await sut.AddAsync(customer);

        //Act
        var result = await sut.ExistsAsync(customer.Id);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsExistAsync_ShouldReturnFalse_WhenCustomerDoesNotExist()
    {
        // Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);

        // Act
        var result = await sut.ExistsAsync(999); 

        // Assert
        Assert.False(result);
    }


    [Fact]
    public async Task AddCustomer_ShouldAddCustomer_WhenValidCustomerProvided()
    {
        // Arrange
       using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);
        var customer = new Customer("Moahmed", "Safwat@gmail.com", "0123456789", "Egypt");


        //Act
        await sut.AddAsync(customer);


        //Assert
        var result = await dbContext.Customers.FirstOrDefaultAsync(c => c.Email == "Safwat@gmail.com");
        Assert.NotNull(result);
        Assert.Equal("Moahmed", result!.Name);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldUpdateCustomer_WhenValidCustomerProvided()
    {
        // Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);
        var customer = new Customer("Loai", "Loai@gmail.com", "0123456789", "Egypt");
        await sut.AddAsync(customer);

        //Act
       customer.UpdateDetails("Loai Mohamed", "Loai@gmail.com", "0123456789", "Egypt");
        await sut.UpdateAsync(customer);

        //Assert
        var result = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
        Assert.NotNull(result);
        Assert.Equal(customer.Id, result!.Id);//ensure I updating the same entity, not creating a new one
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrow_WhenCustomerIsNull()
    {
        // Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => sut.UpdateAsync(null!));
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveCustomer_WhenCustomerExists()
    {
        // Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);
        var customer = new Customer("Ahmed", "Ahmed@gmail.com", "0123456789", "Egypt");
        await sut.AddAsync(customer);

        //Act
        await sut.DeleteAsync(customer.Id);

        var result = await dbContext.Customers.FindAsync(customer.Id); //Should be null after deletion

        //Assert
        Assert.Null(result);

    }

    [Fact]
    public async Task DeleteAsync_ShouldDoNothing_WhenCustomerDoesNotExist()
    {
        // Arrange
        using var dbContext = new InMemoryDbContext();
        var sut = new CustomerRepository(dbContext);


        //Act
        await sut.DeleteAsync(999); 

        //Assert
        var customers = await dbContext.Customers.ToListAsync();
        Assert.Empty(customers); // Ensure no customers were deleted (since none existed)
    }

}

using LayeredArch.Domain.Entities;

namespace LayeredArch.Tests.Domains;

public class CustomerTests
{
    [Fact]
    public void Constructor_ShouldCreateCustomer_WhenValidDetialsProvided()
    {
        // Arrange
        string name = "Mahmoud Maged";
        string email = "mego@gmail.com";

        string phone = "0123456789";
        string address = "Egypt";

        // Act (sut = System Under Test)
        var sut = new Customer(name, email, phone, address);

        // Assert
        Assert.Equal(name, sut.Name);
        Assert.Equal(email, sut.Email);
        Assert.Equal(phone, sut.Phone);
        Assert.Equal(address, sut.Address);
    }

    [Theory]
    [InlineData("", "ahmed@example.com", "12345", "Cairo")]
    [InlineData("Ahmed", "invalid-email", "12345", "Cairo")]
    [InlineData("Ahmed", "ahmed@example.com", "", "Cairo")]
    [InlineData("Ahmed", "ahmed@example.com", "12345", "")]
    public void Constructor_ShouldThrowArgumentException_WhenInvalidDetailsProvided(string name, string email, string phone, string address)
    {
        // Arrange (handled by InlineData)

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Customer(name, email, phone, address));
    }

    [Fact]
    public void UpdateDetials_ShouldUpdateCustomerDetails_WhenValidDetailsProvided()
    {
        // Arrange
        var customer = new Customer("Old Name", "OldEmail@gmail.com", "0123456789", "Old Address");

        // Act
        customer.UpdateDetails("New Name", "NewEmail@gmail.com", "9876543210", "New Address");

        // Assert
        Assert.Equal("New Name", customer.Name);
        Assert.Equal("NewEmail@gmail.com", customer.Email);
        Assert.Equal("9876543210", customer.Phone);
        Assert.Equal("New Address", customer.Address);
    }

    [Theory]
    [InlineData("", "ahmed@example.com", "12345", "Cairo")]
    [InlineData("Ahmed", "invalid-email", "12345", "Cairo")]
    [InlineData("Ahmed", "ahmed@example.com", "", "Cairo")]
    [InlineData("Ahmed", "ahmed@example.com", "12345", "")]
    public void UpdateDetails_ShouldThrow_WhenInvalidDetails(string name, string email, string phone, string address)
    {
        // Arrange
        var customer = new Customer("ValidName", "ValidEmail@example.com", "555", "Valid Address");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => customer.UpdateDetails(name, email, phone, address));
    }

    [Fact]
    public void AddOrder_ShouldAddOrder_WhenValidOrder()
    {
        // Arrange
        var customer = new Customer("Ahmed", "ahmed@example.com", "12345", "Cairo");
        var order = new Order(customerId: 1);

        // Act
        customer.AddOrder(order);

        // Assert
        Assert.Single(customer.Orders); //checks that the customer has exactly one order
        Assert.Contains(order, customer.Orders);
    }

    [Fact]
    public void AddOrder_ShouldThrow_WhenOrderIsNull()
    {
        // Arrange
        var customer = new Customer("Ahmed", "ahmed@example.com", "12345", "Cairo");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => customer.AddOrder(null!));
    }
}

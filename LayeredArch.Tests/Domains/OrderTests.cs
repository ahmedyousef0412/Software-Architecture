using LayeredArch.Domain.Entities;

namespace LayeredArch.Tests.Domains;

public class OrderTests
{
    [Fact]
    public void Constructor_ShouldCreateOrder_WhenValidCustomerIdProvided()
    {
        //Arrange
        var customerId = 1;

        //Act (sut = System Under Test)

        var sut = new Order(customerId);

        //Assert
        Assert.Equal(customerId, sut.CustomerId);
        Assert.Equal("Draft", sut.Status);
        Assert.Empty(sut.Items);
        Assert.True(sut.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenInvalidCustomerIdProvided()
    {
        //Arrange
        var customerId = 0;
        //Act & Assert
        Assert.Throws<ArgumentException>(() => new Order(customerId));
    }

    [Fact]
    public void AddItem_ShouldAddItem_WhenValidDetailsProvided()
    {

        var customerId = 1;
        string product = "Product A";
        int quantity = 2;
        decimal price = 10.5m;


        var order = new Order(customerId);
        order.AddItem(product, quantity, price);


        Assert.Single(order.Items); //If the collection has 0 or more than 1 elements — the test will fail.

        var addedItem = order.Items.First();

        Assert.Equal(product, addedItem.Product);
        Assert.Equal(quantity, addedItem.Quantity);
        Assert.Equal(price, addedItem.Price);

    }


    //This will run the test multiple times with different invalid product values
    [Theory]
    [InlineData("")] //empty string
    [InlineData(" ")] //white space
    [InlineData(null)]

    public void AddItem_ShouldThrowArgumentException_WhenInvalidProduct(string product)
    {
        //Arrange 
        var order = new Order(1);
        int quantity = 2;
        decimal price = 10.5m;

        //Act & Assert

        var exception = Assert.Throws<ArgumentException>(() => order.AddItem(product, quantity, price));


    }

    [Theory]
    [InlineData(0)] //zero quantity
    [InlineData(-1)] //negative quantity
    public void AddItem_ShouldThrowArgumentException_WhenInvalidQuantity(int quantity)
    {
        //Arrange 
        var order = new Order(1);
        string product = "Product A";
        decimal price = 10.5m;


        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => order.AddItem(product, quantity, price));
    }

    [Fact]
    public void AddItem_ShouldThrowArgumentException_WhenInvalidPrice()
    {
        //Arrange 
        var order = new Order(1);
        string product = "Product A";
        int quantity = 2;

        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => order.AddItem(product, quantity, -100m));
    }


    [Fact]
    public void RemoveItem_ShouldRemoveItem_WhenValidItemIdProvided()
    {

        //Must add an item first to be able to remove it

        //Arrange
        var order = new Order(1);
        var item = new OrderItem("Product A", 2, 10.5m);

        typeof(OrderItem).GetProperty(nameof(OrderItem.Id))!
            .SetValue(item, 1);

        order.Items.Add(item);

        //Act
        order.RemoveItem(1);

        //Assert
        Assert.Empty(order.Items);

    }

    [Fact]
    public void RemoveItem_ShouldThrowArgumentException_WhenInvalidItemIdProvided()
    {
        //Arrange
        var orer = new Order(1);
        var item = new OrderItem("Product A", 2, 10.5m);

        typeof(OrderItem).GetProperty(nameof(OrderItem.Id))!
            .SetValue(item, 1); //Manually setting the Id for testing purposes

        orer.Items.Add(item);

        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => orer.RemoveItem(2));
    }


    [Fact]
    public void UpdateItemQuantity_ShouldUpdateQuantity_WhenItemIdAndNewQuantityProvided()
    {
        //Arrange
        var order = new Order(1);
        var item = new OrderItem("Product A", 2, 10.5m);

        typeof(OrderItem).GetProperty(nameof(OrderItem.Id))!
            .SetValue(item, 1); //Manually setting the Id for testing purposes

        order.Items.Add(item);

        //Act
        order.UpdateItemQuantity(itemId: 1, newQuantity: 5);

        //Assert
        Assert.Equal(5, order.Items.First().Quantity); //will get the first item in the list and check its quantity is 5
    }

    [Fact]
    public void UpdateItemQuantity_ShouldThrowArgumentException_WhenInvalidItemIdProvided()
    {
        //Arrange
        var order = new Order(1);
        var item = new OrderItem("Product A", 2, 10.5m);
        typeof(OrderItem).GetProperty(nameof(OrderItem.Id))!
            .SetValue(item, 1); //Manually setting the Id for testing purposes

        order.Items.Add(item);

        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => order.UpdateItemQuantity(itemId: 2, newQuantity: 5));
    }

    [Theory]
    [InlineData(0)] //zero quantity
    [InlineData(-1)] //negative quantity
    public void UpdateItemQuantity_ShouldThrowArgumentException_WhenInvalidNewQuantityProvided(int newQuantity)
    {
        //Arrange
        var order = new Order(1);
        var item = new OrderItem("Product A", 2, 10.5m);
        typeof(OrderItem).GetProperty(nameof(OrderItem.Id))!
            .SetValue(item, 1); //Manually setting the Id for testing purposes


        order.Items.Add(item);

        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => order.UpdateItemQuantity(itemId: 1, newQuantity));
    }

    [Fact]
    public void SetStatus_ShouldUpdateStatus_WhenValidStatusProvided()
    {
        //Arrange
        var order = new Order(1);
        var newStatus = "Confirmed";

        //Act
        order.SetStatus(newStatus);

        //Assert
        Assert.Equal(newStatus, order.Status);
    }

    [Theory]
    [InlineData("")] //empty string
    [InlineData(" ")] //white space
    [InlineData(null)]
    public void SetStatus_ShouldThrowArgumentException_WhenInvalidStatusProvided(string status)
    {
        //Arrange
        var order = new Order(1);
        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => order.SetStatus(status));
    }
}
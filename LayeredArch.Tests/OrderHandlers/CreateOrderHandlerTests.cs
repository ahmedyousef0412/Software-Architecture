
using FakeItEasy;
using LayeredArch.Application.Interfaces;
using LayeredArch.Application.Orders;
using LayeredArch.Domain.Entities;
namespace LayeredArch.Tests.OrderHandlers;

public class CreateOrderHandlerTests
{

    [Fact]
    public async Task HandleAsync_ShouldReturnOrderId_WhenOrderCreatedSuccssfully()
    {
        //Arrange
        var fakedOrderRepository = A.Fake<IOrderRepository>();

        A.CallTo(() => fakedOrderRepository.AddAsync(A<Order>._, A<CancellationToken>._))
            .Invokes((Order o, CancellationToken _) =>
            {
                 o.GetType()
                .GetProperty(nameof(o.Id))!
                .SetValue(o, 1);
            }).Returns(Task.CompletedTask);


        var command = new CreateOrderCommand
        {
            CustomerId = 20,
            Items =
            [
                new(){Product ="Keyboard",Quantity = 2 , Price = 50m},
                new(){Product ="Mouse",Quantity = 5 , Price = 100m},
                new(){Product ="Screen",Quantity = 2 , Price = 1000m},
            ]
        };

        var sut = new CreateOrderHandler(fakedOrderRepository);

        //Act

        var result = await sut.HandleAsync(command);


        //Assert
        A.CallTo(() => fakedOrderRepository.AddAsync(A<Order>._, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();

        Assert.Equal(1, result);
    }

    [Fact]
    public async Task HandleAsync_ShouldThrowException_WhenCustomerIdIsInvalid()
    {
        // Arrange
        var fakedOrderRepository = A.Fake<IOrderRepository>();
        var sut = new CreateOrderHandler(fakedOrderRepository);

        var invalidCommand = new CreateOrderCommand
        {
            CustomerId = -5,
            Items = []
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => sut.HandleAsync(invalidCommand));
    }
}
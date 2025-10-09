
using FakeItEasy;
using LayeredArch.Application.Customers;
using LayeredArch.Application.Interfaces;
using LayeredArch.Domain.Entities;
namespace LayeredArch.Tests.CustommerHandlers;

public class CreateCustomerHandlerTests
{

    [Fact]
    public async Task HandleAsync_ShouldCreateCustomer_AndReturnCustomerId()
    {
        //Arrang
        var fakeCustomerRepository = A.Fake<ICustomerRepository>();
        

        var handler = new CreateCustomerHandler(fakeCustomerRepository);

        var command = new CreateCustomerCommand
            (Name: "Shady", Email: "Shady@gmail.com", Address: "Mansoura", Phone: "0123456789");

        A.CallTo(() => fakeCustomerRepository.AddAsync(A<Customer>._, A<CancellationToken>._))
            .Invokes((Customer c, CancellationToken _) =>
            {
                c.GetType()
               .GetProperty(nameof(c.Id))!
               .SetValue(c, 1);
            }).Returns(Task.CompletedTask);


        //Act
        var result  = await handler.HandleAsync(command);

        //Assert
        A.CallTo(() => fakeCustomerRepository.AddAsync(A<Customer>._,A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();

        Assert.Equal(1,result);


    }

    [Fact]
    public async Task HandleAsync_ShouldThrowArgumentException_WhenInvalidEmailProvided()
    {
        // Arrange
        var fakeCustomerRepository = A.Fake<ICustomerRepository>();
        var handler = new CreateCustomerHandler(fakeCustomerRepository);

        var command = new CreateCustomerCommand
         (Name: "Shady", Email: "InvalidEmail", Address: "Mansoura", Phone: "0123456789");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await handler.HandleAsync(command);
        });

        // Ensure repository was never called
        A.CallTo(() => fakeCustomerRepository.AddAsync(A<Customer>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }
}

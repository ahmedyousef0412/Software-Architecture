using LayeredArch.Application.Interfaces;
using LayeredArch.Domain.Entities;

namespace LayeredArch.Application.Orders;

public class CreateOrderHandler(IOrderRepository orderRepository)
{

    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<int> HandleAsync(CreateOrderCommand command,CancellationToken cancellationToken = default)
    {
        var order = new Order(command.CustomerId);

        foreach (var item in command.Items)
            order.AddItem(item.Product, item.Quantity, item.Price);

        await _orderRepository.AddAsync(order,cancellationToken);
        return order.Id;
    }

    #region Bad Implementation ( Put business rules in the Application layer instead of the Domain)
    //public async Task<int> HandleAsync(CreateOrderCommand command)
    //{

    //    // Business rule check inside Application handler
    //    if (command.Items is null || command.Items.Count == 0) 
    //        throw new ArgumentException("Order must have at least one item.", nameof(command.Items));


    //    if (command.Items.Count > 50)
    //        throw new InvalidOperationException("An order cannot have more than 50 items");

    //    var order = new Order(command.Customer);

    //    foreach (var item in command.Items)
    //    {
    //        if(item.Quantity <= 0)
    //            throw new ArgumentException("Quantity must be greater than zero.", nameof(item.Quantity));


    //        //Another business rule check here
    //        if (item.Price < 0)
    //            throw new ArgumentException("Price cannot be negative.", nameof(item.Price));


    //        //  Directly manipulating Items instead of using Order.AddItem
    //        order.Items.Add(new OrderItem( item.Product, item.Quantity, item.Price));
    //    }


    //    await _orderRepository.AddAsync(order);
    //    return order.Id;
    //}

    #endregion
}

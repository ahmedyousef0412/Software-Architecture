using LayeredArch.Application.Customers;
using LayeredArch.Application.Interfaces;

namespace LayeredArch.Application.Orders;

public class GetOrderByIdHandler(IOrderRepository orderRepository)
{

    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<OrderDto?> HandleAsync(GetOrderByIdQuery query)
    {
        var order = await _orderRepository.GetByIdAsync(query.Id);

        if (order is null)
            return null;

        return new OrderDto
        {
            Id = order.Id,
            Customer = new CustomerDto
            {
                Id = order.Customer.Id,
                Name = order.Customer.Name,
                Email = order.Customer.Email,
                Phone = order.Customer.Phone,
                Address = order.Customer.Address
            },
            Items = [.. order.Items.Select(i => new OrderItemDto
            {
                Product = i.Product,
                Quantity = i.Quantity,
                Price = i.Price
            })]
        };
    }

}

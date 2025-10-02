
using LayeredArch.Application.Customers;

namespace LayeredArch.Application.Orders;

public class OrderDto
{
    public int Id { get; set; }
    public CustomerDto Customer { get; set; } 
    public List<OrderItemDto> Items { get; set; } = [];
}

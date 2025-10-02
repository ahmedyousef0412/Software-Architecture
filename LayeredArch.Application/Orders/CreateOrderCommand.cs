namespace LayeredArch.Application.Orders;

public class CreateOrderCommand
{
    public int CustomerId { get; set; } 
    public List<OrderItemDto> Items { get; set; } = [];
}

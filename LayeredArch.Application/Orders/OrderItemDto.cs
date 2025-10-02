
namespace LayeredArch.Application.Orders;

public class OrderItemDto
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

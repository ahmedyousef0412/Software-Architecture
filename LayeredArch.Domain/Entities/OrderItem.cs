
namespace LayeredArch.Domain.Entities;

public class OrderItem
{

    private OrderItem() { } // For EF Core

    public int Id { get; private set; }
    public string Product { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public OrderItem(string product, int quantity, decimal price)
    {
        if (string.IsNullOrWhiteSpace(product))
            throw new ArgumentException("Product cannot be empty.");
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.");

        Product = product;
        Quantity = quantity;
        Price = price;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));
        Quantity = newQuantity;
    }
}

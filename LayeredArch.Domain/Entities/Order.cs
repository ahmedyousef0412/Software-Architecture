
namespace LayeredArch.Domain.Entities;

public class Order
{
    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public DateTime CreatedAt { get; private set; } 
    public string Status { get; private set; }
    public List<OrderItem> Items { get; private set; } = [];

    public Order(int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException("Invalid Customer Id", nameof(customerId));

        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
        Status = "Draft";
        Items = [];
    }

    public void AddItem(string product, int quantity, decimal price)
    {
        if (string.IsNullOrWhiteSpace(product))
            throw new ArgumentException("Product name cannot be empty.", nameof(product));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        if (price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(price));


        Items.Add(new OrderItem(product, quantity, price));
    }

    public void RemoveItem(int itemId)
    {
        var item = Items.SingleOrDefault(i => i.Id == itemId) 
            ?? throw new ArgumentException("Item not found in the order.", nameof(itemId));

        Items.Remove(item);
    }
    public void UpdateItemQuantity(int itemId, int newQuantity)
    {
        var item = Items.SingleOrDefault(i => i.Id == itemId) 
            ?? throw new ArgumentException("Item not found in the order.", nameof(itemId));

        item.UpdateQuantity(newQuantity);
    }
    public void SetStatus(string status)
    {
        if(string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be empty.", nameof(status));

        Status = status;
    }
}

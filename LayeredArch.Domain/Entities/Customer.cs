
namespace LayeredArch.Domain.Entities;

public class Customer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; } 
    public string Phone { get; private set; } 
    public string Address { get; private set; }

    private readonly List<Order> _orders = [];  
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

   
    public Customer(string name, string email, string phone, string address)
    {
        UpdateDetails(name, email, phone, address);
    }

    public void UpdateDetails(string name, string email, string phone, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Invalid email address.", nameof(email));

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone cannot be empty.", nameof(phone));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be empty.", nameof(address));

        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
    }
    public void AddOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        _orders.Add(order);
    }
}

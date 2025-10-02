using LayeredArch.Application.Interfaces;
using LayeredArch.Domain.Entities;


namespace LayeredArch.Application.Customers;

public class CreateCustomerHandler(ICustomerRepository customerRepository)
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<int> HandleAsync(CreateCustomerCommand command,CancellationToken cancellationToken = default)
    {
        var customer = new Customer (command.Name, command.Email, command.Phone, command.Address);

        await _customerRepository.AddAsync(customer,cancellationToken);
        return customer.Id;
    }

}

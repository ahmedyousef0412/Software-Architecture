using LayeredArch.Domain.Entities;
namespace LayeredArch.Application.Interfaces;

public interface ICustomerRepository
{
    // Prefer not to expose domain entities directly from repositories.
    // Instead, return DTOs or read models for queries to avoid accidental
    // Will Change in future to return CustomerDto


    Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);  
    Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default);

 
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);

    
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

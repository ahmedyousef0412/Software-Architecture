using LayeredArch.Application.Interfaces;
using LayeredArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace LayeredArch.Infrastructure.Persistence;

public class CustomerRepository(ApplicationDbContext context) : ICustomerRepository
{


    //Will Use UOW pattern in future

    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Customers.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email,cancellationToken);
    }

    public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
    }
    

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.AnyAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
       return await _context.Customers.AnyAsync(c => c.Email == email, cancellationToken: cancellationToken);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _context.Customers.AddAsync(customer,cancellationToken);
       await _context.SaveChangesAsync(cancellationToken);

    }
    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(customer);
        _context.Customers.Update(customer);
       await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(int id,CancellationToken cancellationToken = default)
    {
        var customer = await _context.Customers.FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken);

        if (customer is not null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }

}

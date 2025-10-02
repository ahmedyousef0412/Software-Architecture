
using LayeredArch.Application.Interfaces;
using LayeredArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LayeredArch.Infrastructure.Persistence;

public class OrderRepository(ApplicationDbContext context) : IOrderRepository
{

    //Will Use UOW pattern in future
    private readonly ApplicationDbContext _context = context;
    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}


using LayeredArch.Domain.Entities;

namespace LayeredArch.Application.Interfaces;

public interface IOrderRepository
{

    // Prefer not to expose domain entities directly from repositories.
    // Instead, return DTOs or read models for queries to avoid accidental
    // Will Change in future to return Order Dto
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(int id , CancellationToken cancellationToken = default);
}

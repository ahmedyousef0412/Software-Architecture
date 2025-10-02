using LayeredArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace LayeredArch.Infrastructure.Persistence;


public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Customer> Customers => Set<Customer>();
}

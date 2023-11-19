using DatabasteknikG.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabasteknikG.Contexts;

internal class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
    public DbSet<PricingUnitEntity> PricingUnits { get; set; }
}
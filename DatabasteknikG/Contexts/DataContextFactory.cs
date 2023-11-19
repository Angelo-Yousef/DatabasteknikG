using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DatabasteknikG.Contexts;

internal class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(@"Data Source=ANGELO\SQLEXPRESS;Initial Catalog=Databasteknik_db;Integrated Security=True;TrustServerCertificate=True");
        return new DataContext(optionsBuilder.Options);
    }
}
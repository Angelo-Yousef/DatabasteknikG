using DatabasteknikG.Contexts;
using DatabasteknikG.Menus;
using DatabasteknikG.Repositories;
using DatabasteknikG.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DatabasteknikG;

internal class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=ANGELO\SQLEXPRESS;Initial Catalog=Databasteknik_db;Integrated Security=True;TrustServerCertificate=True"));

        // Repositories
        services.AddScoped<AddressRepository>();
        services.AddScoped<CustomerRepository>();
        services.AddScoped<PricingUnitRepository>();
        services.AddScoped<ProductCategoryRepository>();
        services.AddScoped<ProductRepository>();

        // Services
        services.AddScoped<CustomerService>();
        services.AddScoped<ProductService>();

        // Menus
        services.AddScoped<CustomerMenu>();
        services.AddScoped<ProductMenu>();
        services.AddScoped<MainMenu>();


        var sp = services.BuildServiceProvider();
        var mainMenu = sp.GetRequiredService<MainMenu>();
        await mainMenu.StartAsync();
    }
}
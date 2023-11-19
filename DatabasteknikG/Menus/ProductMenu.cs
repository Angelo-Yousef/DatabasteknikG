using DatabasteknikG.Entities;
using DatabasteknikG.Models;
using DatabasteknikG.Services;
using Microsoft.EntityFrameworkCore;

namespace DatabasteknikG.Menus;

internal class ProductMenu
{
    private readonly ProductService _productService;

    public ProductMenu(ProductService productService)
    {
        _productService = productService;
    }

    public async Task ManageProducts()
    {
        Console.Clear();
        Console.WriteLine("Hantera Produkter");
        Console.WriteLine("1. Visa alla Produkter");
        Console.WriteLine("2. Lägg till Produkt");
        Console.WriteLine("3. Visa alla Pricing Units");
        Console.WriteLine("4. Uppdatera Produkt");
        Console.WriteLine("5. Radera Produkt");



        Console.Write("Välj ett alternativ: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await ListAllAsync();
                break;

            case "2":
                await CreateAsync();
                break;

            case "3":
                await ListAllPricingUnitsAsync();
                break;

            case "4":
                await UpdateAsync();
                break;

            case "5":
                await DeleteAsync();
                break;

        }
    }

    public async Task ListAllAsync()
    {
        Console.Clear();

        var products = await _productService.GetAllAsync();

        if (products.Any())
        {
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductName} ({product.ProductCategory.CategoryName})");
                Console.WriteLine($"{product.ProductPrice} ({product.PricingUnit.Unit})");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("Inga produkter hittades.");
        }

        Console.ReadKey();
    }



    public async Task CreateAsync()
    {
        var form = new ProductRegistrationForm();

        Console.Clear();
        Console.Write("Produkt Namn: ");
        form.ProductName = Console.ReadLine()!;

        Console.Write("Produkt Beskrivning: ");
        form.ProductDescription = Console.ReadLine()!;

        Console.Write("Produkt Kategori: ");
        form.ProductCategory = Console.ReadLine()!;

        Console.Write("Produkt Pris (SEK): ");
        form.ProductPrice = decimal.Parse(Console.ReadLine()!);

        Console.Write("Pricing Unit (st/pkt/tim): ");
        form.PricingUnit = Console.ReadLine()!;

        var result = await _productService.CreateProductAsync(form);
        if (result) 
        {
            Console.WriteLine("Produkt Skapad.");

        }
        else 
        {
            Console.WriteLine("Gick inte att skapa Produkt");
        }

        Console.ReadKey();

    }

    public async Task ListAllPricingUnitsAsync()
    {
        Console.Clear();

        var units = await _productService.GetAllPricingUnitsAsync();
        foreach (var unit in units)
        {
            Console.WriteLine($"{unit.Unit}");
            Console.WriteLine("");
        }

        Console.ReadKey();
    }
    public async Task UpdateAsync()
    {
        Console.Clear();
        Console.Write("Ange namn på produkt som ska uppdateras: ");
        var productName = Console.ReadLine();

        var existingProduct = await _productService.GetProductByNameAsync(productName);

        if (existingProduct != null)
        {
            Console.Write("Nytt Produkt Namn: ");
            var newProductName = Console.ReadLine();

            Console.Write("Produkt Beskrivning: ");
            var newProductDescription = Console.ReadLine();

            Console.Write("Produkt Kategori: ");
            var newProductCategory = Console.ReadLine();

            Console.Write("Produkt Pris (SEK): ");
            var newProductPriceString = Console.ReadLine();
            var newProductPrice = string.IsNullOrWhiteSpace(newProductPriceString) ? existingProduct.ProductPrice : decimal.Parse(newProductPriceString);

            Console.Write("Pricing Unit (st/pkt/tim): ");
            var newPricingUnit = Console.ReadLine();

            var result = await _productService.UpdateProductAsync(productName, newProductName, newProductDescription, newProductCategory, newProductPrice, newPricingUnit);

            if (result)
            {
                Console.WriteLine("Produkt Uppdaterad.");
            }
            else
            {
                Console.WriteLine("Gick inte att uppdatera produkt.");
            }
        }
        else
        {
            Console.WriteLine("Produkt hittades ej.");
        }

        Console.ReadKey();
    }

    public async Task DeleteAsync()
    {
        Console.Clear();

        Console.Write("Ange namn på produkt som ska raderas: ");
        var productName = Console.ReadLine();

        var result = await _productService.DeleteProductAsync(productName);

        if (result)
            Console.WriteLine("Produkt Raderad.");
        else
            Console.WriteLine("Produkt gick inte att radera.");

        Console.ReadKey();
    }



}
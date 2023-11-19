using DatabasteknikG.Models;
using DatabasteknikG.Services;

namespace DatabasteknikG.Menus;

internal class CustomerMenu
{
    private readonly CustomerService _customerService;

    public CustomerMenu(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task ManageCustomers()
    {
        Console.Clear();
        Console.WriteLine("Hantera Kunder");
        Console.WriteLine("1. Visa Alla Kunder");
        Console.WriteLine("2. Lägg till Kund");
        Console.WriteLine("3. Uppdatera Kund");
        Console.WriteLine("4. Radera Kund");
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
                await UpdateAsync();
                break;

            case "4":
                await DeleteByEmailAsync();
                break;
        }
    }


    public async Task ListAllAsync()
    {
        Console.Clear();

        var customers = await _customerService.GetAllAsync();

        if (customers.Any())
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName} {customer.Email}");
                Console.WriteLine($"{customer.Address.StreetName}, {customer.Address.PostalCode} {customer.Address.City}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("Inga kunder hittades.");
        }

        Console.ReadKey();
    }


    public async Task CreateAsync()
    {
        var form = new CustomerRegistrationForm();

        Console.Clear();
        Console.Write("Förnamn: ");
        form.FirstName = Console.ReadLine()!;

        Console.Write("Efternamn: ");
        form.LastName = Console.ReadLine()!;

        Console.Write("Email: ");
        form.Email = Console.ReadLine()!;

        Console.Write("Adress: ");
        form.StreetName = Console.ReadLine()!;

        Console.Write("Post Nummer (xxx xx): ");
        form.PostalCode = Console.ReadLine()!;

        Console.Write("Stad: ");
        form.City = Console.ReadLine()!;

        var result = await _customerService.CreateCustomerAsync(form);
        if (result)
            Console.WriteLine("Kund skapad!");
        else
            Console.WriteLine("Gick inte att skapa Kund");

        Console.ReadKey();
    }

    public async Task UpdateAsync()
    {
        Console.Clear();
        Console.Write("Ange email på Kund: ");
        var email = Console.ReadLine();

        var existingCustomer = await _customerService.GetCustomerByEmailAsync(email);

        if (existingCustomer == null)
        {
            Console.WriteLine($"Kund med email {email} hittades ej.");
            return;
        }

        Console.Write($"Nuvarande Förnamn: {existingCustomer.FirstName}. Ange nytt (Enter för att behålla nuvarande): ");
        var newFirstName = Console.ReadLine();
        existingCustomer.FirstName = string.IsNullOrWhiteSpace(newFirstName) ? existingCustomer.FirstName : newFirstName;

        Console.Write($"Nuvarande Efternamn: {existingCustomer.LastName}. Ange nytt (Enter för att behålla nuvarande): ");
        var newLastName = Console.ReadLine();
        existingCustomer.LastName = string.IsNullOrWhiteSpace(newLastName) ? existingCustomer.LastName : newLastName;

        Console.Write($"Nuvarande Email: {existingCustomer.Email}. Ange nytt (Enter för att behålla nuvarande): ");
        var newEmail = Console.ReadLine();
        existingCustomer.Email = string.IsNullOrWhiteSpace(newEmail) ? existingCustomer.Email : newEmail;

        Console.Write($"Nuvarande Adress: {existingCustomer.Address.StreetName}. Ange nytt (Enter för att behålla nuvarande): ");
        var newStreetName = Console.ReadLine();
        existingCustomer.Address.StreetName = string.IsNullOrWhiteSpace(newStreetName) ? existingCustomer.Address.StreetName : newStreetName;

        Console.Write($"Nuvarande Postnummer: {existingCustomer.Address.PostalCode}. Ange nytt (Enter för att behålla nuvarande): ");
        var newPostalCode = Console.ReadLine();
        existingCustomer.Address.PostalCode = string.IsNullOrWhiteSpace(newPostalCode) ? existingCustomer.Address.PostalCode : newPostalCode;

        Console.Write($"Nuvarande Stad: {existingCustomer.Address.City}. Ange nytt (Enter för att behålla nuvarande): ");
        var newCity = Console.ReadLine();
        existingCustomer.Address.City = string.IsNullOrWhiteSpace(newCity) ? existingCustomer.Address.City : newCity;

        var result = await _customerService.UpdateCustomerAsync(existingCustomer);

        if (result)
        {
            Console.WriteLine("Kund Uppdateras.");
        }
        else
        {
            Console.WriteLine("Gick inte att uppdatera kund.");
        }
        Console.ReadKey();

    }
    public async Task DeleteByEmailAsync()
    {
        Console.Clear();
        Console.Write("Ange email på Kund att radera: ");
        var email = Console.ReadLine();

        var result = await _customerService.DeleteCustomerByEmailAsync(email);

        if (result)
        {
            Console.WriteLine("Kund Raderad.");
        }
        else
        {
            Console.WriteLine("Gick inte att radera kund. Kund hittades ej.");
        }
        Console.ReadKey();

    }
}
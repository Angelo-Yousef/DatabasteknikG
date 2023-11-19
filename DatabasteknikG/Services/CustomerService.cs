using DatabasteknikG.Entities;
using DatabasteknikG.Models;
using DatabasteknikG.Repositories;

namespace DatabasteknikG.Services;

internal class CustomerService
{
    private readonly AddressRepository _addressRepository;
    private readonly CustomerRepository _customerRepository;

    public CustomerService(AddressRepository addressRepository, CustomerRepository customerRepository)
    {
        _addressRepository = addressRepository;
        _customerRepository = customerRepository;
    }

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        if (!await _customerRepository.ExistsAsync(x => x.Email == form.Email))
        {
            AddressEntity addressEntity = await _addressRepository.GetAsync(x => x.StreetName == form.StreetName && x.PostalCode == form.PostalCode);
            addressEntity ??= await _addressRepository.CreateAsync(new AddressEntity { StreetName = form.StreetName, PostalCode = form.PostalCode, City = form.City });

            CustomerEntity customerEntity = await _customerRepository.CreateAsync(new CustomerEntity { FirstName = form.FirstName, LastName = form.LastName, Email = form.Email, AddressId = addressEntity.Id });
            if (customerEntity != null)
                return true;

        }

        return false;

    }

    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers;
    }

    public async Task<CustomerEntity?> GetCustomerByEmailAsync(string email)
    {
        return await _customerRepository.GetAsync(x => x.Email == email);
    }
    public async Task<bool> UpdateCustomerAsync(CustomerEntity updatedCustomer)
    {
        var existingCustomer = await _customerRepository.GetAsync(updatedCustomer.Id);
        if (existingCustomer == null)
        {
            return false; 
        }

        existingCustomer.FirstName = updatedCustomer.FirstName;
        existingCustomer.LastName = updatedCustomer.LastName;
        existingCustomer.Email = updatedCustomer.Email;

        if (existingCustomer.Address != null)
        {
            existingCustomer.Address.StreetName = updatedCustomer.Address.StreetName;
            existingCustomer.Address.PostalCode = updatedCustomer.Address.PostalCode;
            existingCustomer.Address.City = updatedCustomer.Address.City;

            await _addressRepository.UpdateAsync(existingCustomer.Address);
        }

        await _customerRepository.UpdateAsync(existingCustomer);

        return true; 
    }
    public async Task<bool> DeleteCustomerByEmailAsync(string email)
    {
        var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(email);

        if (existingCustomer != null)
        {
            await _customerRepository.DeleteByEmailAsync(email);
            return true;
        }

        return false; 
    }
}
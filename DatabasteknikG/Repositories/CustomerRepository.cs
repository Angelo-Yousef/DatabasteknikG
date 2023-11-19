using DatabasteknikG.Contexts;
using DatabasteknikG.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DatabasteknikG.Repositories
{
    internal class CustomerRepository : Repo<CustomerEntity>
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _context.Customers.Include(x => x.Address).ToListAsync();
        }

        public async Task<CustomerEntity?> GetAsync(int customerId)
        {
            return await _context.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == customerId);
        }

        public async Task<CustomerEntity?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers.Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task DeleteByEmailAsync(string email)
        {
            var customer = await _context.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);

            if (customer != null)
            {
                if (customer.Address != null)
                {
                    _context.Addresses.Remove(customer.Address);
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using DatabasteknikG.Contexts;
using DatabasteknikG.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabasteknikG.Repositories
{
    internal class ProductCategoryRepository : Repo<ProductCategoryEntity>
    {
        private readonly DataContext _context;

        public ProductCategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
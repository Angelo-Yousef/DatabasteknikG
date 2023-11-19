using DatabasteknikG.Contexts;
using DatabasteknikG.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabasteknikG.Repositories
{
    internal class PricingUnitRepository : Repo<PricingUnitEntity>
    {
        private readonly DataContext _context;

        public PricingUnitRepository(DataContext context) : base(context)
        {
            _context = context;
        }


    }
}
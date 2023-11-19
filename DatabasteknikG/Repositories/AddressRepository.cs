using DatabasteknikG.Contexts;
using DatabasteknikG.Entities;

namespace DatabasteknikG.Repositories;

internal class AddressRepository : Repo<AddressEntity>
{
    public AddressRepository(DataContext context) : base(context)
    {
    }
}
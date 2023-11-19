using DatabasteknikG.Contexts;
using DatabasteknikG.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabasteknikG.Repositories
{
    internal class ProductRepository : Repo<ProductEntity>
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            return await _context.Products
                .Include(x => x.PricingUnit)
                .Include(x => x.ProductCategory)
                .ToListAsync();
        }

        public override async Task<ProductEntity> UpdateAsync(ProductEntity entity)
        {
            var existingProduct = await _context.Products
                .Include(x => x.PricingUnit)
                .Include(x => x.ProductCategory)
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existingProduct != null)
            {
                existingProduct.ProductName = entity.ProductName;
                existingProduct.ProductDescription = entity.ProductDescription;
                existingProduct.ProductPrice = entity.ProductPrice;

                if (existingProduct.PricingUnit != null)
                {
                    existingProduct.PricingUnit.Unit = entity.PricingUnit?.Unit;
                }
                else
                {
                    existingProduct.PricingUnit = entity.PricingUnit;
                }

                if (existingProduct.ProductCategory != null)
                {
                    existingProduct.ProductCategory.CategoryName = entity.ProductCategory?.CategoryName;
                }
                else
                {
                    existingProduct.ProductCategory = entity.ProductCategory;
                }

                await _context.SaveChangesAsync();
            }

            return existingProduct;
        }



        public async Task DeleteAsync(ProductEntity product)
        {
            if (product.PricingUnit != null)
            {
                _context.Entry(product.PricingUnit).State = EntityState.Deleted;
            }

            if (product.ProductCategory != null)
            {
                _context.Entry(product.ProductCategory).State = EntityState.Deleted;
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
}

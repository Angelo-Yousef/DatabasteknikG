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

                // Update or create PricingUnit
                if (entity.PricingUnit != null)
                {
                    if (existingProduct.PricingUnit != null)
                    {
                        // Update existing PricingUnit
                        existingProduct.PricingUnit.Unit = entity.PricingUnit.Unit;
                    }
                    else
                    {
                        // Create new PricingUnit and associate with the product
                        existingProduct.PricingUnit = new PricingUnitEntity { Unit = entity.PricingUnit.Unit };
                    }
                }
                else
                {
                    // If entity.PricingUnit is null, you may decide whether to clear the association or leave it unchanged.
                    // For example, you can set existingProduct.PricingUnit = null;
                }

                // Update or create ProductCategory
                if (entity.ProductCategory != null)
                {
                    if (existingProduct.ProductCategory != null)
                    {
                        // Update existing ProductCategory
                        existingProduct.ProductCategory.CategoryName = entity.ProductCategory.CategoryName;
                    }
                    else
                    {
                        // Create new ProductCategory and associate with the product
                        existingProduct.ProductCategory = new ProductCategoryEntity { CategoryName = entity.ProductCategory.CategoryName };
                    }
                }
                else
                {
                    // If entity.ProductCategory is null, you may decide whether to clear the association or leave it unchanged.
                    // For example, you can set existingProduct.ProductCategory = null;
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

using DatabasteknikG.Entities;
using DatabasteknikG.Models;
using DatabasteknikG.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DatabasteknikG.Services;

internal class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly PricingUnitRepository _pricingUnitRepository;
    private readonly ProductCategoryRepository _productCategoryRepository;

    public ProductService(ProductRepository productRepository, PricingUnitRepository pricingUnitRepository, ProductCategoryRepository productCategoryRepository)
    {
        _productRepository = productRepository;
        _pricingUnitRepository = pricingUnitRepository;
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<bool> CreateProductAsync(ProductRegistrationForm form)
    {
        if (!await _productRepository.ExistsAsync(x => x.ProductName == form.ProductName))
        {
            var pricingUnitEntity = await _pricingUnitRepository.GetAsync(x => x.Unit == form.PricingUnit);
            pricingUnitEntity ??= await _pricingUnitRepository.CreateAsync(new PricingUnitEntity { Unit = form.PricingUnit });

            var productCategoryEntity = await _productCategoryRepository.GetAsync(x => x.CategoryName == form.ProductCategory);
            productCategoryEntity ??= await _productCategoryRepository.CreateAsync(new ProductCategoryEntity { CategoryName = form.ProductCategory });

            var productEntity = await _productRepository.CreateAsync(new ProductEntity
            {
                ProductName = form.ProductName,
                ProductDescription = form.ProductDescription,
                ProductPrice = form.ProductPrice,
                PricingUnitId = pricingUnitEntity.Id,   
                ProductCategoryId = productCategoryEntity.Id
            });

            if (productEntity != null)
                return true;
        }

        return false;
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products;
    }

    public async Task<IEnumerable<PricingUnitEntity>> GetAllPricingUnitsAsync()
    {
        var units = await _pricingUnitRepository.GetAllAsync();
        return units;
    }
    public async Task<bool> UpdateProductAsync(string productName, string newProductName, string newProductDescription, string newProductCategory, decimal newProductPrice, string newPricingUnit)
    {
        var existingProduct = await _productRepository.GetAsync(x => x.ProductName == productName);

        if (existingProduct != null)
        {
            var pricingUnitEntity = await _pricingUnitRepository.GetAsync(x => x.Unit == newPricingUnit);
            if (pricingUnitEntity == null)
            {
                pricingUnitEntity = await _pricingUnitRepository.CreateAsync(new PricingUnitEntity { Unit = newPricingUnit });
            }

            var productCategoryEntity = await _productCategoryRepository.GetAsync(x => x.CategoryName == newProductCategory);
            if (productCategoryEntity == null)
            {
                productCategoryEntity = await _productCategoryRepository.CreateAsync(new ProductCategoryEntity { CategoryName = newProductCategory });
            }

            existingProduct.ProductName = newProductName;
            existingProduct.ProductDescription = newProductDescription;
            existingProduct.ProductPrice = newProductPrice;

            existingProduct.PricingUnit = pricingUnitEntity;
            existingProduct.ProductCategory = productCategoryEntity;

            await _productRepository.UpdateAsync(existingProduct);

            return true;
        }

        return false;
    }





    public async Task<bool> DeleteProductAsync(string productName)
    {
        var existingProduct = await _productRepository.GetAsync(x => x.ProductName == productName);

        if (existingProduct != null)
        {
            await _productRepository.DeleteAsync(existingProduct);
            return true;
        }

        return false;
    }

    public async Task<ProductEntity> GetProductByNameAsync(string productName)
    {
        return await _productRepository.GetAsync(x => x.ProductName == productName);
    }
}
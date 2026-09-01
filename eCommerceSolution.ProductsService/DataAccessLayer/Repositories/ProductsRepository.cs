using System.Linq.Expressions;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ProductsRepository(ApplicationDbContext dbContext) : IProductsRepository
{
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await dbContext.Products.Where(conditionExpression).ToListAsync();
    }

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await dbContext.Products.FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<Product?> AddProduct(Product product)
    {
        product.ProductID = Guid.NewGuid();

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> UpdateProduct(Product product)
    {
        var existing = await dbContext.Products
            .FirstOrDefaultAsync(p => p.ProductID == product.ProductID);

        if (existing is null) return null;

        existing.ProductName = product.ProductName;
        existing.Category = product.Category;
        existing.UnitPrice = product.UnitPrice;
        existing.QuantityInStock = product.QuantityInStock;

        await dbContext.SaveChangesAsync();

        return existing;
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        var existing = await dbContext.Products
            .FirstOrDefaultAsync(p => p.ProductID == productID);

        if (existing is null) return false;

        dbContext.Products.Remove(existing);
        var rows = await dbContext.SaveChangesAsync();

        return rows > 0;
    }
}

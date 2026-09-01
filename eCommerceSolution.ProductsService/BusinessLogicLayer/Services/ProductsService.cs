using System.Linq.Expressions;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Mappers;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;

namespace BusinessLogicLayer.Services;

public class ProductsService(IProductsRepository repository) : IProductsService
{
    public async Task<List<ProductResponse?>> GetProducts()
    {
        var products = await repository.GetProducts();
        return products.Select(p => p.ToResponse()).ToList()!;
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        var products = await repository.GetProductsByCondition(conditionExpression);
        return products.Select(p => p!.ToResponse()).ToList()!;
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        var product = await repository.GetProductByCondition(conditionExpression);
        return product?.ToResponse();
    }

    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        var added = await repository.AddProduct(productAddRequest.ToEntity());
        return added?.ToResponse();
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        var updated = await repository.UpdateProduct(productUpdateRequest.ToEntity());
        return updated?.ToResponse();
    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        return await repository.DeleteProduct(productID);
    }
}

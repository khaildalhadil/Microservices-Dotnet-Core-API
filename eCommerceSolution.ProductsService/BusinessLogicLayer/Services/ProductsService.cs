using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Mappers;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.RepositoryContracts;

namespace BusinessLogicLayer.Services;

public class ProductsService(IProductsRepository repository) : IProductsService
{
    public async Task<List<ProductResponse?>> GetProducts()
    {
        var products = await repository.GetProducts();
        return products.Select(p => p.ToResponse()).ToList()!;
    }

    public async Task<ProductResponse?> GetProductById(Guid productID)
    {
        var product = await repository.GetProductByCondition(p => p.ProductID == productID);
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

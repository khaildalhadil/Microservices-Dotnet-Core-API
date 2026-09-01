using BusinessLogicLayer.DTO;

namespace BusinessLogicLayer.ServiceContracts;

public interface IProductsService
{
    Task<List<ProductResponse?>> GetProducts();
    Task<ProductResponse?> GetProductById(Guid productID);
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);
    Task<bool> DeleteProduct(Guid productID);
}

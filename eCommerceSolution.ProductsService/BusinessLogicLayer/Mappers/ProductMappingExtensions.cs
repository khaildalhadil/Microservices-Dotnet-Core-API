using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappers;

// Manual DTO <-> Entity mapping (no AutoMapper): plain methods, easy to read and debug.
public static class ProductMappingExtensions
{
    public static Product ToEntity(this ProductAddRequest request) => new()
    {
        ProductName = request.ProductName,
        Category = request.Category,
        UnitPrice = request.UnitPrice,
        QuantityInStock = request.QuantityInStock,
    };

    public static Product ToEntity(this ProductUpdateRequest request) => new()
    {
        ProductID = request.ProductID,
        ProductName = request.ProductName,
        Category = request.Category,
        UnitPrice = request.UnitPrice,
        QuantityInStock = request.QuantityInStock,
    };

    public static ProductResponse ToResponse(this Product product) =>
        new(product.ProductID, product.ProductName, product.Category,
            product.UnitPrice, product.QuantityInStock);
}

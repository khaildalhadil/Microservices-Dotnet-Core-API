using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using FluentValidation;

namespace ProductsMicroService.API.APIEndpoints;

public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products").WithTags("Products");

        group.MapGet("/", async (IProductsService productsService) =>
        {
            var products = await productsService.GetProducts();
            return Results.Ok(products);
        });

        group.MapGet("/{productID:guid}", async (Guid productID, IProductsService productsService) =>
        {
            var product = await productsService.GetProductByCondition(p => p.ProductID == productID);
            return product is null ? Results.NotFound() : Results.Ok(product);
        });

        group.MapPost("/", async (
            ProductAddRequest request,
            IProductsService productsService,
            IValidator<ProductAddRequest> validator) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var added = await productsService.AddProduct(request);
            return added is null ? Results.Problem("Could not add product.") : Results.Ok(added);
        });

        group.MapPut("/", async (
            ProductUpdateRequest request,
            IProductsService productsService,
            IValidator<ProductUpdateRequest> validator) =>
        {
            var validation = await validator.ValidateAsync(request);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var updated = await productsService.UpdateProduct(request);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });

        group.MapDelete("/{productID:guid}", async (Guid productID, IProductsService productsService) =>
        {
            var deleted = await productsService.DeleteProduct(productID);
            return deleted ? Results.Ok(true) : Results.NotFound();
        });

        return app;
    }
}

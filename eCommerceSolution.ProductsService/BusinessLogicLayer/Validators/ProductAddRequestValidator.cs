using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0).When(x => x.UnitPrice.HasValue)
            .WithMessage("Unit price cannot be negative.");

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0).When(x => x.QuantityInStock.HasValue)
            .WithMessage("Quantity in stock cannot be negative.");
    }
}

using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class OrderAddRequestValidator : AbstractValidator<OrderAddRequest>
{
    public OrderAddRequestValidator()
    {
        RuleFor(x => x.UserID).NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Order date is required.");
        RuleFor(x => x.OrderItems).NotEmpty().WithMessage("Order must contain at least one item.");
        RuleForEach(x => x.OrderItems).SetValidator(new OrderItemAddRequestValidator());
    }
}

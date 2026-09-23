using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class OrderUpdateRequestValidator : AbstractValidator<OrderUpdateRequest>
{
    public OrderUpdateRequestValidator()
    {
        RuleFor(x => x.OrderID).NotEmpty().WithMessage("Order ID is required.");
        RuleFor(x => x.UserID).NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Order date is required.");
        RuleFor(x => x.OrderItems).NotEmpty().WithMessage("Order must contain at least one item.");
        RuleForEach(x => x.OrderItems).SetValidator(new OrderItemUpdateRequestValidator());
    }
}

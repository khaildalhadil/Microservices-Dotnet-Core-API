namespace BusinessLogicLayer.DTO;

public record OrderItemAddRequest(
    Guid ProductID,
    decimal UnitPrice,
    int Quantity);

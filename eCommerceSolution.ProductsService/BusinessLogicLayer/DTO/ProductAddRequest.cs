namespace BusinessLogicLayer.DTO;

public record ProductAddRequest(
    string? ProductName,
    string? Category,
    double? UnitPrice,
    int? QuantityInStock);

using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappers;

// Manual DTO <-> Entity mapping (no AutoMapper), same style as ProductsService.
// TotalPrice / TotalBill are always computed here, never taken from the client.
public static class OrderMappingExtensions
{
    public static Order ToEntity(this OrderAddRequest request) =>
        WithTotals(new Order
        {
            UserID = request.UserID,
            OrderDate = request.OrderDate,
            OrderItems = request.OrderItems.Select(i => ToItem(i.ProductID, i.UnitPrice, i.Quantity)).ToList(),
        });

    public static Order ToEntity(this OrderUpdateRequest request) =>
        WithTotals(new Order
        {
            OrderID = request.OrderID,
            UserID = request.UserID,
            OrderDate = request.OrderDate,
            OrderItems = request.OrderItems.Select(i => ToItem(i.ProductID, i.UnitPrice, i.Quantity)).ToList(),
        });

    public static OrderResponse ToResponse(this Order order) =>
        new(order.OrderID, order.UserID, order.TotalBill, order.OrderDate,
            order.OrderItems.Select(i => new OrderItemResponse(i.ProductID, i.UnitPrice, i.Quantity, i.TotalPrice)).ToList());

    private static OrderItem ToItem(Guid productID, decimal unitPrice, int quantity) => new()
    {
        ProductID = productID,
        UnitPrice = unitPrice,
        Quantity = quantity,
        TotalPrice = unitPrice * quantity,
    };

    private static Order WithTotals(Order order)
    {
        order.TotalBill = order.OrderItems.Sum(i => i.TotalPrice);
        return order;
    }
}

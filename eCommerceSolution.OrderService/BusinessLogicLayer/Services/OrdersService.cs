using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Mappers;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using MongoDB.Driver;

namespace BusinessLogicLayer.Services;

public class OrdersService(IOrdersRepository repository) : IOrdersService
{
    public async Task<List<OrderResponse?>> GetOrders()
    {
        var orders = await repository.GetOrders();
        return orders.Select(o => o.ToResponse()).ToList()!;
    }

    public async Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        var orders = await repository.GetOrdersByCondition(filter);
        return orders.Select(o => o!.ToResponse()).ToList()!;
    }

    public async Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        var order = await repository.GetOrderByCondition(filter);
        return order?.ToResponse();
    }

    public async Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest)
    {
        if (orderAddRequest is null)
        {
            throw new ArgumentNullException(nameof(orderAddRequest), "OrderAddRequest cannot be null.");
        }

        var added = await repository.AddOrder(orderAddRequest.ToEntity());

        // TO DO: Add logic for checking if userid exsits in user microservice, if not throw exception and return null

        var reponse = added?.ToResponse();
        return reponse;
    }

    public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
    {
        if (orderUpdateRequest is null)
        {
            throw new ArgumentNullException(nameof(orderUpdateRequest), "OrderUpdateRequest cannot be null.");
        }

        var updated = await repository.UpdateOrder(orderUpdateRequest.ToEntity());
        return updated?.ToResponse();
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
        if (orderID == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(orderID), "OrderID cannot be empty.");
        }

        return await repository.DeleteOrder(orderID);
    }
}

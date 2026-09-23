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
        var added = await repository.AddOrder(orderAddRequest.ToEntity());
        return added?.ToResponse();
    }

    public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
    {
        var updated = await repository.UpdateOrder(orderUpdateRequest.ToEntity());
        return updated?.ToResponse();
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
        return await repository.DeleteOrder(orderID);
    }
}

using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using MongoDB.Driver;

namespace DataAccessLayer.Repositories;

public class OrdersRepository(IMongoDatabase database) : IOrdersRepository
{
    private readonly IMongoCollection<Order> _orders = database.GetCollection<Order>("orders");

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return (await _orders.FindAsync(Builders<Order>.Filter.Empty)).ToList();
    }

    public async Task<IEnumerable<Order?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        return (await _orders.FindAsync(filter)).ToList();
    }

    public async Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        return (await _orders.FindAsync(filter)).FirstOrDefault();
    }

    public async Task<Order?> AddOrder(Order order)
    {
        order.OrderID = Guid.NewGuid();
        order._id = order.OrderID;

        foreach (var item in order.OrderItems)
            item._id = Guid.NewGuid();

        await _orders.InsertOneAsync(order);

        return order;
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
        var filter = Builders<Order>.Filter.Eq(o => o.OrderID, order.OrderID);

        var existing = (await _orders.FindAsync(filter)).FirstOrDefault();
        if (existing is null) return null;

        order._id = existing._id;
        foreach (var item in order.OrderItems)
            item._id = Guid.NewGuid();

        await _orders.ReplaceOneAsync(filter, order);

        return order;
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
        var result = await _orders.DeleteOneAsync(Builders<Order>.Filter.Eq(o => o.OrderID, orderID));
        return result.DeletedCount > 0;
    }
}

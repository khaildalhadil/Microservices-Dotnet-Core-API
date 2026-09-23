using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace OrdersMicroservice.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrdersService ordersService) : ControllerBase
{
    // GET api/Orders
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await ordersService.GetOrders());
    }

    // GET api/Orders/search/orderid/{orderID}
    [HttpGet("search/orderid/{orderID:guid}")]
    public async Task<IActionResult> GetOrderByOrderID(Guid orderID)
    {
        var order = await ordersService.GetOrderByCondition(Builders<Order>.Filter.Eq(o => o.OrderID, orderID));
        return order is null ? NotFound() : Ok(order);
    }

    // GET api/Orders/search/productid/{productID}
    [HttpGet("search/productid/{productID:guid}")]
    public async Task<IActionResult> GetOrdersByProductID(Guid productID)
    {
        var filter = Builders<Order>.Filter.ElemMatch(o => o.OrderItems,
            Builders<OrderItem>.Filter.Eq(i => i.ProductID, productID));
        return Ok(await ordersService.GetOrdersByCondition(filter));
    }

    // GET api/Orders/search/orderDate/{orderDate}   e.g. 2026-09-23
    [HttpGet("search/orderDate/{orderDate:datetime}")]
    public async Task<IActionResult> GetOrdersByOrderDate(DateTime orderDate)
    {
        var filter = Builders<Order>.Filter.Gte(o => o.OrderDate, orderDate.Date)
                   & Builders<Order>.Filter.Lt(o => o.OrderDate, orderDate.Date.AddDays(1));
        return Ok(await ordersService.GetOrdersByCondition(filter));
    }

    // GET api/Orders/search/userid/{userID}
    [HttpGet("search/userid/{userID:guid}")]
    public async Task<IActionResult> GetOrdersByUserID(Guid userID)
    {
        return Ok(await ordersService.GetOrdersByCondition(Builders<Order>.Filter.Eq(o => o.UserID, userID)));
    }

    // POST api/Orders
    [HttpPost]
    public async Task<IActionResult> Post(OrderAddRequest request, IValidator<OrderAddRequest> validator)
    {
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid)
            return ValidationProblem(new ValidationProblemDetails(validation.ToDictionary()));

        var added = await ordersService.AddOrder(request);
        return added is null
            ? Problem("Could not add order.")
            : Created($"api/Orders/search/orderid/{added.OrderID}", added);
    }

    // PUT api/Orders/{orderID}
    [HttpPut("{orderID:guid}")]
    public async Task<IActionResult> Put(Guid orderID, OrderUpdateRequest request, IValidator<OrderUpdateRequest> validator)
    {
        if (orderID != request.OrderID)
            return BadRequest("OrderID in the URL doesn't match the OrderID in the body.");

        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid)
            return ValidationProblem(new ValidationProblemDetails(validation.ToDictionary()));

        var updated = await ordersService.UpdateOrder(request);
        return updated is null ? NotFound() : Ok(updated);
    }

    // DELETE api/Orders/{orderID}
    [HttpDelete("{orderID:guid}")]
    public async Task<IActionResult> Delete(Guid orderID)
    {
        var deleted = await ordersService.DeleteOrder(orderID);
        return deleted ? Ok(true) : NotFound();
    }
}

using LayeredArch.Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Layered_Architecture.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(CreateOrderHandler createOrderHandler, GetOrderByIdHandler getOrderByIdHandler) : ControllerBase
{

    private readonly CreateOrderHandler _createOrderHandler = createOrderHandler;
    private readonly GetOrderByIdHandler _getOrderByIdHandler = getOrderByIdHandler;


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _getOrderByIdHandler.HandleAsync(new GetOrderByIdQuery(id));
        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreateOrderCommand command)
    {
        var orderId = await _createOrderHandler.HandleAsync(command);
        return Ok(new { id = orderId });
    }
}

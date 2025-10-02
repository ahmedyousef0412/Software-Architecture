using LayeredArch.Application.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Layered_Architecture.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(CreateCustomerHandler createCustomerHandler) : ControllerBase
{
    private readonly CreateCustomerHandler _createCustomerHandler = createCustomerHandler;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken = default)
    {
        var customerId = await _createCustomerHandler.HandleAsync(command, cancellationToken);
        return Ok(new { id = customerId });
    }
}

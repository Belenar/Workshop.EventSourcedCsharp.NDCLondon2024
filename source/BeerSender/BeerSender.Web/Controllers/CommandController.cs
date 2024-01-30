using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeerSender.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandController : ControllerBase
{
    // Polymorphic endpoint (requires another fix for OpenAPI spec)
    [HttpPost]
    public void Post([FromBody] Command_message value)
    {
    }

    // Command-specific endpoints
    [HttpPost("{aggregate_id}/Get_box")]
    public void Post([FromRoute]Guid aggregate_id, [FromBody] Get_box value)
    {
    }
}


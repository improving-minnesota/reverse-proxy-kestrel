using Microsoft.AspNetCore.Mvc;
namespace app.Controllers;

[ApiController]
[Route("[controller]")]
public class GenericController : ControllerBase
{
     private readonly ILogger<GenericController> _logger;
     public GenericController(ILogger<GenericController> logger)
     {
         _logger = logger;
     }

    [HttpGet("/hello")]
    public String GetHello()
    {
        return "Hello World";
    }

    [HttpGet("/")]
    public String GetAlive()
    {
        return "Alive!";
    }
}

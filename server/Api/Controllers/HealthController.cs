using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("/health")]
public class HealthController : ApiController
{
    public HealthController(ISender mediator, IMapper mapper) : base(mediator, mapper)
    {
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        Console.WriteLine("Healthy");
        return Ok("Healthy");
    }
    
}
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SDIA.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private ISender _mediator;
    
    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }
}
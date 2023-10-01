using Application.Commands.User;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SDIA.Entities.User.Requests;
using SDIA.Entities.User.Responses;

namespace SDIA.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var registerCommand = request.Adapt<RegisterUserCommand>();

        var response = (await _mediator.Send(registerCommand, cancellationToken)).Adapt<AuthResponse>();

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest loginRequest,
        CancellationToken cancellationToken)
    {
        var loginCommand = loginRequest.Adapt<LoginUserCommand>();
        
        var response = (await _mediator.Send(loginCommand, cancellationToken)).Adapt<AuthResponse>();
        
        return Ok(response);
    }
}
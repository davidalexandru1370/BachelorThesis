using Application.Commands.User;
using Application.Query.User;
using Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDIA.Entities.User.Requests;
using SDIA.Entities.User.Responses;
using SDIA.Security;

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

    [HttpPost("register/google")]
    [Authorize(AuthenticationSchemes = "Google")]
    public async Task<IActionResult> RegisterWithGoogle(CancellationToken cancellationToken)
    {
        var email = User.GetEmail();
        var sid = User.GetSid();

        var registerWithGoogleCommand = new RegisterUserWithGoogleCommand()
        {
            Email = email,
            Sid = sid
        };

        await _mediator.Send(registerWithGoogleCommand, cancellationToken);

        return Ok();
    }

    [Authorize]
    [HttpGet("authorize")]
    public async Task<ActionResult<UserProfileResponse>> Authorize(CancellationToken cancellationToken)
    {
        var sid = User.GetSid();
        var id = User.GetId();

        var getUserProfileQuery = new GetUserProfileById()
        {
            UserId = id,
            UserSid = sid
        };

        var response = await _mediator.Send(getUserProfileQuery, cancellationToken);
        var userProfileResponse = response.Adapt<UserProfileResponse>();
        
        return Ok(userProfileResponse);
    }
}
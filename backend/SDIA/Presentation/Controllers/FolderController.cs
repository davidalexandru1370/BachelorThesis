using Application.Commands.Folder;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDIA.Entities.Folder.Requests;
using SDIA.Entities.Folder.Responses;
using SDIA.Security;

namespace SDIA.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FolderController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public FolderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult<FolderInfoResponse>> CreateFolder([FromBody] CreateFolderRequest createFolderRequest)
    {
        var createFolderCommand = createFolderRequest.Adapt<CreateFolderCommand>();
        
        createFolderCommand.UserId = User.GetId();
        
        var addedFolder = (await _mediator.Send(createFolderCommand)).Adapt<FolderInfoResponse>();
        
        return Ok(addedFolder);
    }
}
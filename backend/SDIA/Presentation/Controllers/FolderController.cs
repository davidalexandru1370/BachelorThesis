using Application.Commands.Folder;
using Application.Query.Folder;
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
    public async Task<ActionResult<FolderInfoResponse>> CreateFolder([FromForm] CreateFolderRequest createFolderRequest, CancellationToken cancellationToken)
    {
        var createFolderCommand = createFolderRequest.Adapt<CreateFolderCommand>();
        createFolderCommand.UserId = User.GetId();
        
        var addedFolder = (await _mediator.Send(createFolderCommand, cancellationToken)).Adapt<FolderInfoResponse>();
        
        return Ok(addedFolder);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<FolderInfoResponse>>> GetFolders()
    {
        var userId = User.GetId();
        var getFoldersQuery = new GetFoldersByUserIdQuery(userId);
        
        var folders = (await _mediator.Send(getFoldersQuery)).Adapt<List<FolderInfoResponse>>();
        
        return Ok(folders);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFolder(Guid id)
    {
        var deleteFolderCommand = new DeleteFolderByIdCommand(id, User.GetId());
        
        await _mediator.Send(deleteFolderCommand);

        return Ok();
    }
}
using Application.Commands.Folder;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDIA.Entities.Folder.Responses;

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
    public async Task<ActionResult<FolderInfoResponse>> CreateFolder([FromBody] FolderInfoResponse folderInfoResponse)
    {
        var createFolderCommand = folderInfoResponse.Adapt<CreateFolderCommand>();
        
        var addedFolder = (await _mediator.Send(createFolderCommand)).Adapt<FolderInfoResponse>();
        
        return Ok(addedFolder);
    }
}
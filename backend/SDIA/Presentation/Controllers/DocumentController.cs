using Application.Commands.Document;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SDIA.Entities.Document.Requests;

namespace SDIA.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    [HttpPut]
    public async Task<IActionResult> UpdateDocument([FromBody] UpdateDocumentRequest updateDocumentRequest)
    {
        var document = await _mediator.Send(updateDocumentRequest);
        return Ok(document);
    }
}
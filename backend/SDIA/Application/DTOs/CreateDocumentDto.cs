using Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs;

public class CreateDocumentDto
{
    public Guid? Id { get; set; }
    public IFormFile File { get; set; } = null!;
    public DocumentType DocumentType { get; set; }
}
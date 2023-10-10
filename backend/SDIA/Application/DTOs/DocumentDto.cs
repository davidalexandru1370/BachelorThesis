using Domain.Constants;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs;

public class DocumentDto : IAudit
{
    public Guid Id { get; set; }
    public DocumentType DocumentType { get; set; }
    public IFormFile Image { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
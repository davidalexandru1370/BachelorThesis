using Domain.Constants.Enums;
using Domain.Interfaces;

namespace Application.DTOs;

public class DocumentDto : IAudit
{
    public Guid Id { get; set; }
    public DocumentType DocumentType { get; set; }
    public string StorageUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
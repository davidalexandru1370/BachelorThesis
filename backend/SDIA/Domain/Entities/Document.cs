using System.ComponentModel.DataAnnotations;
using Domain.Constants;
using Domain.Interfaces;

namespace Domain.Entities;

public class Document : IAudit
{
    [Key]
    public Guid Id { get; set; }
    
    public DocumentType DocumentType { get; set; }

    public string StorageUrl { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
}
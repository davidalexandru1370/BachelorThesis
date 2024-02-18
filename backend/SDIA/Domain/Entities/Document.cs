using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Constants;
using Domain.Interfaces;

namespace Domain.Entities;

public class Document : IAudit, ISoftDelete
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DocumentType DocumentType { get; set; }
    
    [Required]
    public string StorageUrl { get; set; } = null!;
    
    [ForeignKey("Folder")]
    public Guid FolderId { get; set; }
    
    public Folder Folder { get; set; } = null!;
    
    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public bool IsDeleted { get; set; } = false;
    
    public DateTime? DeletedAt { get; set; }
}
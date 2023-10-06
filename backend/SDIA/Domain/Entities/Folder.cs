using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Constants;
using Domain.Interfaces;

namespace Domain.Entities;

public class Folder : IAudit, ISoftDelete
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;

    [Required]
    public string StorageUrl = null!;
    
    [Required]
    public FolderType Type { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }

    public virtual List<Document> Documents { get; set; } = null!;
    
    [Required]
    public bool IsDeleted { get; set; } = false;
    
    public DateTime? DeletedAt { get; set; }
}
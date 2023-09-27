using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;

namespace Domain.Entities;

public class Folder : IAudit
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public string StorageUrl = null!;
    
    public DateTime CreatedAt { get; set; }

    public virtual List<Document> Documents { get; set; } = null!;
}
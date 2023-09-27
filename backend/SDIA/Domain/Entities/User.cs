using System.ComponentModel.DataAnnotations;
using Domain.Interfaces;

namespace Domain.Entities;

public class User : IAudit
{
    [Key]
    public Guid Id { get; set; }

    [Required] 
    public string Username { get; set; } = null!;
    
    [Required] 
    public string Password { get; set; } = null!;

    public virtual List<Folder> Folders { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
}
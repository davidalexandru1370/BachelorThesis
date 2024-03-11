using System.ComponentModel.DataAnnotations;
using Domain.Constants.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Sid), IsUnique = true)]
public class User : IAudit
{
    [Key] public Guid Id { get; set; }

    [Required] public string Email { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;

    [Required] public AuthenticationType AuthenticationType { get; set; } = AuthenticationType.EmailAndPassword;

    [Required] public Role Role { get; set; } = Role.User;

    public string? Sid { get; set; } = null!;

    public virtual List<Folder> Folders { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
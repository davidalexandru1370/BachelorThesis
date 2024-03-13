using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class FolderErrors
{
    [Key] public Guid Id { get; set; }
    
    public Guid FolderId { get; set; }

    public string Error { get; set; } = null!;
}
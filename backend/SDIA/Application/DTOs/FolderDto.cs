using Domain.Interfaces;

namespace Application.DTOs;

public class FolderDto : IAudit
{
    public Guid Id { get; set; }
    public string StorageUrl { get; set; } = null!;
    public UserDto User { get; set; } = null!;
    public List<DocumentDto> Documents { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
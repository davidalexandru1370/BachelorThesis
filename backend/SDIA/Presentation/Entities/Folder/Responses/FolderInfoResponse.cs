using Domain.Constants;
using SDIA.Entities.Document.Responses;

namespace SDIA.Entities.Folder.Responses;

public class FolderInfoResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public FolderType Type { get; set; }
    public List<DocumentInfoResponse> Documents { get; set; } = null!;
}
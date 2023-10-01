using SDIA.Entities.Document.Responses;

namespace SDIA.Entities.Folder.Responses;

public class FolderInfoResponse
{
    public Guid Id { get; set; }
    public string StorageUrl { get; set; }
    public string Name { get; set; }
    public List<DocumentInfoResponse> Documents { get; set; }
}
using SDIA.Entities.Document.Requests;

namespace SDIA.Entities.Folder.Requests;

public class CreateFolderRequest
{
    public string StorageUrl { get; set; } = null!;
    public List<CreateDocumentRequest> Documents { get; set; } = null!;
}
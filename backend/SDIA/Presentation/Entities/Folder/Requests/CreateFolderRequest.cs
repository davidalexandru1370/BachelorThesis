using SDIA.Entities.Document.Requests;

namespace SDIA.Entities.Folder.Requests;

public class CreateFolderRequest
{
    public string Name { get; set; } = null!;
    public IEnumerable<CreateDocumentRequest> Documents { get; set; } = null!;
}
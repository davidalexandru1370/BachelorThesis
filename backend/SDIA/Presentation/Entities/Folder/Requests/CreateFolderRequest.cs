using Domain.Constants.Enums;
using SDIA.Entities.Document.Requests;

namespace SDIA.Entities.Folder.Requests;

public class CreateFolderRequest
{
    public string Name { get; set; } = null!;
    public FolderType FolderType { get; set; }
    public List<CreateDocumentRequest> Documents { get; set; } = new List<CreateDocumentRequest>();
}
namespace SDIA.Entities.Document.Requests;

public class UpdateDocumentRequest
{
    public Guid FolderId { get; set; }
    public string StorageUrl { get; set; } = null!;
}
using Domain.Constants;

namespace SDIA.Entities.Document.Responses;

public class DocumentInfoResponse
{
    public string StorageUrl { get; set; } = null!;
    public DocumentType DocumentType { get; set; }
}
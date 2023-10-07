using Domain.Constants;

namespace SDIA.Entities.Document.Responses;

public class DocumentInfoResponse
{
    public string StorageUrl { get; set; }
    public DocumentType DocumentType { get; set; }
}
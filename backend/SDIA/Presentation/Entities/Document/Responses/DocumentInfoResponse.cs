using Domain.Constants;

namespace SDIA.Entities.Document.Responses;

public class DocumentInfoResponse
{
    public Guid Id { get; set; }
    public string StorageUrl { get; set; } = null!;
    public DocumentType DocumentType { get; set; }
}
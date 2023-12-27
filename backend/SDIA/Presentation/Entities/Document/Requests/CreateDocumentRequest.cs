namespace SDIA.Entities.Document.Requests;

public class CreateDocumentRequest
{
    public IFormFile File { get; set; } = null!;
}
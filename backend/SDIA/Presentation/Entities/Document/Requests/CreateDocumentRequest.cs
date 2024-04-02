using System.ComponentModel.DataAnnotations;

namespace SDIA.Entities.Document.Requests;

public class CreateDocumentRequest
{
    [FileExtensions(Extensions = "jpg,png,jpeg", ErrorMessage = "Invalid extension")]
    public IFormFile File { get; set; } = null!;
}
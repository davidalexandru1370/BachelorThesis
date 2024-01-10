using Domain.Constants;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services;

public interface IDocumentService
{
    public Task<DocumentType> AnalyzeDocumentAsync(IFormFile image);
}
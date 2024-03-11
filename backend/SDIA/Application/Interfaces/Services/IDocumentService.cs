using Domain.Constants.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services;

public interface IDocumentService
{
    public Task<DocumentType> AnalyzeDocumentAsync(IFormFile image);
}
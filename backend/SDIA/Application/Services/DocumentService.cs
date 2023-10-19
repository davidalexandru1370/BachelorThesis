using System.Text.Json;
using Application.Grpc;
using Application.Interfaces.Services;
using Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IOptions<DocumentServiceConfiguration> _documentServiceConfiguration;

    public DocumentService(IHttpClientFactory httpClient, IOptions<DocumentServiceConfiguration> documentServiceConfiguration)
    {
        _httpClient = httpClient;
        _documentServiceConfiguration = documentServiceConfiguration;
    }

    public async Task<DocumentType> AnalyzeDocumentAsync(IFormFile image)
    {
        var client = _httpClient.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/api/document/analyze")
        {
            Content = new MultipartFormDataContent
            {
                { new StreamContent(image.OpenReadStream()), "image", image.FileName }
            } 
        };
        
        var response = await client.SendAsync(request);
        var body = await response.Content.ReadAsStreamAsync();
        return JsonSerializer.Deserialize<DocumentType>(body);
    }
}
using System.Net.Http.Json;
using System.Text.Json;
using Application.Configurations;
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

        var formData = new MultipartFormDataContent();
        formData.Add(new StreamContent(image.OpenReadStream()), "image", image.FileName);
        
        var request = new HttpRequestMessage(HttpMethod.Post, _documentServiceConfiguration.Value.GetAnalyseDocumentEndpoint)
        {
            Content = formData
        };
        
        var response = await client.SendAsync(request);
        var body = await response.Content.ReadFromJsonAsync<DocumentType>();
        return body;
    }
}
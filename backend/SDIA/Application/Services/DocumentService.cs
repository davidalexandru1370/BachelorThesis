using System.Net.Http.Json;
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

    public DocumentService(IHttpClientFactory httpClient,
        IOptions<DocumentServiceConfiguration> documentServiceConfiguration)
    {
        _httpClient = httpClient;
        _documentServiceConfiguration = documentServiceConfiguration;
    }

    public async Task<DocumentType> AnalyzeDocumentAsync(IFormFile image)
    {
        var client = _httpClient.CreateClient();

        using var formData = new MultipartFormDataContent();
        using var memoryStream = new MemoryStream();

        await image.CopyToAsync(memoryStream);
        var imageContent = new ByteArrayContent(memoryStream.ToArray());
        imageContent.Headers.Add("Content-Type", "multipart/form-data");
        formData.Add(new StreamContent(memoryStream), "file", image.FileName);
        var request =
            new HttpRequestMessage(HttpMethod.Post, _documentServiceConfiguration.Value.GetAnalyseDocumentEndpoint)
            {
                Content = formData
            };

        var response = await client.SendAsync(request);
        var body = await response.Content.ReadFromJsonAsync<DocumentType>();
        return body;
    }
}
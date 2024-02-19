using System.Net.Http.Json;
using Application.Configurations;
using Application.Entities.Response;
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
        var imageByteArray = memoryStream.ToArray();
        var imageContent = new ByteArrayContent(imageByteArray, 0, imageByteArray.Length);
        imageContent.Headers.Add("Content-Type", "multipart/form-data");
        formData.Add(imageContent, "file", image.FileName);
        var request =
            new HttpRequestMessage(HttpMethod.Post, _documentServiceConfiguration.Value.GetAnalyseDocumentEndpoint)
            {
                Content = formData
            };
        request.Headers.ExpectContinue = false;
        client.Timeout = new TimeSpan(0, 10, 0);
        var response = await client.PostAsync(_documentServiceConfiguration.Value.GetAnalyseDocumentEndpoint,
            new MultipartFormDataContent
            {
                { new ByteArrayContent(imageByteArray, 0, imageByteArray.Length), "file", image.FileName }
            });
        var body = await response.Content.ReadFromJsonAsync<DocumentClassificationResponse>();
        return body!.DocumentType;
    }
}
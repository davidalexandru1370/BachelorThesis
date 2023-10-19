using System.Net.Http.Json;
using System.Text.Json;
using Grpc.Core;

namespace Application.Grpc.Services
{
    public class DocumentRecognitionService : Application.Grpc.DocumentRecognitionService.DocumentRecognitionServiceBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DocumentRecognitionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public override async Task<DocumentReply> DocumentClassification(DocumentRequest request,
            ServerCallContext context)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var responseDocumentReply = JsonSerializer.Deserialize<DocumentReply>(await (await httpClient.SendAsync(
                new HttpRequestMessage(HttpMethod.Post, "")
                {
                    Content = JsonContent.Create(request)
                })).Content.ReadAsStreamAsync());

            return responseDocumentReply!;
        }
    }
}
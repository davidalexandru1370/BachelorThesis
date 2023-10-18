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
            var responseDocumentReply = JsonSerializer.Deserialize<DocumentReply>(await httpClient.GetStringAsync(""));

            return responseDocumentReply!;
        }
    }
}
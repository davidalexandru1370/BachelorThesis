namespace Application.Services;

public class DocumentServiceConfiguration
{
    public string BaseUrl { get; set; }
    public int Port { get; set; }
    public string AnalyseDocumentEndpoint { get; set; }

    public string GetAnalyseDocumentEndpoint => $"{BaseUrl}:{Port}{AnalyseDocumentEndpoint}";
}
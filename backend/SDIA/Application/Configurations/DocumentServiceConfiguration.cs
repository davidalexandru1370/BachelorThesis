namespace Application.Configurations;

public class DocumentServiceConfiguration
{
    public string BaseUrl { get; set; } = null!;
    public int Port { get; set; }
    public string AnalyseDocumentEndpoint { get; set; } = null!;

    public string GetAnalyseDocumentEndpoint => $"{BaseUrl}:{Port}{AnalyseDocumentEndpoint}";
}
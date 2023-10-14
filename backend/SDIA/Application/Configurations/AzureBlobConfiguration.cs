namespace Application.Configurations;

public sealed class AzureBlobConfiguration 
{
    public string ConnectionString { get; set; } = null!;
    public string ContainerName { get; set; } = null!;
}
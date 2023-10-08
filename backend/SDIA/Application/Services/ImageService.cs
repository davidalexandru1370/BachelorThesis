using Application.Configurations;
using Application.Interfaces.Services;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class ImageService : IImageService
{
    private readonly BlobContainerClient _blobContainerClient;

    public ImageService(IOptions<AzureBlobConfiguration> blobConfiguration)
    {
        var (connectionString, containerName) = (blobConfiguration.Value.ConnectionString, blobConfiguration.Value.ContainerName);
        _blobContainerClient = new BlobContainerClient(connectionString, containerName);
    }
    
    public async Task<string> UploadImageAsync(Guid folderId, IFormFile image)
    {
        var blobClient = _blobContainerClient.GetBlobClient("");
        
        var response = await blobClient.UploadAsync(image.OpenReadStream(), true);

        var url = blobClient.Uri.AbsoluteUri;
    
        return url;
    }

    public Task<List<string>> UploadImagesAsync(List<IFormFile> images)
    {
        throw new NotImplementedException();
    }
}
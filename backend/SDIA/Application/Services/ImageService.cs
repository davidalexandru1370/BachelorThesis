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
        var (connectionString, containerName) =
            (blobConfiguration.Value.ConnectionString, blobConfiguration.Value.ContainerName);
        _blobContainerClient = new BlobContainerClient(connectionString, containerName);
    }

    public async Task<string> UploadImageAsync(Guid folderId, Guid documentId, IFormFile image, CancellationToken cancellationToken)
    {
        var blobClient = _blobContainerClient.GetBlobClient($"{folderId}/{documentId}");
        
        var response = await blobClient.UploadAsync(image.OpenReadStream(), false, cancellationToken);

        var url = blobClient.Uri.AbsoluteUri;

        return url;
    }

    public string GetFolderStorageUrl(Guid folderId)
    {
        return $"{_blobContainerClient.Uri.AbsoluteUri}/{folderId}";
    }

    public Task<List<string>> UploadImagesAsync(Guid folderId, List<IFormFile> images, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
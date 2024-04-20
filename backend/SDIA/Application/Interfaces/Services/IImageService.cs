using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services;

public interface IImageService
{
    public Task<string> UploadImageAsync(string name, IFormFile image, CancellationToken cancellationToken = default);
    public string GetFolderStorageUrl(Guid folderId);
    public Task<List<string>> UploadImagesAsync(Guid folderId, List<IFormFile> images, CancellationToken cancellationToken = default);
}
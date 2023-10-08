using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services;

public interface IImageService
{
    public Task<string> UploadImageAsync(Guid folderId, IFormFile image);
    public Task<List<string>> UploadImagesAsync(List<IFormFile> images);
}
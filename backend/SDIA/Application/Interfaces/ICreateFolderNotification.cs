using Application.Entities.Response;

namespace Application.Interfaces;

public interface ICreateFolderNotification
{
    public Task SendNewStatus(CreateFolderNotificationResponse response, Guid userId);
}
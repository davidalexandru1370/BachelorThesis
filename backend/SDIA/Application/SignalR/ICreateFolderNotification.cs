using Application.Entities.Response;
using Microsoft.AspNetCore.SignalR;

namespace Application.SignalR;

public interface ICreateFolderNotification : IHubContext
{
    public Task SendNewStatus(CreateFolderNotificationResponse response, Guid userId);
}
using Application.Entities.Response;
using Microsoft.AspNetCore.SignalR;

namespace Application.SignalR;

public interface ICreateFolderNotification
{
    public Task SendNewStatus(CreateFolderNotificationResponse response, Guid userId,
        CancellationToken cancellationToken = default);
}
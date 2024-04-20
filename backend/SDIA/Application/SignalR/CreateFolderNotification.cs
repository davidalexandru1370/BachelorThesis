using Application.Entities.Response;
using Microsoft.AspNetCore.SignalR;

namespace Application.SignalR;

public class CreateFolderNotification : ICreateFolderNotification
{
    private readonly IHubContext<CreateFolderHub> _hubContext;

    public CreateFolderNotification(IHubContext<CreateFolderHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNewStatus(CreateFolderNotificationResponse response, Guid userId,
        CancellationToken cancellationToken)
    {
        if (CreateFolderHub.Connections.ContainsKey(userId))
        {
            await _hubContext.Clients.Client(CreateFolderHub.Connections[userId])
                .SendAsync("SendNewStatus", response, cancellationToken);
        }
    }
}
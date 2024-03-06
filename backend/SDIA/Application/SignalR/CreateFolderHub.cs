using System.Collections.Concurrent;
using Application.Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Application.SignalR.Hubs;

[Authorize]
public class CreateFolderHub : Hub
{
    public static readonly ConcurrentDictionary<Guid, string> Connections = new();

    public override Task OnConnectedAsync()
    {
        var context = Context;
        var userId = Guid.Parse(Context.User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value.ToString());
        Connections.TryAdd(userId, context.ConnectionId);
        Log.Information($"User with connection id ${context.ConnectionId} connected.");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Guid.Parse(Context.User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value.ToString());
        if (Connections.ContainsKey(userId))
        {
            var connectionId = "";
            Connections.Remove(userId, out connectionId);
            Log.Information($"User with connection id ${connectionId} disconnected.");
        }

        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendNewStatus(CreateFolderNotificationResponse response, Guid userId)
    {
        Connections.TryGetValue(userId, out string? connectionId);
        if (!string.IsNullOrWhiteSpace(connectionId))
        {
            await Clients.Client(connectionId).SendAsync("SendNewStatus", response);
        }
    }
}
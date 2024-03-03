using System.Collections.Concurrent;
using Application.Entities.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SDIA.Security;
using Serilog;
using Serilog.Core;

namespace SDIA.SignalR.Hubs;

[Authorize]
public class CreateFolderHub : Hub, ICreateFolderNotification
{
    private static ConcurrentDictionary<Guid, string?> _connections = new ConcurrentDictionary<Guid, string?>();

    public override Task OnConnectedAsync()
    {
        var context = Context;
        var userId = Context.User.GetId();
        _connections.TryAdd(userId, context.ConnectionId);
        Log.Information($"User with connection id ${context.ConnectionId} connected.");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User.GetId();
        if (_connections.ContainsKey(userId))
        {
            var connectionId = "";
            _connections.Remove(userId, out connectionId);
            Log.Information($"User with connection id ${connectionId} disconnected.");
        }

        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendNewStatus(CreateFolderNotificationResponse response, Guid userId)
    {
        _connections.TryGetValue(userId, out string? connectionId);
        if (!string.IsNullOrWhiteSpace(connectionId))
        {
            await Clients.Client(connectionId).SendAsync("SendNewStatus", response);
        }
    }
}
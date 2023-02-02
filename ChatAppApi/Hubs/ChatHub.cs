using System.Security.Claims;
using ChatAppApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppApi.Hubs;

public class ChatHub : Hub
{
    private readonly MessagesService _messagesService;

    public ChatHub(MessagesService messagesService)
    {
        _messagesService = messagesService;
    }

    [Authorize]
    public async Task SendMessage(string message)
    {
        var username = Context.User.FindFirstValue(ClaimTypes.Name);
        await Clients.All.SendAsync("NewMessage", username, message);
    }

    [Authorize]
    public async Task SendMessageToGroup(string group, string message)
    {
        var username = Context.User.FindFirstValue(ClaimTypes.Name);

        _messagesService.Messages[group].Add(new Tuple<string, string>(username, message));

        await Clients.Groups(group).SendAsync("NewMessage", username, message);
    }

    public async Task JoinGroup(string group)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }
}
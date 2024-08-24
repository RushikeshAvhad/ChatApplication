using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        string connectionId = Context.ConnectionId;
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override Task OnConnectedAsync()
    {
        string connectionId = Context.ConnectionId;
        Console.WriteLine($"Client connected: {connectionId}");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        string connectionId = Context.ConnectionId;
        Console.WriteLine($"Client disconnected: {connectionId}");
        return base.OnDisconnectedAsync(exception);
    }

    public string GetConnectionId()
    {
        return Context.ConnectionId;
    }

}
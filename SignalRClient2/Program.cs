using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/chathub")
            .Build();

        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"{user}: {message}");
        });

        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connected to the chat server.");

            // Requesting the connection ID
            var connectionId = await connection.InvokeAsync<string>("GetConnectionId");
            Console.WriteLine($"Connection ID: {connectionId}");

            while (true)
            {
                var message = Console.ReadLine();
                await connection.InvokeAsync("SendMessage", "Client", message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
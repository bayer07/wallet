using Microsoft.AspNetCore.SignalR;
using Wallet.Domain;

namespace Wallet.Api.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", new Message() { Date = DateTime.Now, Id = 123, Number = 321, Text = "Text" });
        }
    }
}

using Microsoft.AspNetCore.SignalR.Client;
using Wallet.Domain;

namespace Wallet.Receiver
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/MessageHub")
                .Build();

                await connection.StartAsync();

                connection.On<Message>("ReceiveMessage", (message) =>
                {
                    Console.WriteLine("Received");
                });
                Console.WriteLine("Started");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}

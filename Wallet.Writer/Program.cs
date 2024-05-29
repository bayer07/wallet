using Serilog;
using System.Text;
using System.Text.Json;
using Wallet.Domain;

namespace Wallet.Writer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
            int number = 0;
            while (true)
            {
                try
                {
                    var message = new Message { Text = Guid.NewGuid().ToString(), Number = number };
                    using StringContent jsonContent = new(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json");
                    using HttpResponseMessage httpResponse = await httpClient.PostAsync("api/message", jsonContent);
                    string response = await httpResponse.Content.ReadAsStringAsync();
                    logger.Information("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    number++;
                    await Task.Delay(1000);
                }
            }
        }

        private static ILogger logger;
    }
}







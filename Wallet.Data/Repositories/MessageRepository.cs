using Microsoft.Extensions.Logging;
using Npgsql;
using Wallet.Domain;

namespace Wallet.Data.Repositories
{
    public class MessageRepository : PostgresRepository
    {
        public MessageRepository(NpgsqlConnection dbConnection, ILoggerFactory loggerFactory) : base(dbConnection)
        {
            _logger = loggerFactory.CreateLogger<MessageRepository>();
        }

        private readonly ILogger<MessageRepository> _logger;

        public async Task InsertMessage(Message message)
        {
            await using (var cmd = new NpgsqlCommand("INSERT INTO message (text, date, number) VALUES (@text, @date, @number) RETURNING id;", DbConnection))
            {
                cmd.Parameters.AddWithValue("text", message.Text);
                cmd.Parameters.AddWithValue("date", message.Date);
                cmd.Parameters.AddWithValue("number", message.Number);
                object? id = await cmd.ExecuteScalarAsync();
                message.Id = (int)id;
                _logger.LogInformation($"inserted message:{message}");
            }
        }

        public async Task M()
        {
            await using (var cmd = DbConnection.CreateCommand())
            {
                cmd.CommandText = "SELECT some_field FROM data";
                await using (var reader = await cmd.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader.GetString(0));
                    }
                }
            }
        }
    }
}

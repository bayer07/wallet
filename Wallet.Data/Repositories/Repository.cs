using Npgsql;

namespace Wallet.Data.Repositories
{
    public class PostgresRepository
    {
        public PostgresRepository(NpgsqlConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        protected readonly NpgsqlConnection DbConnection;
    }
}

using System.Data.Common;
using Npgsql;

namespace TestContainers.Demo.Data;

public sealed class DbConnectionProvider(string connectionString)
{
    public DbConnection GetConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
}
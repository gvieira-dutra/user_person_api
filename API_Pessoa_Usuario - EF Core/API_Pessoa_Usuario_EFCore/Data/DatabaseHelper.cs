using Npgsql;
using System.Data;

namespace API_Pessoa_Usuario_EFCore.Data;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    public string GetConnectionString() => _connectionString;
}

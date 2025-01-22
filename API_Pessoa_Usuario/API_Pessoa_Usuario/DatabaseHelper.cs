using Npgsql;
using System.Data;

namespace API_Pessoa_Usuario;

public class DatabaseHelper
{
    private readonly string _connectionString;
    public DatabaseHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}

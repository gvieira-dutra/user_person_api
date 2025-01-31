using API_Pessoa_Usuario_EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace API_Pessoa_Usuario_EFCore.Utilities;

public class DbDiagnostics
{
    private readonly ApplicationContext _context;

    public DbDiagnostics(ApplicationContext context)
    {
        _context = context;
    }

    // Database Health Check
    public void DatabaseHealthCheck()
    {
        Console.WriteLine("---------- Diagnóstico 1 - Database Connection Health Check ----------");
        try
        {
            Console.WriteLine("Conexão feita com SUCESSO\n\n");
        }
        catch
        {
            Console.WriteLine("FALHA ao tentar se conectar\n\n");

        }
    }

    // Detecting migrations   
    public void MigrationsSummary(string msg = "")
    {

        var pendingMigrations = _context.Database.GetPendingMigrations();
        var appliedMigrations = _context.Database.GetAppliedMigrations();
        Console.WriteLine("\n\n");
        Console.WriteLine($"---------- Diagnóstico 2 - Sumário de Migrações {msg} ----------");
        Console.WriteLine("\n");

        Console.WriteLine($"Total Pending Migrations: {pendingMigrations.Count()}");
        Console.WriteLine("Total Applied Migrations: {0}", appliedMigrations.Count());
        Console.WriteLine("\n\n");

        if (pendingMigrations.Count() > 0)
        {
            Console.WriteLine($"---------- Applying Pending Migrations ----------");
            ApplyPendingMigrations();
        }
    }

    // Applying Migrations on Execution Time
    public void ApplyPendingMigrations()
    {
        _context.Database.Migrate();
        MigrationsSummary("Depois de Aplicar Migrações Pendentes");
    }

    // Generating DB Script 
    public void GenerateScript()
    {
        Console.WriteLine($"---------- Gerando Scripts da Base de Dados ----------\n");
        var script = _context.Database.GenerateCreateScript();

        Console.WriteLine(script);
    }

    //TO DO

    // Manage connexion state


    // DONE

    // Split Query
    // Projection
    // Query TAGs
    // Generating DB Script 
    // Applying Migrations on Execution Time
    // Global Filters
    // Database Health Check
    // Generate first migrations
    // Detecting migrations
}

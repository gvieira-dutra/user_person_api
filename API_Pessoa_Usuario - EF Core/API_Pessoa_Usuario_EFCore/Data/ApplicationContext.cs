using API_Pessoa_Usuario_EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace API_Pessoa_Usuario_EFCore.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }

    private readonly DatabaseHelper _databaseHelper;
    private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

    public ApplicationContext(DbContextOptions<ApplicationContext> options, DatabaseHelper databaseHelper)
        : base(options)
    {
        _databaseHelper = databaseHelper;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _databaseHelper.GetConnectionString();

        optionsBuilder
            .UseLoggerFactory(_logger)
            .UseNpgsql(connectionString, p => p.EnableRetryOnFailure(
                maxRetryCount: 2,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorCodesToAdd: null));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        // Global Filters
        modelBuilder.Entity<Pessoa>().HasQueryFilter(p => p.Ativo == true);
        modelBuilder.Entity<Usuario>().HasQueryFilter(p => p.Ativo == true);
    }
}

using API_Pessoa_Usuario_EFCore.Data;
using API_Pessoa_Usuario_EFCore.Utilities;
using API_Pessoa_Usuario_EFCore.Repository.PessoaRepository;
using API_Pessoa_Usuario_EFCore.Repository.UsuarioRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<DatabaseHelper>();
builder.Services.AddScoped<DbDiagnostics>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddDbContext<ApplicationContext>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbDiagnostics = scope.ServiceProvider.GetRequiredService<DbDiagnostics>();
    dbDiagnostics.DatabaseHealthCheck();
    dbDiagnostics.MigrationsSummary();
    dbDiagnostics.GenerateScript();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

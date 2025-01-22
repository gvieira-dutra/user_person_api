using API_Pessoa_Usuario;
using API_Pessoa_Usuario.Repository.PessoaRepository;
using API_Pessoa_Usuario.Repository.UsuarioRepository;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<DatabaseHelper>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().AllowAnonymous();
    app.MapScalarApiReference();


}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using MediatR;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Questao5.Application.Handlers;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;

//TODO: Revisar testes de unidade

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);

// sqlite
var connectionString = configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite");
builder.Services.AddSingleton(new DatabaseConfig { Name =  connectionString });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// SqliteConnection
builder.Services.AddScoped(_ => new SqliteConnection(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add bussiness services
builder.Services.AddScoped<IMovimentoStore, MovimentoStore>();
builder.Services.AddScoped<IContaCorrenteStore, ContaCorrenteStore>();
builder.Services.AddScoped<IIdempotenciaStore, IdempotenciaStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
app.Services.GetService<IDatabaseBootstrap>()?.Setup();

app.Run();

// Informações úteis
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html



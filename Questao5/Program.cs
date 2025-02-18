using MediatR;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using Azure.Identity;

//TODO: Revisar dependecy injection
//TODO: Revisar visbilidade das classes e métodos
//TODO: Revisar async sufix

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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



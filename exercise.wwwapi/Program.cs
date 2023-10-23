using exercise.wwwapi.Data;
using exercise.wwwapi.EndPoint;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IDatabaseRepository<Author>, DatabaseRepository<Author>>();
builder.Services.AddScoped<IDatabaseRepository<Book>, DatabaseRepository<Book>>();
builder.Services.AddScoped<IDatabaseRepository<Publisher>, DatabaseRepository<Publisher>>();

builder.Services.AddControllers();
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

app.Seed();

app.ConfigureAuthorApi();
app.ConfigureBookApi();
app.ConfigurePublisherApi();

app.Run();

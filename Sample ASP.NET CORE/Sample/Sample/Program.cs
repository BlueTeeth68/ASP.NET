using Application;
using Application.Configurations;
using Application.Mappers;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add dependency injection
var configuration = builder.Configuration.Get<AppConfiguration>();
configuration.DatabaseConnection = builder.Configuration.GetConnectionString("DefaultConnection");
configuration.JWTSecretKey = builder.Configuration["JWTSecretKey"];
builder.Services.AddDependency(configuration.DatabaseConnection);
builder.Services.AddSingleton(configuration);


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

app.Run();

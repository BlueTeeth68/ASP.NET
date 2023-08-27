using Application.Configurations;
using Domain.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add dependency injection
var configuration = builder.Configuration.Get<AppConfiguration>();
configuration.DatabaseConnection = builder.Configuration.GetConnectionString("DefaultConnection");
configuration.Key = builder.Configuration["JwtSettings:Key"];
builder.Services.AddDependency(configuration.DatabaseConnection);
builder.Services.AddSingleton(configuration);

//allow enum string value in swagger and front-end instead of int value
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

//Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        //ValidateIssuer = true,
        //ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = _configuration["Jwt:Issuer"],
        //ValidAudience = _configuration["Jwt:Audience"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key)),
        ClockSkew = TimeSpan.Zero
    };
}
    );

//Auhtorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole(Role.Admin.ToString());
    });
    options.AddPolicy("Manager", policy =>
    {
        policy.RequireRole(Role.Manager.ToString());
    });
    options.AddPolicy("ManagerOrAdmin", policy =>
    {
        policy.RequireRole(Role.Manager.ToString(), Role.Admin.ToString());
    });
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

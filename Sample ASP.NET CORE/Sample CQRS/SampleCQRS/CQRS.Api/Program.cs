using System.Text;
using CQRS.Api.ConfigurationOptions;
using CQRS.Application;
using CQRS.Infrastructure;
using CQRS.Infrastructure.Configurations;
using CQRS.Infrastructure.Web.GlobalExceptions;
using CQRS.Persistence;
using CQRS.Persistence.Identity.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var configuration = builder.Configuration;

var appSetting = new AppSettings();
configuration.Bind(appSetting);

services.Configure<AppSettings>(configuration);
services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddSerilog();

//problem detail
services.AddProblemDetails();

// services.AddExceptionHandler<GlobalExceptionsHandler>(null);
//Add infrastructure
services.AddInfrastructure(configuration);
//Add persistence services
services.AddPersistence(appSetting.ConnectionStrings.DefaultConnection);

services.AddApplication();

// Authenticate 
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = appSetting.JwtSettings.Issuer,
        ValidAudience = appSetting.JwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting.JwtSettings.Key))
    };
});

services.AddAuthorization();

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// app.UseExceptionHandler(options => { });

app.UseMiddleware<GlobalExceptionsHandler>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//map identity api
app.MapIdentityApi<AppUser>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
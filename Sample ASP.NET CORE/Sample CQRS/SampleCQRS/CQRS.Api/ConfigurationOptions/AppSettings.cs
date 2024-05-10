using CQRS.Infrastructure.Configurations;

namespace CQRS.Api.ConfigurationOptions;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    
    public JwtSettings JwtSettings { get; set; }
}
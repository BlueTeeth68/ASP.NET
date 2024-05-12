namespace CQRS.Application.Features.Users.DTOs;

public class LoginResultDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;

    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}
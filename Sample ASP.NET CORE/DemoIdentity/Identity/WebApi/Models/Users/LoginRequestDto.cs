using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Users;

public record LoginRequestDto
{
    [Required(ErrorMessage = "Name should not be empty")]
    public string Email { get; init; }

    [Required(ErrorMessage = "Password should not be empty")]
    [DataType(DataType.Password)]
    public string Password { get; init; }
}
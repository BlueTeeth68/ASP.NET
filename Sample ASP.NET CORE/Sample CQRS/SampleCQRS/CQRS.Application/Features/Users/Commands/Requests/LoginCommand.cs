using System.ComponentModel.DataAnnotations;
using CQRS.Application.Features.Users.DTOs;
using MediatR;

namespace CQRS.Application.Features.Users.Commands.Requests;

public record LoginCommand: IRequest<LoginResultDto>
{
    [DataType(DataType.EmailAddress)]
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
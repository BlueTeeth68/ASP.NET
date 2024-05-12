using CQRS.Application.Features.Users.Commands.Requests;
using CQRS.Application.Features.Users.DTOs;

namespace CQRS.Application.Identity;

public interface IAuthService
{
    Task<LoginResultDto> Login(LoginCommand request);
}
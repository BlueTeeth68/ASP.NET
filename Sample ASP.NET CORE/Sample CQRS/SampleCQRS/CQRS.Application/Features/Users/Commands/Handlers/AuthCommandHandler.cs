using CQRS.Application.Features.Users.Commands.Requests;
using CQRS.Application.Features.Users.DTOs;
using CQRS.Application.Identity;
using MediatR;

namespace CQRS.Application.Features.Users.Commands.Handlers;

public class AuthCommandHandler : IRequestHandler<LoginCommand, LoginResultDto>
{
    private IAuthService _authService;

    public AuthCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.Login(request);

        return result;
    }
}
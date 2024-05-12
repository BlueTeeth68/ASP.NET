using CQRS.Application.Features.Users.Commands.Requests;
using CQRS.Application.Features.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResultDto>> LoginAsync([FromBody] LoginCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
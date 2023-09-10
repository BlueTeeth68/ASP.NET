using Application.DTOs;
using Application.ErrorHandlers;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IUserService userService, ILogger<AuthenticationController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLogin userLogin)
        {
            var result = await _userService.LoginAsync(userLogin);
            if (result != null)
            {
                _logger.LogInformation("Log in success.");
                return Ok(result);
            }

            _logger.LogInformation("Log in fail.");
            throw new BadHttpRequestException("Incorrect username or password.");
            // return BadRequest("Incorrect username or password.");
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] CreateUserDTO createUserDto)
        {
            // throw new BadRequestException("Bad request for register information!");

            var createdUser = await _userService.CreateNewAsync(createUserDto);
            if (createdUser != null)
            {
                return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, createdUser);
            }

            return BadRequest("Unable to create the user.");
        }
    }
}
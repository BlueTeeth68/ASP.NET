using Application.DTOs;
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

        public AuthenticationController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLogin userLogin)
        {
            var result = await _userService.LoginAsync(userLogin);
            if (result != null)
                return Ok(result);
            
            return BadRequest("Incorrect username or password.");
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] CreateUserDTO createUserDto)
        {
            //var user = await userService.CreateNewAsync(createUserDTO);
            //return Ok(user);

            var createdUser = await _userService.CreateNewAsync(createUserDto);
            if (createdUser != null)
            {
                return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, createdUser);
            }

            return BadRequest("Unable to create the user.");
        }
    }
}
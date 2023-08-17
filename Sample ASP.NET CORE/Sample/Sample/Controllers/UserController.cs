using Application.DTOs.User;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Entities;

namespace WebAPI.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await userService.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateNew(CreateUserDTO createUserDTO)
        {
            var createdUser = await userService.CreateUser(createUserDTO);
            if (createdUser != null)
            {
                return CreatedAtAction(nameof(GetAll), new { id = createdUser.Id }, createdUser);
            }

            return BadRequest("Unable to create the user.");
        }

    }
}

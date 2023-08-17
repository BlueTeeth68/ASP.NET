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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await userService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(CreateUserDTO createUserDTO)
        {
            var createdUser = await userService.CreateUser(createUserDTO);
            if (createdUser != null)
            {
                return CreatedAtAction(nameof(GetAll), new { id = createdUser.Id }, createdUser);
            }

            return BadRequest("Unable to create the user.");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            return Ok(await userService.UpdateUser(id, updateUserDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id) {
            await userService.DeleteUser(id);
            return Ok();
        }

    }
}

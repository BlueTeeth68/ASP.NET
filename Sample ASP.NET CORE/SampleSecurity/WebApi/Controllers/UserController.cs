using Application.DTOs;
using Application.Interfaces.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v1/users")]
    [Controller]
    public class UserController : Controller
    {

        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] CreateUserDTO createUserDTO)
        {
            //var user = await userService.CreateNewAsync(createUserDTO);
            //return Ok(user);

            var createdUser = await userService.CreateNewAsync(createUserDTO);
            if (createdUser != null)
            {
                return CreatedAtAction(nameof(GetAll), new { id = createdUser.Id }, createdUser);
            }

            return BadRequest("Unable to create the user.");
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById([FromRoute] int id)
        {
            var user = await userService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteById([FromRoute] int id)
        {
            var result = await userService.DeleteAsync(id);
            if(result > 0)
            {
                return Ok();
            }
            return BadRequest("Id is not valid or existed.");
        }
    }
}

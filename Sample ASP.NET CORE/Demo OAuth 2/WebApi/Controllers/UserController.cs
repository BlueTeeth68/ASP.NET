using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v1/users")]
    [Controller]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById([FromRoute] int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteById([FromRoute] int id)
        {
            var result = await _userService.DeleteAsync(id);
            if(result > 0)
            {
                return Ok();
            }
            return BadRequest("Id is not valid or existed.");
        }
    }
}

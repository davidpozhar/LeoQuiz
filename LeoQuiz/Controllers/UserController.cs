using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LeoQuiz.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetAllUsers")]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var result = _userService.GetAllInterviewees(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            var result = await _userService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto User)
        {
            await _userService.Insert(User);
            return Ok(User);
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> PutUser(UserDto User)
        {
            var result = await _userService.Update(User);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
    }
}

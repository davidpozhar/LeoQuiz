using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LeoQuiz.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserRegisterDto dto)
        {
            var result = await _accountService.SignUp(dto).ConfigureAwait(false);

            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserLoginDto dto)
        {
            var result = await _accountService.SignIn(dto).ConfigureAwait(false);

            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout().ConfigureAwait(false);
            return Ok();
        }

        [Authorize]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var userName =  User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault();

            var result = await _accountService.RefreshToken(userName).ConfigureAwait(false);
            return Ok();
        }
    }
}

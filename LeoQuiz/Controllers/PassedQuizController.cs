using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using Microsoft.AspNetCore.Authorization;
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
    public class PassedQuizController : ControllerBase
    {
        private readonly IPassedQuizService _passedQuizService;

        public PassedQuizController(IPassedQuizService passedQuizService)
        {
            this._passedQuizService = passedQuizService;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<PassedQuizDto>>> GetAll()
        {
            var result = await _passedQuizService.GetAll(User.FindFirstValue(ClaimTypes.NameIdentifier)).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("GetPassedQuizById/{id}")]
        public async Task<ActionResult<IEnumerable<PassedQuizFullDto>>> GetPassedQuizById(int id)
        {
            var result = await _passedQuizService.GetById(id).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("PostPassedQuiz")]
        public async Task<ActionResult<PassedQuizDto>> PostQuiz(PassedQuizDto passedQuiz)
        {
            await _passedQuizService.Insert(passedQuiz).ConfigureAwait(false);
            return Ok(passedQuiz);
        }

        [HttpDelete("DeletePassedQuiz/{id}")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            await _passedQuizService.Delete(id).ConfigureAwait(false);
            return NoContent();
        }

    }
}

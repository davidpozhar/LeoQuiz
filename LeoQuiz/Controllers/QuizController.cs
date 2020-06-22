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
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            this._quizService = quizService;
        }

        [HttpGet("GetAllTest")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetAllTest()
        {
            var result = await _quizService.GetAll("string").ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<QuizInfoDto>>> GetAll()
        {
            var result = await _quizService.GetAllInfo(User.FindFirstValue(ClaimTypes.NameIdentifier)).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("GetQuizById/{id}")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizById(int id)
        {
            var result = await _quizService.GetById(id).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("GetQuizViewById/{id}")]
        public async Task<ActionResult<IEnumerable<QuizViewDto>>> GetQuizViewById(int id)
        {
            var result = await _quizService.GetViewById(id).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("PostQuiz")]
        public async Task<ActionResult<QuizDto>> PostQuiz(QuizDto quiz)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            quiz.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _quizService.Insert(quiz).ConfigureAwait(false);
            return Ok(quiz);
        }


        [HttpPut("PutQuiz")]
        public async Task<ActionResult<QuizDto>> PutQuiz( QuizDto quiz)
        {
            var result = await _quizService.Update(quiz).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpDelete("DeleteQuiz/{id}")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            await _quizService.Delete(id).ConfigureAwait(false);
            return NoContent();
        }
    }
}

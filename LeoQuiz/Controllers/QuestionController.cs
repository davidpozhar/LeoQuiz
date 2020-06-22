using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeoQuiz.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            this._questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {
            var result = await _questionService.GetAll().ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("GetCurrentQuestions/{id}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetCurrentQuestions(int id)
        {
            var result = await _questionService.GetAll(id).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            var result = await _questionService.GetById(id).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDto>> PostQuestion(QuestionDto Question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _questionService.Insert(Question).ConfigureAwait(false);
            return Ok(Question);

        }

        [HttpPut]
        public async Task<ActionResult<QuestionDto>> PutQuestion(QuestionDto Question)
        {
            var result = await _questionService.Update(Question).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            await _questionService.Delete(id).ConfigureAwait(false);
            return NoContent();
        }
    }
}

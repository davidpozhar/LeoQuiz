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

    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            this._answerService = answerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerDto>>> GetAnswers()
        {
            var result = await _answerService.GetAll().ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDto>> GetAnswer(int id)
        {
            var result = await _answerService.GetById(id).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AnswerDto>> PostAnswer(AnswerDto answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _answerService.Insert(answer).ConfigureAwait(false);
            return Ok(answer);
        }

        [HttpPut]
        public async Task<ActionResult<AnswerDto>> PutAnswer(AnswerDto answer)
        {
            var result = await _answerService.Update(answer).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnswer(int id)
        {
            await _answerService.Delete(id).ConfigureAwait(false);
            return NoContent();
        }
    }
}

using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var result = await _answerService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDto>> GetAnswer(int id)
        {
            var result = await _answerService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AnswerDto>> PostAnswer(AnswerDto answer)
        {
            try
            {
                await _answerService.Insert(answer);
                return Ok(answer);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AnswerDto>> PutAnswerAsync(int id, AnswerDto answer)
        {
            var result = await _answerService.Update(answer);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnswer(int id)
        {
            await _answerService.Delete(id);
            return NoContent();
        }
    }
}

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
        public ActionResult<IEnumerable<AnswerDto>> GetAnswers()
        {
            try
            {
                var result = _answerService.GetAll();
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDto>> GetAnswer(int id)
        {
            try
            {
                var result = await _answerService.GetById(id);
                return Ok(result);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch
            {
                return Problem();
            }
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
        public ActionResult<AnswerDto> PutAnswer(int id, AnswerDto answer)
        {
            try
            {
                var result = _answerService.Update(answer);
                return Ok(result);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnswer(int id)
        {
            try
            {
                await _answerService.Delete(id);
                return NoContent();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch
            {
                return Problem();
            }
        }
    }
}

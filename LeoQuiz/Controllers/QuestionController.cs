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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            this._questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestionsAsync()
        {
            try
            {
                var result = await _questionService.GetAll();
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            try
            {
                var result = await _questionService.GetById(id);
                return Ok(result);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDto>> PostQuestion(QuestionDto Question)
        {
            try
            {
                await _questionService.Insert(Question);
                return Ok(Question);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<QuestionDto>> PutQuestionAsync(int id, QuestionDto Question)
        {
            try
            {
                var result = await _questionService.Update(Question);
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
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            await _questionService.Delete(id);
            return NoContent();

        }
    }
}

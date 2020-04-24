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
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            this._quizService = quizService;
        }

        [HttpGet("GetAllTest")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetAllTestAsync()
        {
            try
            {
                var result = await _quizService.GetAll(3);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        //Після авторизації айді визначати всередині
        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult<IEnumerable<QuizInfoDto>>> GetAllAsync(int id)
        {
            try
            {
                var result = await _quizService.GetAllInfo(id);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("GetQuizById/{id}")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizById(int id)
        {
                var result = await _quizService.GetById(id);
                return Ok(result);
        }

        [HttpGet("GetQuizViewById/{id}")]
        public async Task<ActionResult<IEnumerable<QuizViewDto>>> GetQuizViewByIdAsync(int id)
        {
                var result = await _quizService.GetViewById(id);
                return Ok(result);
        }

        [HttpPost("PostQuiz")]
        public async Task<ActionResult<QuizDto>> PostQuiz(QuizDto quiz)
        {
            try
            {
                await _quizService.Insert(quiz);
                return Ok(quiz);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("PutQuiz/{id}")]
        public async Task<ActionResult<QuizDto>> PutQuizAsync(int id, QuizDto quiz)
        {
            var result = await _quizService.Update(quiz);
            return Ok(result);
        }

        [HttpDelete("DeleteQuiz/{id}")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            await _quizService.Delete(id);
            return NoContent();
        }
    }
}

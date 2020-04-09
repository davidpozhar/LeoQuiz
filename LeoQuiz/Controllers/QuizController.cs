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
        public ActionResult<IEnumerable<QuizDto>> GetAllTest()
        {
            try
            {
                var result = _quizService.GetAll(3);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        //Після авторизації айді визначати всередині
        [HttpGet("GetAll/{id}")]
        public ActionResult<IEnumerable<QuizInfoDto>> GetAll(int id)
        {
            try
            {
                var result = _quizService.GetAllInfo(id);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("GetQuizById/{id}")]
        public ActionResult<IEnumerable<QuizDto>> GetQuizById(int id)
        {
            try
            {
                var result = _quizService.GetById(id);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("GetQuizViewById/{id}")]
        public ActionResult<IEnumerable<QuizViewDto>> GetQuizViewById(int id)
        {
            try
            {
                var result = _quizService.GetViewById(id);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
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
        public ActionResult<QuizDto> PutQuiz(int id, QuizDto quiz)
        {
            try
            {
                var result = _quizService.Update(quiz);
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

        [HttpDelete("DeleteQuiz/{id}")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            try
            {
                await _quizService.Delete(id);
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

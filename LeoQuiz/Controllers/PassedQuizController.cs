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
    public class PassedQuizController : ControllerBase
    {
        private readonly IPassedQuizService _passedQuizService;

        public PassedQuizController(IPassedQuizService passedQuizService)
        {
            this._passedQuizService = passedQuizService;
        }

        //Після авторизації айді визначати всередині
        [HttpGet("GetAll/{id}")]
        public ActionResult<IEnumerable<PassedQuizDto>> GetAll(int id)
        {
            try
            {
                var result = _passedQuizService.GetAll(id);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("GetPassedQuizById/{id}")]
        public ActionResult<IEnumerable<PassedQuizFullDto>> GetPassedQuizById(int id)
        {
            try
            {
                var result = _passedQuizService.GetById(id);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("PostPassedQuiz")]
        public async Task<ActionResult<PassedQuizDto>> PostQuiz(PassedQuizDto passedQuiz)
        {
            try
            {
                await _passedQuizService.Insert(passedQuiz);
                return Ok(passedQuiz);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePassedQuiz/{id}")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            try
            {
                await _passedQuizService.Delete(id);
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

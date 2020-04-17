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
        public async Task<ActionResult<IEnumerable<PassedQuizDto>>> GetAllAsync(int id)
        {
            var result = await _passedQuizService.GetAll(id);
            return Ok(result);
        }

        [HttpGet("GetPassedQuizById/{id}")]
        public async Task<ActionResult<IEnumerable<PassedQuizFullDto>>> GetPassedQuizById(int id)
        {
            var result = await _passedQuizService.GetById(id);
            return Ok(result);
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
            await _passedQuizService.Delete(id);
            return NoContent();
        }



    }
}

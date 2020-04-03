using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LeoQuiz.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
   

        private readonly ILogger<QuizController> _logger;

        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {
           
        }
    }
}

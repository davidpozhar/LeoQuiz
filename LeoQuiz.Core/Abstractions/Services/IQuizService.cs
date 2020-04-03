using LeoQuiz.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Services
{
    public interface IQuizService
    {
        public List<QuizDto> GetAll();

        public Task<QuizDto> GetById(int Id);

        public Task<QuizDto> Insert(QuizDto quizDto);

        public QuizDto Update(QuizDto quizDto);

        public Task Delete(int Id);
    }
    
}

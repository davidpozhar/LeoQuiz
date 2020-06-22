using LeoQuiz.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Services
{
    public interface IQuizService
    {
        public Task<List<QuizDto>> GetAll();

        public Task<List<QuizDto>> GetAll(string Id);

        public Task<List<QuizInfoDto>> GetAllInfo(string Id);

        public Task<QuizDto> GetById(int Id);

        public Task<QuizViewDto> GetViewById(int Id);

        public Task<QuizDto> Insert(QuizDto quizDto);

        public Task<QuizDto> Update(QuizDto quizDto);

        public Task Delete(int Id);

    }
    
}

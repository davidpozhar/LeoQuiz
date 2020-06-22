using LeoQuiz.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Services
{
    public interface IQuestionService
    {
        public Task<List<QuestionDto>> GetAll();

        public Task<List<QuestionDto>> GetAll(int Id);

        public Task<QuestionDto> GetById(int Id);

        public Task<QuestionDto> Insert(QuestionDto questionDto);

        public Task<QuestionDto> Update(QuestionDto questionDto);

        public Task Delete(int Id);
    }
}

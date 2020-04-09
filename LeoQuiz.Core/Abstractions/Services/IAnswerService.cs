using LeoQuiz.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Services
{
    public interface IAnswerService
    {
        public List<AnswerDto> GetAll();

        public Task<AnswerDto> GetById(int Id);

        public Task<AnswerDto> Insert(AnswerDto answerDto);

        public AnswerDto Update(AnswerDto answerDto);

        public Task Delete(int Id);
    }
}

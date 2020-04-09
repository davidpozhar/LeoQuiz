using LeoQuiz.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Services
{
    public interface IPassedQuizService
    {
        public List<PassedQuizDto> GetAll(int Id);

        public PassedQuizFullDto GetById(int Id);

        public Task<PassedQuizDto> Insert(PassedQuizDto passedQuizDto);

        public Task Delete(int Id);
    }
}

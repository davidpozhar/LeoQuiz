using LeoQuiz.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Services
{
    public interface IPassedQuizService
    {
        public Task<List<PassedQuizDto>> GetAll(string Id);

        public Task<PassedQuizFullDto> GetById(int Id);

        public Task<PassedQuizDto> Insert(PassedQuizDto passedQuizDto);

        public Task Delete(int Id);
    }
}

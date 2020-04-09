using AutoMapper;
using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoQuiz.Services
{
    public class PassedQuizService : IPassedQuizService
    {
        private readonly IPassedQuizRepository _passedquizRepository;

        private readonly IMapper _mapper;

        public PassedQuizService(IMapper mapper, IPassedQuizRepository passedquizRepository)
        {
            this._mapper = mapper;
            this._passedquizRepository = passedquizRepository;
        }

        //return all passedquiz with user and answers (for admin)
        public List<PassedQuizDto> GetAll(int Id)
        {
            return _passedquizRepository.GetAll()
                .Include(pasquiz => pasquiz.PassedQuizAnswers)
                .Include(pasquiz => pasquiz.User)
                .Where(pasquiz => pasquiz.Quiz.UserId == Id)
                .Select(el => _mapper.Map(el, new PassedQuizDto())).ToList();
        }
        
        //return full info about passed quiz with correct answers
        public PassedQuizFullDto GetById(int Id)
        {
            return _passedquizRepository.GetAll()
                .Include(passquiz => passquiz.User)
                .Include(passquiz => passquiz.PassedQuizAnswers)
                .ThenInclude(passanswers => passanswers)
                .ThenInclude(passanswer => passanswer.Answer)
                 .Select(el => _mapper.Map(el, new PassedQuizFullDto()))
                .ToList()
                .FirstOrDefault();
             
        }

        public async Task<PassedQuizDto> Insert(PassedQuizDto passedQuizDto)
        {
            CheckPassedQuiz(passedQuizDto);
            var entity = new PassedQuiz();
            _mapper.Map(passedQuizDto, entity);
            entity.User.UserRoleId = 2;
            await _passedquizRepository.Insert(entity);
            _passedquizRepository.Save();
            _mapper.Map(entity, passedQuizDto);
            return passedQuizDto;
        }

        public async Task Delete(int Id)
        {
            await _passedquizRepository.Delete(Id);
            await _passedquizRepository.SaveAsync();
        }

        private void CheckPassedQuiz(PassedQuizDto dto)
        {
            var PassedQuizzes = _passedquizRepository.GetAll().Where(passquiz => (passquiz.QuizId == dto.QuizId) && (passquiz.UserId == passquiz.UserId)).ToList();
            var result = _passedquizRepository.GetAll().Include(passquiz => passquiz.Quiz).ToList().FirstOrDefault();
            if (result != null)
            {
                var maxAttempts = result.Quiz.MaxAttempts;
                if (PassedQuizzes.Count >= maxAttempts)
                {
                    throw new System.Exception();
                }
            }
        }
    }
}

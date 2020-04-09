using AutoMapper;
using FluentValidation;
using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;
using LeoQuiz.Core.Validators;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoQuiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        private readonly IMapper _mapper;

        public QuizService(IMapper mapper, IQuizRepository quizRepository)
        {
            this._mapper = mapper;
            this._quizRepository = quizRepository;
        }
       
        public List<QuizDto> GetAll()
        {
            return _quizRepository.GetAll().Select(el => _mapper.Map(el, new QuizDto())).ToList();
        }

        //Якщо інфу всю відразу витягувати треба
        public List<QuizDto> GetAll(int Id)
        {
            return _quizRepository.GetAll()
                .Include(quiz => quiz.Questions)
                .ThenInclude(questions => questions)
                .ThenInclude(question => question.Answers)
               // .Where(quiz => quiz.UserId == Id)
                .Select(el => _mapper.Map(el, new QuizDto())).ToList();

        }

        public List<QuizInfoDto> GetAllInfo(int Id)
        {
            return _quizRepository.GetAll()
                .Where(quiz => quiz.UserId == Id)
                .Select(el => _mapper.Map(el, new QuizInfoDto())).ToList();

        }

        public QuizDto GetById(int Id)
        {
            return _quizRepository.GetAll()
                .Include(quiz => quiz.Questions)
                .ThenInclude(questions => questions)
                .ThenInclude(question => question.Answers)
                .Where(quiz => quiz.Id == Id)
                .Select(el => _mapper.Map(el, new QuizDto()))
                .ToList()
                .FirstOrDefault();
        }

        public QuizViewDto GetViewById(int Id)
        {
            return _quizRepository.GetAll()
                .Include(quiz => quiz.Questions)
                .ThenInclude(questions => questions)
                .ThenInclude(question => question.Answers)
                .Where(quiz => quiz.Id == Id)
                .Select(el => _mapper.Map(el, new QuizViewDto()))
                .ToList()
                .FirstOrDefault();
        }
    

        public async Task<QuizDto> Insert(QuizDto quizDto)
        {
            Validation(quizDto);
            var entity = new Quiz();
            _mapper.Map(quizDto, entity);
            entity.QuizUrl = GenerateUrl(quizDto.Name);
            await _quizRepository.Insert(entity);
             _quizRepository.Save();
            _mapper.Map(entity, quizDto);
            return quizDto;
        }

        public QuizDto Update(QuizDto quizDto)
        {
            Validation(quizDto);
            var entity = new Quiz();
            _mapper.Map(quizDto, entity);
            entity.QuizUrl = GenerateUrl(quizDto.Name);
            _quizRepository.Update(entity);
            _quizRepository.Save();
            _mapper.Map(entity, quizDto);
            return quizDto;
        }
        public async Task Delete(int Id)
        {
            await _quizRepository.Delete(Id);
            await _quizRepository.SaveAsync();
        }

        private void Validation(QuizDto dto)
        {
            var validator = new QuizValidator();
            validator.ValidateAndThrow(dto);
        }

        private string GenerateUrl(string quizInfo)
        {
            var randomString = System.IO.Path.GetRandomFileName();
            var result = string.Concat(randomString.Zip(randomString, (a, b) => new[] { a, b }).SelectMany(c => c));
            var Length = System.Math.Min(randomString.Length, quizInfo.Length);

           
            return "http://localhost:5000/LeoQuiz/Quiz/" + randomString.Substring(Length) + quizInfo.Substring(Length); ;
        }
    }
}

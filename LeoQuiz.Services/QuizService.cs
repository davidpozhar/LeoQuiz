using AutoMapper;
using AutoMapper.QueryableExtensions;
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
       
        public async Task<List<QuizDto>> GetAll()
        {
            return await _quizRepository.GetAll()
                .Select(el => _mapper.Map(el, new QuizDto()))
                .ToListAsync();
        }

        //Якщо інфу всю відразу витягувати треба
        public async Task<List<QuizDto>> GetAll(string Id)
        {
            return await _quizRepository.GetAll()
               // .Where(quiz => quiz.UserId == Id)
               .ProjectTo<QuizDto>(_mapper.ConfigurationProvider)
               .ToListAsync();

        }

        public async Task<List<QuizInfoDto>> GetAllInfo(string Id)
        {
            return await _quizRepository.GetAll()
                .Where(quiz => quiz.UserId == Id)
                .Select(el => _mapper.Map(el, new QuizInfoDto()))
                .ToListAsync();

        }

        public async Task<QuizDto> GetById(int Id)
        {
            return await _quizRepository.GetAll()
                .Where(quiz => quiz.Id == Id)
                .ProjectTo<QuizDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<QuizViewDto> GetViewById(int Id)
        {
            return await _quizRepository.GetAll()
                .Where(quiz => quiz.Id == Id)
                .ProjectTo<QuizViewDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    

        public async Task<QuizDto> Insert(QuizDto quizDto)
        {
            Validation(quizDto);
            var entity = _mapper.Map(quizDto, new Quiz());
            entity.QuizUrl = GenerateUrl(quizDto.Name);
            await _quizRepository.Insert(entity);
            await _quizRepository.SaveAsync();
            return _mapper.Map<Quiz, QuizDto>(entity);

        }

        public async Task<QuizDto> Update(QuizDto quizDto)
        {
            Validation(quizDto);
            var entity = _mapper.Map(quizDto, new Quiz());
            entity.QuizUrl = GenerateUrl(quizDto.Name);
            _quizRepository.Update(entity);
            await _quizRepository.SaveAsync();
            return _mapper.Map<Quiz, QuizDto>(entity);
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

           
            return randomString.Substring(Length) + quizInfo.Substring(Length); ;
        }
    }
}

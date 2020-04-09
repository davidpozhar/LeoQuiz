using AutoMapper;
using FluentValidation;
using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;
using LeoQuiz.Core.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoQuiz.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        private readonly IMapper _mapper;

        public QuestionService(IMapper mapper, IQuestionRepository questionRepository)
        {
            this._mapper = mapper;
            this._questionRepository = questionRepository;
        }

        public List<QuestionDto> GetAll()
        {
            return _questionRepository.GetAll().Select(el => _mapper.Map(el, new QuestionDto())).ToList();
        }

        public async Task<QuestionDto> GetById(int Id)
        {
            var entity = await _questionRepository.GetById(Id);
            var dto = new QuestionDto();
            _mapper.Map(entity, dto);
            return dto;
        }

        public async Task<QuestionDto> Insert(QuestionDto questionDto)
        {
            Validation(questionDto);
            var entity = new Question();
            _mapper.Map(questionDto, entity);
            await _questionRepository.Insert(entity);
            await _questionRepository.SaveAsync();
            _mapper.Map(entity, questionDto);
            return questionDto;
        }

        public QuestionDto Update(QuestionDto questionDto)
        {
            Validation(questionDto);
            var entity = new Question();
            _mapper.Map(questionDto, entity);
            _questionRepository.Update(entity);
            _questionRepository.Save();
            _mapper.Map(entity, questionDto);
            return questionDto;
        }

        public async Task Delete(int Id)
        {
            await _questionRepository.Delete(Id);
            await _questionRepository.SaveAsync();
        }

        private void Validation(QuestionDto dto)
        {
            var validator = new QuestionValidator();
            validator.ValidateAndThrow(dto);
        }
    }
}

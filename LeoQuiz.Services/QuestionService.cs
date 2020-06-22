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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        private readonly IMapper _mapper;

        public QuestionService(IMapper mapper, IQuestionRepository questionRepository)
        {
            this._mapper = mapper;
            this._questionRepository = questionRepository;
        }

        public async Task<List<QuestionDto>> GetAll()
        {
            return await _questionRepository.GetAll()
                .Select(el => _mapper.Map(el, new QuestionDto()))
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<QuestionDto>> GetAll(int quizId)
        {
            return await _questionRepository.GetAll().Where(q=>q.QuizId == quizId)
                .Select(el => _mapper.Map(el, new QuestionDto()))
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<QuestionDto> GetById(int Id)
        {
            return await _questionRepository.GetAll()
                .Where(q => q.Id == Id)
                .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync().ConfigureAwait(false);           
        }

        public async Task<QuestionDto> Insert(QuestionDto questionDto)
        {
            Validation(questionDto);
            var entity = _mapper.Map(questionDto, new Question());
            await _questionRepository.Insert(entity).ConfigureAwait(false);
            await _questionRepository.SaveAsync().ConfigureAwait(false);
            return _mapper.Map<Question, QuestionDto>(entity);
        }

        public async Task<QuestionDto> Update(QuestionDto questionDto)
        {
            Validation(questionDto);
            var entity = _mapper.Map(questionDto, new Question());
            _questionRepository.Update(entity);
            await _questionRepository.SaveAsync().ConfigureAwait(false);
            return _mapper.Map<Question, QuestionDto>(entity);
        }

        public async Task Delete(int Id)
        {
            await _questionRepository.Delete(Id).ConfigureAwait(false);
            await _questionRepository.SaveAsync().ConfigureAwait(false);
        }

        private void Validation(QuestionDto dto)
        {
            var validator = new QuestionValidator();
            validator.ValidateAndThrow(dto);
        }
    }
}

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
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        private readonly IMapper _mapper;

        public AnswerService(IMapper mapper, IAnswerRepository answerRepository)
        {
            this._mapper = mapper;
            this._answerRepository = answerRepository;
        }

        public async Task<List<AnswerDto>> GetAll()
        {
            return await _answerRepository.GetAll().Select(el => _mapper.Map(el, new AnswerDto())).ToListAsync().ConfigureAwait(false);
        }

        public async Task<AnswerDto> GetById(int Id)
        {
            var entity = await _answerRepository.GetById(Id).ConfigureAwait(false);
            return _mapper.Map<Answer, AnswerDto>(entity);
        }

        public async Task<AnswerDto> Insert(AnswerDto answerDto)
        {
            Validation(answerDto);
            var entity = _mapper.Map(answerDto, new Answer());
            await _answerRepository.Insert(entity).ConfigureAwait(false);
            await _answerRepository.SaveAsync().ConfigureAwait(false);
            return _mapper.Map<Answer, AnswerDto>(entity);
        }

        public async Task<AnswerDto> Update(AnswerDto answerDto)
        {
            Validation(answerDto);
            var entity = _mapper.Map(answerDto, new Answer());
            _answerRepository.Update(entity);
            await _answerRepository.SaveAsync().ConfigureAwait(false);
            return _mapper.Map<Answer, AnswerDto>(entity);
        }

        public async Task Delete(int Id)
        {
            await _answerRepository.Delete(Id).ConfigureAwait(false);
            await _answerRepository.SaveAsync().ConfigureAwait(false);
        }

        private void Validation(AnswerDto dto)
        {
            var validator = new AnswerValidator();
            validator.ValidateAndThrow(dto);
        }
    }
}

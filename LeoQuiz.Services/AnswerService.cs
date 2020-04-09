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
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        private readonly IMapper _mapper;

        public AnswerService(IMapper mapper, IAnswerRepository answerRepository)
        {
            this._mapper = mapper;
            this._answerRepository = answerRepository;
        }

        public List<AnswerDto> GetAll()
        {
            return _answerRepository.GetAll().Select(el => _mapper.Map(el, new AnswerDto())).ToList();
        }

        public async Task<AnswerDto> GetById(int Id)
        {
            var entity = await _answerRepository.GetById(Id);
            var dto = new AnswerDto();
            _mapper.Map(entity, dto);
            return dto;
        }

        public async Task<AnswerDto> Insert(AnswerDto answerDto)
        {
            Validation(answerDto);
            var entity = new Answer();
            _mapper.Map(answerDto, entity);
            await _answerRepository.Insert(entity);
            await _answerRepository.SaveAsync();
            _mapper.Map(entity, answerDto);
            return answerDto;
        }

        public AnswerDto Update(AnswerDto answerDto)
        {
            Validation(answerDto);
            var entity = new Answer();
            _mapper.Map(answerDto, entity);
            _answerRepository.Update(entity);
            _answerRepository.Save();
            _mapper.Map(entity, answerDto);
            return answerDto;
        }

        public async Task Delete(int Id)
        {
            await _answerRepository.Delete(Id);
            await _answerRepository.SaveAsync();
        }

        private void Validation(AnswerDto dto)
        {
            var validator = new AnswerValidator();
            validator.ValidateAndThrow(dto);
        }
    }
}

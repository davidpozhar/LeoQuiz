using AutoMapper;
using LeoQuiz.Core.Abstractions;
using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoQuiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuizService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
       
        public List<QuizDto> GetAll()
        {
            return _unitOfWork.QuizRepository.GetAll().Select(el => _mapper.Map(el, new QuizDto())).ToList();

        }

        public async Task<QuizDto> GetById(int Id)
        {
            var entity = await _unitOfWork.QuizRepository.GetById(Id);
            var dto = new QuizDto();
            _mapper.Map(entity, dto);
            return dto;
        }

        public async Task<QuizDto> Insert(QuizDto quizDto)
        {
            var entity = new Quiz();
            _mapper.Map(quizDto, entity);
            await _unitOfWork.QuizRepository.Insert(entity);
            await _unitOfWork.SaveAsync();
            _mapper.Map(entity, quizDto);
            return quizDto;
        }

        public QuizDto Update(QuizDto quizDto)
        {
            var entity = new Quiz();
            _mapper.Map(quizDto, entity);
            _unitOfWork.QuizRepository.Update(entity);
            _unitOfWork.Save();
            _mapper.Map(entity, quizDto);
            return quizDto;
        }
        public async Task Delete(int Id)
        {
            await _unitOfWork.QuizRepository.Delete(Id);
            await _unitOfWork.SaveAsync();
        }
    }
}

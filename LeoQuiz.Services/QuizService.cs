using AutoMapper;
using LeoQuiz.Core.Abstractions.Services;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;
using LeoQuiz.DAL;
using LeoQuiz.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeoQuiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly LeoQuizApiContext _dbContext;

        private QuizRepository _quizRepository;

        private readonly IMapper _mapper;

        public QuizService(LeoQuizApiContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
       
        public List<QuizDto> GetAll()
        {
            return _quizRepository.GetAll().Select(el => _mapper.Map(el, new QuizDto())).ToList();

        }

        public async Task<QuizDto> GetById(int Id)
        {
            var entity = await _quizRepository.GetById(Id);
            var dto = new QuizDto();
            _mapper.Map(entity, dto);
            return dto;
        }

        public async Task<QuizDto> Insert(QuizDto quizDto)
        {
            var entity = new Quiz();
            _mapper.Map(quizDto, entity);
            await _quizRepository.Insert(entity);
            await _dbContext.SaveChangesAsync();
            _mapper.Map(entity, quizDto);
            return quizDto;
        }

        public QuizDto Update(QuizDto quizDto)
        {
            var entity = new Quiz();
            _mapper.Map(quizDto, entity);
            _quizRepository.Update(entity);
            _dbContext.SaveChanges();
            _mapper.Map(entity, quizDto);
            return quizDto;
        }
        public async Task Delete(int Id)
        {
            await _quizRepository.Delete(Id);
            await _dbContext.SaveChangesAsync();
        }
    }
}

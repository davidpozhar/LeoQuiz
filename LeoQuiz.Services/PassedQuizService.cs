using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public PassedQuizService(IMapper mapper, IPassedQuizRepository passedquizRepository, IAnswerRepository answerRepository, IQuizRepository quizRepository, IUserService userService)
        {
            this._mapper = mapper;
            this._passedquizRepository = passedquizRepository;
            this._answerRepository = answerRepository;
            this._quizRepository = quizRepository;
            this._userService = userService;
        }

        //return all passedquiz with user and answers (for admin)
        public async Task<List<PassedQuizDto>> GetAll(string Id)
        {
            return await _passedquizRepository.GetAll()
                .Include(pasquiz => pasquiz.PassedQuizAnswers)
                .Include(pasquiz => pasquiz.User)
                .Where(pasquiz => pasquiz.Quiz.UserId == Id)
                .ProjectTo<PassedQuizDto>(_mapper.ConfigurationProvider)
                .ToListAsync().ConfigureAwait(false);
        }

        //return full info about passed quiz with correct answers
        public async Task<PassedQuizFullDto> GetById(int Id)
        {
            var result = await _passedquizRepository.GetAll()
               .Include(passquiz => passquiz.User)
               .Where(passquiz => passquiz.Id == Id)
               .ProjectTo<PassedQuizFullDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync().ConfigureAwait(false);

            if (result == null) throw new System.NullReferenceException();

            return result;
        }

        public async Task<PassedQuizDto> Insert(PassedQuizDto passedQuizDto)
        {
            CheckPassedQuiz(passedQuizDto);
            CheckExistUser(passedQuizDto);
            var entity = new PassedQuiz();
            _mapper.Map(passedQuizDto, entity);
            entity.User.UserRoleId = 2;
            entity.Grade = CalculateGrade(entity.PassedQuizAnswers, entity.QuizId);
            entity.isPassed = IsPassed(entity.QuizId, entity.Grade);
            await _passedquizRepository.Insert(entity).ConfigureAwait(false);
            await _passedquizRepository.SaveAsync().ConfigureAwait(false);
            return _mapper.Map<PassedQuiz, PassedQuizDto>(entity);
        }

        public async Task Delete(int Id)
        {
            await _passedquizRepository.Delete(Id).ConfigureAwait(false);
            await _passedquizRepository.SaveAsync().ConfigureAwait(false);
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

        private int CalculateGrade(List<PassedQuizAnswer> answers, int quizId)
        {
            var allAnswers = _answerRepository.GetAll();
            var passedCorrectCount = 0;
            var quizCorrectCount = 0;
            var quiz = _quizRepository.GetAll().Where(q=>q.Id == quizId).Include(quiz=>quiz.Questions).ThenInclude(question=>question.Answers).FirstOrDefault();
            var questionList = quiz.Questions;

            foreach (var a in answers)
            {
                if (allAnswers.Where(answer => answer.Id == a.AnswerId).ToList().FirstOrDefault().IsCorrect == true)
                {
                    passedCorrectCount += 1;
                }
            }

            foreach (var q in questionList)
            {
                foreach (var answer in q.Answers)
                {
                    quizCorrectCount = answer.IsCorrect ? quizCorrectCount++ : quizCorrectCount;
                }
            }

            var grade = passedCorrectCount / quizCorrectCount * 100;
            return grade;
        }

        private bool IsPassed(int quizId, int passedQuizGrade)
        {
            var result = false;
            var quizPassGrade = _quizRepository.GetById(quizId).Result.PassGrade;

            if(passedQuizGrade >= quizPassGrade)
            {
                result = true;
            }

            return result;
        }

        private void CheckExistUser(PassedQuizDto dto)
        {
            var user = _userService.GetById(dto.UserId).Result;

            if(user != null)
            {
                dto.User = null;
                dto.UserId = user.Id;
            }
        }
    }
}

using AutoMapper;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;

namespace LeoQuiz.Core
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Quiz, QuizDto>().ReverseMap();
            CreateMap<Quiz, QuizInfoDto>().ReverseMap();
            CreateMap<Quiz, QuizViewDto>().ReverseMap();

            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Question, QuestionViewDto>().ReverseMap();

            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<Answer, AnswerViewDto>().ReverseMap();

            CreateMap<PassedQuiz, PassedQuizDto>().ReverseMap();
            CreateMap<PassedQuiz, PassedQuizFullDto>().ReverseMap();

            CreateMap<PassedQuizAnswer, PassedQuizAnswerDto>().ReverseMap();
            CreateMap<PassedQuizAnswer, PassedQuizAnswerFullDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}

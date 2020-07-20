using AutoMapper;
using LeoQuiz.Core.CustomTypes;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;
using System.Linq;

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
            CreateMap<QuestionViewDto, Question>();
            CreateMap<Question, QuestionViewDto>().ForPath(s => s.Type, opt => opt.MapFrom(src => src.Answers.Count(a => a.IsCorrect) > 1 ? 1 : 1));


            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<Answer, AnswerViewDto>().ReverseMap();

            CreateMap<PassedQuiz, PassedQuizDto>().ReverseMap();
            CreateMap<PassedQuiz, PassedQuizFullDto>().ReverseMap();

            CreateMap<PassedQuizAnswer, PassedQuizAnswerDto>().ReverseMap();
            CreateMap<PassedQuizAnswer, PassedQuizAnswerFullDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();

            //CreateMap<EnumAnswerType, string>().ConvertUsing(src => src.ToString());
        }
    }
}

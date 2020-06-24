using AutoMapper;
using LeoQuiz.Core.Dto;
using LeoQuiz.Core.Entities;

namespace LeoQuiz.Core
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //CreateMap<Quiz, QuizDto>()
            //    .ForPath(e=>e.TimeLimit.Hours, opt=>opt.MapFrom(src=>src.TimeLimit.Hours))
            //    .ForPath(e => e.TimeLimit.Minutes, opt => opt.MapFrom(src => src.TimeLimit.Minutes))
            //    .ForPath(e => e.TimeLimit.Seconds, opt => opt.MapFrom(src => src.TimeLimit.Seconds));
            //CreateMap<QuizDto, Quiz>()
            //    .ForPath(e => e.TimeLimit.Hours, opt => opt.MapFrom(src => src.TimeLimit.Hours))
            //    .ForPath(e => e.TimeLimit.Minutes, opt => opt.MapFrom(src => src.TimeLimit.Minutes))
            //    .ForPath(e => e.TimeLimit.Seconds, opt => opt.MapFrom(src => src.TimeLimit.Seconds));
            //CreateMap<Quiz, QuizInfoDto>().ReverseMap();
            //CreateMap<Quiz, QuizViewDto>()
            //    .ForPath(e => e.TimeLimit.Hours, opt => opt.MapFrom(src => src.TimeLimit.Hours))
            //    .ForPath(e => e.TimeLimit.Minutes, opt => opt.MapFrom(src => src.TimeLimit.Minutes))
            //    .ForPath(e => e.TimeLimit.Seconds, opt => opt.MapFrom(src => src.TimeLimit.Seconds));
            //CreateMap<QuizViewDto, Quiz>()
            //    .ForPath(e => e.TimeLimit.Hours, opt => opt.MapFrom(src => src.TimeLimit.Hours))
            //    .ForPath(e => e.TimeLimit.Minutes, opt => opt.MapFrom(src => src.TimeLimit.Minutes))
            //    .ForPath(e => e.TimeLimit.Seconds, opt => opt.MapFrom(src => src.TimeLimit.Seconds));

            CreateMap<Quiz, QuizDto>().ReverseMap();
            CreateMap<Quiz, QuizInfoDto>().ReverseMap();
            CreateMap<Quiz, QuizViewDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Question, QuestionViewDto>().ReverseMap();
            CreateMap<Question, PassedQuizQuestionDto>().ReverseMap();

            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<Answer, AnswerViewDto>().ReverseMap();
            CreateMap<Answer, PassedAnswerDto>().ReverseMap();
            CreateMap<PassedQuizAnswer, PassedQuizAnswerDto>().ReverseMap();
            CreateMap<PassedQuizAnswer, PassedQuizAnswerFullDto>().ReverseMap();

            CreateMap<PassedQuiz, PassedQuizDto>().ReverseMap();
            CreateMap<PassedQuiz, PassedQuizFullDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}

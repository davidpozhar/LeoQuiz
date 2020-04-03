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
        }
    }
}

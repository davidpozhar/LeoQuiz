using System;
using System.Collections.Generic;

namespace LeoQuiz.Core.Dto
{
    public class PassedQuizDto : IDto<int>
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public DateTime PassDate { get; set; }

        public int QuizId { get; set; }

        public bool isPassed { get; set; }

        public string UserId { get; set; }

        public UserDto User { get; set; }

        public List<PassedQuizAnswerDto> PassedQuizAnswers { get; set; }
    }
}

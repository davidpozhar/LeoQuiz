using System;
using System.Collections.Generic;

namespace LeoQuiz.Core.Dto
{
    public class PassedQuizQuestionDto : IDto<int>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public TimeSpan TimeLimit { get; set; }

        public List<PassedQuizAnswerDto> Answers { get; set; }

    }
}
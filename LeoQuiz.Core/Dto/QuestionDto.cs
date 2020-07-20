using LeoQuiz.Core.CustomTypes;
using System;
using System.Collections.Generic;

namespace LeoQuiz.Core.Dto
{
    public class QuestionDto : IDto<int>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int QuizId { get; set; }

        public List<AnswerDto> Answers { get; set; }

    }
}

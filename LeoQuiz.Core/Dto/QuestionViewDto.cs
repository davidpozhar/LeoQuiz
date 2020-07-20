using LeoQuiz.Core.CustomTypes;
using System.Collections.Generic;

namespace LeoQuiz.Core.Dto
{
    public class QuestionViewDto : IDto<int>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int TimeLimit { get; set; }

        public int Type { get; set; }

        public int QuizId { get; set; }

        public List<AnswerViewDto> Answers { get; set; }

    }
}

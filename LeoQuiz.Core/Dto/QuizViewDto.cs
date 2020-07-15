using LeoQuiz.Core.CustomTypes;
using System.Collections.Generic;

namespace LeoQuiz.Core.Dto
{
    public class QuizViewDto : IDto<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TimeLimit { get; set; }

        public int MaxAttempts { get; set; }

        public int PassGrade { get; set; }

        public string QuizUrl { get; set; }

        public List<QuestionViewDto> Questions { get; set; }
    }
}

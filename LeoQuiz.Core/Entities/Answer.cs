using System.Collections.Generic;

namespace LeoQuiz.Core.Entities
{
    public class Answer : IEntity<int>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public List<PassedQuizAnswer> PassedQuizAnswers { get; set; }
    }
}

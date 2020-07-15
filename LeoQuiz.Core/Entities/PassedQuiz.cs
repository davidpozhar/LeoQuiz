using System;
using System.Collections.Generic;

namespace LeoQuiz.Core.Entities
{
    public class PassedQuiz : IEntity<int>
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public DateTime PassDate { get; set; }

        public bool isPassed { get; set; }

        public int QuizId { get; set; }

        public string UserId { get; set; }

        public Quiz Quiz { get; set; }

        public User User { get; set; }

        public List<PassedQuizAnswer> PassedQuizAnswers { get; set; }
    }
}

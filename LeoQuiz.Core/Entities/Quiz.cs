using LeoQuiz.Core.CustomTypes;
using System;
using System.Collections.Generic;

namespace LeoQuiz.Core.Entities
{
    public class Quiz : IEntity<int>
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public int TimeLimit { get; set; }

        public int MaxAttempts { get; set; }

        public int PassGrade { get; set; }

        public string QuizUrl { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public List<Question> Questions { get; set; }

        public List<PassedQuiz> PassedQuizzes { get; set; }
    }
}

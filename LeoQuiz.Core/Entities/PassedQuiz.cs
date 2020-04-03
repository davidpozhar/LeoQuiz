using System;

namespace LeoQuiz.Core.Entities
{
    public class PassedQuiz : IEntity<int>
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public DateTime PassDate { get; set; }

        public int QuizId { get; set; }

        public int UserId { get; set; }

        public Quiz Quiz { get; set; }

        public User User { get; set; }
    }
}

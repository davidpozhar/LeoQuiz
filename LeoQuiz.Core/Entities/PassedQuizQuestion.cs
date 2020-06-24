using System.Collections.Generic;

namespace LeoQuiz.Core.Entities
{
    public class PassedQuizQuestion : IEntity<int>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int PassedQuizId { get; set; }

        public PassedQuiz PassedQuiz { get; set; }


        public List<PassedQuizAnswer> Answers { get; set; }
    }
}


using System.Collections.Generic;

namespace LeoQuiz.Core.Entities
{
    public class User : IEntity<int> 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public List<Quiz> Quizzes { get; set; }

        public List<PassedQuiz> PassedQuizzes { get; set; }

    }
}

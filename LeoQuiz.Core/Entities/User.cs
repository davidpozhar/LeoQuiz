using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LeoQuiz.Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public int UserRoleId { get; set; }

        public List<Quiz> Quizzes { get; set; }

        public List<PassedQuiz> PassedQuizzes { get; set; }
    }
}

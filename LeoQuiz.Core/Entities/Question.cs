using LeoQuiz.Core.CustomTypes;
using System.Collections.Generic;
using System.Linq;

namespace LeoQuiz.Core.Entities
{
    public class Question : IEntity<int>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public List<Answer> Answers { get; set; }
    }
}

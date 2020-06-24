namespace LeoQuiz.Core.Entities
{
    public class PassedQuizAnswer : IEntity<int>
    {
        public int Id { get; set; }

        public int PassedQuizId { get; set; }

        public int AnswerId { get; set; }

        public bool IsChecked { get; set; }

        public bool IsCorrect { get; set; }

        public int PassedQuizQuestionId { get; set; }

        public PassedQuizQuestion PassedQuizQuestion { get; set; }

        public Answer Answer { get; set; }
    }
}

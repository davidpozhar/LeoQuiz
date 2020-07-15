namespace LeoQuiz.Core.Entities
{
    public class PassedQuizAnswer : IEntity<int>
    {
        public int Id { get; set; }

        public int PassedQuizId { get; set; }

        public int AnswerId { get; set; }

        public PassedQuiz PassedQuiz { get; set; }

        public Answer Answer { get; set; }
    }
}

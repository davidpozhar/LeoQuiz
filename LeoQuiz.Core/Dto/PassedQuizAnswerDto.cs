namespace LeoQuiz.Core.Dto
{
    public class PassedQuizAnswerDto : IDto<int>
    {
        public int Id { get; set; }

        public int PassedQuizId { get; set; }

        public int AnswerId { get; set; }
    }
}

namespace LeoQuiz.Core.Dto
{
    public class AnswerDto : IDto<int>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
    }
}

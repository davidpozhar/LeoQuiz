namespace LeoQuiz.Core.Dto
{
    public class QuizInfoDto : IDto<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

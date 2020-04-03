namespace LeoQuiz.Core.Dto
{
    public interface IDto<TId>
    {
        TId Id { get; set; }
    }
}

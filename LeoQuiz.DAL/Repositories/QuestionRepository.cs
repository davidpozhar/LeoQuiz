using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Entities;

namespace LeoQuiz.DAL.Repositories
{
    public class QuestionRepository : BaseRepository<Question, int>, IQuestionRepository
    {
        public QuestionRepository(LeoQuizApiContext context) : base(context)
        {
        }
    }
}

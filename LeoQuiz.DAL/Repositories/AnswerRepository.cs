using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Entities;

namespace LeoQuiz.DAL.Repositories
{
    public class AnswerRepository : BaseRepository<Answer, int>, IAnswerRepository
    {
        public AnswerRepository(LeoQuizApiContext context) : base(context)
        {
        }
    }
}

using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Entities;

namespace LeoQuiz.DAL.Repositories
{
    public class QuizRepository : BaseRepository<Quiz, int>, IQuizRepository
    {
        public QuizRepository(LeoQuizApiContext context) : base(context)
        {
        }
    }
}

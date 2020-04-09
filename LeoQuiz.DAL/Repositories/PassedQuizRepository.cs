using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Entities;

namespace LeoQuiz.DAL.Repositories
{
    public class PassedQuizRepository :BaseRepository<PassedQuiz, int>, IPassedQuizRepository
    {
        public PassedQuizRepository(LeoQuizApiContext context) : base(context) { }
    }
}

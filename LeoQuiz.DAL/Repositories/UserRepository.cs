using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Entities;

namespace LeoQuiz.DAL.Repositories
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(LeoQuizApiContext context) : base(context)
        {
        }
    }
}

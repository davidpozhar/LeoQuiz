using LeoQuiz.Core.Abstractions;
using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.DAL.Repositories;
using System.Threading.Tasks;

namespace LeoQuiz.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LeoQuizApiContext _dbContext;

        private QuizRepository _quizRepository;

        public UnitOfWork(LeoQuizApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQuizRepository QuizRepository => _quizRepository ??= new QuizRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

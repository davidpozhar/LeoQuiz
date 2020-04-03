using LeoQuiz.Core.Abstractions.Repositories;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions
{
    public interface IUnitOfWork
    {
        public IQuizRepository QuizRepository { get; }

        public void Save();

        public Task SaveAsync();
    }
}

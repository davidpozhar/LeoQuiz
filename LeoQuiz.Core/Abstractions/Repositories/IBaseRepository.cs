using LeoQuiz.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace LeoQuiz.Core.Abstractions.Repositories
{
    public interface IBaseRepository<TEntity, TId> where TEntity : class
    {
        public IQueryable<TEntity> GetAll();

        public Task<TEntity> GetById(TId Id);

        public Task Insert(TEntity Entity);

        public TEntity Update(TEntity Entity);

        public Task Delete(TId Id);

        public void Save();

        public Task SaveAsync();
    }
}

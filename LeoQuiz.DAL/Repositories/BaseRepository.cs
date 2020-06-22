using LeoQuiz.Core.Abstractions.Repositories;
using LeoQuiz.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LeoQuiz.DAL.Repositories
{
    public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : class
    {
        private readonly LeoQuizApiContext _dbContext;
        public BaseRepository(LeoQuizApiContext context)
        {
            _dbContext = context;
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(TId id)
        {
            var result =  await _dbContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
            NullChecked(result);
            return result;
        }

        public async Task Insert(TEntity Entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(Entity).ConfigureAwait(false);
        }

        public TEntity Update(TEntity Entity)
        {
            NullChecked(Entity);
            var result = _dbContext.Set<TEntity>().Update(Entity);
            return result.Entity;
        }

        public async Task Delete(TId Id)
        {
            var entityToDelete = await _dbContext.Set<TEntity>().FindAsync(Id).ConfigureAwait(false);
            NullChecked(entityToDelete);
            _dbContext.Set<TEntity>().Remove(entityToDelete);

        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void SaveWithSoft()
        {
            UpdateSoftDeleteStatuses();
            _dbContext.SaveChanges();

        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["isDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["isDeleted"] = true;
                        break;
                }
            }
        }

        private void NullChecked(TEntity entityToCheck)
        {
            if (!(_dbContext.Set<TEntity>().Contains(entityToCheck)))
            {
                throw new System.NullReferenceException();
            }
        }
    }
}

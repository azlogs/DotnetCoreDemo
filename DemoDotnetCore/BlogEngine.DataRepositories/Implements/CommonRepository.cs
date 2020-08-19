using BlogEngine.DataModels.Models;
using BlogEngine.DataRepositories.Interfaces;
using BlogEngine.DataRepositories.Interfaces.IUserRepositories;
using BlogEngine.ViewModels.UserViewmodels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Implements
{
    public abstract class CommonRepository<TEntity> : ICommonRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly BlogEngineDatabaseContext _context;
        protected readonly DbSet<TEntity> _entity;
        protected readonly IUserReolverRepository _currentUser;

        public CommonRepository(BlogEngineDatabaseContext context, DbSet<TEntity> entity, IUserReolverRepository currentUser)
        {
            _entity = entity;
            _context = context;
            _currentUser = currentUser;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await GetById(id);
            _entity.Remove(entity);

            return (await _context.SaveChangesAsync() > 0);
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var entity = await _entity.FindAsync(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public virtual DbSet<TEntity> GetEntity()
        {
            return _entity;
        }

        public virtual async Task<bool> InsertAsync(TEntity entity)
        {
            await UpdateCommonFields(entity, true);

            _entity.Add(entity);

            return (await _context.SaveChangesAsync() > 0);
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            await UpdateCommonFields(entity);

            if (Exists(entity))
            {
                _entity.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _entity.Update(entity);
            }

            return (await _context.SaveChangesAsync() > 0);
        }

        public bool Exists<T>(T entity) where T : class
        {
            return _context.Set<T>().Local.Any(e => e == entity);
            //context.ChangeTracker.Entries<YourEntity>().Any(e => e.Entity.Id == id);
        }

        private async Task UpdateCommonFields(TEntity entity, bool isCreated = false)
        {
            var currentUser = await _currentUser.GetCurrentUser();
            if (currentUser == null)
            {
                currentUser = new UserViewModel();
            }

            if (isCreated)
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedUserId = currentUser.Id;
                entity.CreatedDate = DateTime.Now;
            }

            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedUserId = currentUser.Id;
        }
    }
}
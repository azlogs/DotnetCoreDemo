using BlogEngine.DataModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BlogEngine.DataRepositories.Interfaces
{
    public interface ICommonRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(Guid id);

        DbSet<TEntity> GetEntity();

        Task<bool> InsertAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(Guid id);
    }
}
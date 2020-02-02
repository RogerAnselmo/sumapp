using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SumApp.API.Repositories.Context;

namespace SumApp.API.Repositories
{
    public class BaseRepository<TEntity> : IDisposable where TEntity : class
    {
        public SumAppContext Db;
        public DbSet<TEntity> DbSet;

        public BaseRepository(SumAppContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task<bool> SaveAsync(TEntity obj)
        {
            DbSet.Add(obj);
            return await SaveChangesAsync();
        }

        public virtual async Task<TEntity> Get(int id) => await DbSet.FindAsync(id);

        public virtual async Task<IQueryable<TEntity>> Get() => DbSet;

        public virtual async Task<bool> UpdateAsync(TEntity obj)
        {
            DbSet.Update(obj);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> RemoveAsync(int id)
        {
            DbSet.Remove(DbSet.Find(id));
            return await SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        private async Task<bool> SaveChangesAsync() =>
            (await Db.SaveChangesAsync()) > 0;
    }
}

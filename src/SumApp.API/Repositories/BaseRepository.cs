using System;
using System.Linq;
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

        public virtual bool Save(TEntity obj)
        {
            DbSet.Add(obj);
            return SaveChanges();
        }

        public virtual TEntity Get(int id) => DbSet.Find(id);

        public virtual IQueryable<TEntity> Get() => DbSet;

        public virtual bool Update(TEntity obj)
        {
            DbSet.Update(obj);
            return SaveChanges();
        }

        public virtual bool Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));
            return SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
        
        private bool SaveChanges() => Db.SaveChanges() > 0;
    }
}

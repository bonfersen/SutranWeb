using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using EntitiesLayer;

namespace DataModelLayer
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly SUTRANEntities dbContext;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(SUTRANEntities dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void InsertEntity(TEntity entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
        }

        public virtual void DeleteById(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            DeleteEntity(entityToDelete);
        }

        public virtual void DeleteEntity(TEntity entityToDelete)
        {
            if (dbContext.Entry<TEntity>(entityToDelete).State == System.Data.Entity.EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void UpdateEntity(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dbContext.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void Dispose() { return; }
    }
}

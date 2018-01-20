using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using EntitiesLayer;

namespace DataModelLayer
{
    public class GenericViewRepositoryImpl<TEntity> : IGenericViewRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly SUTRANEntities dbContext;
        internal DbSet<TEntity> dbSet;

        public GenericViewRepositoryImpl(SUTRANEntities dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public List<TEntity> GetViewSutranReportEvent(int startIndex, int count,
                                                Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return (count > 0)
                    ? orderBy(query).Skip(startIndex).Take(count).ToList() //Paging
                    : orderBy(query).ToList(); //No paging
            }
            else
            {
                return query.ToList();
            }

        }

        public void Dispose() { return; }
    }
}

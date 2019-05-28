using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using EntitiesLayer;

namespace DataModelLayer
{
    public class GenericEntityRepositoryImpl<TEntity> : IGenericEntityRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly SUTRANEntities dbContext;
        internal DbSet<TEntity> dbSet;

        public GenericEntityRepositoryImpl(SUTRANEntities dbContext)
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
                List<TEntity> list = orderBy(query).ToList();
                return list;
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

        public enum Order
        {
            Asc,
            Desc
        }

        public virtual List<TEntity> OrderByDynamic(
            string sortColumn,
            bool descending)
        {
            IQueryable<TEntity> query = dbSet;

            // Dynamically creates a call like this: query.OrderBy(p =&gt; p.SortColumn)
            var parameter = Expression.Parameter(typeof(TEntity), "p");

            string command = "OrderBy";

            if (descending)
            {
                command = "OrderByDescending";
            }
            
            Expression resultExpression = null;

            var property = typeof(TEntity).GetProperty(sortColumn);
            // this is the part p.SortColumn
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            // this is the part p =&gt; p.SortColumn
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            // finally, call the "OrderBy" / "OrderByDescending" method with the order by lamba expression
            resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { typeof(TEntity), property.PropertyType },
               query.Expression, Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<TEntity>(resultExpression).ToList();
        }

        public void Dispose() { return; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DataModelLayer
{
    public interface IGenericEntityRepository<TEntity> where TEntity : class
    {
        List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                string includeProperties = "");
        TEntity GetByID(object id);
        void InsertEntity(TEntity entity);
        void UpdateEntity(TEntity entityToUpdate);
        void DeleteById(object id);
        void DeleteEntity(TEntity entityToDelete);
        List<TEntity> OrderByDynamic(string sortColumn, bool descending);
    }
}

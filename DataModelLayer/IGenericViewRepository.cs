using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DataModelLayer
{
    public interface IGenericViewRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetViewSutranReportEvent(int startIndex, int count, Expression<Func<TEntity, bool>> filter = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        //List<TEntity> GetViewSutranReportEventByFilter(string name, int cityId, int startIndex, int count, string sorting);
    }
}

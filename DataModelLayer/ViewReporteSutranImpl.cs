using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelLayer;
using EntitiesLayer;

namespace DataModelLayer
{
    public class ViewReporteSutranImpl : IViewReporteSutranService
    {
        private GenericViewRepositoryImpl<viewReporteDynafleet> genericViewRepository = new GenericViewRepositoryImpl<viewReporteDynafleet>(new SUTRANEntities());

        public List<viewReporteDynafleet> GetViewSutranReportEvent(int startIndex, int count, System.Linq.Expressions.Expression<Func<EntitiesLayer.viewReporteDynafleet, bool>> filter = null, Func<IQueryable<EntitiesLayer.viewReporteDynafleet>, IOrderedQueryable<EntitiesLayer.viewReporteDynafleet>> orderBy = null)
        {
            return genericViewRepository.GetViewSutranReportEvent(startIndex, count, filter, orderBy);
        }
    }
}

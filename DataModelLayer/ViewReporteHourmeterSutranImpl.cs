using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelLayer;
using EntitiesLayer;

namespace DataModelLayer
{
    public class ViewReporteHourmeterSutranImpl : IViewReporteHourmeterSutranService
    {
        private GenericViewRepositoryImpl<viewReporteHorometro> genericViewRepository = new GenericViewRepositoryImpl<viewReporteHorometro>(new SUTRANEntities());
        
        public List<viewReporteHorometro> GetViewSutranReportEvent(int startIndex, int count, Boolean checkUltimoEvento, 
                                        System.Linq.Expressions.Expression<Func<viewReporteHorometro, bool>> filter = null, Func<IQueryable<viewReporteHorometro>, IOrderedQueryable<viewReporteHorometro>> orderBy = null)
        {
            return genericViewRepository.GetViewSutranReportEvent(startIndex, count, checkUltimoEvento, filter, orderBy);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelLayer;
using EntitiesLayer;
using DTOLayer;

namespace BusinessLayer
{
    public class ViewBusinessImpl : IViewBusiness
    {
        public object GetViewSutranReportEvent(int startIndex, int count, string sorting)
        {
            ViewReporteSutranImpl reporteEventoDAO = new ViewReporteSutranImpl();

            Func<IQueryable<viewReporteDynafleet>, IOrderedQueryable<viewReporteDynafleet>> expr = null;

                        
            // Preparar la lista paginada desde BD
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("USUARIO ASC"))
            {
                expr = reporteIQ => reporteIQ.OrderBy(reporte => reporte.Usuario);
            }
            else if (sorting.Equals("USUARIO DESC"))
            {
                expr = reporteIQ => reporteIQ.OrderByDescending(reporte => reporte.Usuario);
            }
            else
            {
                expr = reporteIQ => reporteIQ.OrderBy(reporte => reporte.VIN); // Default
            }
            List<viewReporteDynafleet> lstViewReporteDynafleet = reporteEventoDAO.GetViewSutranReportEvent(startIndex,count,orderBy: expr);

            // Obtener la cantidad de registros de la vista
            //List<viewReporteDynafleet> lstViewReporteDynafleetCount = reporteEventoDAO.GetViewSutranReportEvent(0, 0, filter: x => x.Nombre_Flota == "" , orderBy: reporteIQ => reporteIQ.OrderBy(reporte => reporte.VIN));
            List<viewReporteDynafleet> lstViewReporteDynafleetCount = reporteEventoDAO.GetViewSutranReportEvent(0, 0, orderBy: reporteIQ => reporteIQ.OrderBy(reporte => reporte.VIN));
            int reportCount = lstViewReporteDynafleetCount.Count;

            //Return result to jTable
            return new { Result = "OK", Records = lstViewReporteDynafleet, TotalRecordCount = reportCount };
        }
    }
}

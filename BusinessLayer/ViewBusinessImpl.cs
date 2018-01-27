using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataModelLayer;
using EntitiesLayer;
using DTOLayer;

namespace BusinessLayer
{
    public class ViewBusinessImpl : IViewBusiness
    {
        public List<viewReporteDynafleet> lstViewReporteDynafleetSinPaginar;

        public Dictionary<string, object> GetViewSutranReportEvent(string txtFechaEventoInicial, string txtFechaEventoFinal, string txtVin, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            ViewReporteSutranImpl reporteEventoDAO = new ViewReporteSutranImpl();
                        
            // Preparar la expresion para ordenar la data de acuerdo al campo solicitado
            Func<IQueryable<viewReporteDynafleet>, IOrderedQueryable<viewReporteDynafleet>> reporteOrderBy = null;

            if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("fechaRegistroGPS ASC"))
            {
                reporteOrderBy = reporteIQ => reporteIQ.OrderBy(reporte => reporte.usuario);
            }
            else if (jtSorting.Equals("fechaRegistroGPS DESC"))
            {
                reporteOrderBy = reporteIQ => reporteIQ.OrderByDescending(reporte => reporte.usuario);
            }
            else
            {
                // Default
                reporteOrderBy = reporteIQ => reporteIQ.OrderBy(reporte => reporte.vin); 
            }

            // Preparar la expresion para filtrar la data de acuerdo los filtros seleccionados
            Expression<Func<viewReporteDynafleet, bool>> reporteFilter = null;
                        
            if (!string.IsNullOrEmpty(txtVin))
                reporteFilter = reporteIQ => reporteIQ.vin == txtVin;
            if (!string.IsNullOrEmpty(txtFechaEventoInicial) && !string.IsNullOrEmpty(txtFechaEventoFinal))
            {
                txtFechaEventoInicial = txtFechaEventoInicial + ":00";
                DateTime dtFechaEventoInicial = Convert.ToDateTime(txtFechaEventoInicial);
                txtFechaEventoFinal = txtFechaEventoFinal + ":59";
                DateTime dtFechaEventoFinal = Convert.ToDateTime(txtFechaEventoFinal);
                if (string.IsNullOrEmpty(txtVin))
                    reporteFilter = reporteIQ => reporteIQ.fechaRegistroGPS >= dtFechaEventoInicial && reporteIQ.fechaRegistroGPS <= dtFechaEventoFinal;
                else
                    reporteFilter = reporteIQ => reporteIQ.fechaRegistroGPS >= dtFechaEventoInicial && reporteIQ.fechaRegistroGPS <= dtFechaEventoFinal && reporteIQ.vin == txtVin;
            }

            // Obtener la consulta paginada desde BD
            List<viewReporteDynafleet> lstViewReporteDynafleetPaginado = reporteEventoDAO.GetViewSutranReportEvent(jtStartIndex, jtPageSize, filter: reporteFilter, orderBy: reporteOrderBy);

            // Obtener la cantidad de registros de la consulta sin paginacion
            lstViewReporteDynafleetSinPaginar = reporteEventoDAO.GetViewSutranReportEvent(0, 0, filter: reporteFilter, orderBy: reporteOrderBy);
            int reportCount = lstViewReporteDynafleetSinPaginar.Count;

            //Return result to jTable
            Dictionary<string, object> reporteDictionary = new Dictionary<string, object>();
            reporteDictionary.Add("Result" , "OK");
            reporteDictionary.Add("Records", lstViewReporteDynafleetPaginado);
            reporteDictionary.Add("TotalRecordCount" , reportCount);
            reporteDictionary.Add("RecordsSinPaginar", lstViewReporteDynafleetSinPaginar);

            return reporteDictionary;
        }
    }
}

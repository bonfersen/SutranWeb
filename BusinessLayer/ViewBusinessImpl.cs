using System;
using System.Globalization;
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
                reporteOrderBy = reporteIQ => reporteIQ.OrderBy(reporte => reporte.fechaRegistroGPS);
            }
            else if (jtSorting.Equals("fechaRegistroGPS DESC"))
            {
                reporteOrderBy = reporteIQ => reporteIQ.OrderByDescending(reporte => reporte.fechaRegistroGPS);
            }
            else
            {
                // Default
                reporteOrderBy = reporteIQ => reporteIQ.OrderBy(reporte => reporte.fechaRegistroGPS); 
            }

            // Preparar la expresion para filtrar la data de acuerdo los filtros seleccionados
            Expression<Func<viewReporteDynafleet, bool>> reporteFilter = null;
                        
            if (!string.IsNullOrEmpty(txtVin))
                reporteFilter = reporteIQ => reporteIQ.vin == txtVin;
            if (!string.IsNullOrEmpty(txtFechaEventoInicial) && !string.IsNullOrEmpty(txtFechaEventoFinal))
            {
                IFormatProvider culture = new CultureInfo("en-US", true); // por defecto
                txtFechaEventoInicial = txtFechaEventoInicial + ":00";                
                DateTime dtFechaEventoInicial = DateTime.ParseExact(txtFechaEventoInicial, "dd/MM/yyyy HH:mm:ss", culture);
                txtFechaEventoFinal = txtFechaEventoFinal + ":59";
                DateTime dtFechaEventoFinal = DateTime.ParseExact(txtFechaEventoFinal, "dd/MM/yyyy HH:mm:ss", culture);
                if (string.IsNullOrEmpty(txtVin))
                    reporteFilter = reporteIQ => reporteIQ.fechaRegistroGPS >= dtFechaEventoInicial && reporteIQ.fechaRegistroGPS <= dtFechaEventoFinal;
                else
                    reporteFilter = reporteIQ => reporteIQ.fechaRegistroGPS >= dtFechaEventoInicial && reporteIQ.fechaRegistroGPS <= dtFechaEventoFinal && reporteIQ.vin == txtVin;
            }

            // Obtener la consulta paginada y ordenada desde BD
            List<viewReporteDynafleet> lstViewReporteDynafleetPaginado = reporteEventoDAO.GetViewSutranReportEvent(jtStartIndex, jtPageSize, filter: reporteFilter, orderBy: reporteOrderBy);

            // Obtener la cantidad de registros de la consulta sin paginacion
            lstViewReporteDynafleetSinPaginar = reporteEventoDAO.GetViewSutranReportEvent(0, 0, filter: reporteFilter);
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

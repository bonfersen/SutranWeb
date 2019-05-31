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
        public Dictionary<string, object> GetViewSutranReportEvent(string txtFechaEventoInicial, string txtFechaEventoFinal, string txtVin, string txtNombreFlota,
                                    Boolean checkUltimoEvento, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            ViewReporteSutranImpl reporteEventoDAO = new ViewReporteSutranImpl();
            Boolean existeFechas = false;
            DateTime dtFechaEventoInicial = new DateTime();
            DateTime dtFechaEventoFinal = new DateTime();

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
                        
            if (!string.IsNullOrEmpty(txtFechaEventoInicial) && !string.IsNullOrEmpty(txtFechaEventoFinal))
            {
                IFormatProvider culture = new CultureInfo("en-US", true); // por defecto
                txtFechaEventoInicial = txtFechaEventoInicial + ":00";                
                dtFechaEventoInicial = DateTime.ParseExact(txtFechaEventoInicial, "dd/MM/yyyy HH:mm:ss", culture);
                txtFechaEventoFinal = txtFechaEventoFinal + ":59";
                dtFechaEventoFinal = DateTime.ParseExact(txtFechaEventoFinal, "dd/MM/yyyy HH:mm:ss", culture);
                existeFechas = true;               
            }

            if (existeFechas)
                if (!string.IsNullOrEmpty(txtNombreFlota) && !string.IsNullOrEmpty(txtVin))
                    reporteFilter = reporteIQ => reporteIQ.fechaRegistroGPS >= dtFechaEventoInicial && reporteIQ.fechaRegistroGPS <= dtFechaEventoFinal
                        && reporteIQ.nombreFlota == txtNombreFlota && reporteIQ.vin == txtVin;
                else if (!string.IsNullOrEmpty(txtNombreFlota))
                    reporteFilter = reporteIQ => reporteIQ.fechaRegistroGPS >= dtFechaEventoInicial && reporteIQ.fechaRegistroGPS <= dtFechaEventoFinal
                        && reporteIQ.nombreFlota == txtNombreFlota;
                else if (!string.IsNullOrEmpty(txtVin))
                    reporteFilter = reporteIQ => reporteIQ.fechaRegistroGPS >= dtFechaEventoInicial && reporteIQ.fechaRegistroGPS <= dtFechaEventoFinal
                        && reporteIQ.vin == txtVin;
                else
                    reporteFilter = reporteIQ => reporteIQ.fechaRegistroGPS >= dtFechaEventoInicial && reporteIQ.fechaRegistroGPS <= dtFechaEventoFinal;
            else
                reporteFilter = reporteIQ => (string.IsNullOrEmpty(txtNombreFlota) || reporteIQ.nombreFlota == txtNombreFlota) &&
                             (string.IsNullOrEmpty(txtVin) || reporteIQ.vin == txtVin);

            // Obtener la consulta paginada y ordenada desde BD
            List<viewReporteDynafleet> lstViewReporteDynafleetPaginado = reporteEventoDAO.GetViewSutranReportEvent(jtStartIndex, jtPageSize,
                                                        checkUltimoEvento, filter: reporteFilter, orderBy: reporteOrderBy);

            // Obtener la cantidad de registros de la consulta sin paginacion
            List<viewReporteDynafleet> lstViewReporteDynafleetSinPaginar = reporteEventoDAO.GetViewSutranReportEvent(0, 0, checkUltimoEvento, filter: reporteFilter);
            int reportCount = lstViewReporteDynafleetSinPaginar.Count;

            //Return result to jTable
            Dictionary<string, object> reporteDictionary = new Dictionary<string, object>();
            reporteDictionary.Add("Result" , "OK");
            reporteDictionary.Add("Records", lstViewReporteDynafleetPaginado);
            reporteDictionary.Add("TotalRecordCount" , reportCount);
            reporteDictionary.Add("RecordsSinPaginar", lstViewReporteDynafleetSinPaginar);

            return reporteDictionary;
        }

        public Dictionary<string, object> GetViewSutranReportHourmeter(string txtFechaInicio, string txtFechaFin, string txtVin, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            ViewReporteHourmeterSutranImpl reporteHourmeterDAO = new ViewReporteHourmeterSutranImpl();

            // Preparar la expresion para ordenar la data de acuerdo al campo solicitado
            Func<IQueryable<viewReporteHorometro>, IOrderedQueryable<viewReporteHorometro>> reporteOrderBy = null;

            if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("fechaInicio ASC"))
            {
                reporteOrderBy = reporteIQ => reporteIQ.OrderBy(reporte => reporte.fechaInicio);
            }
            else if (jtSorting.Equals("fechaInicio DESC"))
            {
                reporteOrderBy = reporteIQ => reporteIQ.OrderByDescending(reporte => reporte.fechaInicio);
            }
            else
            {
                // Default
                reporteOrderBy = reporteIQ => reporteIQ.OrderBy(reporte => reporte.fechaInicio);
            }

            // Preparar la expresion para filtrar la data de acuerdo los filtros seleccionados
            Expression<Func<viewReporteHorometro, bool>> reporteFilter = null;

            if (!string.IsNullOrEmpty(txtVin))
                reporteFilter = reporteIQ => reporteIQ.vin == txtVin;
            if (!string.IsNullOrEmpty(txtFechaInicio) && !string.IsNullOrEmpty(txtFechaFin))
            {
                IFormatProvider culture = new CultureInfo("en-US", true); // por defecto
                txtFechaInicio = txtFechaInicio + ":00";
                DateTime dtFechaInicio = DateTime.ParseExact(txtFechaInicio, "dd/MM/yyyy HH:mm:ss", culture);
                txtFechaFin = txtFechaFin + ":59";
                DateTime dtFechaFin = DateTime.ParseExact(txtFechaFin, "dd/MM/yyyy HH:mm:ss", culture);
                if (string.IsNullOrEmpty(txtVin))
                    reporteFilter = reporteIQ => reporteIQ.fechaInicio >= dtFechaInicio && reporteIQ.fechaFin <= dtFechaFin;
                else
                    reporteFilter = reporteIQ => reporteIQ.fechaInicio >= dtFechaInicio && reporteIQ.fechaFin <= dtFechaFin && reporteIQ.vin == txtVin;
            }

            // Obtener la consulta paginada y ordenada desde BD
            List<viewReporteHorometro> lstViewReporteDynafleetPaginado = reporteHourmeterDAO.GetViewSutranReportEvent(jtStartIndex, jtPageSize, false, filter: reporteFilter, orderBy: reporteOrderBy);

            // Obtener la cantidad de registros de la consulta sin paginacion
            List<viewReporteHorometro> lstViewReporteDynafleetSinPaginar = reporteHourmeterDAO.GetViewSutranReportEvent(0, 0, false, filter: reporteFilter);
            int reportCount = lstViewReporteDynafleetSinPaginar.Count;

            //Return result to jTable
            Dictionary<string, object> reporteDictionary = new Dictionary<string, object>();
            reporteDictionary.Add("Result", "OK");
            reporteDictionary.Add("Records", lstViewReporteDynafleetPaginado);
            reporteDictionary.Add("TotalRecordCount", reportCount);
            reporteDictionary.Add("RecordsSinPaginar", lstViewReporteDynafleetSinPaginar);

            return reporteDictionary;
        }
    }
}

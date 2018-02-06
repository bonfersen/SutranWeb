using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using SutranWeb;
using EntitiesLayer;
using SutranWeb.Classes;

namespace SutranWeb
{
    public partial class FrmReporteEvento : System.Web.UI.Page
    {
        static ViewBusinessImpl viewBusiness;

        protected void Page_Load(object sender, EventArgs e)
        {
            viewBusiness = (ViewBusinessImpl)UnityLoad<IViewBusiness>.getUnityContainer();
            if (!IsPostBack)
            {               
            }
        }

        [WebMethod(EnableSession = true)]
        public static object ReporteEventoByFilter(string txtFechaEventoInicial, string txtFechaEventoFinal, string txtVin,
                                                        int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrEmpty(txtFechaEventoInicial) && !string.IsNullOrEmpty(txtFechaEventoFinal))
                    throw new Exception("Se debe ingresar la fecha de inicio");
                if (!string.IsNullOrEmpty(txtFechaEventoInicial) && string.IsNullOrEmpty(txtFechaEventoFinal))
                    throw new Exception("Se debe ingresar la fecha final");
                if (string.IsNullOrEmpty(txtFechaEventoInicial) && string.IsNullOrEmpty(txtFechaEventoFinal) && 
                    string.IsNullOrEmpty(txtVin))
                    throw new Exception("Se debe ingresar un criterio de busqueda");
                
                // Consulta de vista reportes
                Dictionary<string, object> reporteDictionary = viewBusiness.GetViewSutranReportEvent(txtFechaEventoInicial, 
                                                                txtFechaEventoFinal, txtVin, jtStartIndex, jtPageSize, jtSorting);
                System.Web.HttpContext.Current.Session["listaReporteEventos"] = reporteDictionary["RecordsSinPaginar"];

                // Respuesta al control JTable
                return new { Result = reporteDictionary["Result"], Records = reporteDictionary["Records"],
                             TotalRecordCount = reporteDictionary["TotalRecordCount"] };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }

        }

        protected void hdntextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {   
                exportarExcel();                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
        }

        /// <summary>
        /// El metodo exportarExcel contiene la implementacion de generacion de un Excel desde un Response.
        /// </summary>
        protected void exportarExcel()
        {
            try
            {
                List<viewReporteDynafleet> listaReporteEventos = System.Web.HttpContext.Current.Session["listaReporteEventos"] as List<viewReporteDynafleet>;
                int t = listaReporteEventos.Count;
                String reportName = SutranWebConstants.EXCEL_REPORTE_EVENTO;
                ExportToExcel.CreateExcelDocument(listaReporteEventos, reportName, Response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
            finally
            {
                //System.Web.HttpContext.Current.Session["listaReporteEventos"] = null;
            }
        }
    }
}
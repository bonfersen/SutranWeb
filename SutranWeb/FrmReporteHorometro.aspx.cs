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
    public partial class FrmReporteHorometro : System.Web.UI.Page
    {
        static ViewBusinessImpl viewBusiness;

        protected void Page_Load(object sender, EventArgs e)
        {
            viewBusiness = (ViewBusinessImpl)UnityLoad<IViewBusiness>.getUnityContainer();
        }

        [WebMethod(EnableSession = true)]
        public static object ReporteHorometroByFilter(string txtFechaInicio, string txtFechaFin, string txtVin,
                                                        int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrEmpty(txtFechaInicio) && !string.IsNullOrEmpty(txtFechaFin))
                    throw new Exception("Se debe ingresar la fecha de inicio");
                if (!string.IsNullOrEmpty(txtFechaInicio) && string.IsNullOrEmpty(txtFechaFin))
                    throw new Exception("Se debe ingresar la fecha final");
                if (string.IsNullOrEmpty(txtFechaInicio) && string.IsNullOrEmpty(txtFechaFin) &&
                    string.IsNullOrEmpty(txtVin))
                    throw new Exception("Se debe ingresar un criterio de busqueda");

                // Consulta de vista reportes
                Dictionary<string, object> reporteDictionary = viewBusiness.GetViewSutranReportHourmeter(txtFechaInicio,
                                                                txtFechaFin, txtVin, jtStartIndex, jtPageSize, jtSorting);
                System.Web.HttpContext.Current.Session["listaReporteHorometro"] = reporteDictionary["RecordsSinPaginar"];

                // Respuesta al control JTable
                return new
                {
                    Result = reporteDictionary["Result"],
                    Records = reporteDictionary["Records"],
                    TotalRecordCount = reporteDictionary["TotalRecordCount"]
                };
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
                List<viewReporteHorometro> listaReporteHorometro = System.Web.HttpContext.Current.Session["listaReporteHorometro"] as List<viewReporteHorometro>;
                int t = listaReporteHorometro.Count;
                String reportName = SutranWebConstants.EXCEL_REPORTE_HOROMETRO;
                ExportToExcel.CreateExcelDocument(listaReporteHorometro, reportName, Response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
            finally
            {
                //System.Web.HttpContext.Current.Session["listaReporteHorometro"] = null;
            }
        }
    }
}
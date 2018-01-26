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
    public partial class FrmReporteFlota : System.Web.UI.Page
    {
        static ViewBusinessImpl viewBusiness;

        protected void Page_Load(object sender, EventArgs e)
        {
            viewBusiness = (ViewBusinessImpl)UnityLoad<IViewBusiness>.getUnityContainer();
            if (!IsPostBack)
            {
                //String volvi = null;
            }
        }

        [WebMethod(EnableSession = true)]
        public static object ReporteEventoByFilter(string txtFechaEventoInicial, string txtFechaEventoFinal, string txtVin,
                                                        int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                Dictionary<string, object> reporteDictionary = viewBusiness.GetViewSutranReportEvent(txtFechaEventoInicial, 
                                                                txtFechaEventoFinal, txtVin, jtStartIndex, jtPageSize, jtSorting);
                System.Web.HttpContext.Current.Session["listaReporteEventos"] = reporteDictionary["RecordsSinPaginar"];

                return new { Result = reporteDictionary["Result"], Records = reporteDictionary["Records"],
                             TotalRecordCount = reporteDictionary["TotalRecordCount"] };
            }
            catch (Exception ee)
            {
                return new { Result = "ERROR", Message = ee.Message };
            }

        }

        protected void hdntextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {   
                List<viewReporteDynafleet> listaReporteEventos = System.Web.HttpContext.Current.Session["listaReporteEventos"] as List<viewReporteDynafleet>;
                int t =listaReporteEventos.Count;
                String reportName = SutranWebConstants.EXCEL_REPORTE_EVENTOS;

                ExportToExcel.CreateExcelDocument(listaReporteEventos, reportName, Response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
        }

        
    }
}
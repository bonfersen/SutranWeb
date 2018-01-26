using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

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
                return viewBusiness.GetViewSutranReportEvent(txtFechaEventoInicial, txtFechaEventoFinal, txtVin, jtStartIndex, jtPageSize, jtSorting);
            }
            catch (Exception ee)
            {
                return new { Result = "ERROR", Message = ee.Message };
            }

        }
    }
}
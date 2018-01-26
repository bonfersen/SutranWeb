using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SutranWeb;
using SutranWeb.Classes;
using BusinessLayer;
using DTOLayer;

namespace SutranWeb
{
    public partial class FrmAgregarFlota : System.Web.UI.Page
    {
        FlotaBusinessImpl flotaBusiness;

        protected void Page_Load(object sender, EventArgs e)
        {
            flotaBusiness = (FlotaBusinessImpl)UnityLoad<IFlotaBusiness>.getUnityContainer();
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            FlotaDTO flotaDTO = new FlotaDTO();
            flotaDTO.nombreFlota = txtCliente.Text;
            flotaDTO.usuario = txtUsuario.Text;
            flotaDTO.password = txtPassword.Text;
            flotaDTO.activo = (chkActivo.Checked) ? "1" : "0";
            flotaBusiness.saveFlotas(flotaDTO);
            Response.Redirect("FrmMantenimientoFlota.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmMantenimientoFlota.aspx");
        }

    }
}
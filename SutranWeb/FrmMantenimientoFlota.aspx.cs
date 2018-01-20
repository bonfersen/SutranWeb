using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DTOLayer;

namespace SutranWeb
{

    public partial class FrmMantenimientoFlota : System.Web.UI.Page
    {
        FlotaBusinessImpl flotaBusiness;

        protected void Page_Load(object sender, EventArgs e)
        {
            flotaBusiness = (FlotaBusinessImpl)UnityLoad<IFlotaBusiness>.getUnityContainer();
            //LLenar Datos
            getFlotas();
        }

        protected void gridFlotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FlotaDTO flotaDTO = (FlotaDTO)e.Row.DataItem;
                if (flotaDTO.activo == "1")
                {
                    e.Row.Cells[4].Text = "Si";
                }
                else
                {
                    e.Row.Cells[4].Text = "No";
                }
            }
        }

        protected void gridFlotas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridFlotas.PageIndex = e.NewPageIndex;
            //LLenar Datos
            getFlotas();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmAgregarFlota.aspx");
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                GridViewRow gridViewRow = (GridViewRow)lnk.NamingContainer;
                //DataKeys debe estar enlazado con un campo en la grilla del ASP
                String idFlota = gridFlotas.DataKeys[gridViewRow.RowIndex].Value.ToString();

                //Se obtiene la flota a actualizar
                FlotaDTO flotaDTO = flotaBusiness.GetByID(Int32.Parse(idFlota));

                txtIdFlota.Text = (flotaDTO.idFlota).ToString();
                txtCliente.Text = flotaDTO.nombreFlota;
                txtUsuario.Text = flotaDTO.usuario;
                txtPassword.Text = flotaDTO.password;
                chkActivo.Checked = (flotaDTO.activo == "1") ? true : false;
                mpeEdit.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
            finally
            {

            }
        }

        private void getFlotas()
        {
            gridFlotas.DataSource = flotaBusiness.getFlotasDTO();
            gridFlotas.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                FlotaDTO flotaDTO = new FlotaDTO();
                flotaDTO.idFlota = Int32.Parse(txtIdFlota.Text);
                flotaDTO.nombreFlota = txtCliente.Text;
                flotaDTO.usuario = txtUsuario.Text;
                flotaDTO.password = txtPassword.Text;
                flotaDTO.activo = (chkActivo.Checked) ? "1" : "0";
                flotaBusiness.UpdateEntity(flotaDTO);
                mpeEdit.Hide();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
            finally
            {
                getFlotas();
            }
        }

        protected void btnCancelUpdate_Click(object sender, EventArgs e)
        {
           // clearData(panelEdit);
        }

        void clearData(Panel p)
        {
            foreach (dynamic control in p.Controls)
            {

                if (control is TextBox)
                    control.Text = String.Empty;
                if (control is CheckBox)
                    control.Checked = false;
            }
            mpeEdit.Hide();
        }

    }
}
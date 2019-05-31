using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using SutranWeb;
using SutranWeb.Classes;
using DTOLayer;

namespace SutranWeb
{

    public partial class FrmMantenimientoFlota : System.Web.UI.Page
    {
        /*
         * Variables globales envian el resultado al cliente y luego se reinician
         * a menos que sean estaticos
         * */
        private FlotaBusinessImpl flotaBusiness;

        /*
         * Ante cualquier evento siempre se ejecuta este metodo
         * */
        protected void Page_Load(object sender, EventArgs e)
        {
            flotaBusiness = (FlotaBusinessImpl)UnityLoad<IFlotaBusiness>.getUnityContainer();
            // Valida que se ejecute la primera vez que se ingresa a la pagina
            if (!IsPostBack)
            {
                //LLenar Datos
                SortColumn = "nombreFlota";
                SortDirection = "ASC";
                getFlotas();
            }
        }

        protected void gridFlotas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FlotaDTO flotaDTO = (FlotaDTO)e.Row.DataItem;
                if (flotaDTO.activo == "1")
                    e.Row.Cells[5].Text = "Si";
                else
                    e.Row.Cells[5].Text = "No";
                if (flotaDTO.tipoFlota != null)
                    if (flotaDTO.tipoFlota == "1")
                       e.Row.Cells[2].Text = "Buses";
                    else
                        e.Row.Cells[2].Text = "Camiones";
            }
        }

        protected void gridFlotas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridFlotas.PageIndex = e.NewPageIndex;
            //LLenar Datos inmediatamente despues del evento
            getFlotas();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            mpeAdd.Show();
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

                txtEditIdFlota.Text = (flotaDTO.idFlota).ToString();
                txtEditCliente.Text = flotaDTO.nombreFlota;
                txtEditUsuario.Text = flotaDTO.usuario;
                txtEditPassword.Text = flotaDTO.password;
                ddlEditTipoFlota.SelectedIndex = (flotaDTO.tipoFlota == "0") ? 1 : 2;
                chkEditActivo.Checked = (flotaDTO.activo == "1") ? true : false;
                mpeEdit.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
            finally
            {
                //sin esto no pinta tipo flotas lo mismo en el agregar y cancelar de ambos metodos
                //getFlotas(); 
            }
        }

        private void getFlotas()
        {
            gridFlotas.DataSource = flotaBusiness.getFlotasDTO(SortColumn, SortDirection);
            gridFlotas.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                FlotaDTO flotaDTO = new FlotaDTO();
                flotaDTO.idFlota = Int32.Parse(txtEditIdFlota.Text);
                flotaDTO.nombreFlota = txtEditCliente.Text;
                flotaDTO.usuario = txtEditUsuario.Text;
                flotaDTO.password = txtEditPassword.Text;
                flotaDTO.tipoFlota = ddlEditTipoFlota.SelectedValue;
                flotaDTO.activo = (chkEditActivo.Checked) ? "1" : "0";
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FlotaDTO flotaDTO = new FlotaDTO();
                flotaDTO.nombreFlota = txtSaveCliente.Text;
                flotaDTO.usuario = txtSaveUsuario.Text;
                flotaDTO.password = txtSavePassword.Text;
                flotaDTO.tipoFlota = ddlSaveTipoFlota.SelectedValue;
                flotaDTO.activo = (chkSaveActivo.Checked) ? "1" : "0";
                flotaBusiness.saveFlotas(flotaDTO);
                Response.Redirect("FrmMantenimientoFlota.aspx");
                mpeAdd.Hide();
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

        protected void btnCancelSave_Click(object sender, EventArgs e)
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

        protected void gridFlotas_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortColumn = e.SortExpression;
            // Si viene ASC del ViewState se cambia a DESC y viceversa
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";

            getFlotas();
        }

        /*
         * Se obtiene el nombre de la columna ordenada desde el ViewState (objeto que viaja al cliente por cada Request)
         **/
        public string SortColumn
        {
            get { return Convert.ToString(ViewState["SortColumn"]); }
            set { ViewState["SortColumn"] = value; }
        }

        /*
         * Se obtiene el orden de la columna desde el ViewState
         **/
        public string SortDirection
        {
            get { return Convert.ToString(ViewState["SortDirection"]); }
            set { ViewState["SortDirection"] = value; }
        }
    }
}
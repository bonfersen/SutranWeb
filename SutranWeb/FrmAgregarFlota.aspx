<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmAgregarFlota.aspx.cs"
    Inherits="SutranWeb.FrmAgregarFlota" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<style>
    h3
    {
        text-align: center;
        background-color: #5D7B9D;
        color: white;
    }
    div.ex1 
    {
        background-color: #FFFDF8;
        width:500px;
        margin: auto;
        border: 2px solid #5D7B9D;        
    }
    th
    {
        text-align:left;
    }
</style>
<body>
    <form id="form1" runat="server">
    <div class="ex1">
        <h3>
            Nuevo Cliente</h3>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
        <table>
            <tr>
                <th>
                    <asp:Label ID="lblCliente" runat="server" Text="Cliente: "></asp:Label>
                </th>
                <td>
                    <asp:TextBox ID="txtCliente" runat="server" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCliente"
                        ErrorMessage="Debe ingresar un cliente" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario: "></asp:Label>
                </th>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsuario"
                        ErrorMessage="Debe ingresar el usuario" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
                </th>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="Debe ingresar un password" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblActivo" runat="server" Text="Activo: "></asp:Label>
                </th>
                <td>
                    <asp:CheckBox ID="chkActivo" runat="server" Checked="true" AutoPostBack="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnGrabar" runat="server" Text="Grabar" Width="150" OnClick="btnGrabar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="150" OnClick="btnCancelar_Click"
                        CausesValidation="false" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

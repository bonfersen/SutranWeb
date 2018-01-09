<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmMantenimientoFlota.aspx.cs"
    Inherits="SutranWeb.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<link href="Sutran.css" rel="stylesheet" type="text/css" />
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .modalBackground
        {
            background-color: silver;
            opacity: 0.7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div align="center">
        <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar Flota" OnClick="btnAgregar_Click" />
                <br />
                <br />
                <asp:GridView ID="gridFlotas" runat="server" AutoGenerateColumns="False" Width="1024px"
                    DataKeyNames="idFlota" CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-CssClass="alinear"
                    OnPageIndexChanging="gridFlotas_PageIndexChanging" OnRowDataBound="gridFlotas_RowDataBound"
                    AllowPaging="True" PageSize="30">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="idFlota" HeaderText="IdFlota" />
                        <asp:BoundField DataField="nombreFlota" HeaderText="Nombre Flota" />
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="password" HeaderText="Password" />
                        <asp:BoundField DataField="activo" HeaderText="Activo" />
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" Text="Editar" OnClick="lnkEdit_Click" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                
                <!--Panel to Edit Cliente Flota-->
                <asp:Button ID="btnDummy1" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="mpeEdit" runat="server" TargetControlID="btnDummy1"
                    PopupControlID="panelEdit" RepositionMode="RepositionOnWindowResizeAndScroll"
                    DropShadow="true" PopupDragHandleControlID="panelEditTitle" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="panelEdit" runat="server" Style="display: none; background-color: #FFFDF8;"
                    ForeColor="#F7F6F3" Width="500" Height="210">
                    <asp:Panel ID="panelEditTitle" runat="server" Style="cursor: move; font-family: Tahoma;
                        padding: 2px;" HorizontalAlign="Center" BackColor="#5D7B9D" ForeColor="White"
                        Height="25">
                        Editar</asp:Panel>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                    <table>
                        <td>
                            <asp:TextBox ID="txtIdFlota" runat="server" Style="display: none" />
                            <tr>
                                <td>
                                    <asp:Label ID="lblCliente" runat="server" Text="Cliente: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCliente" runat="server" Width="400px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCliente"
                                        ErrorMessage="Debe ingresar un cliente" ForeColor="Red" ValidationGroup="edit">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUsuario" runat="server" Width="400px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsuario"
                                        ErrorMessage="Debe ingresar el usuario" ForeColor="Red" ValidationGroup="edit">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" Width="400px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword"
                                        ErrorMessage="Debe ingresar un password" ForeColor="Red" ValidationGroup="edit">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblActivo" runat="server" Text="Activo: "></asp:Label>
                                </td>
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
                                    <asp:Button ID="btnUpdate" runat="server" Text="Grabar" Width="150" OnClick="btnUpdate_Click"
                                        ValidationGroup="edit" />
                                    <asp:Button ID="btnCancelUpdate" runat="server" Text="Cancelar" Width="150" OnClick="btnCancelUpdate_Click"
                                        CausesValidation="false" />
                                </td>
                            </tr>
                    </table>
                </asp:Panel>
                <!--End Panel  Cliente Flota-->

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

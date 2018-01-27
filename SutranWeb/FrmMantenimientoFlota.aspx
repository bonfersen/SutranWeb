<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmMantenimientoFlota.aspx.cs"
    Inherits="SutranWeb.FrmMantenimientoFlota" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="Sutran.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>

    <div class="filtering">
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Flota" OnClick="btnAgregar_Click" />
    </div>

    <div class="div.jtable-main-container">
        <asp:UpdatePanel runat="server" ID="up" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gridFlotas" runat="server" AutoGenerateColumns="False" Width="100%" 
                    DataKeyNames="idFlota" AllowPaging="True" PageSize="30"
                    OnPageIndexChanging="gridFlotas_PageIndexChanging" OnRowDataBound="gridFlotas_RowDataBound"
                    CssClass="mydatagrid" HeaderStyle-CssClass="header" 
                    AlternatingRowStyle-CssClass="altrows" RowStyle-CssClass="rows"
                    PagerStyle-CssClass="pager">                    
                    <Columns>
                        <asp:BoundField DataField="idFlota" HeaderText="IdFlota" />
                        <asp:BoundField DataField="nombreFlota" HeaderText="Nombre Flota" />
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="password" HeaderText="Password" />
                        <asp:BoundField DataField="activo" HeaderText="Activo" />
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" runat="server"><img src="/images/edit.png" alt="Editar" /></asp:LinkButton>
                            </ItemTemplate>
                             
                        </asp:TemplateField>
                    </Columns>
                    
                </asp:GridView>
                
                <!--Panel Edit Cliente Flota-->
                <asp:Button ID="btnEditFlota" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="mpeEdit" runat="server" TargetControlID="btnEditFlota"
                    PopupControlID="panelEdit" RepositionMode="RepositionOnWindowResizeAndScroll"
                    DropShadow="true" PopupDragHandleControlID="panelEditTitle" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="panelEdit" runat="server" Style="display: inline-block; background-color: #f9f9f9;"
                    ForeColor="#000" Width="500" Height="210">
                    <asp:Panel ID="panelEditTitle" runat="server" Style="cursor: move; font-family: Tahoma;
                        padding: 2px;" HorizontalAlign="Center" CssClass="header"
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
                <!--End Panel Edit Cliente Flota-->


                <!--Panel Add Cliente Flota-->
                <asp:Button ID="btnAddFlota" runat="server" Style="display: none" />
                <ajaxToolkit:ModalPopupExtender ID="mpeAdd" runat="server" TargetControlID="btnAddFlota"
                    PopupControlID="panelAdd" RepositionMode="RepositionOnWindowResizeAndScroll"
                    DropShadow="true" PopupDragHandleControlID="panelAddTitle" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="panelAdd" runat="server" Style="display: inline-block; background-color: #f9f9f9;"
                    ForeColor="#000" Width="500" Height="210">
                    <asp:Panel ID="panelAddTitle" runat="server" Style="cursor: move; font-family: Tahoma;
                        padding: 2px;" HorizontalAlign="Center" CssClass="header"
                        Height="25">
                        Agregar</asp:Panel>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" />
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Cliente: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSaveCliente" runat="server" Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSaveCliente"
                                    ErrorMessage="Debe ingresar un cliente" ForeColor="Red" ValidationGroup="add">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Usuario: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSaveUsuario" runat="server" Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSaveUsuario"
                                    ErrorMessage="Debe ingresar el usuario" ForeColor="Red" ValidationGroup="add">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Password: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSavePassword" runat="server" Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSavePassword"
                                    ErrorMessage="Debe ingresar un password" ForeColor="Red" ValidationGroup="add">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Activo: "></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSaveActivo" runat="server" Checked="true" AutoPostBack="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Grabar" Width="150" OnClick="btnSave_Click" 
                                    ValidationGroup="add" />
                                <asp:Button ID="btnCancelSave" runat="server" Text="Cancelar" Width="150" OnClick="btnCancelSave_Click"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <!--End Panel Add Cliente Flota-->

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
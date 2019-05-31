<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmReporteEvento.aspx.cs"
    Inherits="SutranWeb.FrmReporteEvento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reporte de Eventos</title>
    <link href="/Content/Site.css" rel="stylesheet" type="text/css" />
    <!-- JQuery UI -->
    <link href="/Content/themes/metroblue/jquery-ui.css" rel="stylesheet" type="text/css" />
    <!-- jTable style file -->
    <link href="/Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/modernizr-2.6.2.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.9.2.min.js" type="text/javascript"></script>
    <script src="/Scripts/jtablesite.js" type="text/javascript"></script>
    <!-- A helper library for JSON serialization -->
    <script type="text/javascript" src="/Scripts/jtable/external/json2.js"></script>
    <!-- Core jTable script file -->
    <script type="text/javascript" src="/Scripts/jtable/jquery.jtable.js"></script>
    <!-- ASP.NET Web Forms extension for jTable -->
    <script type="text/javascript" src="/Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
    <!-- Jquery TimePicker -->
    <link rel="stylesheet" media="all" type="text/css" href="/Content/jquery-ui-timepicker-addon.css" />
    <script src="/Scripts/jquery-ui-timepicker-addon.min.js" type="text/javascript"></script>
    <!-- Sutran -->
    <link href="Sutran.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <script type="text/javascript">

        $(document).ready(function () {

            //Prepare jtable plugin
            $('#StudentTableContainer').jtable({
                title: 'Reporte de Eventos Sutran',
                paging: true,
                pageSize: 30,
                sorting: true,
                defaultSorting: 'fechaRegistroGPS DESC',
                actions: {
                    listAction: '/FrmReporteEvento.aspx/ReporteEventoByFilter'
                },
                toolbar: {
                    hoverAnimation: true,
                    hoverAnimationDuration: 60,
                    hoverAnimationEasing: undefined,
                    items: [{
                        tooltip: 'XLS',
                        icon: '/images/excel.png',
                        text: 'Exportar a Excel',
                        click: function () {
                            $('input[id$=hdntextbox]').val(Math.random()).change();                            
                        }
                    }]
                },
                fields: {
                    usuario: {
                        title: 'Usuario',
                        sorting: false,
                        width: '15%'
                    },
                    nombreFlota: {
                        title: 'Nombre Flota',
                        sorting: false,
                        width: '10%'
                    },
                    vin: {
                        title: 'VIN',
                        sorting: false,
                        width: '8%'
                    },
                    evento: {
                        title: 'Evento',
                        width: '8%',
                        sorting: false,
                        options: { 'ER': 'ER', 'EX': 'EX', 'PA': 'PA' }
                    },
                    fechaRegistroGPS: {
                        title: 'fechaRegistroGPS',
                        list: false
                    },
                    fechaRegistroGPSFormato: {
                        title: 'Fecha Evento',
                        width: '10%'

                    },
                    velocidad: {
                        title: 'Velocidad',
                        sorting: false,
                        width: '10%'

                    },
                    latitud: {
                        title: 'Latitud',
                        sorting: false,
                        width: '10%'
                    },
                    longitud: {
                        title: 'Longitud',
                        sorting: false,
                        width: '10%'
                    },
                    porcentajeCombustible: {
                        title: '% Combus.',
                        sorting: false,
                        width: '8%'
                    },
                    combustibleAcumuladoFormat: {
                        title: 'Combus. Acum.',
                        sorting: false,
                        width: '10%'
                    },
                    odometro: {
                        title: 'Odometro',
                        sorting: false,
                        width: '10%'
                    }
                }
            });

            // Add datepickers to filter input
            $('#txtFechaEventoInicial').datetimepicker({
                dateFormat: 'dd/mm/yy',
                timeFormat: 'HH:mm'
            });
            $('#txtFechaEventoFinal').datetimepicker({
                dateFormat: 'dd/mm/yy',
                timeFormat: 'HH:mm'
            });

            // Re-load records when user click 'load records' button.
            $('#LoadRecordsButton').click(function (e) {
                e.preventDefault();
                $('#StudentTableContainer').jtable('load', {
                    txtFechaEventoInicial: $('#txtFechaEventoInicial').val(),
                    txtFechaEventoFinal: $('#txtFechaEventoFinal').val(),
                    txtVin: $('#txtVin').val(),
                    txtNombreFlota: $('#txtNombreFlota').val(),
                    checkUltimoEvento: $('#checkUltimoEvento').prop("checked")
                });
            });
        });

    </script>
    <div class="filtering">
        <form id="FormReporte" runat="server">
        <asp:TextBox runat="server" ID="hdntextbox" Value="" Style="display:none;" AutoPostBack="true" OnTextChanged="hdntextbox_TextChanged"></asp:TextBox>

        <table>
            <tr>
                <th>
                    Fecha Evento Inicial:
                </th>
                <td>
                    <input type="text" name="txtFechaEventoInicial" id="txtFechaEventoInicial" />
                </td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <th>
                    Fecha Evento Final:
                </th>
                <td>
                    <input type="text" name="txtFechaEventoFinal" id="txtFechaEventoFinal" />
                </td>
            </tr>
            <tr>
                <th align="left">
                    VIN:
                </th>                
                <td>
                    <input type="text" name="txtVin" id="txtVin" />
                </td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <th align="left">
                    Nombre Flota:
                </th>                
                <td>
                    <input type="text" name="txtNombreFlota" id="txtNombreFlota" />
                </td>
            </tr>
            <tr>
                <th align="left">
                    Mostrar &uacute;ltimo Evento:
                </th>                
                <td>
                    <input type="checkbox" checked="checked" name="checkUltimoEvento" id="checkUltimoEvento" value="1"/>
                </td>
            </tr>
        </table>
        <br/>
        <button type="submit" id="LoadRecordsButton">
            Filtrar Eventos</button>
        </form>
    </div>
    <div id="StudentTableContainer">
    </div>
</body>
</html>

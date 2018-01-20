<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmReporteFlota.aspx.cs"
    Inherits="SutranWeb.FrmReporteFlota" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Filtering With jTable and ASP.NET Web Forms</title>
    <link href="/Content/Site.css" rel="stylesheet" type="text/css" />
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
    <style>
        div.filtering
        {
            border: 1px solid #999;
            margin-bottom: 5px;
            padding: 10px;
            background-color: #EEE;
        }
    </style>
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
                defaultSorting: 'Usuario ASC',
                actions: {
                    listAction: '/FrmReporteFlota.aspx/ReporteEventoByFilter'

                },
                fields: {
                    Usuario: {
                        title: 'Usuario',
                        width: '23%'
                    },
                    Nombre_Flota: {
                        title: 'Nombre Flota',                        
                    },
                    VIN: {
                        title: 'VIN',                        
                    },
                    Evento: {
                        title: 'Evento',
                        width: '13%',
                        options: { 'ER': 'ER-', 'EX': 'EX-', 'PA': 'PA-' },                        
                    },
                    Fecha_Hora_evento: {
                        title: 'Fecha_Hora_evento',
                        width: '15%',
                        type: 'datetime',
                        displayFormat: 'yy-mm-dd hh:mm:ss',
                        
                    },
                    Velocidad: {
                        title: 'Velocidad',
                        width: '15%',
                        
                    },
                    Latitud: {
                        title: 'Latitud',
                        
                    },
                    Longitud: {
                        title: 'Longitud',
                        
                    },
                    Porc__combustible: {
                        title: '% Combustible',
                        
                    },
                    Combustible_acumulado: {
                        title: 'Combustible Acumulado',
                        
                    },
                    Odometro: {
                        title: 'Odometro',
                        
                    }
                }
            });

            //Load all records when page is first shown
            $('#StudentTableContainer').jtable('load');
        });

    </script>

    <div id="StudentTableContainer">
    </div>
</body>
</html>

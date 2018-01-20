using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOLayer
{
    public class ViewReporteEventoDTO
    {
        public string Usuario { get; set; }
        public string Nombre_Flota { get; set; }
        public string VIN { get; set; }
        public string Evento { get; set; }
        public string Fecha_Hora_evento { get; set; }
        public int Velocidad { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public decimal Porc__combustible { get; set; }
        public long Combustible_acumulado { get; set; }
        public long Odometro { get; set; }
    }
}

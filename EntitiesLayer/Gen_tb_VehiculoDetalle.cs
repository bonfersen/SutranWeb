//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace EntitiesLayer
{
    public partial class Gen_tb_VehiculoDetalle
    {
        public int idVehiDetalle { get; set; }
        public Nullable<decimal> latitud { get; set; }
        public Nullable<decimal> longitud { get; set; }
        public Nullable<int> rumbo { get; set; }
        public Nullable<int> velocidad { get; set; }
        public string evento { get; set; }
        public Nullable<System.DateTime> fechaRegistroGPS { get; set; }
        public int idVehiculo { get; set; }
        public string estaTransmitido { get; set; }
        public Nullable<long> combustibleAcumulado { get; set; }
        public Nullable<long> odometro { get; set; }
        public Nullable<decimal> porcentajeCombustible { get; set; }
    
        public virtual Gen_tb_Vehiculo Gen_tb_Vehiculo { get; set; }
    }
    
}
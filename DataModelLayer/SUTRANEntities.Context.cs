﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EntitiesLayer
{
    public partial class SUTRANEntities : DbContext
    {
        public SUTRANEntities()
            : base("name=SUTRANEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Gen_tb_Flota> Gen_tb_Flota { get; set; }
        public DbSet<Gen_tb_Vehiculo> Gen_tb_Vehiculo { get; set; }
        public DbSet<Gen_tb_VehiculoDetalle> Gen_tb_VehiculoDetalle { get; set; }
    }
}
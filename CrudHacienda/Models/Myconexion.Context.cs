﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrudHacienda.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyonexionEntities : DbContext
    {
        public MyonexionEntities()
            : base("name=MyonexionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DetalleProduccion> DetalleProduccion { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<EstadoEmpleado> EstadoEmpleado { get; set; }
        public virtual DbSet<MisProductos> MisProductos { get; set; }
        public virtual DbSet<Produccion> Produccion { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<Puestos> Puestos { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}

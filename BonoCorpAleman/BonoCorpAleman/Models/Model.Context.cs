﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BonoCorpAleman.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BonoCorpAlemanEntities1 : DbContext
    {
        public BonoCorpAlemanEntities1()
            : base("name=BonoCorpAlemanEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bono> Bono { get; set; }
        public virtual DbSet<Bono_Tasa> Bono_Tasa { get; set; }
        public virtual DbSet<Capitalizacion> Capitalizacion { get; set; }
        public virtual DbSet<Costes_Gastos> Costes_Gastos { get; set; }
        public virtual DbSet<Entidad> Entidad { get; set; }
        public virtual DbSet<Inflacion> Inflacion { get; set; }
        public virtual DbSet<PlazoBono> PlazoBono { get; set; }
        public virtual DbSet<PlazoGracia> PlazoGracia { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TipoTasa> TipoTasa { get; set; }
    }
}
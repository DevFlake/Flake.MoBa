﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Flake.MoBa.Db.Dal
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MoBaDbEntities : DbContext
    {
        public MoBaDbEntities()
            : base("name=MoBaDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Artikel> Artikel { get; set; }
        public virtual DbSet<Dokumente> Dokumente { get; set; }
        public virtual DbSet<DokumenteZuArtikeln> DokumenteZuArtikeln { get; set; }
        public virtual DbSet<DokumenteZuHerstellern> DokumenteZuHerstellern { get; set; }
        public virtual DbSet<Dokumenttypen> Dokumenttypen { get; set; }
        public virtual DbSet<Hersteller> Hersteller { get; set; }
        public virtual DbSet<Links> Links { get; set; }
        public virtual DbSet<LinksZuArtikeln> LinksZuArtikeln { get; set; }
        public virtual DbSet<LinksZuHerstellern> LinksZuHerstellern { get; set; }
        public virtual DbSet<Schlagworte> Schlagworte { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<LocomotiveDataSheets> LocomotiveDataSheets { get; set; }
        public virtual DbSet<LocomotiveFunctions> LocomotiveFunctions { get; set; }
        public virtual DbSet<Locomotives> Locomotives { get; set; }
    
        public virtual int InsertLinkZuArtikel(Nullable<int> artikelNid, string link, string bezeichnung)
        {
            var artikelNidParameter = artikelNid.HasValue ?
                new ObjectParameter("ArtikelNid", artikelNid) :
                new ObjectParameter("ArtikelNid", typeof(int));
    
            var linkParameter = link != null ?
                new ObjectParameter("Link", link) :
                new ObjectParameter("Link", typeof(string));
    
            var bezeichnungParameter = bezeichnung != null ?
                new ObjectParameter("Bezeichnung", bezeichnung) :
                new ObjectParameter("Bezeichnung", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertLinkZuArtikel", artikelNidParameter, linkParameter, bezeichnungParameter);
        }
    
        public virtual int InsertLinkZuHersteller(Nullable<int> herstellerNid, string link, string bezeichnung)
        {
            var herstellerNidParameter = herstellerNid.HasValue ?
                new ObjectParameter("HerstellerNid", herstellerNid) :
                new ObjectParameter("HerstellerNid", typeof(int));
    
            var linkParameter = link != null ?
                new ObjectParameter("Link", link) :
                new ObjectParameter("Link", typeof(string));
    
            var bezeichnungParameter = bezeichnung != null ?
                new ObjectParameter("Bezeichnung", bezeichnung) :
                new ObjectParameter("Bezeichnung", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertLinkZuHersteller", herstellerNidParameter, linkParameter, bezeichnungParameter);
        }
    
        public virtual int InsertSchlagwortZuArtikel(Nullable<int> artikelNid, string schlagwort)
        {
            var artikelNidParameter = artikelNid.HasValue ?
                new ObjectParameter("ArtikelNid", artikelNid) :
                new ObjectParameter("ArtikelNid", typeof(int));
    
            var schlagwortParameter = schlagwort != null ?
                new ObjectParameter("Schlagwort", schlagwort) :
                new ObjectParameter("Schlagwort", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertSchlagwortZuArtikel", artikelNidParameter, schlagwortParameter);
        }
    
        public virtual int InsertSchlagwortZuHersteller(Nullable<int> herstellerNid, string schlagwort)
        {
            var herstellerNidParameter = herstellerNid.HasValue ?
                new ObjectParameter("HerstellerNid", herstellerNid) :
                new ObjectParameter("HerstellerNid", typeof(int));
    
            var schlagwortParameter = schlagwort != null ?
                new ObjectParameter("Schlagwort", schlagwort) :
                new ObjectParameter("Schlagwort", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertSchlagwortZuHersteller", herstellerNidParameter, schlagwortParameter);
        }
    }
}

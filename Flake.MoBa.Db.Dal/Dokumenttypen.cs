//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Dokumenttypen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dokumenttypen()
        {
            this.DokumenteZuArtikeln = new HashSet<DokumenteZuArtikeln>();
            this.DokumenteZuHerstellern = new HashSet<DokumenteZuHerstellern>();
        }
    
        public int DokumenttypNid { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DokumenteZuArtikeln> DokumenteZuArtikeln { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DokumenteZuHerstellern> DokumenteZuHerstellern { get; set; }
    }
}
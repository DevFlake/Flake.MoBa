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
    
    public partial class Locomotives
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Locomotives()
        {
            this.LocomotiveFunctions = new HashSet<LocomotiveFunctions>();
        }
    
        public int LocomotiveNid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DigitalAddress { get; set; }
        public Nullable<int> LocomotiveDataSheetNid { get; set; }
    
        public virtual LocomotiveDataSheets LocomotiveDataSheets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocomotiveFunctions> LocomotiveFunctions { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace emlakkkk.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class counties
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public counties()
        {
            this.products = new HashSet<products>();
        }
    
        public int countyId { get; set; }
        public string countyName { get; set; }
        public Nullable<int> prvCode { get; set; }
    
        public virtual provinces provinces { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<products> products { get; set; }
    }
}

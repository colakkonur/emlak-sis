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
    
    public partial class users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public users()
        {
            this.favorites = new HashSet<favorites>();
            this.messages = new HashSet<messages>();
            this.messages1 = new HashSet<messages>();
            this.posts = new HashSet<posts>();
            this.products = new HashSet<products>();
        }
    
        public int userId { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
        public string adress { get; set; }
        public string type { get; set; }
        public Nullable<int> advertisability { get; set; }
        public string avatarSource { get; set; }
        public string about { get; set; }
        public string socialFacebook { get; set; }
        public string socialInstagram { get; set; }
        public string socialPinterest { get; set; }
        public string socialTwitter { get; set; }
        public string socialLinkedin { get; set; }
        public string socialYoutube { get; set; }
        public string socialSkype { get; set; }
        public string socialWebsite { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<favorites> favorites { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<messages> messages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<messages> messages1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<posts> posts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<products> products { get; set; }
    }
}

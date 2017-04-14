//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MobileNewsModel.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public service()
        {
            this.category_service = new HashSet<category_service>();
            this.service_icon = new HashSet<service_icon>();
        }
    
        public int service_id { get; set; }
        public Nullable<int> agency_id { get; set; }
        public int language_id { get; set; }
        public string title { get; set; }
        public string service_content { get; set; }
        public System.DateTime created_date { get; set; }
        public System.DateTime modified_date { get; set; }
        public Nullable<System.DateTime> logical_delete_date { get; set; }
    
        public virtual agency agency { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<category_service> category_service { get; set; }
        public virtual language language { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<service_icon> service_icon { get; set; }
    }
}

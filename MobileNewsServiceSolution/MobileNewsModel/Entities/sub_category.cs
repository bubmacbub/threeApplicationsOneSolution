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
    
    public partial class sub_category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sub_category()
        {
            this.category_location = new HashSet<category_location>();
            this.category_news = new HashSet<category_news>();
            this.category_service = new HashSet<category_service>();
        }
    
        public int sub_category_id { get; set; }
        public int category_id { get; set; }
        public string sub_category_name { get; set; }
        public System.DateTime created_date { get; set; }
        public System.DateTime modified_date { get; set; }
        public Nullable<System.DateTime> logical_delete_date { get; set; }
    
        public virtual category category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<category_location> category_location { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<category_news> category_news { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<category_service> category_service { get; set; }
    }
}

using System;

namespace MobileNewsServices.Models
{
    public class agencyViewModel
    {
        public int agency_id { get; set; }
        public string agency_name { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public Nullable<DateTime> logical_delete_date { get; set; }

    }
}

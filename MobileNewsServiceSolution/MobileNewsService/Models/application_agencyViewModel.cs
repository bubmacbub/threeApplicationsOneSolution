using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNewsServices.Models
{
    public class application_agencyViewModel
    {
        public int application_agency_id { get; set; }
        public int application_id { get; set; }
        public int agency_id { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public Nullable<DateTime> logical_delete_date { get; set; }
    }
}

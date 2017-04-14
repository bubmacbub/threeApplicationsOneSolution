using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNewsServices.Models
{
    public class category_serviceViewModel
    {
        public int category_service_id { get; set; }
        public int service_id { get; set; }
        public int category_id { get; set; }
        //public Nullable<int> sub_category_id { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public Nullable<DateTime> logical_delete_date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNewsService.Models
{
    //[Table("service")]
    public partial class serviceViewModel
    {

        public int service_id { get; set; }

        //public int category_id { get; set; }

        //public int? sub_category_id { get; set; }

        public int? agency_id { get; set; }

        public int language_id { get; set; }

        public string title { get; set; }

        public string service_content { get; set; }

        public DateTime created_date { get; set; }

        public DateTime? modified_date { get; set; }

        public DateTime? logical_delete_date { get; set; }

        //public string agency_name { get; set; }

        //public string category_name { get; set; }

        //public string sub_category_name { get; set; }
    }
}

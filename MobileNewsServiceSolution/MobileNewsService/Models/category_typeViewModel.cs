using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNewsServices.Models
{
    public class category_typeViewModel
    {
        public int category_type_id { get; set; }
        public string category_type_name { get; set; }
        public DateTime created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public DateTime? logical_delete_date { get; set; }
    }
}

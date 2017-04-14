using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNewsServices.Models
{
   
    public class category_newsViewModel
    {
        public int category_news_id { get; set; }
        public int news_id { get; set; }
        public int category_id { get; set; }
        //public Nullable<int> sub_category_id { get; set; }
        public System.DateTime created_date { get; set; }
        public System.DateTime modified_date { get; set; }
        public Nullable<System.DateTime> logical_delete_date { get; set; }

    }
}

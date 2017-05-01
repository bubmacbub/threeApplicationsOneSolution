using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNewsService.Models
{
    public class locationViewModel
    {
        public int location_id { get; set; }

        public int? agency_id { get; set; }

        public int language_id { get; set; }

        public string location_name { get; set; }

        public string address { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zip { get; set; }

        public double? latitude { get; set; }

        public double? longitude { get; set; }

        public string website { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public string comment { get; set; }

        //public string language { get; set; }
    }
}

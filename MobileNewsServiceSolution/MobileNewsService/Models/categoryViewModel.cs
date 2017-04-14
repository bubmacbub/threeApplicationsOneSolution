using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYSOFA_api.Models
{
    public class categoryViewModel
    {
        [Key]
        public int category_id { get; set; }

        public int agency_id { get; set; }

        [Required]
        [StringLength(120)]
        public string category_name { get; set; }

        public DateTime created_date { get; set; }

        public DateTime? modified_date { get; set; }

        public DateTime? logical_delete_date { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MobileNewsService.Models
{

    public partial class newsViewModel
    {
        public int news_id { get; set; }

        public int agency_id { get; set; }

        public int language_id { get; set; }

        public string news_title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string news_content { get; set; }

        public byte[] news_image { get; set; }

        public DateTime? publish_date { get; set; }

        public DateTime created_date { get; set; }

        public DateTime? modified_date { get; set; }

        public DateTime? logical_delete_date { get; set; }

        public string archive_flag { get; set; }
    }
}

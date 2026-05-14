using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Domain.Models
{
    public class Publisher
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public bool enabled { get; set; }
        public DateTime? dateLastUpdated { get; set; }
    }
}

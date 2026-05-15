using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Domain.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool Enabled { get; set; }
        public DateTime? DateLastUpdated { get; set; }
    }
}

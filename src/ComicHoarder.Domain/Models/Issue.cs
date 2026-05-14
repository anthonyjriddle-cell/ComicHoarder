using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Domain.Models
{
    public class Issue
    {
        public int id { get; set; }
        public int volumeId { get; set; }
        public string? name { get; set; }
        public float issueNumber { get; set; }
        public int publishMonth { get; set; }
        public int publishYear { get; set; }
        public bool collected { get; set; }
        public bool enabled { get; set; }
        public string? issueNumberSuffix { get; set; }
        public int formatId { get; set; }
        public bool reprint { get; set; }
        public DateTime? dateAdded { get; set; }
        public string? summary { get; set; }
        public DateTime? dateLastUpdated { get; set; }
        public DateTime? coverDate { get; set; }
    }
}

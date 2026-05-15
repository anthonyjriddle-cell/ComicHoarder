using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Domain.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public int VolumeId { get; set; }
        public string? Name { get; set; }
        public float IssueNumber { get; set; }
        public int PublishMonth { get; set; }
        public int PublishYear { get; set; }
        public bool Collected { get; set; }
        public bool Enabled { get; set; }
        public string? IssueNumberSuffix { get; set; }
        public int FormatId { get; set; }
        public bool Reprint { get; set; }
        public DateTime? DateAdded { get; set; }
        public string? Summary { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public DateTime? CoverDate { get; set; }
    }
}

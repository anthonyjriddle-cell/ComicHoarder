using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComicHoarder.Domain;

namespace ComicHoarder.Domain.Models
{
    public class Volume
    {
        public int Id { get; set; }
        public int PublisherId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public bool Collectable { get; set; }
        public int CountOfIssues { get; set; }
        public int StartYear { get; set; }
        public bool Enabled { get; set; }
        public bool Complete { get; set; }
        public string? ImageLink { get; set; }

        public string DescriptionNoHtml
        {
            get { return Regex.Replace(Description ?? "", "<.*?>", string.Empty); }
        }
    }
}

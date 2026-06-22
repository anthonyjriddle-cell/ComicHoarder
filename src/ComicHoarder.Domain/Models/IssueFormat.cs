using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Domain.Models
{
    public class IssueFormat
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Enabled { get; set; }
    }
}

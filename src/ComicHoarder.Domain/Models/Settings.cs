using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Domain.Models
{
    internal class Settings
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? value { get; set; }
    }
}

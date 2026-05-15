using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Domain.Models
{
    internal class Settings
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}

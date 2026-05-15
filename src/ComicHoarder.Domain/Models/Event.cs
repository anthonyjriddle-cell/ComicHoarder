using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Domain.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Type { get; set; }
        public int Order { get; set; }
        public int PublisherId { get; set; }
    }
}

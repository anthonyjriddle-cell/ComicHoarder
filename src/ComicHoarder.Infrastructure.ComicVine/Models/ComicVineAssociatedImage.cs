using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Infrastructure.ComicVine.Models
{
    public class ComicVineAssociatedImage
    {
        public string? original_url { get; set; }
        public int id { get; set; }
        public object? caption { get; set; }
        public string? image_tags { get; set; }
    }
}

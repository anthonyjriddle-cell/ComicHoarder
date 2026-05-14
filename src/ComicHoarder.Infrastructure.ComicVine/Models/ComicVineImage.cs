using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Infrastructure.ComicVine.Models
{
    public class ComicVineImage
    {
        public string? icon_url { get; set; }
        public string? medium_url { get; set; }
        public string? screen_url { get; set; }
        public string? screen_large_url { get; set; }
        public string? small_url { get; set; }
        public string? super_url { get; set; }
        public string? thumb_url { get; set; }
        public string? tiny_url { get; set; }
        public string? original_url { get; set; }
        public string? image_tags { get; set; }
    }
}

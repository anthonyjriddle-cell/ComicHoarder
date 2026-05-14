using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComicHoarder.Infrastructure.ComicVine.Models
{
    public class ComicVinePublisher
    {
        public string? error { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int number_of_page_results { get; set; }
        public int number_of_total_results { get; set; }
        public int status_code { get; set; }
        public ComicVinePublisherResults? results { get; set; }
        public string? version { get; set; }
    }
}

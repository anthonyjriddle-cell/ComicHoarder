namespace ComicHoarder.Infrastructure.ComicVine.Models
{
    public class ComicVineVolumesResult
    {
        public object? aliases { get; set; }
        public string? api_detail_url { get; set; }
        public int count_of_issues { get; set; }
        public string? date_added { get; set; }
        public string? date_last_updated { get; set; }
        public string? deck { get; set; }
        public string? description { get; set; }
        public ComicVineFirstIssue? first_issue { get; set; }
        public int id { get; set; }
        public ComicVineImage? image { get; set; }
        public ComicVineLastIssue? last_issue { get; set; }
        public string? name { get; set; }
        public ComicVineAbbreviatedPublisher? publisher { get; set; }
        public string? site_detail_url { get; set; }
        public string? start_year { get; set; }
    }

}

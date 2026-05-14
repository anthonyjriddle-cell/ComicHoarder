namespace ComicHoarder.Infrastructure.ComicVine.Models
{
    public class ComicVineIssueResults
    {
        public object? aliases { get; set; }
        public string? api_detail_url { get; set; }
        public List<object>? associated_images { get; set; }
        public List<ComicVineCharacterCredit>? character_credits { get; set; }
        public List<object>? character_died_in { get; set; }
        public List<ComicVineConceptCredit>? concept_credits { get; set; }
        public string? cover_date { get; set; }
        public string? date_added { get; set; }
        public string? date_last_updated { get; set; }
        public object? deck { get; set; }
        public string? description { get; set; }
        public object? first_appearance_characters { get; set; }
        public object? first_appearance_concepts { get; set; }
        public object? first_appearance_locations { get; set; }
        public object? first_appearance_objects { get; set; }
        public object? first_appearance_storyarcs { get; set; }
        public object? first_appearance_teams { get; set; }
        public object? has_staff_review { get; set; }
        public int id { get; set; }
        public ComicVineImage? image { get; set; }
        public string? issue_number { get; set; }
        public List<ComicVineLocationCredit>? location_credits { get; set; }
        public string? name { get; set; }
        public List<ComicVineObjectCredit>? object_credits { get; set; }
        public List<ComicVinePersonCredit>? person_credits { get; set; }
        public string? site_detail_url { get; set; }
        public string? store_date { get; set; }
        public List<object>? story_arc_credits { get; set; }
        public List<ComicVineTeamCredit>? team_credits { get; set; }
        public List<object>? team_disbanded_in { get; set; }
        public ComicVineAbbreviatedVolume? volume { get; set; }
    }
}


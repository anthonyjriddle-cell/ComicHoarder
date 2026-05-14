namespace ComicHoarder.Infrastructure.ComicVine.Models
{
    public static class Enums
    {
        public enum Resource
        {
            issue,
            issues,
            publisher,
            publishers,
            volume,
            volumes
        }

        public enum Format
        {
            xml,
            json
        }

        public enum SortDirection
        {
            asc,
            desc
        }

        public enum IssueFields
        {
            aliases,
            api_detail_url,
            character_credits,
            characters_died_in,
            concept_credits,
            cover_date,
            date_added,
            date_last_updated,
            deck,
            description,
            disbanded_teams,
            first_appearance_characters,
            first_appearance_concepts,
            first_appearance_locations,
            first_appearance_objects,
            first_appearance_storyarcs,
            first_appearance_teams,
            has_staff_review,
            id,
            image,
            issue_number,
            location_credits,
            name,
            object_credits,
            person_credits,
            site_detail_url,
            store_date,
            story_arc_credits,
            team_credits,
            teams_disbanded_in,
            volume
        };

        public enum IssuesFields
        {
            aliases,
            api_detail_url,
            cover_date,
            date_added,
            date_last_updated,
            deck,
            description,
            has_staff_review,
            id,
            image,
            issue_number,
            name,
            site_detail_url,
            store_date,
            volume
        };

        public enum PublisherFields
        {
            aliases,
            api_detail_url,
            characters,
            date_added,
            date_last_updated,
            deck,
            description,
            id,
            image,
            location_address,
            location_city,
            location_state,
            name,
            site_detail_url,
            story_arcs,
            teams,
            volumes
        };

        public enum PublishersFields
        {
            aliases,
            api_detail_url,
            date_added,
            date_last_updated,
            deck,
            description,
            id,
            image,
            location_address,
            location_city,
            location_state,
            name,
            site_detail_url
        };

        public enum VolumeFields
        {
            aliases,
            api_detail_url,
            character_credits,
            concept_credits,
            count_of_issues,
            date_added,
            date_last_updated,
            deck,
            description,
            first_issue,
            id,
            image,
            last_issue,
            location_credits,
            name,
            object_credits,
            person_credits,
            publisher,
            site_detail_url,
            start_year,
            team_credits,
            issues //issues is not in the ComicVine documentation, but it does work
        };

        public enum VolumesFields
        {
            aliases,
            api_detail_url,
            count_of_issues,
            date_added,
            date_last_updated,
            deck,
            description,
            first_issue,
            id,
            image,
            last_issue,
            name,
            publisher,
            site_detail_url,
            start_year
        };
    }
}
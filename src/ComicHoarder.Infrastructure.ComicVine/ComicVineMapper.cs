using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Infrastructure.ComicVine.Models;
using ComicHoarder.Domain.Models;
using ComicHoarder.Application;

namespace ComicHoarder.Infrastructure.ComicVine
{
    public static class ComicVineMapper
    {
        #region Issues
        public static Issue ToIssue(this ComicVineIssueResults cvIssue)
        {
            float issueNumber = 0.0f;
            string? issueSuffix = null;
            if (cvIssue.issue_number is not null)
            {
                if (cvIssue.issue_number.Contains("au"))
                {
                    float n = 0;
                    string p = "";
                    float.TryParse(new string(cvIssue.issue_number.Where(a => (Char.IsDigit(a) || a == '.')).ToArray()), out n);
                    issueNumber = ParseHelper.ParseFloat(n.ToString());
                    //issueNumber = issueNumber + .1f;
                    p = new string(cvIssue.issue_number.Where(a => !(Char.IsDigit(a) || a == '.')).ToArray());
                    issueSuffix = p;
                }
                else
                {
                    issueNumber = ParseHelper.ParseFloat(cvIssue.issue_number);
                }
                if (float.IsPositiveInfinity(issueNumber))
                {
                    issueNumber = 0;
                    issueSuffix = "Infinity";
                }
            }
            DateTime publishDate = new DateTime();
            DateTime.TryParse(cvIssue.cover_date, out publishDate);

            var issue = new Issue()
            {
                Id = cvIssue.id,
                Name = cvIssue.name,
                VolumeId = cvIssue.volume is not null ? cvIssue.volume.id : 0,
                IssueNumber = issueNumber,
                IssueNumberSuffix = issueSuffix,
                PublishMonth = publishDate.Month,
                PublishYear = publishDate.Year,
                Collected = false,
                Enabled = true,
                Summary = cvIssue.deck is not null ? cvIssue.ToString() : "",
                CoverDate = ParseHelper.ParseNullableDateTime(cvIssue.cover_date),
                DateAdded = ParseHelper.ParseNullableDateTime(cvIssue.date_added),
                DateLastUpdated = ParseHelper.ParseNullableDateTime(cvIssue.date_last_updated)
            };
            return issue;
        }

        public static List<Issue>? ToIssues(this List<ComicVineIssuesResult> cvIssues)
        {
            var issues = new List<Issue>();
            foreach(var cvIssue in cvIssues)
            {
                float issueNumber = 0.0f;
                string? issueSuffix = null;
                if (cvIssue.issue_number is not null)
                {
                    if (cvIssue.issue_number.Contains("au"))
                    {
                        float n = 0;
                        string p = "";
                        float.TryParse(new string(cvIssue.issue_number.Where(a => (Char.IsDigit(a) || a == '.')).ToArray()), out n);
                        issueNumber = ParseHelper.ParseFloat(n.ToString());
                        p = new string(cvIssue.issue_number.Where(a => !(Char.IsDigit(a) || a == '.')).ToArray());
                        issueSuffix = p;
                    }
                    else
                    {
                        issueNumber = ParseHelper.ParseFloat(cvIssue.issue_number);
                    }
                    if (float.IsPositiveInfinity(issueNumber))
                    {
                        issueNumber = 0;
                        issueSuffix = "Infinity";
                    }
                }
                DateTime publishDate = new DateTime();
                DateTime.TryParse(cvIssue.cover_date, out publishDate);

                var issue = new Issue()
                {
                    Id = cvIssue.id,
                    Name = cvIssue.name,
                    VolumeId = cvIssue.volume is not null ? cvIssue.volume.id : 0,
                    IssueNumber = issueNumber,
                    IssueNumberSuffix = issueSuffix,
                    PublishMonth = publishDate.Month,
                    PublishYear = publishDate.Year,
                    Collected = false,
                    Enabled = true,
                    Summary = cvIssue.deck is not null ? cvIssue.ToString() : "",
                    CoverDate = ParseHelper.ParseNullableDateTime(cvIssue.cover_date),
                    DateAdded = ParseHelper.ParseNullableDateTime(cvIssue.date_added),
                    DateLastUpdated = ParseHelper.ParseNullableDateTime(cvIssue.date_last_updated)
                };
                issues.Add(issue);
            }
            return issues;
        }
        public static List<Issue>? ToLiteIssues(this ComicVineVolumeResults cvVolume)
        {
            List<Issue> issues = new List<Issue>();
            if (cvVolume.issues != null)
            {
                foreach (var cvIssue in cvVolume.issues)
                {
                    Issue issue = new Issue();
                    issue.Id = cvIssue.id;
                    issue.Name = cvIssue.name;
                    if (cvIssue.issue_number is not null)
                    {
                        if (cvIssue.issue_number.Contains("au"))
                        {
                            float n = 0;
                            string p = "";
                            float.TryParse(new string(cvIssue.issue_number.Where(a => (Char.IsDigit(a) || a == '.')).ToArray()), out n);
                            issue.IssueNumber = ParseHelper.ParseFloat(n.ToString());
                            //issue.issueNumber = issue.issueNumber + .1f;
                            p = new string(cvIssue.issue_number.Where(a => !(Char.IsDigit(a) || a == '.')).ToArray());
                            issue.IssueNumberSuffix = p;
                        }
                        else
                        {
                            issue.IssueNumber = ParseHelper.ParseFloat(cvIssue.issue_number);
                        }
                    }
                    issues.Add(issue);
                }
            }
            return issues;
        }
        #endregion

        #region Volumes
        public static Volume ToVolume(this ComicVineVolumeResults cvVolume)
        {
            Volume volume = new Volume();
            volume.Id = cvVolume.id;
            volume.PublisherId = cvVolume.publisher is not null ? cvVolume.publisher.id : 0;
            volume.Name = cvVolume.name;
            volume.Description = cvVolume.description;
            volume.DateAdded = ParseHelper.ParseNullableDateTime(cvVolume.date_added);
            volume.DateLastUpdated = ParseHelper.ParseNullableDateTime(cvVolume.date_last_updated);
            volume.Collectable = true;
            volume.CountOfIssues = cvVolume.count_of_issues;
            volume.StartYear = ParseHelper.ParseInt(cvVolume.start_year);
            volume.Enabled = true;
            if (volume.DateLastUpdated > DateTime.Now.AddMonths(-13))
            {
                volume.Complete = false;
            }
            else
            {
                volume.Complete = true;
            }
            volume.Collectable = DetectReprint(volume);
            return volume;
        }

        public static List<Volume> ToVolumes(this ComicVinePublisherResults cvPublisher)
        {
            List<Volume> volumes = new List<Volume>();
            if (cvPublisher.volumes is not null)
            {
                foreach (var cvVolume in cvPublisher.volumes)
                {
                    Volume volume = new Volume();
                    volume.Id = cvVolume.id;
                    volume.Name = cvVolume.name;
                    volumes.Add(volume);
                }
            }
            return volumes;
        }
        #endregion

        #region Publishers
        public static Publisher ToPublisher(this ComicVinePublisherResults cvPublisher)
        {
            Publisher publisher = new Publisher();
            publisher.Id = cvPublisher.id;
            publisher.Name = cvPublisher.name;
            publisher.Description = cvPublisher.deck;
            publisher.Enabled = true;
            publisher.DateLastUpdated = ParseHelper.ParseNullableDateTime(cvPublisher.date_last_updated);
            return publisher;
        }

        public static List<Publisher>? ToPublishers(this List<ComicVinePublishersResult> cvPublishers)
        {
            var publishers = new List<Publisher>();
            foreach (var cvPublisher in cvPublishers)
            {
                var publisher = new Publisher()
                {
                    Id = cvPublisher.id,
                    Name = cvPublisher.name,
                    Description = cvPublisher.deck,
                    Enabled = true,
                    DateLastUpdated = ParseHelper.ParseNullableDateTime(cvPublisher.date_last_updated)
                };
                publishers.Add(publisher);
            }
            return publishers;
        }


        public static bool DetectReprint(Volume volume)
        {
            if (volume.Description is not null)
            {
                if (volume.Description.Contains("trade paperback") || volume.Description.Contains("tradepaperback") || volume.Description.Contains("tpb") || volume.Description.Contains("a hardcover book which reprints") || volume.Description.Contains("reprinting") || volume.Description.Contains("reprints") || volume.Description.Contains("collected in the following paperbacks"))
                {
                    volume.Collectable = false;
                    return true;
                }
                else if (volume.Description.Contains("collects") || volume.Description.Contains("collecting"))
                {
                    volume.Collectable = false;
                    return true;
                }
            }
            return false;
        }

        //public static List<Publisher> ConvertToPublishers(string xml)
        //{
        //    List<Publisher> publishers = new List<Publisher>();
        //    CVSearchPublisher.response response = ConvertToPublisherSearchResponse(xml);
        //    foreach (ComicHoarder.WebData.CVSearchPublisher.responseResultsPublisher comicvinepublisher in response.results[0].publisher)
        //    {
        //        Publisher publisher = new Publisher();
        //        publisher.id = ParseHelper.ParseInt(comicvinepublisher.id);
        //        publisher.name = comicvinepublisher.name;
        //        publisher.description = comicvinepublisher.deck;
        //        publisher.enabled = true;
        //        publisher.dateLastUpdated = ParseHelper.ParseDateTime(comicvinepublisher.date_last_updated);
        //        publishers.Add(publisher);
        //    }
        //    return publishers;
        //}
        #endregion
    }
}
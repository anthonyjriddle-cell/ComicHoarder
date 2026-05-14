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
                id = cvIssue.id,
                name = cvIssue.name,
                volumeId = cvIssue.volume is not null ? cvIssue.volume.id : 0,
                issueNumber = issueNumber,
                issueNumberSuffix = issueSuffix,
                publishMonth = publishDate.Month,
                publishYear = publishDate.Year,
                collected = false,
                enabled = true,
                summary = cvIssue.deck is not null ? cvIssue.ToString() : "",
                coverDate = ParseHelper.ParseNullableDateTime(cvIssue.cover_date),
                dateAdded = ParseHelper.ParseNullableDateTime(cvIssue.date_added),
                dateLastUpdated = ParseHelper.ParseNullableDateTime(cvIssue.date_last_updated)
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
                    id = cvIssue.id,
                    name = cvIssue.name,
                    volumeId = cvIssue.volume is not null ? cvIssue.volume.id : 0,
                    issueNumber = issueNumber,
                    issueNumberSuffix = issueSuffix,
                    publishMonth = publishDate.Month,
                    publishYear = publishDate.Year,
                    collected = false,
                    enabled = true,
                    summary = cvIssue.deck is not null ? cvIssue.ToString() : "",
                    coverDate = ParseHelper.ParseNullableDateTime(cvIssue.cover_date),
                    dateAdded = ParseHelper.ParseNullableDateTime(cvIssue.date_added),
                    dateLastUpdated = ParseHelper.ParseNullableDateTime(cvIssue.date_last_updated)
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
                    issue.id = cvIssue.id;
                    issue.name = cvIssue.name;
                    if (cvIssue.issue_number is not null)
                    {
                        if (cvIssue.issue_number.Contains("au"))
                        {
                            float n = 0;
                            string p = "";
                            float.TryParse(new string(cvIssue.issue_number.Where(a => (Char.IsDigit(a) || a == '.')).ToArray()), out n);
                            issue.issueNumber = ParseHelper.ParseFloat(n.ToString());
                            //issue.issueNumber = issue.issueNumber + .1f;
                            p = new string(cvIssue.issue_number.Where(a => !(Char.IsDigit(a) || a == '.')).ToArray());
                            issue.issueNumberSuffix = p;
                        }
                        else
                        {
                            issue.issueNumber = ParseHelper.ParseFloat(cvIssue.issue_number);
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
            volume.id = cvVolume.id;
            volume.publisherId = cvVolume.publisher is not null ? cvVolume.publisher.id : 0;
            volume.name = cvVolume.name;
            volume.description = cvVolume.description;
            volume.dateAdded = ParseHelper.ParseNullableDateTime(cvVolume.date_added);
            volume.dateLastUpdated = ParseHelper.ParseNullableDateTime(cvVolume.date_last_updated);
            volume.collectable = true;
            volume.countOfIssues = cvVolume.count_of_issues;
            volume.startYear = ParseHelper.ParseInt(cvVolume.start_year);
            volume.enabled = true;
            if (volume.dateLastUpdated > DateTime.Now.AddMonths(-13))
            {
                volume.complete = false;
            }
            else
            {
                volume.complete = true;
            }
            volume.collectable = DetectReprint(volume);
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
                    volume.id = cvVolume.id;
                    volume.name = cvVolume.name;
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
            publisher.id = cvPublisher.id;
            publisher.name = cvPublisher.name;
            publisher.description = cvPublisher.deck;
            publisher.enabled = true;
            publisher.dateLastUpdated = ParseHelper.ParseNullableDateTime(cvPublisher.date_last_updated);
            return publisher;
        }

        public static List<Publisher>? ToPublishers(this List<ComicVinePublishersResult> cvPublishers)
        {
            var publishers = new List<Publisher>();
            foreach (var cvPublisher in cvPublishers)
            {
                var publisher = new Publisher()
                {
                    id = cvPublisher.id,
                    name = cvPublisher.name,
                    description = cvPublisher.deck,
                    enabled = true,
                    dateLastUpdated = ParseHelper.ParseNullableDateTime(cvPublisher.date_last_updated)
                };
                publishers.Add(publisher);
            }
            return publishers;
        }


        public static bool DetectReprint(Volume volume)
        {
            if (volume.description is not null)
            {
                if (volume.description.Contains("trade paperback") || volume.description.Contains("tradepaperback") || volume.description.Contains("tpb") || volume.description.Contains("a hardcover book which reprints") || volume.description.Contains("reprinting") || volume.description.Contains("reprints") || volume.description.Contains("collected in the following paperbacks"))
                {
                    volume.collectable = false;
                    return true;
                }
                else if (volume.description.Contains("collects") || volume.description.Contains("collecting"))
                {
                    volume.collectable = false;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Infrastructure.ComicVine.Models;
using ComicHoarder.Domain.Models;
using ComicHoarder.Application.Interfaces;
using ComicHoarder.Infrastructure.ComicVine.Interfaces;

namespace ComicHoarder.Infrastructure.ComicVine.ComicVine
{
    public class WebDataService : IWebDataService
    {
        IWebConnection connection;
        IURLBuilder urlBuilder;

        public WebDataService(IWebConnection connection, IURLBuilder urlBuilder)
        {
            this.connection = connection;
            this.urlBuilder = urlBuilder;
        }

        public WebDataService(string? key)
        {
            this.connection = new WebConnection();
            this.urlBuilder = new URLBuilder(key);
        }

        public Publisher? GetPublisher(int publisherId)
        {
            string? url = urlBuilder.Publisher(publisherId, null);
            if (url is not null)
            {
                var result = connection.Query(url).DeserializePublisher();
                if (result is not null && result.results is not null)
                {
                    return result.results.ToPublisher();
                }
            }
            return null;
        }

        public List<Volume>? GetVolumesFromPublisher(int publisherId)
        {
            string? url = urlBuilder.Publisher(publisherId, null);
            if (url is not null)
            {
                var jsonresult = connection.Query(url);
                if (jsonresult is not null && jsonresult.StartsWith("Message "))
                {
                    return null;
                }
                var result = jsonresult.DeserializePublisher();
                if (result is not null && result.results is not null)
                {
                    return result.results.ToVolumes();
                }
            }
            return null;
        }

        public Volume? GetVolume(int volumeId)
        {
            string? url = urlBuilder.Volume(volumeId, null);
            if (url is not null)
            {
                var result = connection.Query(url).DeserializeVolume();
                if (result is not null && result.results is not null)
                {
                    return result.results.ToVolume();
                }
            }
            return null;
        }

        public List<Issue>? GetIssuesFromVolume(int volumeId)
        {
            string? url = urlBuilder.Volume(volumeId, null);
            if (url is not null)
            {
                var result = connection.Query(url).DeserializeVolume();
                if (result is not null && result.results is not null)
                {
                    return result.results.ToLiteIssues();
                }
            }
            return null;
        }

        //TODO Write test for this
        public Issue? GetIssue(int issueId)
        {
            string? url = urlBuilder.Issue(issueId, null);
            if (url is not null)
            {
                var result = connection.Query(url).DeserializeIssue();
                if (result is not null && result.results is not null)
                {
                    return result.results.ToIssue();
                }
            }
            return null;
        }

        public List<Issue>? GetNewIssues(int offset = 0)
        {
            var fieldList = new List<Enums.IssuesFields>() { Enums.IssuesFields.id, Enums.IssuesFields.name,Enums.IssuesFields.issue_number,Enums.IssuesFields.store_date, Enums.IssuesFields.name, Enums.IssuesFields.description, Enums.IssuesFields.deck, Enums.IssuesFields.date_added, Enums.IssuesFields.date_last_updated, Enums.IssuesFields.volume, Enums.IssuesFields.cover_date };
            var sort = new KeyValuePair<Enums.IssuesFields, Enums.SortDirection>(Enums.IssuesFields.date_added, Enums.SortDirection.desc);
            //TODO need filter and sort
            string ? url = urlBuilder.Issues(fieldList, null, offset, sort, null);
            if (url is not null)
            {
                var result = connection.Query(url).DeserializeIssues();
                if (result is not null && result.results is not null)
                {
                    return result.results.ToIssues();
                }
            }
            return null;
        }

        public List<Publisher>? SearchPublishers(string PartialPublisherName)
        {
            
            var fieldList = new List<Enums.PublishersFields>() { Enums.PublishersFields.id, Enums.PublishersFields.name, Enums.PublishersFields.description, Enums.PublishersFields.date_added, Enums.PublishersFields.date_last_updated, Enums.PublishersFields.deck };

            var filterList = new Dictionary<Enums.PublishersFields, string>();
            filterList.Add(Enums.PublishersFields.name, PartialPublisherName);
            //need to do offset
            string? url = urlBuilder.SearchPublishers(fieldList, null, 0, null, filterList);
            if (url is not null)
            {
                var result = connection.Query(url).DeserializePubishers();
                if (result is not null && result.results is not null)
                {
                    return result.results.ToPublishers();
                }
            }
            return null;
        }

        public List<Volume> SearchVolumes(string PartialVolumeName)
        {
            throw new NotImplementedException();
        }

        public List<Issue> SearchIssues(string PartialIssueName)
        {
            throw new NotImplementedException();
        }
    }
}

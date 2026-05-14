using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Domain.Models;


namespace ComicHoarder.Application.Interfaces
{
    public interface IWebDataService
    {
        //void WebDataService(IWebConnection connection, IURLBuilder urlBuilder);
        //void WebDataService(string? key);
        //publisher
        Publisher? GetPublisher(int publisherId);
        //volume
        List<Volume>? GetVolumesFromPublisher(int publisherId);
        Volume? GetVolume(int volumeId);
        //issue
        List<Issue>? GetIssuesFromVolume(int volumeId);
        Issue? GetIssue(int issueId);
        //search
        List<Publisher>? SearchPublishers(string PartialPublisherName);
        List<Volume>? SearchVolumes(string PartialVolumeName);
        List<Issue>? SearchIssues(string PartialIssueName);
    }
}

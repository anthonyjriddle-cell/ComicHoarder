using ComicHoarder.Infrastructure.ComicVine.Models;

namespace ComicHoarder.Infrastructure.ComicVine.Interfaces
{
    public interface IURLBuilder
    {
        string? Issue(int id, List<Enums.IssueFields>? fieldList);
        string? Issues(List<Enums.IssuesFields>? fieldList, int? limit, int? offset, KeyValuePair<Enums.IssuesFields, Enums.SortDirection>? sort, Dictionary<Enums.IssueFields, string>? filter);
        string? Publisher(int id, List<Enums.PublisherFields>? fieldList);
        string? Publishers(List<Enums.PublishersFields>? fieldList, int? limit, int? offset, KeyValuePair<Enums.PublishersFields, Enums.SortDirection>? sort, Dictionary<Enums.PublishersFields, string>? filter);
        string? SearchIssues(string name);
        string? SearchPublishers(List<Enums.PublishersFields>? fieldList, int? limit, int? offset, KeyValuePair<Enums.PublishersFields, Enums.SortDirection>? sort, Dictionary<Enums.PublishersFields, string>? filter);
        string? SearchVolumes(string name);
        string? Volume(int id, List<Enums.VolumeFields>? fieldList);
        string? Volumes(List<Enums.VolumesFields>? fieldList, int? limit, int? offset, KeyValuePair<Enums.VolumesFields, Enums.SortDirection>? sort, Dictionary<Enums.VolumeFields, string>? filter);
    }
}
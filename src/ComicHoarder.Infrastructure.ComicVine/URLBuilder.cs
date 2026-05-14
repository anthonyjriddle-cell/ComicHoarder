using ComicHoarder.Infrastructure.ComicVine.Interfaces;
using ComicHoarder.Infrastructure.ComicVine.Models;
using System.Text;


namespace ComicHoarder.Infrastructure.ComicVine.ComicVine
{
    public class Sort
    {
        public Enums.IssueFields issueField;
        public Enums.SortDirection sortDirection;
    }
    public class URLBuilder : IURLBuilder
    {
        private string? apikey { get; set; }

        private static string BaseUrl = @"https://www.comicvine.com/api/";

        private static string SearchUrlHeader = @"https://www.comicvine.com/api/search/?api_key=";

        Enums.Format format = Enums.Format.json;

        List<Sort> sort = new List<Sort>()
        {
             new Sort { issueField = Enums.IssueFields.date_added, sortDirection = Enums.SortDirection.asc },
             new Sort { issueField = Enums.IssueFields.id, sortDirection = Enums.SortDirection.desc }
        };

        public URLBuilder(string? inkey)
        {
            apikey = inkey;
        }

        //format,field_list,limit,offset,sort,filter

        public string? Issues(List<Enums.IssuesFields>? fieldList, int? limit, int? offset, KeyValuePair<Enums.IssuesFields, Enums.SortDirection>? sort, Dictionary<Enums.IssueFields, string>? filter)
        {
            var sb = new StringBuilder();
            var lsh = new ListToStringHelper<Enums.IssuesFields>();

            sb.Append(BaseUrl);
            sb.Append("issues/");
            sb.Append("?api_key=");
            sb.Append(apikey);
            sb.Append("&format=");
            sb.Append(format.ToString());
            if (fieldList != null)
            {
                sb.Append(lsh.FieldListToString(fieldList));
            }
            if (limit != null)
            {
                sb.Append("&limit=");
                sb.Append(limit);
            }
            if (offset != null)
            {
                sb.Append("&offset=");
                sb.Append(offset);
            }
            if (sort != null)
            {
                sb.Append(lsh.SortListToString(sort));
            }

            return sb.ToString();
        }

        public string? Issue(int id, List<Enums.IssueFields>? fieldList)
        {
            var sb = new StringBuilder();
            var lsh = new ListToStringHelper<Enums.IssueFields>();

            sb.Append(BaseUrl);
            sb.Append("issue/4000-");
            sb.Append(id);
            sb.Append("?api_key=");
            sb.Append(apikey);
            sb.Append("&format=");
            sb.Append(format.ToString());
            if (fieldList != null)
            {
                sb.Append(lsh.FieldListToString(fieldList));
            }

            return sb.ToString();
        }

        public string? Publishers(List<Enums.PublishersFields>? fieldList, int? limit, int? offset, KeyValuePair<Enums.PublishersFields, Enums.SortDirection>? sort, Dictionary<Enums.PublishersFields, string>? filter)
        {
            var sb = new StringBuilder();
            var lsh = new ListToStringHelper<Enums.PublishersFields>();

            sb.Append(BaseUrl);
            sb.Append("publishers/");
            sb.Append("?api_key=");
            sb.Append(apikey);
            sb.Append("&format=");
            sb.Append(format.ToString());
            if (fieldList != null)
            {
                sb.Append(lsh.FieldListToString(fieldList));
            }
            if (limit != null)
            {
                sb.Append("&limit=");
                sb.Append(limit);
            }
            if (offset != null)
            {
                sb.Append("&offset=");
                sb.Append(offset);
            }
            if (sort != null)
            {
                sb.Append(lsh.SortListToString(sort));
            }
            if (filter != null)
            {
                sb.Append("&filter=");
                sb.Append(lsh.FilterListToString(filter));
            }

            return sb.ToString();
        }

        public string? Publisher(int id, List<Enums.PublisherFields>? fieldList)
        {
            var sb = new StringBuilder();
            var lsh = new ListToStringHelper<Enums.PublisherFields>();

            sb.Append(BaseUrl);
            sb.Append("publisher/4010-");
            sb.Append(id);
            sb.Append("?api_key=");
            sb.Append(apikey);
            sb.Append("&format=");
            sb.Append(format.ToString());
            if (fieldList != null)
            {
                sb.Append(lsh.FieldListToString(fieldList));
            }

            return sb.ToString();
        }

        public string? Volumes(List<Enums.VolumesFields>? fieldList, int? limit, int? offset, KeyValuePair<Enums.VolumesFields, Enums.SortDirection>? sort, Dictionary<Enums.VolumeFields, string>? filter)
        {
            var sb = new StringBuilder();
            var lsh = new ListToStringHelper<Enums.VolumesFields>();

            sb.Append(BaseUrl);
            sb.Append("volumes/");
            sb.Append("?api_key=");
            sb.Append(apikey);
            sb.Append("&format=");
            sb.Append(format.ToString());
            if (fieldList != null)
            {
                sb.Append(lsh.FieldListToString(fieldList));
            }
            if (limit != null)
            {
                sb.Append("&limit=");
                sb.Append(limit);
            }
            if (offset != null)
            {
                sb.Append("&offset=");
                sb.Append(offset);
            }
            if (sort != null)
            {
                sb.Append(lsh.SortListToString(sort));
            }

            return sb.ToString();
        }

        public string? Volume(int id, List<Enums.VolumeFields>? fieldList)
        {
            var sb = new StringBuilder();
            var lsh = new ListToStringHelper<Enums.VolumeFields>();

            sb.Append(BaseUrl);
            sb.Append("volume/4050-");
            sb.Append(id);
            sb.Append("?api_key=");
            sb.Append(apikey);
            sb.Append("&format=");
            sb.Append(format.ToString());
            if (fieldList != null)
            {
                sb.Append(lsh.FieldListToString(fieldList));
            }

            return sb.ToString();
        }

        public string? SearchVolumes(string name)
        {
            //TODO implement Search Volumes comic vine service
            throw new NotImplementedException();
        }

        public string? SearchIssues(string name)
        {
            //TODO implement Search Issue comic vine service
            throw new NotImplementedException();
        }

        public string? SearchPublishers(List<Enums.PublishersFields>? fieldList, int? limit, int? offset, KeyValuePair<Enums.PublishersFields, Enums.SortDirection>? sort, Dictionary<Enums.PublishersFields, string>? filter)
        {
            //return SearchUrlHeader + apikey + "&resources=publisher&query=" + name + "&format=xml";
            return Publishers(fieldList, limit, offset, sort, filter);
        }

    }
}


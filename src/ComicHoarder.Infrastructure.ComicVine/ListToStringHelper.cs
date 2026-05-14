using ComicHoarder.Infrastructure.ComicVine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Infrastructure.ComicVine.ComicVine
{
    internal class ListToStringHelper<T>
    {
        public string? FieldListToString(List<T> fields)
        {
            var sb = new StringBuilder();
            sb.Append("&field_list=");
            foreach (var field in fields)
            {
                if (field is not null)
                {
                    sb.Append(field.ToString() + ",");
                }
            }
            return sb.ToString().Remove(sb.Length - 1);
        }
        public string? SortListToString(KeyValuePair<T, Enums.SortDirection>? sort)
        {
            var sb = new StringBuilder();
            if (sort is null)
            {
                return "";
            }
            if (sort.Value.Key is not null)
            {
                sb.Append(@"&");
                sb.Append(@"sort=");
                sb.Append(sort.Value.Key.ToString());
                sb.Append(@":");
                sb.Append(sort.Value.Value.ToString());
            }
            return sb.ToString();
        }

        public string? FilterListToString(Dictionary<T, string>? filters)
        {
            var sb = new StringBuilder();
            if (filters is null)
            {
                return "";
            }
            foreach (var filter in filters)
            {
                if (filter.Key is not null)
                {
                    sb.Append(filter.Key.ToString() + ":" + filter.Value + ",");
                }
            }
            return sb.ToString().Remove(sb.Length - 1);
        }
    }
}

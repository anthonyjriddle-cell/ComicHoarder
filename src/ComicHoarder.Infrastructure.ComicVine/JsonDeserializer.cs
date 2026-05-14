using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using ComicHoarder.Infrastructure.ComicVine.Models;
using ComicHoarder.Domain.Models;
//using Newtonsoft.Json.Serialization;
//using Newtonsoft.Json;

namespace ComicHoarder.Infrastructure.ComicVine.ComicVine
{
    public static class JsonDeserializer
    {
        public static ComicVinePublishers? DeserializePubishers(this string? json)
        {
            if (json is null) { return null; }
            ComicVinePublishers? result = JsonSerializer.Deserialize<ComicVinePublishers>(json);
            return result;
        }

        public static ComicVinePublisher? DeserializePublisher(this string? json)
        {
            if (json is null) { return null; }
            ComicVinePublisher? result = JsonSerializer.Deserialize<ComicVinePublisher>(json);
            return result;
        }
        public static ComicVineIssues? DeserializeIssues(this string? json)
        {
            if (json is null) { return null; }
            ComicVineIssues? result = JsonSerializer.Deserialize<ComicVineIssues>(json);
            return result;
        }

        public static ComicVineIssue? DeserializeIssue(this string? json)
        {
            if (json is null || (json.Contains("Object Not Found") && json.Contains("error"))) { return null; }
            ComicVineIssue? result = JsonSerializer.Deserialize<ComicVineIssue>(json);
            return result;
        }

        public static ComicVineVolumes? DeserializeVolumes(this string? json)
        {
            if (json is null) { return null; }
            ComicVineVolumes? result = JsonSerializer.Deserialize<ComicVineVolumes>(json);
            return result;
        }

        public static ComicVineVolume? DeserializeVolume(this string? json)
        {
            if (json is null) { return null; }
            ComicVineVolume? result = JsonSerializer.Deserialize<ComicVineVolume>(json);
            return result;
        }


    }
}

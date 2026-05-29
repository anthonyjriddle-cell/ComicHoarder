using System;

namespace ComicHoarder.Infrastructure.ComicVine.Interfaces
{
    public interface IWebConnection
    {
        string? Query(string Url);
    }
}
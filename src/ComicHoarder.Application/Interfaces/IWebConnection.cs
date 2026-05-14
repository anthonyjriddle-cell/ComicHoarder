using System;

namespace ComicHoarder.Application.Interfaces
{
    public interface IWebConnection
    {
        string? Query(string Url);
    }
}
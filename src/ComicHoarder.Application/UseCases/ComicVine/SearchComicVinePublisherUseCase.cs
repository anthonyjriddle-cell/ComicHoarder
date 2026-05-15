using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.ComicVine.Interfaces;
using ComicHoarder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Application.UseCases.ComicVine
{
    public class SearchComicVinePublisherUseCase : ISearchComicVinePublisherUseCase
    {
        private readonly IWebDataService webDataService;

        public SearchComicVinePublisherUseCase(IWebDataService webDataService)
        {
            this.webDataService = webDataService;
        }

        public async Task<IEnumerable<Publisher>> ExecuteAsync(string partialPublisherName)
        {
            //comic vine search doesn't work
            var results = webDataService.SearchPublishers(partialPublisherName);
            return results;
        }
    }
}

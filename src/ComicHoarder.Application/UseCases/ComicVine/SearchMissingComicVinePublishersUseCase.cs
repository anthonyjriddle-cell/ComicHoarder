using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.ComicVine.Interfaces;
using ComicHoarder.Domain.Models;


namespace ComicHoarder.Application.UseCases.ComicVine
{
    public class SearchMissingComicVinePublishersUseCase : ISearchMissingComicVinePublishersUseCase
    {
        private readonly ISearchComicVinePublisherUseCase comicVineSearch;
        private readonly IPublisherRepository publisherRepository;

        public SearchMissingComicVinePublishersUseCase(
            ISearchComicVinePublisherUseCase comicVineSearch,
            IPublisherRepository publisherRepository)
        {
            this.comicVineSearch = comicVineSearch;
            this.publisherRepository = publisherRepository;
        }

        public async Task<IEnumerable<Publisher>> ExecuteAsync(string partialPublisherName)
        {
            var comicVinePublishers = await comicVineSearch.ExecuteAsync(partialPublisherName);
            var localPublishers = await publisherRepository.GetAllPublishersAsync();

            var localIds = localPublishers.Select(x => x.Id).ToHashSet();

            return comicVinePublishers
                .Where(x => !localIds.Contains(x.Id))
                .ToList();
        }
    }
}
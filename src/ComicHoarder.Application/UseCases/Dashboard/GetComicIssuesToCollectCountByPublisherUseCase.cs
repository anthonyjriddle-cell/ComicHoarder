using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Dashboard.Interfaces;
using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Dashboard
{
    public class GetComicIssuesToCollectCountByPublisherUseCase
        : IGetComicIssuesToCollectCountByPublisherUseCase
    {
        private readonly IComicIssuesToCollectCountByPublisherEFCoreRepository repository;

        public GetComicIssuesToCollectCountByPublisherUseCase(
            IComicIssuesToCollectCountByPublisherEFCoreRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ComicIssuesToCollectCountByPublisher>> ExecuteAsync()
        {
            return await repository.GetAllAsync();
        }
    }
}

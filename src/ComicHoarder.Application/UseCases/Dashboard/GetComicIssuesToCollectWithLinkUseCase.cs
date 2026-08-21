using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Dashboard.Interfaces;
using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Dashboard
{
    public class GetComicIssuesToCollectWithLinkUseCase : IGetComicIssuesToCollectWithLinkUseCase
    {
        private readonly IComicIssuesToCollectWithLinkEFCoreRepository repository;

        public GetComicIssuesToCollectWithLinkUseCase(
            IComicIssuesToCollectWithLinkEFCoreRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ComicIssuesToCollectWithLink>> ExecuteAsync(int publisherId)
        {
            return await repository.GetByPublisherAsync(publisherId);
        }
    }
}

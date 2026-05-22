using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.ComicVine.Interfaces;
using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.ComicVine
{
    public class SearchMissingComicVineVolumesByPublisherUseCase : ISearchMissingComicVineVolumesByPublisherUseCase
    {
        private readonly IWebDataService webDataService;
        private readonly IVolumeRepository volumeRepository;

        public SearchMissingComicVineVolumesByPublisherUseCase(
            IWebDataService webDataService,
            IVolumeRepository volumeRepository)
        {
            this.webDataService = webDataService;
            this.volumeRepository = volumeRepository;
        }

        public async Task<IEnumerable<Volume>> ExecuteAsync(int publisherId)
        {
            var comicVineVolumes = webDataService.GetVolumesFromPublisher(publisherId)
                ?? new List<Volume>();

            var localVolumeIds = await volumeRepository.GetAllVolumeId();

            var volumesNotInDatabase =  comicVineVolumes
                .Where(v => !localVolumeIds.Contains(v.Id))
                .ToList();

            return volumesNotInDatabase;
        }
    }
}
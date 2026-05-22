using ComicHoarder.Domain.Models;
using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Volumes.Interfaces;

namespace ComicHoarder.Application.UseCases.Volumes
{
    public class AddVolumeUseCase : IAddVolumeUseCase
    {
        private readonly IVolumeRepository volumeRepository;

        public AddVolumeUseCase(IVolumeRepository volumeRepository)
        {
            this.volumeRepository = volumeRepository;
        }

        public async Task ExecuteAsync(Volume volume)
        {
            await volumeRepository.AddVolumeAsync(volume);
        }
    }
}

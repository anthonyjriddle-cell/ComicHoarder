using ComicHoarder.Infrastructure;
using ComicHoarder.Infrastructure.ComicVine;
using ComicHoarder.Infrastructure.ComicVine.ComicVine;
using ComicHoarder.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace ComicVineDBSync.Services
{
    public class VolumeService
    {
        private readonly WebDataService _webDataService;
        private readonly PublisherEFCoreRepository _publisherRepository;
        private readonly VolumeEFCoreRepository _readVolumeRepository;
        private readonly VolumeEFCoreRepository _writeVolumeRepository;
        private readonly ILogger<VolumeService> _logger;

        public VolumeService(WebDataService webDataService, PublisherEFCoreRepository publisherRepository, VolumeEFCoreRepository readVolumeRepository, VolumeEFCoreRepository writeVolumeRepository, ILogger<VolumeService> logger)
        {
            _webDataService = webDataService;
            _publisherRepository = publisherRepository;
            _readVolumeRepository = readVolumeRepository;
            _writeVolumeRepository = writeVolumeRepository;
            _logger = logger;
        }

        public async Task FindNewVolumesAsync()
        {
            var publisherIds = await _publisherRepository.GetAllEnabledPublisherIdsAsync();
            var volumeIds = await _readVolumeRepository.GetAllVolumeIdAsync();

            foreach (var publisherId in publisherIds)
            {
                if (publisherId != 31)
                    continue;

                var publisherName = _publisherRepository.GetPublisherByIdAsync(publisherId);
                _logger.LogInformation("Checking publisher {PublisherName} for updates", publisherName);

                var volumes = _webDataService.GetVolumesFromPublisher(publisherId);
                Thread.Sleep(2000);

                var newVolumes = volumes.Where(x => !volumeIds.Contains(x.Id)).ToList();
                _logger.LogInformation("Creating {Count} volumes for publisher {PublisherName}", newVolumes.Count, publisherName);

                try
                {
                    foreach (var newVolume in newVolumes)
                    {
                        var volume = _webDataService.GetVolume(newVolume.Id);
                        Thread.Sleep(2000);

                        if (volume is not null)
                        {
                            _logger.LogInformation("Creating volume {VolumeName} for {PublisherName}", newVolume.Name, publisherName);
                            var reprint = ReprintDetector.DetectReprint(volume);
                            await _writeVolumeRepository.AddVolumeAsync(volume);
                        }
                    }
                }
                finally
                {
                    _logger.LogInformation("Saved volumes for publisher {PublisherName}", publisherName);
                }
            }
        }
    }
}
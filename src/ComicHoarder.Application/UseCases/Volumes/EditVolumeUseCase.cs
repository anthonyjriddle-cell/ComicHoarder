using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Volumes.Interfaces;
using ComicHoarder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Application.UseCases.Volumes
{
    public class EditVolumeUseCase : IEditVolumeUseCase
    {
        private readonly IVolumeRepository volumeRepository;

        public EditVolumeUseCase(IVolumeRepository volumeRepository)
        {
            this.volumeRepository = volumeRepository;
        }
        public async Task ExecuteAsync(Volume volume)
        {
            await volumeRepository.UpdateVolumeAsnc(volume);
        }
    }
}

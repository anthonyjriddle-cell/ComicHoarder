using ComicHoarder.Domain.Models;
using ComicHoarder.Application.UseCases;
using ComicHoarder.Application.UseCases.Volumes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Application.Interfaces;

namespace CH.UseCases.RepositoryUseCases.Volumes
{
    public class DeleteVolumeUseCase : IDeleteVolumeUseCase
    {
        private readonly IVolumeRepository volumeRepository;

        public DeleteVolumeUseCase(IVolumeRepository volumeRepository)
        {
            this.volumeRepository = volumeRepository;
        }
        public async Task ExecuteAsync(int volumeId)
        {
            await volumeRepository.DeleteVolumeAsync(volumeId);
        }
    }
}


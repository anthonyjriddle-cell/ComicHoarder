using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Volumes.Interfaces;
using ComicHoarder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.UseCases.RepositoryUseCases.Volumes
{
    public class ViewVolumesByPublisherAndNameUseCase : IViewVolumesByPublisherAndNameUseCase
    {
        private readonly IVolumeRepository volumeRepository;

        public ViewVolumesByPublisherAndNameUseCase(IVolumeRepository volumeRepository)
        {
            this.volumeRepository = volumeRepository;
        }

        public async Task<IEnumerable<Volume>> ExecuteAsync(int id, string name)
        {
            return await volumeRepository.GetVolumesByPublisherAndNameAsync(id, name);
        }
    }
}

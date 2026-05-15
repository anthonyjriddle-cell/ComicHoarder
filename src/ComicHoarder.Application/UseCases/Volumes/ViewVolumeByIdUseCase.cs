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
    public class ViewVolumeByIdUseCase : IViewVolumeByIdUseCase
    {
        private readonly IVolumeRepository VolumeRepository;

        public ViewVolumeByIdUseCase(IVolumeRepository VolumeRepository)
        {
            this.VolumeRepository = VolumeRepository;
        }
        public async Task<Volume> ExecuteAsync(int VolumeId)
        {
            return await VolumeRepository.GetVolumeByIdAsync(VolumeId);
        }
    }
}

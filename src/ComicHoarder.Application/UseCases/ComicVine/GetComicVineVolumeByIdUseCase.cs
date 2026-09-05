using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.ComicVine.Interfaces;
using ComicHoarder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Application.UseCases.ComicVine
{
    public class GetComicVineVolumeByIdUseCase : IGetComicVineVolumeByIdUseCase
    {
        private readonly IWebDataService webDataService;

        public GetComicVineVolumeByIdUseCase(IWebDataService webDataService)
        {
            this.webDataService = webDataService;
        }

        public async Task<Volume?> ExecuteAsync(int id)
        {
            return webDataService.GetVolume(id);
        }
    }
}

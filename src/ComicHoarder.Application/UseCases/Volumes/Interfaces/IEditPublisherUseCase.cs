using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Volumes.Interfaces
{
    public interface IEditVolumeUseCase
    {
        Task ExecuteAsync(Volume volume);
    }
}
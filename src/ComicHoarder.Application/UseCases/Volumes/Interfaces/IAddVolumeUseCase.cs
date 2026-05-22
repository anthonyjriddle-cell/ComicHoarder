using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Volumes.Interfaces
{
    public interface IAddVolumeUseCase
    {
        Task ExecuteAsync(Volume volume);
    }
}

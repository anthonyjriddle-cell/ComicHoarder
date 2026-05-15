using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Volumes.Interfaces
{
    public interface IDeleteVolumeUseCase
    {
        Task ExecuteAsync(int volumeId);
    }
}
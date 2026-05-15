using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Volumes.Interfaces
{
    public interface IViewVolumeByIdUseCase
    {
        Task<Volume> ExecuteAsync(int VolumeId);
    }
}
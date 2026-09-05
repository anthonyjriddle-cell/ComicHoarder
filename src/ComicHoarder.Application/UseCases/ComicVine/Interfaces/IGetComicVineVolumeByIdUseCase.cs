using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.ComicVine.Interfaces
{
    public interface IGetComicVineVolumeByIdUseCase
    {
        Task<Volume?> ExecuteAsync(int id);
    }
}
using ComicHoarder.Domain.Models;
using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Publishers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Application.UseCases.Publishers
{
    public class DeletePublisherUseCase : IDeletePublisherUseCase
    {
        private readonly IPublisherRepository publisherRepository;

        public DeletePublisherUseCase(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }
        public async Task ExecuteAsync(int publisherId)
        {
            await publisherRepository.DeletePublisherAsync(publisherId);
        }
    }
}

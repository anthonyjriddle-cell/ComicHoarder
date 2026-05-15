using ComicHoarder.Domain.Models;
using ComicHoarder.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Application.UseCases.Publishers.Interfaces;

namespace ComicHoarder.Application.UseCases.Publishers
{
    public class ViewPublisherByIdUseCase : IViewPublisherByIdUseCase
    {
        private readonly IPublisherRepository publisherRepository;

        public ViewPublisherByIdUseCase(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }
        public async Task<Publisher> ExecuteAsync(int publisherId)
        {
            return await publisherRepository.GetPublisherByIdAsync(publisherId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Domain.Models;
using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Publishers.Interfaces;

namespace ComicHoarder.Application.UseCases.Publishers
{
    public class ViewPublishersByNameUseCase : IViewPublishersByNameUseCase
    {
        private readonly IPublisherRepository publisherRepository;

        public ViewPublishersByNameUseCase(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

        public async Task<IEnumerable<Publisher>> ExecuteAsync(string name)
        {
            return await publisherRepository.GetPublishersByNameAsync(name);
        }
    }
}

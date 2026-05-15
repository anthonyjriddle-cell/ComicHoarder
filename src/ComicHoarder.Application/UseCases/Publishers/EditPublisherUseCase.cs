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
    public class EditPublisherUseCase : IEditPublisherUseCase
    {
        private readonly IPublisherRepository publisherRepository;

        public EditPublisherUseCase(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }
        public async Task ExecuteAsync(Publisher publisher)
        {
            await publisherRepository.UpdatePublisherAsnc(publisher);
        }
    }
}

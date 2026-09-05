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
    public class GetComicVineIssueByIdUseCase : IGetComicVineIssueByIdUseCase
    {
        private readonly IWebDataService webDataService;

        public GetComicVineIssueByIdUseCase(IWebDataService webDataService)
        {
            this.webDataService = webDataService;
        }

        public async Task<Issue?> ExecuteAsync(int id)
        {
            return webDataService.GetIssue(id);
        }
    }
}

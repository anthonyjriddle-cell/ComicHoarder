using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Infrastructure
{
    public class ComicIssuesToCollectCountByPublisherEFCoreRepository
    {
        private readonly IDbContextFactory<CHContext> contextFactory;

        public ComicIssuesToCollectCountByPublisherEFCoreRepository(IDbContextFactory<CHContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }


    }
}

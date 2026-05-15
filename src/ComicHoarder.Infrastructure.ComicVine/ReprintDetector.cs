using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Domain.Models;

namespace ComicHoarder.Infrastructure.ComicVine.ComicVine
{
    public static class ReprintDetector
    {
        public static bool DetectReprint(Volume volume)
        {
            if (volume.Description is not null)
            {
                if (volume.Description.Contains("trade paperback") || volume.Description.Contains("tradepaperback") || volume.Description.Contains("tpb") || volume.Description.Contains("a hardcover book which reprints") || volume.Description.Contains("reprinting") || volume.Description.Contains("reprints") || volume.Description.Contains("collected in the following paperbacks"))
                {
                    volume.Collectable = false;
                    return true;
                }
                else if (volume.Description.Contains("collects") || volume.Description.Contains("collecting"))
                {
                    volume.Collectable = false;
                    return true;
                }
            }
            return false;
        }
    }
}

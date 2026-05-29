using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetComicsDownload
{
    public class DownloadLink
    {
        public DownloadLink()
        {
            ComicName = string.Empty;
            Link = string.Empty;
        }
        public string ComicName { get; set; }
        public string Link { get; set; }
    }
}

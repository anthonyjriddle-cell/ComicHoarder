using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetComicsDownloader.Services
{
    public class GetComicsHttpService : IDisposable
    {
        private readonly HttpClient _client;
        public GetComicsHttpService()
        {
            _client = new HttpClient();
        }
        public void Dispose()
        {
            _client.Dispose();
        }
        public async Task<string> GetHtmlFromUrl(string url)
        {
            var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> DownloadFileFromUrl(string url, string downloadPath)
        {
            var response = await _client.GetAsync(url);
            using (var ms = new MemoryStream())
            {
                var filename = System.Net.WebUtility.UrlDecode(response.RequestMessage.RequestUri.ToString().Split('/').Last());
                filename = Path.Combine(downloadPath, filename);

                await response.Content.CopyToAsync(ms);
                byte[] data;
                data = ms.ToArray();

                using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                }
                return filename;
            }
        }
    }
}

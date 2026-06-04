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
            var response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var filename = System.Net.WebUtility.UrlDecode(
                response.RequestMessage.RequestUri.ToString().Split('/').Last());
            var fullPath = Path.Combine(downloadPath, filename);

            using var contentStream = await response.Content.ReadAsStreamAsync();
            using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
            await contentStream.CopyToAsync(fileStream);

            return fullPath;
        }
    }
}

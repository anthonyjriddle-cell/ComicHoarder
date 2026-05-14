using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Domain.Models;
using System.IO;
using System.Net;
using ComicHoarder.Application.Interfaces;

namespace ComicHoarder.Infrastructure.ComicVine.ComicVine
{
    public class WebConnection : IWebConnection
    {
        public string? Query(string Url)
        {
            string responseBody = "";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.114 Safari/537.36 Edg/103.0.1264.49");
            try
            {
                HttpResponseMessage response = client.GetAsync(Url).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseBody = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (HttpRequestException e)
            {
                responseBody = "Message :{0} " + e.Message;
            }

            return responseBody;
        }
    }
}
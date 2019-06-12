﻿using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Siotrix.Discord.Developer
{
    public class StackExchangeService
    {
        private string _ApiReferenceUrl =
            $"http://api.stackexchange.com/2.2/search/advanced" +
            "?key={0}" +
            $"&order=desc" +
            $"&sort=votes" +
            $"&filter=default";
        public async Task<StackExchangeResponse> GetStackExchangeResultsAsync(string token, string phrase, string site, string tags)
        {
            _ApiReferenceUrl = string.Format(_ApiReferenceUrl, token);
            phrase = Uri.EscapeDataString(phrase);
            site = Uri.EscapeDataString(site);
            tags = Uri.EscapeDataString(tags);
            var query = _ApiReferenceUrl += $"&site={site}&tags={tags}&q={phrase}";
            var client = new HttpClient();
            var response = await client.GetAsync(query);
            if (!response.IsSuccessStatusCode)
            {
                throw new WebException("Something failed while querying the Stack Exchange API.");
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<StackExchangeResponse>(jsonResponse);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Discord.Commands;
using Discord.Net;
using Newtonsoft.Json;

namespace Siotrix.Discord.Developer
{
    public class CSDocsService
    {
        private const string ApiReferenceUrl = "https://docs.microsoft.com/api/apibrowser/dotnet/search?search=";

        public async Task<CSDocsApiResponse> GetDocumentationResultsAsync(string term)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ApiReferenceUrl + term);

            if (!response.IsSuccessStatusCode)
            {
                throw new WebException("Something failed while querying the .NET Api docs.");
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CSDocsApiResponse>(jsonResponse);
        }
    }
}
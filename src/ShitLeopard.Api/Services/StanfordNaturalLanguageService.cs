using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Services
{
    public class StanfordNaturalLanguageService : INaturalLanguageService
    {
        private readonly string _path = @"C:\Development\ShitLeopard\src\ShitLeopard.Api\SampleResponse.json";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IConnectionStringProvider _connectionStringProvider;

        public StanfordNaturalLanguageService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IConnectionStringProvider connectionStringProvider )
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _connectionStringProvider = connectionStringProvider;
        }

        public async Task<ParsedSentence> ParseSentenceAsync(string sentence)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var ls = _connectionStringProvider.GetString("languageService");
               var qs = new StringBuilder("properties=")
                .Append(WebUtility.UrlEncode(JsonConvert.SerializeObject(new
                {
                    annotators = "tokenize,ssplit,pos,ner,depparse,openie,sentiment"
                }))).Append("&pipelineLanguage=en");
                var uriBuilder = new UriBuilder(ls);
                uriBuilder.Query = qs.ToString();
                var message = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri);
                message.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>(string.Empty, sentence) });
                try
                {
                    var response = await client.SendAsync(message);
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var entity = JObject.Parse(content);

                        return ParsedSentence.Build(entity);
                    }
                }
                catch
                {
                }

                return new ParsedSentence();
            }
        }
    }
}
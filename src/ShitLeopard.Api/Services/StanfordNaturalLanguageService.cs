using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShitLeopard.Common.Contracts;

namespace ShitLeopard.Api.Services
{
    public class StanfordNaturalLanguageService : INaturalLanguageService
    {
        private readonly string _path = @"C:\Development\ShitLeopard\src\ShitLeopard.Api\SampleResponse.json";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public StanfordNaturalLanguageService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<dynamic> ParseSentenceAsync(string sentence)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var qs = new StringBuilder("properties=")
                .Append(WebUtility.UrlEncode(JsonConvert.SerializeObject(new
                {
                    annotators = "tokenize,ssplit,pos,ner,depparse,openie,sentiment"
                }))).Append("&pipelineLanguage=en");
                var uriBuilder = new UriBuilder(_configuration["nlpHost"]);
                uriBuilder.Query = qs.ToString();
                var message = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri);
                message.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>(string.Empty, sentence) });
                var response = await client.SendAsync(message);
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var entity = JObject.Parse(content);
                    var arrayOfSentences = entity.SelectToken("sentences") as JArray;
                    foreach (var sentenceToken in arrayOfSentences)
                    {
                        var tokens = sentenceToken.SelectToken("tokens") as JArray;
                        var isQuestion = IsQuestion(tokens);



                    }
                    return entity;
                }

                return new { };
            }
        }

        private bool IsQuestion( JArray tokens)
        {
            return tokens.OfType<JObject>().Any(x => x.Property("pos").Value.ToString().StartsWith("W"));
        }
    }
}
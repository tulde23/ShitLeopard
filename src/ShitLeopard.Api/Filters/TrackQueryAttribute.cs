using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Filters
{
    public class TrackQueryAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public string Key { get; }

        public TrackQueryAttribute(string key)
        {
            Key = key;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService<ITrackedQueryService>();
            var tag = serviceProvider.GetService<ITagService>();
            return new AsyncFilter(service, tag, Key);
        }

        internal class AsyncFilter : IAsyncActionFilter
        {
            private readonly ITrackedQueryService _requestProfileService;
            private readonly ITagService _tagService;
            private readonly string _key;

            public AsyncFilter(ITrackedQueryService requestProfileService, ITagService tagService, string key)
            {
                _requestProfileService = requestProfileService;
                _tagService = tagService;
                _key = key;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var model = new TrackedQueryModel();
                var headers = context.HttpContext.Request.Headers;
                var question = context.ActionArguments[_key] as Question;
                if( question?.Text?.Length <= 3)
                {
                    return;
                }

                //if (headers.ContainsKey("Host") && headers["Host"].ToString().Contains("localhost"))
                //{
                //    await next();
                //    return;
                //}

                if (headers.ContainsKey("User-Agent"))
                {
                    model.AgentString = headers["User-Agent"].ToString();
                }

                model.Ipaddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
                model.LastAccessTime = DateTime.UtcNow;
                model.Headers = new Dictionary<string, object>();
                foreach ( var item in context.HttpContext.Request.Headers)
                {
                    model.Headers.Add(item.Key, item.Value);
                }
               
                model.Route = $"{context.HttpContext.Request.Method}:  {context.HttpContext.Request.Path}";
                model.Query = question.Text;

               
                await _requestProfileService.SaveAsync(model);
                await _tagService.SaveTagAsync(new TagsModel
                {
                    Category = $"DialogSearch",
                    Frequency = 0,
                    Name = question.Text,
                    IpAddress = model.Ipaddress
                });
                await next();
            }
        }
    }
}
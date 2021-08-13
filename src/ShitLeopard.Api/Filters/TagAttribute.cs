using System;
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
    public class TagAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public string Key { get; }

        public TagAttribute(string key)
        {
            Key = key;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService<IRequestProfileService>();
            var tag = serviceProvider.GetService<ITagService>();
            return new AsyncFilter(service, tag, Key);
        }

        internal class AsyncFilter : IAsyncActionFilter
        {
            private readonly IRequestProfileService _requestProfileService;
            private readonly ITagService _tagService;
            private readonly string _key;

            public AsyncFilter(IRequestProfileService requestProfileService, ITagService tagService, string key)
            {
                _requestProfileService = requestProfileService;
                _tagService = tagService;
                _key = key;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var model = new RequestProfileModel();
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
                model.Headers = JsonConvert.SerializeObject(context.HttpContext.Request.Headers, Formatting.Indented);
                model.Route = $"{context.HttpContext.Request.Method}:  {context.HttpContext.Request.Path}";
                if (context.ActionArguments != null)
                {
                   var body = JsonConvert.SerializeObject(context.ActionArguments, Formatting.Indented);
                    if (body.Length > 255)
                    {
                        context.Result = new BadRequestResult();

                        return;
                    }
                    model.Body = body;
                }

               
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
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Filters
{
    public class InboundRequestInspectorFilterAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService<IRequestProfileService>();
            return new AsyncFilter(service);
        }

        internal class AsyncFilter : IAsyncActionFilter
        {
            private readonly IRequestProfileService _requestProfileService;

            public AsyncFilter(IRequestProfileService requestProfileService)
            {
                _requestProfileService = requestProfileService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var model = new RequestProfileModel();
                var headers = context.HttpContext.Request.Headers;

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
                   var body = $" Body: {JsonConvert.SerializeObject(context.ActionArguments, Formatting.Indented)}";
                    if (body.Length > 1000)
                    {
                        context.Result = new BadRequestResult();

                        return;
                    }
                    model.Route += body;
                }

               
                await _requestProfileService.SaveAsync(model);
                await next();
            }
        }
    }
}
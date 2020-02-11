using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ShitLeopard.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class ServiceController<TService> : ControllerBase
    {
        protected ILogger Logger { get; }

        protected ServiceController(ILoggerFactory loggerFactory, TService service)
        {
            Service = service;
            Logger = loggerFactory.CreateLogger(this.GetType());
        }

        public TService Service { get; }
    }
}
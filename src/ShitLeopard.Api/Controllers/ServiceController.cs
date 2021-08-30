using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ShitLeopard.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class ServiceController<TService> : Controller
    {
        protected ILogger Logger { get; }

        protected ServiceController(ILoggerFactory loggerFactory, TService service)
        {
            Service = service;
            Logger = loggerFactory.CreateLogger(this.GetType());
        }

        public TService Service { get; }
    }

    [Route("api/[controller]")]
    public abstract class ServiceController<TService, TService2> : Controller
    {
        protected ILogger Logger { get; }

        protected ServiceController(ILoggerFactory loggerFactory, TService service, TService2 service2)
        {
            Service = service;
            Service2 = service2;
            Logger = loggerFactory.CreateLogger(this.GetType());
        }

        public TService Service { get; }
        public TService2 Service2 { get; }
    }

    [Route("api/[controller]")]
    public abstract class ServiceController<TService, TService2, TService3> : Controller
    {
        protected ILogger Logger { get; }

        protected ServiceController(ILoggerFactory loggerFactory, TService service, TService2 service2, TService3 service3)
        {
            Service = service;
            Service2 = service2;
            Service3 = service3;
            Logger = loggerFactory.CreateLogger(this.GetType());
        }

        public TService Service { get; }
        public TService2 Service2 { get; }
        public TService3 Service3 { get; }
    }
}
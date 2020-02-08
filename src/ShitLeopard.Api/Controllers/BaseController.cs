using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController : Controller
    {
        protected ILogger<BaseController> Logger { get; }
        protected ShitLeopardContext Context { get; }

        protected BaseController(ILogger<BaseController> logger, ShitLeopardContext shitLeopardContext)
        {
            Logger = logger;
            Context = shitLeopardContext;
        }
    }
}
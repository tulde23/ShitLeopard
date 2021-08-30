using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Controllers
{
    public class ShowController : ServiceController<IShowService>
    {
        public ShowController(ILoggerFactory loggerFactory, IShowService service) : base(loggerFactory, service)
        {
        }

        //    [InboundRequestInspectorFilter]
        [HttpGet]
        public async Task<IEnumerable<ShowModel>> Shows()
        {
            return await Service.GetShowsAsync();
        }
    }
}


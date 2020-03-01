using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Controllers
{
    public class SiteMetricsController : ServiceController<IRequestProfileService>
    {
        public SiteMetricsController(ILoggerFactory loggerFactory, IRequestProfileService service) : base(loggerFactory, service)
        {
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(PagedResult<SiteMetricsModel>))]
        public async Task<PagedResult<SiteMetricsModel>> Search([FromBody]RequestProfileSearchCommand command)
        {
            return await Service.SearchAsync(command);
        }
    }
}
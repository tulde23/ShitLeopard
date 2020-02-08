using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Controllers
{
    public class CharacterController : BaseController
    {
        public CharacterController(ILogger<BaseController> logger, ShitLeopardContext shitLeopardContext) : base(logger, shitLeopardContext)
        {
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(List<Character>))]
        public async Task<IEnumerable<Character>> GetCharacters()
        {
            return await Context.Character.AsNoTracking().ToListAsync();
        }
    }
}
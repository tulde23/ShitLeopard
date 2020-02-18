using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Controllers
{
    public class CharacterController : ServiceController<ICharacterService>
    {
        public CharacterController(ILoggerFactory loggerFactory, ICharacterService service) : base(loggerFactory, service)
        {
        }

        /// <summary>
        /// Retrieves all Episodes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(List<CharacterModel>))]
        public async Task<IEnumerable<CharacterModel>> GetCharacters()
        {
            return await Service.GetCharactersAsync();
        }

  
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShitLeopard.Api.Models;

namespace ShitLeopard.Api.Contracts
{
    public interface ICharacterService
    {
        Task<IEnumerable<CharacterModel>> GetCharactersAsync();
    }
}

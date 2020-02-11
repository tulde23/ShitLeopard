using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Models;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Services
{
    public class CharacterService : BaseService, ICharacterService
    {
        public CharacterService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper) : base(loggerFactory, contextProvider, mapper)
        {
        }

        public async Task<IEnumerable<CharacterModel>> GetCharactersAsync()
        {
            using (var context = ContextProvider())
            {
                return Mapper.MapCollection<CharacterModel, Character>(await context.Character.AsNoTracking().ToListAsync());
            }
        }
    }
}
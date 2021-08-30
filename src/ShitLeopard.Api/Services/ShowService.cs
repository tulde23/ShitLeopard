using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShitLeopard.Common.Contracts;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api.Services
{
    public class ShowService : IShowService
    {
        public Task<ShowModel> GetShowAsync(string showId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShowModel>> GetShowsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
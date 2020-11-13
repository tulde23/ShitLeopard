using AutoMapper;
using Microsoft.Extensions.Logging;
using ShitLeopard.Common.Contracts;

namespace ShitLeopard.Api.Services
{
    public class EntityContext : IEntityContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public EntityContext(ILoggerFactory loggerFactory, IMapper mapper)
        {
            _loggerFactory = loggerFactory;
            Mapper = mapper;
        }

        public ILogger Logger { get; }
        public IMapper Mapper { get; }

        public ILogger<T> GetLogger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }
    }
}
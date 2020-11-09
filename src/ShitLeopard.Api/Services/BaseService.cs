using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ShitLeopard.Common.Contracts;

namespace ShitLeopard.Api.Services
{
    public abstract class BaseService
    {
        protected BaseService(ILoggerFactory loggerFactory,IMongoProvider contextProvider, IMapper mapper)
        {
            Logger = loggerFactory.CreateLogger(this.GetType());
            ContextProvider = contextProvider;
            Mapper = mapper;
        }

        public ILogger Logger { get; }
        public IMongoProvider ContextProvider { get; }
        public IMapper Mapper { get; }
    }
}
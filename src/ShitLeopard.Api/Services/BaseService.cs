using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api.Services
{
    public abstract class BaseService
    {
        protected BaseService(ILoggerFactory loggerFactory, Func<ShitLeopardContext> contextProvider, IMapper mapper)
        {
            Logger = loggerFactory.CreateLogger(this.GetType());
            ContextProvider = contextProvider;
            Mapper = mapper;
        }

        public ILogger Logger { get; }
        public Func<ShitLeopardContext> ContextProvider { get; }
        public IMapper Mapper { get; }
    }
}
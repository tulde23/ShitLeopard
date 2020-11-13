using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ShitLeopard.Common.Contracts
{
    public interface IEntityContext
    {
        IMapper Mapper { get; }

        ILogger<T> GetLogger<T>();
    }
}
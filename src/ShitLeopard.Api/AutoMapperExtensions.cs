using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace ShitLeopard.Api
{
    public static class AutoMapperExtensions
    {
        public static IEnumerable<TTarget> MapCollection<TTarget, TSource>(this IMapper mapper, IEnumerable<TSource> source)
        {
            return mapper.Map<List<TSource>, List<TTarget>>(source.ToList());
        }
    }
}
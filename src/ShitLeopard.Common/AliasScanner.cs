using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ShitLeopard.Common
{
    public static class AliasScanner
    {
        public static IEnumerable<AliasAttribute> FindAttributes(params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(x => x.GetTypes());
            foreach (var type in types)
            {
                var a = type.GetCustomAttributes(typeof(AliasAttribute), true).Select(x => (AliasAttribute)x).FirstOrDefault();
                if (a != null)
                {
                    yield return a;
                }
            }
        }

        public static Type GetTypeFromAlias(string alias, params Assembly[] assemblies)
        {
            return FindAttributes(assemblies).Where(x => x.HasAlias(alias)).Select(x => x.Type).FirstOrDefault();
        }
    }
}
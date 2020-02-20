using System;
using System.Collections.Generic;
using System.Linq;

namespace ShitLeopard.Common
{
    public class AliasAttribute : Attribute
    {
        public IReadOnlyCollection<string> Aliases { get; }

        public bool HasAlias(string alias)
        {
            return Aliases.Select(x => x.ToLower()).Contains(alias.ToLower());
        }

        public string Name { get; }
        public Type Type { get; }

        public AliasAttribute(Type type, params string[] aliases)
        {
            Aliases = aliases.ToList();
            Name = type.Name;
            Type = type;
        }
    }
}
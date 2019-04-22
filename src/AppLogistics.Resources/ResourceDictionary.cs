using System;
using System.Collections.Concurrent;

namespace AppLogistics.Resources
{
    internal class ResourceDictionary : ConcurrentDictionary<string, string>
    {
        public ResourceDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}

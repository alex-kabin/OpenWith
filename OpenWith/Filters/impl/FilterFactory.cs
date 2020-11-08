using System;
using System.Collections.Generic;

namespace OpenWith.Filters.impl
{
    public class FilterFactory : IFilterFactory
    {
        private readonly Dictionary<string, Func<object, IFilter>> _registry = new Dictionary<string, Func<object, IFilter>>();
        
        public FilterFactory WithFilter(string key, Func<object, IFilter> builder) {
            _registry[key] = builder;
            return this;
        }

        public IFilter GetFilter(string key, object config) {
            var builder = _registry[key];
            try {
                return builder?.Invoke(config);
            }
            catch (Exception ex) {
                throw new ApplicationException($"Failed building filter '{key}'", ex);
            }
        }
    }
}

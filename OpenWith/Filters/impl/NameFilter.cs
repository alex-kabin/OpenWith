using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace OpenWith.Filters.impl
{
    public class NameFilter : IFilter
    {
        public static NameFilter Build(object config) {
            if (config is IEnumerable<object> list) {
                return new NameFilter(list.Where(s => s != null).Select(s => s.ToString()));
            }
            if (config != null) {
                return new NameFilter(config.ToString());
            }
            return null;
        }

        private readonly ISet<string> _set;
        private readonly Regex _regex;

        public NameFilter(string expr) {
            if (expr == null) {
                throw new ArgumentNullException(nameof(expr));
            }
            _set = null;
            _regex = new Regex($"^({expr})$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        public NameFilter(IEnumerable<string> list) {
            _regex = null;
            _set = new HashSet<string>(list.Select(s => s.ToLowerInvariant()));
        }

        public bool Accepts(string path) {
            if (path == null) {
                throw new ArgumentNullException(nameof(path));
            }
            if (string.IsNullOrEmpty(path)) {
                return false;
            }

            var ext = Path.GetFileNameWithoutExtension(path);
            if (_set != null) {
                return _set.Contains(ext.ToLowerInvariant());
            }
            if (_regex != null) {
                return _regex.IsMatch(ext);
            }
            return false;
        }
    }
}

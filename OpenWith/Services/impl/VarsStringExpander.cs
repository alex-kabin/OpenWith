using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWith.Services.impl
{
    public class VarsStringExpander : IStringExpander
    {
        private const char MARKER = '%';
        
        private readonly IReadOnlyDictionary<string, string> _vars;

        public VarsStringExpander(IReadOnlyDictionary<string, string> vars) {
            _vars = vars;
        }

        public string Expand(string template) {
            if (string.IsNullOrEmpty(template) || template.IndexOf(MARKER) < 0) {
                return template;
            }

            if (_vars != null && _vars.Count > 0) {
                StringBuilder sb = new StringBuilder(template);
                foreach ((string key, string value) in _vars) {
                    sb.Replace(MARKER + key + MARKER, value);
                }
                template = sb.ToString();
            }

            if (template.IndexOf(MARKER) >= 0) {
                template = Environment.ExpandEnvironmentVariables(template);
            }

            return template;
        }
    }
}

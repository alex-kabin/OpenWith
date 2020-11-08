using System;
using System.Collections.Generic;

namespace OpenWith.Model
{
    public class Mapping
    {
        public int Priority { get; set; } = 0;
        public Dictionary<string, object> Filters { get; set; }
        public string Name { get; set; }
        public string Command { get; set; }
        public string Args { get; set; } = "\"%1\"";
        public bool Admin { get; set; } = false;
        public string Icon { get; set; }
    }
}
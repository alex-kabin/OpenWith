using System;
using System.Collections.Generic;

namespace OpenWith.Model
{
    public class Config
    {
        public int AutoCloseTimeout { get; set; } = 10;
        public double Opacity { get; set; } = 1.0;
        public string HeaderTemplate { get; set; } = "Open {0} with:";
        public Theme Theme { get; set; }
        public Dictionary<string, string> Vars { get; set; }
        public List<Mapping> Mappings { get; set; }
    }
}

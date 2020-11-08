using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenWith.Services.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWith.Services.impl.Tests
{
    [TestClass()]
    public class VarsStringExpanderTests
    {
        [TestMethod()]
        public void ExpandTest()
        {
            var se = new VarsStringExpander(new Dictionary<string, string>() {
                { "A", "VAR-A" },
                { "B", "VAR-B" },
            });
            var res = se.Expand("ab%A%cd%B%ef%C%");
            Assert.AreEqual("abVAR-AcdVAR-Bef%C%", res);
        }
    }
}
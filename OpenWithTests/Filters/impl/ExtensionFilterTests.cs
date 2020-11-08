using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenWith.Filters.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWith.Filters.impl.Tests
{
    [TestClass()]
    public class ExtensionFilterTests
    {
        [TestMethod()]
        public void BuildTest1()
        {
            ExtensionFilter ef = ExtensionFilter.Build(new List<string>() {"txt", "doc", "avi"});
            Assert.IsTrue(ef.Accepts("test.txt"));
            Assert.IsTrue(ef.Accepts("test.doc"));
            Assert.IsFalse(ef.Accepts("test.mkv"));
        }

        [TestMethod()]
        public void BuildTest2()
        {
            ExtensionFilter ef = ExtensionFilter.Build("docx?");
            Assert.IsTrue(ef.Accepts("test.doc"));
            Assert.IsTrue(ef.Accepts("test.docx"));
            Assert.IsFalse(ef.Accepts("test.mkv"));
        }
    }
}
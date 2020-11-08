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
    public class SizeFilterTests
    {
        [TestMethod()]
        public void BuildTest1() {
            SizeFilter sf = SizeFilter.Build(new Dictionary<object, object>() { { "min", "100" }, { "max", "200" } });
            Assert.AreEqual(100, sf.MinSize);
            Assert.AreEqual(200, sf.MaxSize);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BuildTest2()
        {
            SizeFilter.Build(new Dictionary<object, object>() { { "min", "100k" }, { "max", "200" } });
        }

        [TestMethod()]
        public void BuildTest3()
        {
            SizeFilter sf = SizeFilter.Build(new Dictionary<object, object>() { { "min", "100k" }, { "max", "200m" } });
            Assert.AreEqual(100 * 1024, sf.MinSize);
            Assert.AreEqual(200 * 1024 * 1024, sf.MaxSize);
        }

        [TestMethod()]
        public void BuildTest4()
        {
            SizeFilter sf = SizeFilter.Build("200k:500m");
            Assert.AreEqual(200 * 1024, sf.MinSize);
            Assert.AreEqual(500 * 1024 * 1024, sf.MaxSize);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void BuildTest5()
        {
           SizeFilter.Build("200k:notnumk");
        }
    }
}
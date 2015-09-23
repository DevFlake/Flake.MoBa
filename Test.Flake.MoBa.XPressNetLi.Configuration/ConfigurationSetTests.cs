using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flake.MoBa.XPressNetLi.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flake.MoBa.XPressNetLi.Configuration.Tests
{
    [TestClass()]
    public class ConfigurationSetTests
    {
        [TestMethod()]
        public void ConfigurationSetTest()
        {
            var test = new ConfigData();
            test.TimeoutForLIResponse_s = 10;

            var x = new ConfigurationSet(test);
            Assert.AreEqual(test.GetHashCode(), x.Data.GetHashCode());
        }
    }
}
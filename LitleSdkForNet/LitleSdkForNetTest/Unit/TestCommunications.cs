using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestClass]
    public class TestCommunications
    {
        private Communications objectUnderTest;

        [TestInitialize]
        public void SetUpLitle()
        {
            objectUnderTest = new Communications();
        }

        [TestMethod]
        public void TestSettingProxyPropertiesToNullShouldTurnOffProxy()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("proxyHost", null);
            config.Add("proxyPort", null);

            Assert.IsFalse(objectUnderTest.isProxyOn(config));
        }


    }
}

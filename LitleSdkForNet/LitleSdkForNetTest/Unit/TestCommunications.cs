using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestCommunications
    {
        private Communications objectUnderTest;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            objectUnderTest = new Communications();
        }

        [Test]
        public void TestSettingProxyPropertiesToNullShouldTurnOffProxy()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("proxyHost", null);
            config.Add("proxyPort", null);

            Assert.IsFalse(objectUnderTest.isProxyOn(config));
        }


    }
}

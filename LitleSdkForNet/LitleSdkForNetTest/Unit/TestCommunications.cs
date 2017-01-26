using System.Collections.Generic;
using NUnit.Framework;



namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestCommunications
    {
        private Communications _objectUnderTest;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _objectUnderTest = new Communications();
        }

        [Test]
        public void TestSettingProxyPropertiesToNullShouldTurnOffProxy()
        {
            var config = new Dictionary<string, string> {{"proxyHost", null}, {"proxyPort", null}};

            Assert.IsFalse(_objectUnderTest.IsProxyOn(config));
        }


    }
}

using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestCommunications
    {
        private Communications objectUnderTest;
        private IDictionary<string, StringBuilder> memoryStreams;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            memoryStreams = new Dictionary<string, StringBuilder>();
            objectUnderTest = new Communications(memoryStreams);
        }

        [Test]
        public void TestSettingProxyPropertiesToNullShouldTurnOffProxy()
        {
            var config = new Dictionary<string, string>();
            config.Add("proxyHost", null);
            config.Add("proxyPort", null);

            Assert.IsFalse(objectUnderTest.isProxyOn(config));
        }
    }
}

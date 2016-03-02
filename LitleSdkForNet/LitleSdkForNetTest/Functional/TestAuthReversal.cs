using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestAuthReversal
    {
        private LitleOnline litle;
        private IDictionary<string, StringBuilder> _memoryCache;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _memoryCache = new Dictionary<string, StringBuilder>();
            var config = new Dictionary<string, string>();
            config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "5000");
            config.Add("merchantId", "101");
            config.Add("password", "TESTCASE");
            config.Add("printxml", "true");
            config.Add("proxyHost", Settings.Default.proxyHost);
            config.Add("proxyPort", Settings.Default.proxyPort);
            config.Add("logFile", Settings.Default.logFile);
            config.Add("neuterAccountNums", "true");
            litle = new LitleOnline(_memoryCache, config);
        }

        [Test]
        public void SimpleAuthReversal()
        {
            var reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            var response = litle.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void testAuthReversalHandleSpecialCharacters()
        {
            var reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "<'&\">";

            var response = litle.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

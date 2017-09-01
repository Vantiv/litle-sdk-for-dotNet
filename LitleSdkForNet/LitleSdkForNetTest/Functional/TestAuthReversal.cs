using System.Collections.Generic;
using NUnit.Framework;


namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestAuthReversal
    {
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            var config = new Dictionary<string, string>
            {
                {"url", "https://www.testvantivcnp.com/sandbox/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "8.13"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };
            _litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleAuthReversal()
        {
            var reversal = new authReversal
            {
                litleTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "Notes"
            };

            var response = _litle.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
            
        [Test]
        public void TestAuthReversalHandleSpecialCharacters()
        {
            var reversal = new authReversal
            {
                litleTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "<'&\">"
            };

            var response = _litle.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

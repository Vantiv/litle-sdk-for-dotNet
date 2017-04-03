using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestAuthReversal
    {
        private LitleOnline _litle;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _config = new Dictionary<string, string>
            {
                {"url", "https://www.testlitle.com/sandbox/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "11.0"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _litle = new LitleOnline(_config);
        }

        [Test]
        public void SimpleAuthReversal()
        {
            var reversal = new authReversal
            {
                id = "1",
                reportGroup = "Planets",
                litleTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "Notes"
            };

            var response = _litle.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void testAuthReversalHandleSpecialCharacters()
        {
            var reversal = new authReversal
            {
                id = "1",
                reportGroup = "Planets",
                litleTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "<'&\">"
            };


            var response = _litle.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

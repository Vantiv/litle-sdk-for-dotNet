using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestPhysicalCheck
    {
        private LitleOnline _litle;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
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
        public void PhysicalCheckCredit()
        {
            var physicalCheckCredit = new physicalCheckCredit
            {
                // attributes.
                id = "1",
                reportGroup = "Default Report Group",
                // required child elements.
                amount = 1000,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _litle.PhysicalCheckCreditResponse(physicalCheckCredit);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void PhysicalCheckDebit()
        {
            var physicalCheckDebit = new physicalCheckDebit
            {
                // attributes.
                id = "1",
                reportGroup = "Planets",
                // required child elements.
                amount = 1000,
                fundingSubmerchantId = "value for fundingSubmerchantId",
                fundsTransferId = "value for fundsTransferId"
            };

            var response = _litle.PhysicalCheckDebitResponse(physicalCheckDebit);
            Assert.AreEqual("000", response.response);
        }
    }
}

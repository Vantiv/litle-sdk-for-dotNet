using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litle.Sdk.Test.Functional
{
    internal class TestRegisterTokenRequest
    {
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            var config = new Dictionary<string, string>
            {
                {"url", "https://www.testlitle.com/sandbox/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "9.10"},
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
        public void TestSimpleRegisterTokenRequest()
        {
            var tokenRequest = new registerTokenRequestType
            {
                orderId = "androidpay",
                accountNumber = "4100000000000001",
                paypageRegistrationId = "558987412"
            };

            var tokenResponse = _litle.RegisterToken(tokenRequest);
            Assert.AreEqual("01", tokenResponse.androidpayResponse.expMonth);
            Assert.AreEqual("2050", tokenResponse.androidpayResponse.expYear);
            Assert.NotNull(tokenResponse.androidpayResponse.cryptogram);
        }
    }
}

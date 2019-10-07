using System.Collections.Generic;
using System.Net;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional {
    [TestFixture]
    internal class TestTimeout
    {
        private LitleOnline _cnp;
        private Dictionary<string, string> _config;
        
        [Test]
        public void TestTimeoutNotDefined()
        {
            _config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "11.0"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _cnp = new LitleOnline(_config);
            
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                accountNumber = "1233456789103801",
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
        }

        [Test]
        public void TestTimeoutNotParsable()
        {
            _config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "11.0"},
                {"timeout", "notparsableasint"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _cnp = new LitleOnline(_config);
            
            var registerTokenRequest = new registerTokenRequestType
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                accountNumber = "1233456789103801",
            };

            var rtokenResponse = _cnp.RegisterToken(registerTokenRequest);
            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
        }

        [Test]
        public void TestTimeoutReached() {
            _config = new Dictionary<string, string> {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "11.0"},
                {"timeout", "0"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _cnp = new LitleOnline(_config);

            var registerTokenRequest = new registerTokenRequestType {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                accountNumber = "1233456789103801",
            };

            Assert.Throws<WebException>(() => { _cnp.RegisterToken(registerTokenRequest); });
        }
    }
}
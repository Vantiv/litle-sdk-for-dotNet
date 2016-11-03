using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckRedeposit
    {
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var config = new Dictionary<string, string>
            {
                {"url", "https://www.testlitle.com/sandbox/communicator/online"},
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
        public void SimpleEcheckRedeposit() {
            var echeckredeposit = new echeckRedeposit {litleTxnId = 123456};
            var response = _litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void EcheckRedepositWithEcheck() {
            var echeckredeposit = new echeckRedeposit
            {
                litleTxnId = 123456,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                }
            };
            
            var response = _litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void EcheckRedepositWithEcheckToken() {
            var echeckredeposit = new echeckRedeposit
            {
                litleTxnId = 123456,
                token = new echeckTokenType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    litleToken = "1234565789012",
                    routingNum = "123456789",
                    checkNum = "123455"
                }
            };
            
            var response = _litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }
            
    }
}

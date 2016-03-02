using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckRedeposit
    {
        private LitleOnline litle;
        private IDictionary<string, StringBuilder> _memoryCache;

        [TestFixtureSetUp]
        public void setUp()
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
        public void simpleEcheckRedeposit()
        {
            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            var response = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void echeckRedepositWithEcheck()
        {
            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";

            echeckredeposit.echeck = echeck;
            var response = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void echeckRedepositWithEcheckToken()
        {
            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            var echeckToken = new echeckTokenType();
            echeckToken.accType = echeckAccountTypeEnum.Checking;
            echeckToken.litleToken = "1234565789012";
            echeckToken.routingNum = "123456789";
            echeckToken.checkNum = "123455";

            echeckredeposit.token = echeckToken;
            var response = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

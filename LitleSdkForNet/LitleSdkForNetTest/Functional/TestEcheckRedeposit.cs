using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestEcheckRedeposit
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void setUp()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "65");
            config.Add("merchantId", "101");
            config.Add("password", "TESTCASE");
            config.Add("printxml", "true");
            config.Add("proxyHost", Properties.Settings.Default.proxyHost);
            config.Add("proxyPort", Properties.Settings.Default.proxyPort);
            config.Add("logFile", Properties.Settings.Default.logFile);
            config.Add("neuterAccountNums", "true");
            litle = new LitleOnline(config);
        }


        [Test]
        public void simpleEcheckRedeposit() {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.id = "1";
            echeckredeposit.reportGroup = "Planets";
            echeckredeposit.litleTxnId = 123456;
            echeckRedepositResponse response = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void echeckRedepositWithEcheck() {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.id = "1";
            echeckredeposit.reportGroup = "Planets";
            echeckredeposit.litleTxnId = 123456;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";

            echeckredeposit.echeck = echeck;
            echeckRedepositResponse response = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void echeckRedepositWithEcheckToken() {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.id = "1";
            echeckredeposit.reportGroup = "Planets";
            echeckredeposit.litleTxnId = 123456;
            echeckTokenType echeckToken = new echeckTokenType();
            echeckToken.accType = echeckAccountTypeEnum.Checking;
            echeckToken.litleToken = "1234565789012";
            echeckToken.routingNum = "123456789";
            echeckToken.checkNum = "123455";

            echeckredeposit.token = echeckToken;
            echeckRedepositResponse response = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }
            
    }
}

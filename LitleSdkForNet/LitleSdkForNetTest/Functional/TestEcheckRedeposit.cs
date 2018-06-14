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

        private Dictionary<string, string> config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "10.0"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };
            litle = new LitleOnline(config);
        }


        [Test]
        public void simpleEcheckRedeposit() {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.id = "1";
            echeckredeposit.litleTxnId = 123456;
            echeckRedepositResponse response = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Transaction Received", response.message);
        }

        [Test]
        public void echeckRedepositWithEcheck() {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.id = "1";
            echeckredeposit.litleTxnId = 123456;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";

            echeckredeposit.echeck = echeck;
            echeckRedepositResponse response = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Transaction Received", response.message);
        }

        [Test]
        public void echeckRedepositWithEcheckToken() {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.id = "1";
            echeckredeposit.litleTxnId = 123456;
            echeckTokenType echeckToken = new echeckTokenType();
            echeckToken.accType = echeckAccountTypeEnum.Checking;
            echeckToken.litleToken = "1234565789012";
            echeckToken.routingNum = "123456789";
            echeckToken.checkNum = "123455";

            echeckredeposit.token = echeckToken;
            echeckRedepositResponse response = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Transaction Received", response.message);
        }
            
    }
}

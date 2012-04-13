using System;
using System.Collections.Generic;
using System.Linq;
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
            litle = new LitleOnline();
        }


        [Test]
        public void simpleEcheckRedeposit() {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckRedepositResponse response = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void echeckRedepositWithEcheck() {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
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

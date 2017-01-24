using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestGiftCardAuthReversal
    {
        private LitleOnline litle;
        private Dictionary<string, string> config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            config = new Dictionary<string, string>();
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
        public void TestTransaction()
        {
            giftCardAuthReversal giftCard = new giftCardAuthReversal();
            giftCard.id = "1";
            giftCard.reportGroup = "Planets";
            giftCard.litleTxnId = 123;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            giftCard.card = card;
            giftCard.originalRefCode = "abc123";
            giftCard.originalAmount = 500;
            giftCard.originalTxnTime = DateTime.Now;
            giftCard.originalSystemTraceId = 123;
            giftCard.originalSequenceNumber = "123456";

            giftCardAuthReversalResponse response = litle.GiftCardAuthReversal(giftCard);
            Assert.AreEqual("000", response.response);
        }
    }
}

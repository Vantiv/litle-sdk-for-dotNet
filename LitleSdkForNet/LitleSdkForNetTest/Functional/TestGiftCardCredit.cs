using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestGiftCardCredit
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
        public void SimpleCreditTxnId()
        {
            giftCardCredit creditObj = new giftCardCredit();
            creditObj.id = "1";
            creditObj.reportGroup = "planets";
            creditObj.litleTxnId = 123456000;
            creditObj.creditAmount = 106;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "4100000000000000";
            card.expDate = "1210";
            creditObj.card = card;

            giftCardCreditResponse response = litle.GiftCardCredit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditOrderId()
        {
            giftCardCredit creditObj = new giftCardCredit();
            creditObj.id = "1";
            creditObj.reportGroup = "planets";
            creditObj.creditAmount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.echeckppd;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "4100000000000000";
            card.expDate = "1210";
            creditObj.card = card;

            giftCardCreditResponse response = litle.GiftCardCredit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

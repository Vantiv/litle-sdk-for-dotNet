using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestGiftCardCapture
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
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
        public void simpleGiftCardCapture()
        {
            giftCardCapture giftCardCapture = new giftCardCapture();
            giftCardCapture.id = "1";
            giftCardCapture.reportGroup = "Planets";
            giftCardCapture.litleTxnId = 123456000;
            giftCardCapture.captureAmount = 106;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            giftCardCapture.card = card;
            giftCardCapture.originalRefCode = "abc123";
            giftCardCapture.originalAmount = 43534345;
            giftCardCapture.originalTxnTime = DateTime.Now;


            giftCardCaptureResponse response = litle.GiftCardCapture(giftCardCapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleGiftCardWithPartial()
        {
            giftCardCapture giftCardCapture = new giftCardCapture();
            giftCardCapture.id = "1";
            giftCardCapture.litleTxnId = 123456000;
            giftCardCapture.captureAmount = 106;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            giftCardCapture.card = card;
            giftCardCapture.originalRefCode = "abc123";
            giftCardCapture.partial = true;

            giftCardCaptureResponse response = litle.GiftCardCapture(giftCardCapture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestGiftCardParentReversal
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
        public void DepositReversal()
        {
            depositReversal reversal = new depositReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.litleTxnId = 123456000;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;
            reversal.originalRefCode = "123";
            reversal.originalAmount = 123;
            reversal.originalTxnTime = DateTime.Now;
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            depositReversalResponse response = litle.DepositReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void RefundReversal()
        {
            refundReversal reversal = new refundReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.litleTxnId = 123456000;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;
            reversal.originalRefCode = "123";
            reversal.originalAmount = 123;
            reversal.originalTxnTime = DateTime.Now;
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            refundReversalResponse response = litle.RefundReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ActivateReversal()
        {
            activateReversal reversal = new activateReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.litleTxnId = 123456000;
            reversal.virtualGiftCardBin = "123";
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;            
            reversal.originalRefCode = "123";
            reversal.originalAmount = 123;
            reversal.originalTxnTime = DateTime.Now;
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            activateReversalResponse response = litle.ActivateReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void DeactivateReversal()
        {
            deactivateReversal reversal = new deactivateReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.litleTxnId = 123456000;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;            
            reversal.originalRefCode = "123";
            reversal.originalTxnTime = DateTime.Now;
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            deactivateReversalResponse response = litle.DeactivateReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void LoadReversal()
        {
            loadReversal reversal = new loadReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.litleTxnId = 123456000;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;
            reversal.originalRefCode = "123";
            reversal.originalAmount = 123;
            reversal.originalTxnTime = DateTime.Now;
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            loadReversalResponse response = litle.LoadReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void UnloadReversal()
        {
            unloadReversal reversal = new unloadReversal();
            reversal.id = "1";
            reversal.reportGroup = "planets";
            reversal.litleTxnId = 123456000;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            reversal.card = card;
            reversal.originalRefCode = "123";
            reversal.originalAmount = 123;
            reversal.originalTxnTime = DateTime.Now;
            reversal.originalSystemTraceId = 123;
            reversal.originalSequenceNumber = "123456";

            unloadReversalResponse response = litle.UnloadReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

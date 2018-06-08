using System.Collections.Generic;
using NUnit.Framework;
using System;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestGiftCardParentReversal
    {
        private LitleOnline _litle;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "11.0"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _litle = new LitleOnline(_config);
        }

        [Test]
        public void DepositReversal()
        {
            var reversal = new depositReversal
            {
                id = "1",
                reportGroup = "planets",
                litleTxnId = 123456000,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"
                },


                originalRefCode = "123",
                originalAmount = 123,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _litle.DepositReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void RefundReversal()
        {
            var reversal = new refundReversal
            {
                id = "1",
                reportGroup = "planets",
                litleTxnId = 123456000,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"
                },
                originalRefCode = "123",
                originalAmount = 123,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _litle.RefundReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ActivateReversal()
        {
            var reversal = new activateReversal
            {
                id = "1",
                reportGroup = "planets",
                litleTxnId = 123456000,
                virtualGiftCardBin = "123",
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"
                },
                originalRefCode = "123",
                originalAmount = 123,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _litle.ActivateReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void DeactivateReversal()
        {
            var reversal = new deactivateReversal
            {
                id = "1",
                reportGroup = "planets",
                litleTxnId = 123456000,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"
                },


                originalRefCode = "123",
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _litle.DeactivateReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void LoadReversal()
        {
            var reversal = new loadReversal
            {
                id = "1",
                reportGroup = "planets",
                litleTxnId = 123456000,
                card = new giftCardCardType
                {

                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"
                },

                originalRefCode = "123",
                originalAmount = 123,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _litle.LoadReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void UnloadReversal()
        {
            var reversal = new unloadReversal
            {
                id = "1",
                reportGroup = "planets",
                litleTxnId = 123456000,
                card = new giftCardCardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234"

                },

                originalRefCode = "123",
                originalAmount = 123,
                originalTxnTime = DateTime.Now,
                originalSystemTraceId = 123,
                originalSequenceNumber = "123456"
            };

            var response = _litle.UnloadReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

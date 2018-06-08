using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestForceCapture
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void setUp()
        {
            Dictionary<string, string> config = new Dictionary<string, string>
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
            litle = new LitleOnline(config);
        }

        [Test]
        public void simpleForceCaptureWithCard() {
            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            forcecapture.card = card;
            forceCaptureResponse response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleForceCaptureWithMpos()
        {
            mposType mpos = new mposType();
            mpos.ksn = "77853211300008E00016";
            mpos.encryptedTrack = "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD";
            mpos.formatId = "30";
            mpos.track1Status = 0;
            mpos.track2Status = 0;
            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 322;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.mpos = mpos;
            forceCaptureResponse response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleForceCaptureWithToken() {
            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardTokenType token = new cardTokenType();
            token.litleToken = "123456789101112";
            token.expDate = "1210";
            token.cardValidationNum = "555";
            token.type = methodOfPaymentTypeEnum.VI;
            forcecapture.token = token;
            forceCaptureResponse response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message); ;
        }

        [Test]
        public void simpleForceCaptureWithSecondaryAmount()
        {
            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.secondaryAmount = 50;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            forcecapture.card = card;
            forceCaptureResponse response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void simpleForceCaptureWithCardAccountFunding()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingTypeEnum.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void simpleForceCaptureWithCardInitialRecurring()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingTypeEnum.initialRecurring,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void simpleForceCaptureWithCardInitialInstallment()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingTypeEnum.initialInstallment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void simpleForceCaptureWithCardInitialCOF()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingTypeEnum.initialCOF,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void simpleForceCaptureWithCardMerchantInitiatedCOF()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingTypeEnum.merchantInitiatedCOF,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
        
        [Test]
        public void simpleForceCaptureWithCardCardholderInitiatedCOF()
        {
            var forcecapture = new forceCapture
            {
                id = "1",
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingTypeEnum.cardholderInitiatedCOF,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

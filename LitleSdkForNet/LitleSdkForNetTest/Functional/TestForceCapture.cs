using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestForceCapture
    {
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var config = new Dictionary<string, string>
            {
                { "url", "https://www.testlitle.com/sandbox/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "8.13"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleForceCaptureWithCard() {
            var forcecapture = new forceCapture
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };
            
            var response = _litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleForceCaptureWithMpos()
        {
            var forcecapture = new forceCapture
            {
                amount = 322,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                mpos = new mposType
                {
                    ksn = "77853211300008E00016",
                    encryptedTrack =
                    "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD",
                    formatId = "30",
                    track1Status = 0,
                    track2Status = 0
                }
            };

            var response = _litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleForceCaptureWithToken() {
            var forcecapture = new forceCapture
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                token = new cardTokenType
                {
                    litleToken = "123456789101112",
                    expDate = "1210",
                    cardValidationNum = "555",
                    type = methodOfPaymentTypeEnum.VI
                }
            };
            
            var response = _litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleForceCaptureWithProcessingType()
        {
            var forcecapture = new forceCapture
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                },
                processingType = processingType.initialRecurring
            };

            var response = _litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

    }
}

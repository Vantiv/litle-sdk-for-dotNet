using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCredit
    {
        private LitleOnline _litle;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _config = new Dictionary<string, string>
            {
                {"url", "https://www.testlitle.com/sandbox/communicator/online"},
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
        public void SimpleCreditWithCard()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = _litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithMpos()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                mpos = new mposType
                {
                    ksn = "77853211300008E00016",
                    encryptedTrack = "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD",
                    formatId = "30",
                    track1Status = 0,
                    track2Status = 0,
                }
            };

            creditResponse response = _litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithPaypal()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "123456",
                orderSource = orderSourceType.ecommerce,
                paypal = new payPal { payerId = "1234" }
            };

            var response = _litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void PaypalNotes()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "123456",
                payPalNotes = "Hello",
                orderSource = orderSourceType.ecommerce,

                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210",
                }
            };

            var response = _litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ProcessingInstructionAndAmexData()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 2000,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingInstructions = new processingInstructions { bypassVelocityCheck = true },
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };

            var response = _litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithCardAndSpecialCharacters()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                amount = 106,
                orderId = "<&'>",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000<>0000001",
                    expDate = "1210"
                }
            };

            var response = _litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithPin()
        {
            var creditObj = new credit
            {
                id = "1",
                reportGroup = "planets",
                litleTxnId = 123456000,
                pin = "1234",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1210"
                }
            };

            var response = _litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

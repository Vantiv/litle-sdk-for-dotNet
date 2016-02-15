using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCredit
    {
        private LitleOnline litle;
        private IDictionary<string, StringBuilder> _memoryCache;

        [TestFixtureSetUp]
        public void setUp()
        {
            _memoryCache = new Dictionary<string, StringBuilder>();
            var config = new Dictionary<string, string>();
            config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "5000");
            config.Add("merchantId", "101");
            config.Add("password", "TESTCASE");
            config.Add("printxml", "true");
            config.Add("proxyHost", Settings.Default.proxyHost);
            config.Add("proxyPort", Settings.Default.proxyPort);
            config.Add("logFile", Settings.Default.logFile);
            config.Add("neuterAccountNums", "true");
            litle = new LitleOnline(_memoryCache, config);
        }

        [Test]
        public void SimpleCreditWithCard()
        {
            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";

            creditObj.card = card;

            var response = litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithMpos()
        {
            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            var mpos = new mposType();
            mpos.ksn = "77853211300008E00016";
            mpos.encryptedTrack =
                "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD";
            mpos.formatId = "30";
            mpos.track1Status = 0;
            mpos.track2Status = 0;
            creditObj.mpos = mpos;

            var response = litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithPaypal()
        {
            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "123456";
            creditObj.orderSource = orderSourceType.ecommerce;
            var payPalObj = new payPal();
            payPalObj.payerId = "1234";

            creditObj.paypal = payPalObj;

            var response = litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void PaypalNotes()
        {
            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "123456";
            creditObj.payPalNotes = "Hello";
            creditObj.orderSource = orderSourceType.ecommerce;

            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";

            creditObj.card = card;

            var response = litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ProcessingInstructionAndAmexData()
        {
            var creditObj = new credit();
            creditObj.amount = 2000;
            creditObj.orderId = "12344";
            creditObj.orderSource = orderSourceType.ecommerce;

            var processingInstructionsObj = new processingInstructions();
            processingInstructionsObj.bypassVelocityCheck = true;

            creditObj.processingInstructions = processingInstructionsObj;

            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";

            creditObj.card = card;

            var response = litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithCardAndSpecialCharacters()
        {
            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "<&'>";
            creditObj.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000<>0000001";
            card.expDate = "1210";

            creditObj.card = card;

            var response = litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Certification
{
    [TestFixture]
    internal class TestCert3AuthReversal
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
            config.Add("logFile", null);
            config.Add("neuterAccountNums", null);
            config.Add("proxyHost", Settings.Default.proxyHost);
            config.Add("proxyPort", Settings.Default.proxyPort);
            litle = new LitleOnline(_memoryCache, config);
        }

        [Test]
        public void test32()
        {
            var auth = new authorization();
            auth.orderId = "32";
            auth.amount = 10010;
            auth.orderSource = orderSourceType.ecommerce;
            var billToAddress = new contact();
            billToAddress.name = "John Smith";
            billToAddress.addressLine1 = "1 Main St.";
            billToAddress.city = "Burlington";
            billToAddress.state = "MA";
            billToAddress.zip = "01803-3747";
            billToAddress.country = countryTypeEnum.US;
            auth.billToAddress = billToAddress;
            var card = new cardType();
            card.number = "4457010000000009";
            card.expDate = "0112";
            card.cardValidationNum = "349";
            card.type = methodOfPaymentTypeEnum.VI;
            auth.card = card;

            var authorizeResponse = litle.Authorize(auth);
            Assert.AreEqual("000", authorizeResponse.response);
            Assert.AreEqual("Approved", authorizeResponse.message);
            Assert.AreEqual("11111 ", authorizeResponse.authCode);
            Assert.AreEqual("01", authorizeResponse.fraudResult.avsResult);
            Assert.AreEqual("M", authorizeResponse.fraudResult.cardValidationResult);

            var capture = new capture();
            capture.litleTxnId = authorizeResponse.litleTxnId;
            capture.amount = 5005;
            var captureResponse = litle.Capture(capture);
            Assert.AreEqual("000", captureResponse.response);
            Assert.AreEqual("Approved", captureResponse.message);

            var reversal = new authReversal();
            reversal.litleTxnId = authorizeResponse.litleTxnId;
            var reversalResponse = litle.AuthReversal(reversal);
            Assert.AreEqual("111", reversalResponse.response);
            Assert.AreEqual("Authorization amount has already been depleted", reversalResponse.message);
        }

        [Test]
        public void test33()
        {
            var auth = new authorization();
            auth.orderId = "33";
            auth.amount = 20020;
            auth.orderSource = orderSourceType.ecommerce;
            var billToAddress = new contact();
            billToAddress.name = "Mike J. Hammer";
            billToAddress.addressLine1 = "2 Main St.";
            billToAddress.addressLine2 = "Apt. 222";
            billToAddress.city = "Riverside";
            billToAddress.state = "RI";
            billToAddress.zip = "02915";
            billToAddress.country = countryTypeEnum.US;
            auth.billToAddress = billToAddress;
            var card = new cardType();
            card.number = "5112010000000003";
            card.expDate = "0212";
            card.cardValidationNum = "261";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;
            var fraud = new fraudCheckType();
            fraud.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            auth.cardholderAuthentication = fraud;

            var authorizeResponse = litle.Authorize(auth);
            Assert.AreEqual("000", authorizeResponse.response);
            Assert.AreEqual("Approved", authorizeResponse.message);
            Assert.AreEqual("22222", authorizeResponse.authCode);
            Assert.AreEqual("10", authorizeResponse.fraudResult.avsResult);
            Assert.AreEqual("M", authorizeResponse.fraudResult.cardValidationResult);

            var reversal = new authReversal();
            reversal.litleTxnId = authorizeResponse.litleTxnId;
            var reversalResponse = litle.AuthReversal(reversal);
            Assert.AreEqual("000", reversalResponse.response);
            Assert.AreEqual("Approved", reversalResponse.message);
        }

        [Test]
        public void test34()
        {
            var auth = new authorization();
            auth.orderId = "34";
            auth.amount = 30030;
            auth.orderSource = orderSourceType.ecommerce;
            var billToAddress = new contact();
            billToAddress.name = "Eileen Jones";
            billToAddress.addressLine1 = "3 Main St.";
            billToAddress.city = "Bloomfield";
            billToAddress.state = "CT";
            billToAddress.zip = "06002";
            billToAddress.country = countryTypeEnum.US;
            auth.billToAddress = billToAddress;
            var card = new cardType();
            card.number = "6011010000000003";
            card.expDate = "0312";
            card.cardValidationNum = "758";
            card.type = methodOfPaymentTypeEnum.DI;
            auth.card = card;

            var authorizeResponse = litle.Authorize(auth);
            Assert.AreEqual("000", authorizeResponse.response);
            Assert.AreEqual("Approved", authorizeResponse.message);
            Assert.AreEqual("33333", authorizeResponse.authCode);
            Assert.AreEqual("10", authorizeResponse.fraudResult.avsResult);
            Assert.AreEqual("M", authorizeResponse.fraudResult.cardValidationResult);

            var reversal = new authReversal();
            reversal.litleTxnId = authorizeResponse.litleTxnId;
            var reversalResponse = litle.AuthReversal(reversal);
            Assert.AreEqual("000", reversalResponse.response);
            Assert.AreEqual("Approved", reversalResponse.message);
        }

        [Test]
        public void test35()
        {
            var auth = new authorization();
            auth.orderId = "35";
            auth.amount = 40040;
            auth.orderSource = orderSourceType.ecommerce;
            var billToAddress = new contact();
            billToAddress.name = "Bob Black";
            billToAddress.addressLine1 = "4 Main St.";
            billToAddress.city = "Laurel";
            billToAddress.state = "MD";
            billToAddress.zip = "20708";
            billToAddress.country = countryTypeEnum.US;
            auth.billToAddress = billToAddress;
            var card = new cardType();
            card.number = "375001000000005";
            card.expDate = "0412";
            card.type = methodOfPaymentTypeEnum.AX;
            auth.card = card;

            var authorizeResponse = litle.Authorize(auth);
            Assert.AreEqual("000", authorizeResponse.response);
            Assert.AreEqual("Approved", authorizeResponse.message);
            Assert.AreEqual("44444", authorizeResponse.authCode);
            Assert.AreEqual("12", authorizeResponse.fraudResult.avsResult);

            var capture = new capture();
            capture.litleTxnId = authorizeResponse.litleTxnId;
            capture.amount = 20020;
            var captureResponse = litle.Capture(capture);
            Assert.AreEqual("000", captureResponse.response);
            Assert.AreEqual("Approved", captureResponse.message);

            var reversal = new authReversal();
            reversal.litleTxnId = authorizeResponse.litleTxnId;
            reversal.amount = 20020;
            var reversalResponse = litle.AuthReversal(reversal);
            Assert.AreEqual("000", reversalResponse.response);
            Assert.AreEqual("Approved", reversalResponse.message);
        }

        [Test]
        public void test36()
        {
            var auth = new authorization();
            auth.orderId = "36";
            auth.amount = 20500;
            auth.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.number = "375000026600004";
            card.expDate = "0512";
            card.type = methodOfPaymentTypeEnum.AX;
            auth.card = card;

            var authorizeResponse = litle.Authorize(auth);
            Assert.AreEqual("000", authorizeResponse.response);
            Assert.AreEqual("Approved", authorizeResponse.message);

            var reversal = new authReversal();
            reversal.litleTxnId = authorizeResponse.litleTxnId;
            reversal.amount = 10000;
            var reversalResponse = litle.AuthReversal(reversal);
            Assert.AreEqual("336", reversalResponse.response);
            Assert.AreEqual("Reversal Amount does not match Authorization amount", reversalResponse.message);
        }
    }
}

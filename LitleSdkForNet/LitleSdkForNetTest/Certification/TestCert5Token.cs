using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Certification
{
    [TestFixture]
    internal class TestCert5Token
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
        public void test50()
        {
            var request = new registerTokenRequestType();
            request.orderId = "50";
            request.accountNumber = "4457119922390123";

            var response = litle.RegisterToken(request);
            Assert.AreEqual("445711", response.bin);
            Assert.AreEqual(methodOfPaymentTypeEnum.VI, response.type);
            Assert.AreEqual("801", response.response);
            Assert.AreEqual("1111222233330123", response.litleToken);
            Assert.AreEqual("Account number was successfully registered", response.message);
        }

        [Test]
        public void test51()
        {
            var request = new registerTokenRequestType();
            request.orderId = "51";
            request.accountNumber = "4457119999999999";

            var response = litle.RegisterToken(request);
            Assert.AreEqual("820", response.response);
            Assert.AreEqual("Credit card number was invalid", response.message);
        }

        [Test]
        public void test52()
        {
            var request = new registerTokenRequestType();
            request.orderId = "52";
            request.accountNumber = "4457119922390123";

            var response = litle.RegisterToken(request);
            Assert.AreEqual("445711", response.bin);
            Assert.AreEqual(methodOfPaymentTypeEnum.VI, response.type);
            Assert.AreEqual("802", response.response);
            Assert.AreEqual("1111222233330123", response.litleToken);
            Assert.AreEqual("Account number was previously registered", response.message);
        }

        [Test]
        public void test53()
        {
            var request = new registerTokenRequestType();
            request.orderId = "53";
            var echeck = new echeckForTokenType();
            echeck.accNum = "1099999998";
            echeck.routingNum = "114567895";
            request.echeckForToken = echeck;
            ;

            var response = litle.RegisterToken(request);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.type);
            Assert.AreEqual("998", response.eCheckAccountSuffix);
            Assert.AreEqual("801", response.response);
            Assert.AreEqual("Account number was successfully registered", response.message);
            Assert.AreEqual("111922223333000998", response.litleToken);
        }

        [Test]
        public void test54()
        {
            var request = new registerTokenRequestType();
            request.orderId = "54";
            var echeck = new echeckForTokenType();
            echeck.accNum = "1022222102";
            echeck.routingNum = "1145_7895";
            request.echeckForToken = echeck;
            ;

            var response = litle.RegisterToken(request);
            Assert.AreEqual("900", response.response);
            Assert.AreEqual("Invalid bank routing number", response.message);
        }

        [Test]
        public void test55()
        {
            var auth = new authorization();
            auth.orderId = "55";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.number = "5435101234510196";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            var response = litle.Authorize(auth);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.MC, response.tokenResponse.type);
            Assert.AreEqual("543510", response.tokenResponse.bin);
        }

        [Test]
        public void test56()
        {
            var auth = new authorization();
            auth.orderId = "56";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.number = "5435109999999999";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            var response = litle.Authorize(auth);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid account number", response.message);
        }

        [Test]
        public void test57()
        {
            var auth = new authorization();
            auth.orderId = "57";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.number = "5435101234510196";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            var response = litle.Authorize(auth);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("802", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was previously registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.MC, response.tokenResponse.type);
            Assert.AreEqual("543510", response.tokenResponse.bin);
        }

        [Test]
        public void test59()
        {
            var auth = new authorization();
            auth.orderId = "59";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            var token = new cardTokenType();
            token.litleToken = "1712990000040196";
            token.expDate = "1112";
            auth.token = token;

            var response = litle.Authorize(auth);
            Assert.AreEqual("822", response.response);
            Assert.AreEqual("Token was not found", response.message);
        }

        [Test]
        public void test60()
        {
            var auth = new authorization();
            auth.orderId = "60";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            var token = new cardTokenType();
            token.litleToken = "1712999999999999";
            token.expDate = "1112";
            auth.token = token;

            var response = litle.Authorize(auth);
            Assert.AreEqual("823", response.response);
            Assert.AreEqual("Token was invalid", response.message);
        }

        [Test]
        public void test61()
        {
            var sale = new echeckSale();
            sale.orderId = "61";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            var billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            ;
            echeck.accNum = "1099999003";
            echeck.routingNum = "114567895";
            sale.echeck = echeck;

            var response = litle.EcheckSale(sale);
            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            Assert.AreEqual("111922223333444003", response.tokenResponse.litleToken);
        }

        [Test]
        public void test62()
        {
            var sale = new echeckSale();
            sale.orderId = "62";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            var billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            ;
            echeck.accNum = "1099999999";
            echeck.routingNum = "114567895";
            sale.echeck = echeck;

            var response = litle.EcheckSale(sale);
            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            Assert.AreEqual("999", response.tokenResponse.eCheckAccountSuffix);
            Assert.AreEqual("111922223333444999", response.tokenResponse.litleToken);
        }

        [Test]
        public void test63()
        {
            var sale = new echeckSale();
            sale.orderId = "63";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            var billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            ;
            echeck.accNum = "1099999999";
            echeck.routingNum = "214567892";
            sale.echeck = echeck;

            var response = litle.EcheckSale(sale);
            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            Assert.AreEqual("999", response.tokenResponse.eCheckAccountSuffix);
            Assert.AreEqual("111922223333555999", response.tokenResponse.litleToken);
        }
    }
}

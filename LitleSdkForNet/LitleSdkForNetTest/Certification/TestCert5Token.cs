using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Certification
{
    [TestFixture]
    class TestCert5Token
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void setUp()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("url", "https://payments.vantivprelive.com/vap/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", Properties.Settings.Default.username);
            config.Add("version", "8.33");
            config.Add("timeout", "20000");
            config.Add("merchantId", Properties.Settings.Default.merchantId);
            config.Add("password", Properties.Settings.Default.password);
            config.Add("printxml", "true");
            config.Add("logFile", null);
            config.Add("neuterAccountNums", null);
            config.Add("proxyHost", Properties.Settings.Default.proxyHost);
            config.Add("proxyPort", Properties.Settings.Default.proxyPort);
            litle = new LitleOnline(config);
        }

        [Test]
        public void test50()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.id = "1";
            request.orderId = "50";
            request.accountNumber = "4457119922390123";

            registerTokenResponse response = litle.RegisterToken(request);
            Assert.AreEqual("445711", response.bin);
            Assert.AreEqual(methodOfPaymentTypeEnum.VI, response.type);
//            Assert.AreEqual("801", response.response);
            Assert.AreEqual("0123", response.litleToken.Substring(response.litleToken.Length-4));
//            Assert.AreEqual("Account number was successfully registered", response.message);
        }

        [Test]
        public void test51()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.orderId = "51";
            request.accountNumber = "4457119999999999";

            registerTokenResponse response = litle.RegisterToken(request);
            Assert.AreEqual("820", response.response);
            Assert.AreEqual("Credit card number was invalid", response.message);
        }

        [Test]
        public void test52()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.orderId = "52";
            request.accountNumber = "4457119922390123";

            registerTokenResponse response = litle.RegisterToken(request);
            Assert.AreEqual("445711", response.bin);
            Assert.AreEqual(methodOfPaymentTypeEnum.VI, response.type);
            Assert.AreEqual("802", response.response);
            Assert.AreEqual("0123", response.litleToken.Substring(response.litleToken.Length-4));
            Assert.AreEqual("Account number was previously registered", response.message);
        }

        [Test]
        public void test53()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.orderId = "53";
            echeckForTokenType echeck = new echeckForTokenType();
            echeck.accNum = "1099999998";
            echeck.routingNum = "011100012";
            request.echeckForToken = echeck; ;

            registerTokenResponse response = litle.RegisterToken(request);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.type);
            Assert.AreEqual("998", response.eCheckAccountSuffix);
//            Assert.AreEqual("801", response.response);
//            Assert.AreEqual("Account number was successfully registered", response.message);
//            Assert.AreEqual("111922223333000998", response.litleToken);
        }

        [Test]
        public void test54()
        {
            registerTokenRequestType request = new registerTokenRequestType();
            request.orderId = "54";
            echeckForTokenType echeck = new echeckForTokenType();
            echeck.accNum = "1022222102";
            echeck.routingNum = "1145_7895";
            request.echeckForToken = echeck; ;

            registerTokenResponse response = litle.RegisterToken(request);
            Assert.AreEqual("900", response.response);
            Assert.AreEqual("Invalid Bank Routing Number", response.message);
        }

        [Test]
        public void test55()
        {
            authorization auth = new authorization();
            auth.orderId = "55";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.number = "5435101234510196";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            authorizationResponse response = litle.Authorize(auth);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
//            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
//            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.MC, response.tokenResponse.type);
            Assert.AreEqual("543510", response.tokenResponse.bin);
        }

        [Test]
        public void test56()
        {
            authorization auth = new authorization();
            auth.orderId = "56";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.number = "5435109999999999";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            authorizationResponse response = litle.Authorize(auth);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
        }

        [Test]
        public void test57()
        {
            authorization auth = new authorization();
            auth.orderId = "57";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.number = "5435101234510196";
            card.expDate = "1112";
            card.cardValidationNum = "987";
            card.type = methodOfPaymentTypeEnum.MC;
            auth.card = card;

            authorizationResponse response = litle.Authorize(auth);
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
            authorization auth = new authorization();
            auth.orderId = "59";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardTokenType token = new cardTokenType();
            token.litleToken = "1712990000040196";
            token.expDate = "1112";
            auth.token = token;

            authorizationResponse response = litle.Authorize(auth);
            Assert.AreEqual("101", response.response);
            Assert.AreEqual("Issuer Unavailable", response.message);
        }

        [Test]
        public void test60()
        {
            authorization auth = new authorization();
            auth.orderId = "60";
            auth.amount = 15000;
            auth.orderSource = orderSourceType.ecommerce;
            cardTokenType token = new cardTokenType();
            token.litleToken = "1112000100000085";
            token.expDate = "1121";
            auth.token = token;

            authorizationResponse response = litle.Authorize(auth);
            Assert.AreEqual("823", response.response);
            Assert.AreEqual("Token was invalid", response.message);
        }

        [Test]
        public void test61()
        {
            echeckSale sale = new echeckSale();
            sale.orderId = "61";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking; ;
            echeck.accNum = "1099999003";
            echeck.routingNum = "011100012";
            sale.echeck = echeck;

            echeckSalesResponse response = litle.EcheckSale(sale);
//            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
//            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
//            Assert.AreEqual("111922223333444003", response.tokenResponse.litleToken);
        }

        [Test]
        public void test62()
        {
            echeckSale sale = new echeckSale();
            sale.orderId = "62";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking; ;
            echeck.accNum = "1099999999";
            echeck.routingNum = "011100012";
            sale.echeck = echeck;

            echeckSalesResponse response = litle.EcheckSale(sale);
//            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
//            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            Assert.AreEqual("999", response.tokenResponse.eCheckAccountSuffix);
//            Assert.AreEqual("111922223333444999", response.tokenResponse.litleToken);
        }

        [Test]
        public void test63()
        {
            echeckSale sale = new echeckSale();
            sale.orderId = "63";
            sale.amount = 15000;
            sale.orderSource = orderSourceType.ecommerce;
            contact billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking; ;
            echeck.accNum = "1099999999";
            echeck.routingNum = "011100012";
            sale.echeck = echeck;

            echeckSalesResponse response = litle.EcheckSale(sale);
//            Assert.AreEqual("801", response.tokenResponse.tokenResponseCode);
//            Assert.AreEqual("Account number was successfully registered", response.tokenResponse.tokenMessage);
            Assert.AreEqual(methodOfPaymentTypeEnum.EC, response.tokenResponse.type);
            Assert.AreEqual("999", response.tokenResponse.eCheckAccountSuffix);
//            Assert.AreEqual("111922223333555999", response.tokenResponse.litleToken);
        }
            
    }
}

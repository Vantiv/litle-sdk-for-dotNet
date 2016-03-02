using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Certification
{
    [TestFixture]
    internal class TestCert1Base
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
            config.Add("version", "9.00");
            config.Add("timeout", Settings.Default.timeout);
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
        public void Test1Auth()
        {
            var authorization = new authorization();
            authorization.orderId = "1";
            authorization.amount = 10010;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "John Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010000000009";
            card.expDate = "0112";
            card.cardValidationNum = "349";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("11111 ", response.authCode);
            Assert.AreEqual("01", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            var capture = new capture();
            capture.litleTxnId = response.litleTxnId;
            var captureResponse = litle.Capture(capture);
            Assert.AreEqual("000", captureResponse.response);
            Assert.AreEqual("Approved", captureResponse.message);

            var credit = new credit();
            credit.litleTxnId = captureResponse.litleTxnId;
            var creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            var newvoid = new voidTxn();
            newvoid.litleTxnId = creditResponse.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void Test1AVS()
        {
            var authorization = new authorization();
            authorization.orderId = "1";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "John Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010000000009";
            card.expDate = "0112";
            card.cardValidationNum = "349";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("11111 ", response.authCode);
            Assert.AreEqual("01", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test1Sale()
        {
            var sale = new sale();
            sale.orderId = "1";
            sale.amount = 10010;
            sale.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "John Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010000000009";
            card.expDate = "0112";
            card.cardValidationNum = "349";
            sale.card = card;

            var response = litle.Sale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("11111 ", response.authCode);
            Assert.AreEqual("01", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            var credit = new credit();
            credit.litleTxnId = response.litleTxnId;
            var creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);


            var newvoid = new voidTxn();
            newvoid.litleTxnId = creditResponse.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void test2Auth()
        {
            var authorization = new authorization();
            authorization.orderId = "2";
            authorization.amount = 20020;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Mike J. Hammer";
            contact.addressLine1 = "2 Main St.";
            contact.addressLine2 = "Apt. 222";
            contact.city = "Riverside";
            contact.state = "RI";
            contact.zip = "02915";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010000000003";
            card.expDate = "0212";
            card.cardValidationNum = "261";
            authorization.card = card;
            var authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            authorization.cardholderAuthentication = authenticationvalue;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("22222", response.authCode);
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            var capture = new capture();
            capture.litleTxnId = response.litleTxnId;
            var captureresponse = litle.Capture(capture);
            Assert.AreEqual("000", captureresponse.response);
            Assert.AreEqual("Approved", captureresponse.message);

            var credit = new credit();
            credit.litleTxnId = captureresponse.litleTxnId;
            var creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            var newvoid = new voidTxn();
            newvoid.litleTxnId = creditResponse.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void test2AVS()
        {
            var authorization = new authorization();
            authorization.orderId = "2";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Mike J. Hammer";
            contact.addressLine1 = "2 Main St.";
            contact.addressLine2 = "Apt. 222";
            contact.city = "Riverside";
            contact.state = "RI";
            contact.zip = "02915";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010000000003";
            card.expDate = "0212";
            card.cardValidationNum = "261";
            authorization.card = card;
            var authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            authorization.cardholderAuthentication = authenticationvalue;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("22222", response.authCode);
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test2Sale()
        {
            var sale = new sale();
            sale.orderId = "2";
            sale.amount = 20020;
            sale.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Mike J. Hammer";
            contact.addressLine1 = "2 Main St.";
            contact.addressLine2 = "Apt. 222";
            contact.city = "Riverside";
            contact.state = "RI";
            contact.zip = "02915";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010000000003";
            card.expDate = "0212";
            card.cardValidationNum = "261";
            sale.card = card;
            var authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            sale.cardholderAuthentication = authenticationvalue;

            var response = litle.Sale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("22222", response.authCode);
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            var credit = new credit();
            credit.litleTxnId = response.litleTxnId;
            var creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            var newvoid = new voidTxn();
            newvoid.litleTxnId = creditResponse.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void test3Auth()
        {
            var authorization = new authorization();
            authorization.orderId = "3";
            authorization.amount = 30030;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Eileen Jones";
            contact.addressLine1 = "3 Main St.";
            contact.city = "Bloomfield";
            contact.state = "CT";
            contact.zip = "06002";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010000000003";
            card.expDate = "0312";
            card.cardValidationNum = "758";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("33333", response.authCode);
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            var capture = new capture();
            capture.litleTxnId = response.litleTxnId;
            var captureResponse = litle.Capture(capture);
            Assert.AreEqual("000", captureResponse.response);
            Assert.AreEqual("Approved", captureResponse.message);

            var credit = new credit();
            credit.litleTxnId = captureResponse.litleTxnId;
            var creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            var newvoid = new voidTxn();
            newvoid.litleTxnId = creditResponse.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void test3AVS()
        {
            var authorization = new authorization();
            authorization.orderId = "3";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Eileen Jones";
            contact.addressLine1 = "3 Main St.";
            contact.city = "Bloomfield";
            contact.state = "CT";
            contact.zip = "06002";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010000000003";
            card.expDate = "0312";
            card.cardValidationNum = "758";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("33333", response.authCode);
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test3Sale()
        {
            var sale = new sale();
            sale.orderId = "3";
            sale.amount = 30030;
            sale.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Eileen Jones";
            contact.addressLine1 = "3 Main St.";
            contact.city = "Bloomfield";
            contact.state = "CT";
            contact.zip = "06002";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010000000003";
            card.expDate = "0312";
            card.cardValidationNum = "758";
            sale.card = card;

            var response = litle.Sale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("33333", response.authCode);
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            var credit = new credit();
            credit.litleTxnId = response.litleTxnId;
            var creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            var newvoid = new voidTxn();
            newvoid.litleTxnId = creditResponse.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void test4Auth()
        {
            var authorization = new authorization();
            authorization.orderId = "4";
            authorization.amount = 40040;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Bob Black";
            contact.addressLine1 = "4 Main St.";
            contact.city = "Laurel";
            contact.state = "MD";
            contact.zip = "20708";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001000000005";
            card.expDate = "0412";
            card.cardValidationNum = "758";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("44444", response.authCode);
            Assert.AreEqual("12", response.fraudResult.avsResult);

            var capture = new capture();
            capture.litleTxnId = response.litleTxnId;
            var captureresponse = litle.Capture(capture);
            Assert.AreEqual("000", captureresponse.response);
            Assert.AreEqual("Approved", captureresponse.message);

            var credit = new credit();
            credit.litleTxnId = captureresponse.litleTxnId;
            var creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            var newvoid = new voidTxn();
            newvoid.litleTxnId = creditResponse.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void test4AVS()
        {
            var authorization = new authorization();
            authorization.orderId = "4";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Bob Black";
            contact.addressLine1 = "4 Main St.";
            contact.city = "Laurel";
            contact.state = "MD";
            contact.zip = "20708";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001000000005";
            card.expDate = "0412";
            card.cardValidationNum = "758";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("44444", response.authCode);
            Assert.AreEqual("12", response.fraudResult.avsResult);
        }

        [Test]
        public void test4Sale()
        {
            var sale = new sale();
            sale.orderId = "4";
            sale.amount = 40040;
            sale.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Bob Black";
            contact.addressLine1 = "4 Main St.";
            contact.city = "Laurel";
            contact.state = "MD";
            contact.zip = "20708";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001000000005";
            card.expDate = "0412";
            card.cardValidationNum = "758";
            sale.card = card;

            var response = litle.Sale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("44444", response.authCode);
            Assert.AreEqual("12", response.fraudResult.avsResult);

            var credit = new credit();
            credit.litleTxnId = response.litleTxnId;
            var creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            var newvoid = new voidTxn();
            newvoid.litleTxnId = creditResponse.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void test5Auth()
        {
            var authorization = new authorization();
            authorization.orderId = "5";
            authorization.amount = 50050;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010200000007";
            card.expDate = "0512";
            card.cardValidationNum = "463";
            authorization.card = card;
            var authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            authorization.cardholderAuthentication = authenticationvalue;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("55555 ", response.authCode);
            Assert.AreEqual("32", response.fraudResult.avsResult);
            Assert.AreEqual("N", response.fraudResult.cardValidationResult);

            var capture = new capture();
            capture.litleTxnId = response.litleTxnId;
            var captureresponse = litle.Capture(capture);
            Assert.AreEqual("000", captureresponse.response);
            Assert.AreEqual("Approved", captureresponse.message);

            var credit = new credit();
            credit.litleTxnId = captureresponse.litleTxnId;
            var creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            var newvoid = new voidTxn();
            newvoid.litleTxnId = creditResponse.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void test5AVS()
        {
            var authorization = new authorization();
            authorization.orderId = "5";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010200000007";
            card.expDate = "0512";
            card.cardValidationNum = "463";
            authorization.card = card;
            var authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            authorization.cardholderAuthentication = authenticationvalue;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("55555 ", response.authCode);
            Assert.AreEqual("32", response.fraudResult.avsResult);
            Assert.AreEqual("N", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test5Sale()
        {
            var sale = new sale();
            sale.orderId = "5";
            sale.amount = 50050;
            sale.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010200000007";
            card.expDate = "0512";
            card.cardValidationNum = "463";
            sale.card = card;
            var authenticationvalue = new fraudCheckType();
            authenticationvalue.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
            sale.cardholderAuthentication = authenticationvalue;

            var response = litle.Sale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("55555 ", response.authCode);
            Assert.AreEqual("32", response.fraudResult.avsResult);
            Assert.AreEqual("N", response.fraudResult.cardValidationResult);

            var credit = new credit();
            credit.litleTxnId = response.litleTxnId;
            var creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            var newvoid = new voidTxn();
            newvoid.litleTxnId = creditResponse.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void test6Auth()
        {
            var authorization = new authorization();
            authorization.orderId = "6";
            authorization.amount = 60060;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Joe Green";
            contact.addressLine1 = "6 Main St.";
            contact.city = "Derry";
            contact.state = "NH";
            contact.zip = "03038";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010100000008";
            card.expDate = "0612";
            card.cardValidationNum = "992";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("110", response.response);
            Assert.AreEqual("Insufficient Funds", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("P", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test6Sale()
        {
            var sale = new sale();
            sale.orderId = "6";
            sale.amount = 60060;
            sale.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Joe Green";
            contact.addressLine1 = "6 Main St.";
            contact.city = "Derry";
            contact.state = "NH";
            contact.zip = "03038";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010100000008";
            card.expDate = "0612";
            card.cardValidationNum = "992";
            sale.card = card;

            var response = litle.Sale(sale);
            Assert.AreEqual("110", response.response);
            Assert.AreEqual("Insufficient Funds", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("P", response.fraudResult.cardValidationResult);

            var newvoid = new voidTxn();
            newvoid.litleTxnId = response.litleTxnId;
            var voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("360", voidResponse.response);
            Assert.AreEqual("No transaction found with specified litleTxnId", voidResponse.message);
        }

        [Test]
        public void test7Auth()
        {
            var authorization = new authorization();
            authorization.orderId = "7";
            authorization.amount = 70070;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Jane Murray";
            contact.addressLine1 = "7 Main St.";
            contact.city = "Amesbury";
            contact.state = "MA";
            contact.zip = "01913";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010100000002";
            card.expDate = "0712";
            card.cardValidationNum = "251";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("N", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test7AVS()
        {
            var authorization = new authorization();
            authorization.orderId = "7";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Jane Murray";
            contact.addressLine1 = "7 Main St.";
            contact.city = "Amesbury";
            contact.state = "MA";
            contact.zip = "01913";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010100000002";
            card.expDate = "0712";
            card.cardValidationNum = "251";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("N", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test7Sale()
        {
            var sale = new sale();
            sale.orderId = "7";
            sale.amount = 70070;
            sale.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Jane Murray";
            contact.addressLine1 = "7 Main St.";
            contact.city = "Amesbury";
            contact.state = "MA";
            contact.zip = "01913";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010100000002";
            card.expDate = "0712";
            card.cardValidationNum = "251";
            sale.card = card;

            var response = litle.Sale(sale);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("N", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test8Auth()
        {
            var authorization = new authorization();
            authorization.orderId = "8";
            authorization.amount = 80080;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Mark Johnson";
            contact.addressLine1 = "8 Main St.";
            contact.city = "Manchester";
            contact.state = "NH";
            contact.zip = "03101";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010100000002";
            card.expDate = "0812";
            card.cardValidationNum = "184";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("123", response.response);
            Assert.AreEqual("Call Discover", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("P", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test8AVS()
        {
            var authorization = new authorization();
            authorization.orderId = "8";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Mark Johnson";
            contact.addressLine1 = "8 Main St.";
            contact.city = "Manchester";
            contact.state = "NH";
            contact.zip = "03101";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010100000002";
            card.expDate = "0812";
            card.cardValidationNum = "184";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("123", response.response);
            Assert.AreEqual("Call Discover", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("P", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test8Sale()
        {
            var sale = new sale();
            sale.orderId = "8";
            sale.amount = 80080;
            sale.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "Mark Johnson";
            contact.addressLine1 = "8 Main St.";
            contact.city = "Manchester";
            contact.state = "NH";
            contact.zip = "03101";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010100000002";
            card.expDate = "0812";
            card.cardValidationNum = "184";
            sale.card = card;

            var response = litle.Sale(sale);
            Assert.AreEqual("123", response.response);
            Assert.AreEqual("Call Discover", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("P", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void test9Auth()
        {
            var authorization = new authorization();
            authorization.orderId = "9";
            authorization.amount = 90090;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "James Miller";
            contact.addressLine1 = "9 Main St.";
            contact.city = "Boston";
            contact.state = "MA";
            contact.zip = "02134";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001010000003";
            card.expDate = "0912";
            card.cardValidationNum = "0421";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("303", response.response);
            Assert.AreEqual("Pick Up Card", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
        }

        [Test]
        public void test9AVS()
        {
            var authorization = new authorization();
            authorization.orderId = "9";
            authorization.amount = 0;
            authorization.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "James Miller";
            contact.addressLine1 = "9 Main St.";
            contact.city = "Boston";
            contact.state = "MA";
            contact.zip = "02134";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001010000003";
            card.expDate = "0912";
            card.cardValidationNum = "0421";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("303", response.response);
            Assert.AreEqual("Pick Up Card", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
        }

        [Test]
        public void test9Sale()
        {
            var sale = new sale();
            sale.orderId = "9";
            sale.amount = 90090;
            sale.orderSource = orderSourceType.ecommerce;
            var contact = new contact();
            contact.name = "James Miller";
            contact.addressLine1 = "9 Main St.";
            contact.city = "Boston";
            contact.state = "MA";
            contact.zip = "02134";
            contact.country = countryTypeEnum.US;
            sale.billToAddress = contact;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001010000003";
            card.expDate = "0912";
            card.cardValidationNum = "0421";
            sale.card = card;

            var response = litle.Sale(sale);
            Assert.AreEqual("303", response.response);
            Assert.AreEqual("Pick Up Card", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
        }

        [Test]
        public void test10()
        {
            var authorization = new authorization();
            authorization.orderId = "10";
            authorization.amount = 40000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010140000141";
            card.expDate = "0912";
            authorization.card = card;
            authorization.allowPartialAuth = true;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("010", response.response);
            Assert.AreEqual("Partially Approved", response.message);
            Assert.AreEqual("32000", response.approvedAmount);
        }

        [Test]
        public void test11()
        {
            var authorization = new authorization();
            authorization.orderId = "11";
            authorization.amount = 60000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010140000004";
            card.expDate = "1111";
            authorization.card = card;
            authorization.allowPartialAuth = true;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("010", response.response);
            Assert.AreEqual("Partially Approved", response.message);
            Assert.AreEqual("48000", response.approvedAmount);
        }

        [Test]
        public void test12()
        {
            var authorization = new authorization();
            authorization.orderId = "12";
            authorization.amount = 50000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.AX;
            card.number = "375001014000009";
            card.expDate = "0412";
            authorization.card = card;
            authorization.allowPartialAuth = true;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("010", response.response);
            Assert.AreEqual("Partially Approved", response.message);
            Assert.AreEqual("40000", response.approvedAmount);
        }

        [Test]
        public void test13()
        {
            var authorization = new authorization();
            authorization.orderId = "13";
            authorization.amount = 15000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.DI;
            card.number = "6011010140000004";
            card.expDate = "0812";
            authorization.card = card;
            authorization.allowPartialAuth = true;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("010", response.response);
            Assert.AreEqual("Partially Approved", response.message);
            Assert.AreEqual("12000", response.approvedAmount);
        }
    }
}

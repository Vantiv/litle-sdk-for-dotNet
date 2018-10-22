using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Certification
{
    [TestFixture]
    class TestCert1Base
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUp()
        {
            Dictionary<string, string> config = new Dictionary<string, string>
            {
                { "url", "https://payments.vantivprelive.com/vap/communicator/online" },
                { "reportGroup", "Default Report Group" },
                { "username", Properties.Settings.Default.username },
                { "version", "8.31" },
                { "timeout", "500" },
                { "merchantId", Properties.Settings.Default.merchantId },
                { "password", Properties.Settings.Default.password },
                { "printxml", "true" },
                { "logFile", null },
                { "neuterAccountNums", null },
                { "proxyHost", Properties.Settings.Default.proxyHost },
                { "proxyPort", Properties.Settings.Default.proxyPort }
            };
            litle = new LitleOnline(config);
        }


        [Test]
        public void Test1Auth()
        {

            authorization authorization = new authorization
            {
                orderId = "1",
                amount = 10010,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "John Smith",
                addressLine1 = "1 Main St.",
                city = "Burlington",
                state = "MA",
                zip = "01803-3747",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4457010000000009",
                expDate = "0112",
                cardValidationNum = "349"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("11111 ", response.authCode);
            Assert.AreEqual("01", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            capture capture = new capture
            {
                litleTxnId = response.litleTxnId,
                id = response.id
            };
            captureResponse captureResponse = litle.Capture(capture);
            Assert.AreEqual("000", captureResponse.response);
            Assert.AreEqual("Approved", captureResponse.message);

            credit credit = new credit
            {
                litleTxnId = captureResponse.litleTxnId,
                id = captureResponse.id
            };
            creditResponse creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            //Intermittent behavior
            //voidTxn newvoid = new voidTxn
            //{
            //    litleTxnId = creditResponse.litleTxnId,
            //    id = creditResponse.id
            //};
            //litleOnlineResponseTransactionResponseVoidResponse voidresponse = litle.DoVoid(newvoid);
            //Assert.AreEqual("000", voidresponse.response);
            //Assert.AreEqual("Approved", voidresponse.message);
        }

        [Test]
        public void Test1AVS()
        {
            authorization authorization = new authorization
            {
                orderId = "1",
                amount = 0,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "John Smith",
                addressLine1 = "1 Main St.",
                city = "Burlington",
                state = "MA",
                zip = "01803-3747",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4457010000000009",
                expDate = "0112",
                cardValidationNum = "349"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("11111 ", response.authCode);
            Assert.AreEqual("01", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void Test1Sale()
        {
            sale sale = new sale
            {
                orderId = "1",
                amount = 10010,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "John Smith",
                addressLine1 = "1 Main St.",
                city = "Burlington",
                state = "MA",
                zip = "01803-3747",
                country = countryTypeEnum.US
            };
            sale.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4457010000000009",
                expDate = "0112",
                cardValidationNum = "349"
            };
            sale.card = card;

            saleResponse response = litle.Sale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("11111 ", response.authCode);
            Assert.AreEqual("01", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            credit credit = new credit
            {
                litleTxnId = response.litleTxnId,
                id = response.id
            };
            creditResponse creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);


            //Intermittent behavior
            //voidTxn newvoid = new voidTxn
            //{
            //    litleTxnId = creditResponse.litleTxnId,
            //    id = creditResponse.id
            //};
            //litleOnlineResponseTransactionResponseVoidResponse voidResponse = litle.DoVoid(newvoid);
            //Assert.AreEqual("000", voidResponse.response);
            //Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void Test2Auth()
        {
            authorization authorization = new authorization
            {
                orderId = "2",
                amount = 20020,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Mike J. Hammer",
                addressLine1 = "2 Main St.",
                addressLine2 = "Apt. 222",
                city = "Riverside",
                state = "RI",
                zip = "02915",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.MC,
                number = "5112010000000003",
                expDate = "0212",
                cardValidationNum = "261"
            };
            authorization.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType
            {
                authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA="
            };
            authorization.cardholderAuthentication = authenticationvalue;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("22222 ".Trim(), response.authCode.Trim());
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            capture capture = new capture
            {
                litleTxnId = response.litleTxnId,
                id = response.id
            };
            captureResponse captureresponse = litle.Capture(capture);
            Assert.AreEqual("000", captureresponse.response);
            Assert.AreEqual("Approved", captureresponse.message);

            credit credit = new credit
            {
                litleTxnId = captureresponse.litleTxnId,
                id = captureresponse.id
            };
            creditResponse creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            //Intermittent behavior
            //voidTxn newvoid = new voidTxn
            //{
            //    litleTxnId = creditResponse.litleTxnId,
            //    id = creditResponse.id
            //};
            //litleOnlineResponseTransactionResponseVoidResponse voidResponse = litle.DoVoid(newvoid);
            //Assert.AreEqual("000", voidResponse.response);
            //Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void Test2AVS()
        {
            authorization authorization = new authorization
            {
                orderId = "2",
                amount = 0,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Mike J. Hammer",
                addressLine1 = "2 Main St.",
                addressLine2 = "Apt. 222",
                city = "Riverside",
                state = "RI",
                zip = "02915",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.MC,
                number = "5112010000000003",
                expDate = "0212",
                cardValidationNum = "261"
            };
            authorization.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType
            {
                authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA="
            };
            authorization.cardholderAuthentication = authenticationvalue;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("22222 ".Trim(), response.authCode.Trim());
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

        }

        [Test]
        public void Test2Sale()
        {
            sale sale = new sale
            {
                orderId = "2",
                amount = 20020,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Mike J. Hammer",
                addressLine1 = "2 Main St.",
                addressLine2 = "Apt. 222",
                city = "Riverside",
                state = "RI",
                zip = "02915",
                country = countryTypeEnum.US
            };
            sale.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.MC,
                number = "5112010000000003",
                expDate = "0212",
                cardValidationNum = "261"
            };
            sale.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType
            {
                authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA="
            };
            sale.cardholderAuthentication = authenticationvalue;

            saleResponse response = litle.Sale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("22222 ".Trim(), response.authCode.Trim());
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            credit credit = new credit
            {
                litleTxnId = response.litleTxnId,
                id = response.id
            };
            creditResponse creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            //Intermittent behavior
            //voidTxn newvoid = new voidTxn
            //{
            //    litleTxnId = creditResponse.litleTxnId,
            //    id = creditResponse.id
            //};
            //litleOnlineResponseTransactionResponseVoidResponse voidResponse = litle.DoVoid(newvoid);
            //Assert.AreEqual("000", voidResponse.response);
            //Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void Test3Auth()
        {
            authorization authorization = new authorization
            {
                orderId = "3",
                amount = 30030,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Eileen Jones",
                addressLine1 = "3 Main St.",
                city = "Bloomfield",
                state = "CT",
                zip = "06002",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.DI,
                number = "6011010000000003",
                expDate = "0312",
                cardValidationNum = "758"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("33333 ".Trim(), response.authCode.Trim());
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            capture capture = new capture
            {
                litleTxnId = response.litleTxnId,
                id = response.id
            };
            captureResponse captureResponse = litle.Capture(capture);
            Assert.AreEqual("000", captureResponse.response);
            Assert.AreEqual("Approved", captureResponse.message);

            credit credit = new credit
            {
                litleTxnId = captureResponse.litleTxnId,
                id = captureResponse.id
            };
            creditResponse creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            //Intermittent behavior
            //voidTxn newvoid = new voidTxn
            //{
            //    litleTxnId = creditResponse.litleTxnId,
            //    id = response.id
            //};
            //litleOnlineResponseTransactionResponseVoidResponse voidresponse = litle.DoVoid(newvoid);
            //Assert.AreEqual("000", voidresponse.response);
            //Assert.AreEqual("Approved", voidresponse.message);
        }

        [Test]
        public void Test3AVS()
        {
            authorization authorization = new authorization
            {
                orderId = "3",
                amount = 0,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Eileen Jones",
                addressLine1 = "3 Main St.",
                city = "Bloomfield",
                state = "CT",
                zip = "06002",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.DI,
                number = "6011010000000003",
                expDate = "0312",
                cardValidationNum = "758"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("33333 ".Trim(), response.authCode.Trim());
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

        }

        [Test]
        public void Test3Sale()
        {
            sale sale = new sale
            {
                orderId = "3",
                amount = 30030,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Eileen Jones",
                addressLine1 = "3 Main St.",
                city = "Bloomfield",
                state = "CT",
                zip = "06002",
                country = countryTypeEnum.US
            };
            sale.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.DI,
                number = "6011010000000003",
                expDate = "0312",
                cardValidationNum = "758"
            };
            sale.card = card;

            saleResponse response = litle.Sale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("33333 ".Trim(), response.authCode.Trim());
            Assert.AreEqual("10", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            credit credit = new credit
            {
                litleTxnId = response.litleTxnId,
                id = response.id
            };
            creditResponse creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            //Intermittent behavior
            //voidTxn newvoid = new voidTxn
            //{
            //    litleTxnId = creditResponse.litleTxnId,
            //    id = creditResponse.id
            //};
            //litleOnlineResponseTransactionResponseVoidResponse voidResponse = litle.DoVoid(newvoid);
            //Assert.AreEqual("000", voidResponse.response);
            //Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void Test4Auth()
        {
            authorization authorization = new authorization
            {
                orderId = "4",
                amount = 10100,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Bob Black",
                addressLine1 = "4 Main St.",
                city = "Laurel",
                state = "MD",
                zip = "20708",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.AX,
                number = "375001000000005",
                expDate = "0421"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("44444 ".Trim(), response.authCode.Trim());
            Assert.AreEqual("13", response.fraudResult.avsResult);

            capture capture = new capture
            {
                litleTxnId = response.litleTxnId,
                id = response.id
            };
            captureResponse captureresponse = litle.Capture(capture);
            Assert.AreEqual("000", captureresponse.response);
            Assert.AreEqual("Approved", captureresponse.message);

            credit credit = new credit
            {
                litleTxnId = captureresponse.litleTxnId,
                id = captureresponse.id
            };
            creditResponse creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            voidTxn newvoid = new voidTxn
            {
                litleTxnId = creditResponse.litleTxnId,
                id = creditResponse.id
            };
            litleOnlineResponseTransactionResponseVoidResponse voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("000", voidResponse.response);
            Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void Test4AVS()
        {
            authorization authorization = new authorization
            {
                orderId = "4",
                amount = 0,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Bob Black",
                addressLine1 = "4 Main St.",
                city = "Laurel",
                state = "MD",
                zip = "20708",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.AX,
                number = "375001000000005",
                expDate = "0421",
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("44444 ".Trim(), response.authCode.Trim());
            Assert.AreEqual("13", response.fraudResult.avsResult);
        }

        [Test]
        public void Test4Sale()
        {
            sale sale = new sale
            {
                orderId = "4",
                amount = 10100,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Bob Black",
                addressLine1 = "4 Main St.",
                city = "Laurel",
                state = "MD",
                zip = "20708",
                country = countryTypeEnum.US
            };
            sale.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.AX,
                number = "375001000000005",
                expDate = "0421"
            };
            sale.card = card;

            saleResponse response = litle.Sale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("44444 ".Trim(), response.authCode.Trim());
            Assert.AreEqual("13", response.fraudResult.avsResult);

            credit credit = new credit
            {
                litleTxnId = response.litleTxnId,
                id = response.id
            };
            creditResponse creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            //voidTxn newvoid = new voidTxn
            //{
            //    litleTxnId = creditResponse.litleTxnId,
            //    id = creditResponse.id
            //};
            //litleOnlineResponseTransactionResponseVoidResponse voidResponse = litle.DoVoid(newvoid);
            //Assert.AreEqual("000", voidResponse.response);
            //Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void Test5Auth()
        {
            authorization authorization = new authorization
            {
                orderId = "5",
                amount = 50050,
                orderSource = orderSourceType.ecommerce
            };
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4457010200000007",
                expDate = "0512",
                cardValidationNum = "463"
            };
            authorization.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType
            {
                authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA="
            };
            authorization.cardholderAuthentication = authenticationvalue;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("55555 ", response.authCode);
            Assert.AreEqual("32", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            capture capture = new capture
            {
                litleTxnId = response.litleTxnId,
                id = response.id
            };
            captureResponse captureresponse = litle.Capture(capture);
            Assert.AreEqual("000", captureresponse.response);
            Assert.AreEqual("Approved", captureresponse.message);

            credit credit = new credit
            {
                litleTxnId = captureresponse.litleTxnId,
                id = captureresponse.id
            };
            creditResponse creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            //voidTxn newvoid = new voidTxn
            //{
            //    litleTxnId = creditResponse.litleTxnId,
            //    id = creditResponse.id
            //};
            //litleOnlineResponseTransactionResponseVoidResponse voidResponse = litle.DoVoid(newvoid);
            //Assert.AreEqual("000", voidResponse.response);
            //Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void Test5AVS()
        {
            authorization authorization = new authorization
            {
                orderId = "5",
                amount = 10100,
                orderSource = orderSourceType.ecommerce
            };
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4100200300011001",
                expDate = "0512",
                cardValidationNum = "463"
            };
            authorization.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType
            {
                authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA="
            };
            authorization.cardholderAuthentication = authenticationvalue;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("55555 ", response.authCode);
            Assert.AreEqual("32", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void Test5Sale()
        {
            sale sale = new sale
            {
                orderId = "5",
                amount = 50050,
                orderSource = orderSourceType.ecommerce
            };
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4457010200000007",
                expDate = "0512",
                cardValidationNum = "463"
            };
            sale.card = card;
            fraudCheckType authenticationvalue = new fraudCheckType
            {
                authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA="
            };
            sale.cardholderAuthentication = authenticationvalue;

            saleResponse response = litle.Sale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("55555 ", response.authCode);
            Assert.AreEqual("32", response.fraudResult.avsResult);
            Assert.AreEqual("M", response.fraudResult.cardValidationResult);

            credit credit = new credit
            {
                litleTxnId = response.litleTxnId,
                id = response.id
            };
            creditResponse creditResponse = litle.Credit(credit);
            Assert.AreEqual("000", creditResponse.response);
            Assert.AreEqual("Approved", creditResponse.message);

            //voidTxn newvoid = new voidTxn
            //{
            //    litleTxnId = creditResponse.litleTxnId,
            //    id = creditResponse.id
            //};
            //litleOnlineResponseTransactionResponseVoidResponse voidResponse = litle.DoVoid(newvoid);
            //Assert.AreEqual("000", voidResponse.response);
            //Assert.AreEqual("Approved", voidResponse.message);
        }

        [Test]
        public void Test6Auth()
        {
            authorization authorization = new authorization
            {
                orderId = "6",
                amount = 60060,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Joe Green",
                addressLine1 = "6 Main St.",
                city = "Derry",
                state = "NH",
                zip = "03038",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4457010100000008",
                expDate = "0612",
                cardValidationNum = "992"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("110", response.response);
            Assert.AreEqual("Insufficient Funds", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("P", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void Test6Sale()
        {
            sale sale = new sale
            {
                orderId = "6",
                amount = 60060,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Joe Green",
                addressLine1 = "6 Main St.",
                city = "Derry",
                state = "NH",
                zip = "03038",
                country = countryTypeEnum.US
            };
            sale.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4457010100000008",
                expDate = "0612",
                cardValidationNum = "992"
            };
            sale.card = card;

            saleResponse response = litle.Sale(sale);
            Assert.AreEqual("110", response.response);
            Assert.AreEqual("Insufficient Funds", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("P", response.fraudResult.cardValidationResult);

            voidTxn newvoid = new voidTxn
            {
                litleTxnId = response.litleTxnId
            };
            litleOnlineResponseTransactionResponseVoidResponse voidResponse = litle.DoVoid(newvoid);
            Assert.AreEqual("360", voidResponse.response);
            Assert.AreEqual("No transaction found with specified transaction Id", voidResponse.message);
        }

        [Test]
        public void Test7Auth()
        {
            authorization authorization = new authorization
            {
                orderId = "7",
                amount = 70070,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Jane Murray",
                addressLine1 = "7 Main St.",
                city = "Amesbury",
                state = "MA",
                zip = "01913",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.MC,
                number = "5112010100000002",
                expDate = "0712",
                cardValidationNum = "251"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("N", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void Test7AVS()
        {
            authorization authorization = new authorization
            {
                orderId = "7",
                amount = 0,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Jane Murray",
                addressLine1 = "7 Main St.",
                city = "Amesbury",
                state = "MA",
                zip = "01913",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.MC,
                number = "5112010100000002",
                expDate = "0712",
                cardValidationNum = "251"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("N", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void Test7Sale()
        {
            sale sale = new sale
            {
                orderId = "7",
                amount = 70070,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Jane Murray",
                addressLine1 = "7 Main St.",
                city = "Amesbury",
                state = "MA",
                zip = "01913",
                country = countryTypeEnum.US
            };
            sale.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.MC,
                number = "5112010100000002",
                expDate = "0712",
                cardValidationNum = "251"
            };
            sale.card = card;

            saleResponse response = litle.Sale(sale);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("N", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void Test8Auth()
        {
            authorization authorization = new authorization
            {
                orderId = "8",
                amount = 80080,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Mark Johnson",
                addressLine1 = "8 Main St.",
                city = "Manchester",
                state = "NH",
                zip = "03101",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.DI,
                number = "6011010100000002",
                expDate = "0812",
                cardValidationNum = "184"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("123", response.response);
            Assert.AreEqual("Call Discover", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("P", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void Test8AVS()
        {
            authorization authorization = new authorization
            {
                orderId = "8",
                amount = 0,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Mark Johnson",
                addressLine1 = "8 Main St.",
                city = "Manchester",
                state = "NH",
                zip = "03101",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.DI,
                number = "6011010100000002",
                expDate = "0812",
                cardValidationNum = "184"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("123", response.response);
            Assert.AreEqual("Call Discover", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("P", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void Test8Sale()
        {
            sale sale = new sale
            {
                orderId = "8",
                amount = 80080,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "Mark Johnson",
                addressLine1 = "8 Main St.",
                city = "Manchester",
                state = "NH",
                zip = "03101",
                country = countryTypeEnum.US
            };
            sale.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.DI,
                number = "6011010100000002",
                expDate = "0812",
                cardValidationNum = "184"
            };
            sale.card = card;

            saleResponse response = litle.Sale(sale);
            Assert.AreEqual("123", response.response);
            Assert.AreEqual("Call Discover", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
            Assert.AreEqual("P", response.fraudResult.cardValidationResult);
        }

        [Test]
        public void Test9Auth()
        {
            authorization authorization = new authorization
            {
                orderId = "9",
                amount = 90090,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "James Miller",
                addressLine1 = "9 Main St.",
                city = "Boston",
                state = "MA",
                zip = "02134",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.AX,
                number = "375001010000003",
                expDate = "0912",
                cardValidationNum = "0421"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("303", response.response);
            Assert.AreEqual("Pick Up Card", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
        }

        [Test]
        public void Test9AVS()
        {
            authorization authorization = new authorization
            {
                orderId = "9",
                amount = 0,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "James Miller",
                addressLine1 = "9 Main St.",
                city = "Boston",
                state = "MA",
                zip = "02134",
                country = countryTypeEnum.US
            };
            authorization.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.AX,
                number = "375001010000003",
                expDate = "0912",
                cardValidationNum = "0421"
            };
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("303", response.response);
            Assert.AreEqual("Pick Up Card", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
        }

        [Test]
        public void Test9Sale()
        {
            sale sale = new sale
            {
                orderId = "9",
                amount = 90090,
                orderSource = orderSourceType.ecommerce
            };
            contact contact = new contact
            {
                name = "James Miller",
                addressLine1 = "9 Main St.",
                city = "Boston",
                state = "MA",
                zip = "02134",
                country = countryTypeEnum.US
            };
            sale.billToAddress = contact;
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.AX,
                number = "375001010000003",
                expDate = "0912",
                cardValidationNum = "0421"
            };
            sale.card = card;

            saleResponse response = litle.Sale(sale);
            Assert.AreEqual("303", response.response);
            Assert.AreEqual("Pick Up Card", response.message);
            Assert.AreEqual("34", response.fraudResult.avsResult);
        }

        [Test]
        public void Test10()
        {
            authorization authorization = new authorization
            {
                orderId = "10",
                amount = 40000,
                orderSource = orderSourceType.ecommerce
            };
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4457010140000141",
                expDate = "0912"
            };
            authorization.card = card;
            authorization.allowPartialAuth = true;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("010", response.response);
            Assert.AreEqual("Partially Approved", response.message);
            Assert.AreEqual("32000", response.approvedAmount);
        }

        [Test]
        public void Test11()
        {
            authorization authorization = new authorization
            {
                orderId = "11",
                amount = 60000,
                orderSource = orderSourceType.ecommerce
            };
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.MC,
                number = "5112010140000004",
                expDate = "1111"
            };
            authorization.card = card;
            authorization.allowPartialAuth = true;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("010", response.response);
            Assert.AreEqual("Partially Approved", response.message);
            Assert.AreEqual("48000", response.approvedAmount);
        }

        [Test]
        public void Test12()
        {
            authorization authorization = new authorization
            {
                orderId = "12",
                amount = 50000,
                orderSource = orderSourceType.ecommerce
            };
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.AX,
                number = "375001014000009",
                expDate = "0412"
            };
            authorization.card = card;
            authorization.allowPartialAuth = true;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("010", response.response);
            Assert.AreEqual("Partially Approved", response.message);
            Assert.AreEqual("40000", response.approvedAmount);
        }

        [Test]
        public void Test13()
        {
            authorization authorization = new authorization
            {
                orderId = "13",
                amount = 15000,
                orderSource = orderSourceType.ecommerce
            };
            cardType card = new cardType
            {
                type = methodOfPaymentTypeEnum.DI,
                number = "6011010140000004",
                expDate = "0812"
            };
            authorization.card = card;
            authorization.allowPartialAuth = true;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("010", response.response);
            Assert.AreEqual("Partially Approved", response.message);
            Assert.AreEqual("12000", response.approvedAmount);

        }
            
    }
}

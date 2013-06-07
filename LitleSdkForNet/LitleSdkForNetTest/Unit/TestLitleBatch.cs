using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestLitleBatch
    {

        private LitleBatch litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleBatch();
        }

        [Test]
        public void TestAuth()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<litleRequest.*<batchRequest.*<authorization.*<card>.*<number>4100000000000002</number>.*</card>.*</authorization>.*<authorization.*<card>.*<number>4100000000000002</number>.*</card>.*</authorization>.*</batchRequest.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse><authorizationResponse><litleTxnId>124</litleTxnId></authorizationResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addAuthorization(authorization);
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse[1].litleTxnId);
        }

        [Test]
        public void testAuthReversal()
        {
            authReversal authreversal = new authReversal();
            authreversal.litleTxnId = 12345678000;
            authreversal.amount = 106;
            authreversal.payPalNotes = "Notes";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<authReversal.*?<litleTxnId>12345678000</litleTxnId>.*?</authReversal>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><authReversalResponse><litleTxnId>123</litleTxnId></authReversalResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addAuthReversal(authreversal);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfAuthReversalResponse[0].litleTxnId);
        }

        [Test]
        public void testCapture()
        {
            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";


            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<capture.*?<litleTxnId>123456000</litleTxnId>.*?</capture>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><captureResponse><litleTxnId>123</litleTxnId></captureResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addCapture(capture);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfCaptureResponse[0].litleTxnId);
        }

        [Test]
        public void testCaptureGivenAuth()
        {
            captureGivenAuth capturegivenauth = new captureGivenAuth();
            capturegivenauth.orderId = "12344";
            capturegivenauth.amount = 106;
            authInformation authinfo = new authInformation();
            authinfo.authDate = new DateTime(2002, 10, 9);
            authinfo.authCode = "543216";
            authinfo.authAmount = 12345;
            capturegivenauth.authInformation = authinfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            capturegivenauth.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<captureGivenAuth.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</captureGivenAuth>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfCaptureGivenAuthResponse[0].litleTxnId);
        }

        [Test]
        public void testCredit()
        {
            credit credit = new credit();
            credit.orderId = "12344";
            credit.amount = 106;
            credit.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            credit.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<credit.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</credit>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><creditResponse><litleTxnId>123</litleTxnId></creditResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addCredit(credit);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfCreditResponse[0].litleTxnId);
        }

        [Test]
        public void testEcheckCredit()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12;
            echeckcredit.litleTxnId = 123456789101112;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<echeckCredit.*?<litleTxnId>123456789101112</litleTxnId>.*?</echeckCredit>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><echeckCreditResponse><litleTxnId>123</litleTxnId></echeckCreditResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addEcheckCredit(echeckcredit);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfEcheckCreditResponse[0].litleTxnId);
        }

        [Test]
        public void testEcheckRedeposit()
        {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<echeckRedeposit.*?<litleTxnId>123456</litleTxnId>.*?</echeckRedeposit>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><echeckRedepositResponse><litleTxnId>123</litleTxnId></echeckRedepositResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfEcheckRedepositResponse[0].litleTxnId);
        }

        [Test]
        public void testEcheckSale()
        {
            echeckSale echecksale = new echeckSale();
            echecksale.orderId = "12345";
            echecksale.amount = 123456;
            echecksale.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echecksale.echeck = echeck;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            echecksale.billToAddress = contact;


            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<echeckSale.*?<echeck>.*?<accNum>12345657890</accNum>.*?</echeck>.*?</echeckSale>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><echeckSalesResponse><litleTxnId>123</litleTxnId></echeckSalesResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addEcheckSale(echecksale);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfEcheckSalesResponse[0].litleTxnId);
        }

        [Test]
        public void testEcheckVerification()
        {
            echeckVerification echeckverification = new echeckVerification();
            echeckverification.orderId = "12345";
            echeckverification.amount = 123456;
            echeckverification.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckverification.echeck = echeck;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            echeckverification.billToAddress = contact;


            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<echeckVerification.*?<echeck>.*?<accNum>12345657890</accNum>.*?</echeck>.*?</echeckVerification>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><echeckVerificationResponse><litleTxnId>123</litleTxnId></echeckVerificationResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addEcheckVerification(echeckverification);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfEcheckVerificationResponse[0].litleTxnId);
        }

        [Test]
        public void testForceCapture()
        {
            forceCapture forcecapture = new forceCapture();
            forcecapture.orderId = "12344";
            forcecapture.amount = 106;
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            forcecapture.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<forceCapture.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</forceCapture>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><forceCaptureResponse><litleTxnId>123</litleTxnId></forceCaptureResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addForceCapture(forcecapture);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfForceCaptureResponse[0].litleTxnId);
        }

        [Test]
        public void testSale()
        {
            sale sale = new sale();
            sale.orderId = "12344";
            sale.amount = 106;
            sale.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            sale.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<sale.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</sale>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><saleResponse><litleTxnId>123</litleTxnId></saleResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addSale(sale);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfSaleResponse[0].litleTxnId);
        }

        [Test]
        public void testToken()
        {
            registerTokenRequestType token = new registerTokenRequestType();
            token.orderId = "12344";
            token.accountNumber = "1233456789103801";


            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<registerTokenRequest.*?<accountNumber>1233456789103801</accountNumber>.*?</registerTokenRequest>.*?</batchRequest>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><registerTokenResponse><litleTxnId>123</litleTxnId></registerTokenResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addRegisterTokenRequest(token);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfRegisterTokenResponse[0].litleTxnId);


        }

        [Test]
        public void testLitleOnlineException()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<authorization.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='1' message='Error validating xml data against the schema' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            try
            {

                litle.setCommunication(mockedCommunication);
                litleBatchRequest litleBatchRequest = new litleBatchRequest();
                litleBatchRequest.addAuthorization(authorization);
                litle.addBatch(litleBatchRequest);

                litleResponse litleResponse = litle.sendToLitle();
            }
            catch (LitleOnlineException e)
            {
                Assert.AreEqual("Error validating xml data against the schema", e.Message);
            }
        }

        [Test]
        public void testInvalidOperationException()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<authorization.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("no xml");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            try
            {
                litle.setCommunication(mockedCommunication);
                litleBatchRequest litleBatchRequest = new litleBatchRequest();
                litleBatchRequest.addAuthorization(authorization);
                litle.addBatch(litleBatchRequest);

                litleResponse litleResponse = litle.sendToLitle();
            }
            catch (LitleOnlineException e)
            {
                Assert.AreEqual("Error validating xml data against the schema", e.Message);
            }
        }

        [Test]
        public void testDefaultReportGroup()
        {
            authorization authorization = new authorization();
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?<batchRequest.*?<authorization.*? reportGroup=\"Default Report Group\">.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?</batchRequest.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><batchResponse><authorizationResponse reportGroup='Default Report Group'></authorizationResponse></batchResponse></litleResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitle();
            Assert.AreEqual("Default Report Group", litleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse[0].reportGroup);
        }

        [Test]
        public void testLargeBatch()
        {
            authorization authorization = new authorization();
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            sale sale = new sale();
            sale.orderId = "12344";
            sale.amount = 106;
            sale.orderSource = orderSourceType.ecommerce;
            sale.card = card;

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addAuthorization(authorization);
            litleBatchRequest.addSale(sale);

            litleBatchRequest litleBatchRequest2 = new litleBatchRequest();
            litleBatchRequest2.addAuthorization(authorization);
            litleBatchRequest2.addSale(sale);

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleRequest.*?" + 
                "<batchRequest.*?<authorization.*? reportGroup=\"Default Report Group\">.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?" + 
                "<sale.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</sale>*?</batchRequest.*?" +
                "<batchRequest.*?<authorization.*? reportGroup=\"Default Report Group\">.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?" +
                "<sale.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</sale>*?</batchRequest.*?"
                , RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                
                .Returns("<litleResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'>"+
                "<batchResponse><authorizationResponse reportGroup='Default Report Group'><litleTxnId>123</litleTxnId></authorizationResponse><saleResponse><litleTxnId>123</litleTxnId></saleResponse></batchResponse>" + 
                "<batchResponse><authorizationResponse reportGroup='Default Report Group'><litleTxnId>123</litleTxnId></authorizationResponse><saleResponse><litleTxnId>123</litleTxnId></saleResponse></batchResponse></litleResponse>");
            
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);

            litle.addBatch(litleBatchRequest);
            litle.addBatch(litleBatchRequest2);

            litleResponse litleResponse = litle.sendToLitle();
            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfSaleResponse[0].litleTxnId);
            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[1].listOfSaleResponse[0].litleTxnId);
            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse[0].litleTxnId);
            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[1].listOfAuthorizationResponse[0].litleTxnId);
        }

        [Test]
        public void testSerializeToFile()
        {
            string filePath = "";
            authorization authorization = new authorization();
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addAuthorization(authorization);
            filePath = litle.addBatch(litleBatchRequest);

            string resultFile = litle.SerializeToFile(filePath);

            Assert.IsTrue(resultFile.Contains(DateTime.Now.ToString("MM-dd-yyyy")));
        }
    }
}

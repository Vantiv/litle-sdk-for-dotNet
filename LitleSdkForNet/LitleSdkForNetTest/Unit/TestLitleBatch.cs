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
        private const string timeFormat = "MM-dd-yyyy_HH-mm-ss-ffff_";
        private const string timeRegex = "[0-1][0-9]-[0-3][0-9]-[0-9]{4}_[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{4}_";
        private const string batchNameRegex = timeRegex + "[A-Z]{8}";

        [SetUp]
        public void SetUpLitle()
        {
            litle = new LitleBatch();
        }

        [Test]
        public void testAuth()
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

            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            litleResponse mockLitleResponse = new litleResponse();

            authorizationResponse mockAuthorizationResponse1 = new authorizationResponse();
            mockAuthorizationResponse1.litleTxnId = 123;
            authorizationResponse mockAuthorizationResponse2 = new authorizationResponse();
            mockAuthorizationResponse2.litleTxnId = 124;

            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse.Add(mockAuthorizationResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse.Add(mockAuthorizationResponse2);

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addAuthorization(authorization);
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse[1].litleTxnId);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
        }

        [Test]
        public void testAuthReversal()
        {
            authReversal authreversal = new authReversal();
            authreversal.litleTxnId = 12345678000;
            authreversal.amount = 106;
            authreversal.payPalNotes = "Notes";

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            authReversalResponse mockAuthReversalResponse1 = new authReversalResponse();
            mockAuthReversalResponse1.litleTxnId = 123;
            authReversalResponse mockAuthReversalResponse2 = new authReversalResponse();
            mockAuthReversalResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfAuthReversalResponse.Add(mockAuthReversalResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfAuthReversalResponse.Add(mockAuthReversalResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addAuthReversal(authreversal);
            litleBatchRequest.addAuthReversal(authreversal);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfAuthReversalResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfAuthReversalResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
        }

        [Test]
        public void testCapture()
        {
            capture capture = new capture();
            capture.litleTxnId = 12345678000;
            capture.amount = 106;

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            captureResponse mockCaptureResponse1 = new captureResponse();
            mockCaptureResponse1.litleTxnId = 123;
            captureResponse mockCaptureResponse2 = new captureResponse();
            mockCaptureResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfCaptureResponse.Add(mockCaptureResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfCaptureResponse.Add(mockCaptureResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addCapture(capture);
            litleBatchRequest.addCapture(capture);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfCaptureResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfCaptureResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
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

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            captureGivenAuthResponse mockCaptureGivenAuthResponse1 = new captureGivenAuthResponse();
            mockCaptureGivenAuthResponse1.litleTxnId = 123;
            captureGivenAuthResponse mockCaptureGivenAuthResponse2 = new captureGivenAuthResponse();
            mockCaptureGivenAuthResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfCaptureGivenAuthResponse.Add(mockCaptureGivenAuthResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfCaptureGivenAuthResponse.Add(mockCaptureGivenAuthResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfCaptureGivenAuthResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfCaptureGivenAuthResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
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

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            creditResponse mockCreditResponse1 = new creditResponse();
            mockCreditResponse1.litleTxnId = 123;
            creditResponse mockCreditResponse2 = new creditResponse();
            mockCreditResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfCreditResponse.Add(mockCreditResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfCreditResponse.Add(mockCreditResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addCredit(credit);
            litleBatchRequest.addCredit(credit);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfCreditResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfCreditResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
        }

        [Test]
        public void testEcheckCredit()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12;
            echeckcredit.litleTxnId = 123456789101112;

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            echeckCreditResponse mockEcheckCreditResponse1 = new echeckCreditResponse();
            mockEcheckCreditResponse1.litleTxnId = 123;
            echeckCreditResponse mockEcheckCreditResponse2 = new echeckCreditResponse();
            mockEcheckCreditResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfEcheckCreditResponse.Add(mockEcheckCreditResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfEcheckCreditResponse.Add(mockEcheckCreditResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addEcheckCredit(echeckcredit);
            litleBatchRequest.addEcheckCredit(echeckcredit);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfEcheckCreditResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfEcheckCreditResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
        }

        [Test]
        public void testEcheckRedeposit()
        {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            echeckRedepositResponse mockEcheckRedepositResponse1 = new echeckRedepositResponse();
            mockEcheckRedepositResponse1.litleTxnId = 123;
            echeckRedepositResponse mockEcheckRedepositResponse2 = new echeckRedepositResponse();
            mockEcheckRedepositResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfEcheckRedepositResponse.Add(mockEcheckRedepositResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfEcheckRedepositResponse.Add(mockEcheckRedepositResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfEcheckRedepositResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfEcheckRedepositResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
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

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            echeckSalesResponse mockEcheckSalesResponse1 = new echeckSalesResponse();
            mockEcheckSalesResponse1.litleTxnId = 123;
            echeckSalesResponse mockEcheckSalesResponse2 = new echeckSalesResponse();
            mockEcheckSalesResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfEcheckSalesResponse.Add(mockEcheckSalesResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfEcheckSalesResponse.Add(mockEcheckSalesResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addEcheckSale(echecksale);
            litleBatchRequest.addEcheckSale(echecksale);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfEcheckSalesResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfEcheckSalesResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
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

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            echeckVerificationResponse mockEcheckVerificationResponse1 = new echeckVerificationResponse();
            mockEcheckVerificationResponse1.litleTxnId = 123;
            echeckVerificationResponse mockEcheckVerificationResponse2 = new echeckVerificationResponse();
            mockEcheckVerificationResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfEcheckVerificationResponse.Add(mockEcheckVerificationResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfEcheckVerificationResponse.Add(mockEcheckVerificationResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addEcheckVerification(echeckverification);
            litleBatchRequest.addEcheckVerification(echeckverification);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfEcheckVerificationResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfEcheckVerificationResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
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

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            forceCaptureResponse mockForceCaptureResponse1 = new forceCaptureResponse();
            mockForceCaptureResponse1.litleTxnId = 123;
            forceCaptureResponse mockForceCaptureResponse2 = new forceCaptureResponse();
            mockForceCaptureResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfForceCaptureResponse.Add(mockForceCaptureResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfForceCaptureResponse.Add(mockForceCaptureResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addForceCapture(forcecapture);
            litleBatchRequest.addForceCapture(forcecapture);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfForceCaptureResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfForceCaptureResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
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

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            saleResponse mockSaleResponse1 = new saleResponse();
            mockSaleResponse1.litleTxnId = 123;
            saleResponse mockSaleResponse2 = new saleResponse();
            mockSaleResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfSaleResponse.Add(mockSaleResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfSaleResponse.Add(mockSaleResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addSale(sale);
            litleBatchRequest.addSale(sale);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfSaleResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfSaleResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
        }

        [Test]
        public void testToken()
        {
            registerTokenRequestType token = new registerTokenRequestType();
            token.orderId = "12344";
            token.accountNumber = "1233456789103801";

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            registerTokenResponse mockRegisterTokenResponse1 = new registerTokenResponse();
            mockRegisterTokenResponse1.litleTxnId = 123;
            registerTokenResponse mockRegisterTokenResponse2 = new registerTokenResponse();
            mockRegisterTokenResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfRegisterTokenResponse.Add(mockRegisterTokenResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfRegisterTokenResponse.Add(mockRegisterTokenResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addRegisterTokenRequest(token);
            litleBatchRequest.addRegisterTokenRequest(token);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfRegisterTokenResponse[0].litleTxnId);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfRegisterTokenResponse[1].litleTxnId);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
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

            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            authorizationResponse mockAuthorizationResponse1 = new authorizationResponse();
            mockAuthorizationResponse1.litleTxnId = 123;
            authorizationResponse mockAuthorizationResponse2 = new authorizationResponse();
            mockAuthorizationResponse2.litleTxnId = 124;
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse.Add(mockAuthorizationResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse.Add(mockAuthorizationResponse2);
            mockLitleResponse.message = "Error validating xml data against the schema";
            mockLitleResponse.response = "1";

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            try
            {
                litle.setCommunication(mockedCommunications);
                litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
                litleBatchRequest litleBatchRequest = new litleBatchRequest();
                litleBatchRequest.addAuthorization(authorization);
                litleBatchRequest.addAuthorization(authorization);
                litle.addBatch(litleBatchRequest);

                string batchFileName = litle.sendToLitle();
                litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
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


            litleResponse mockLitleResponse = null;
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            try
            {
                litle.setCommunication(mockedCommunications);
                litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
                litleBatchRequest litleBatchRequest = new litleBatchRequest();
                litleBatchRequest.addAuthorization(authorization);
                litleBatchRequest.addAuthorization(authorization);
                litle.addBatch(litleBatchRequest);

                string batchFileName = litle.sendToLitle();
                litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
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


            litleResponse mockLitleResponse = new litleResponse();
            var mockCommunications = new Mock<Communications>();
            var mockXml = new Mock<litleXmlSerializer>();

            authorizationResponse mockAuthorizationResponse1 = new authorizationResponse();
            mockAuthorizationResponse1.litleTxnId = 123;
            mockAuthorizationResponse1.reportGroup = "Default Report Group";
            authorizationResponse mockAuthorizationResponse2 = new authorizationResponse();
            mockAuthorizationResponse2.litleTxnId = 124;
            mockAuthorizationResponse2.reportGroup = "Default Report Group";
            mockLitleResponse.listOfLitleBatchResponse.Add(new litleBatchResponse());
            mockLitleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse.Add(mockAuthorizationResponse1);
            mockLitleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse.Add(mockAuthorizationResponse2);

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>()))
                .Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addAuthorization(authorization);
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse litleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);

            Assert.AreEqual(123, litleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse[0].litleTxnId);
            Assert.AreEqual("Default Report Group", litleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse[0].reportGroup);
            Assert.AreEqual(124, litleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse[1].litleTxnId);
            Assert.AreEqual("Default Report Group", litleResponse.listOfLitleBatchResponse[0].listOfAuthorizationResponse[0].reportGroup);
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsRegex(batchNameRegex, RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), It.IsRegex(batchNameRegex, RegexOptions.Singleline)));
        }

        [Test]
        public void testSerializeToFile()
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

            var mockLitleTime = new Mock<litleTime>();
            mockLitleTime.Setup(litleTime => litleTime.getCurrentTime(It.Is<String>(resultFormat => resultFormat == timeFormat))).Returns("01-01-1960_01-22-30-1234_");
            litleTime mockedLitleTime = mockLitleTime.Object;
            litle.setLitleTime(mockedLitleTime);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            string resultFile = litle.Serialize();

            Assert.IsTrue(resultFile.Contains("01-01-1960_01-22-30-1234_"));
        }
    }
}

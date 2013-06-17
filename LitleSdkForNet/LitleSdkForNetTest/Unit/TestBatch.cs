using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;
using Moq.Language.Flow;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestBatch
    {
        private LitleBatch litle;
        private const string timeFormat = "MM-dd-yyyy_HH-mm-ss-ffff_";
        private const string timeRegex = "[0-1][0-9]-[0-3][0-9]-[0-9]{4}_[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{4}_";
        private const string batchNameRegex = timeRegex + "[A-Z]{8}";
        private const string mockFileName = "TheRainbow.xml";
        private const string mockFilePath = "C:\\Somewhere\\\\Over\\\\" + mockFileName;

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
            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            authorizationResponse mockAuthorizationResponse1 = new authorizationResponse();
            mockAuthorizationResponse1.litleTxnId = 123;
            authorizationResponse mockAuthorizationResponse2 = new authorizationResponse();
            mockAuthorizationResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextAuthorizationResponse())
                .Returns(mockAuthorizationResponse1)
                .Returns(mockAuthorizationResponse2)
                .Returns((authorizationResponse)null);
            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addAuthorization(authorization);
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();

            Assert.AreSame(mockedLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual(123, actualLitleBatchResponse.nextAuthorizationResponse().litleTxnId);
            Assert.AreEqual(124, actualLitleBatchResponse.nextAuthorizationResponse().litleTxnId);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testAuthReversal()
        {
            authReversal authreversal = new authReversal();
            authreversal.litleTxnId = 12345678000;
            authreversal.amount = 106;
            authreversal.payPalNotes = "Notes";

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            authReversalResponse mockAuthReversalResponse1 = new authReversalResponse();
            mockAuthReversalResponse1.litleTxnId = 123;
            authReversalResponse mockAuthReversalResponse2 = new authReversalResponse();
            mockAuthReversalResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextAuthReversalResponse())
                .Returns(mockAuthReversalResponse1)
                .Returns(mockAuthReversalResponse2)
                .Returns((authReversalResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addAuthReversal(authreversal);
            litleBatchRequest.addAuthReversal(authreversal);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            authReversalResponse actualAuthReversalResponse1 = actualLitleBatchResponse.nextAuthReversalResponse();
            authReversalResponse actualAuthReversalResponse2 = actualLitleBatchResponse.nextAuthReversalResponse();
            authReversalResponse nullAuthReversalResponse = actualLitleBatchResponse.nextAuthReversalResponse();

            Assert.AreEqual(123, actualAuthReversalResponse1.litleTxnId);
            Assert.AreEqual(124, actualAuthReversalResponse2.litleTxnId);
            Assert.IsNull(nullAuthReversalResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testCapture()
        {
            capture capture = new capture();
            capture.litleTxnId = 12345678000;
            capture.amount = 106;

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            captureResponse mockCaptureResponse1 = new captureResponse();
            mockCaptureResponse1.litleTxnId = 123;
            captureResponse mockCaptureResponse2 = new captureResponse();
            mockCaptureResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextCaptureResponse())
                .Returns(mockCaptureResponse1)
                .Returns(mockCaptureResponse2)
                .Returns((captureResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addCapture(capture);
            litleBatchRequest.addCapture(capture);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            captureResponse actualCaptureResponse1 = actualLitleBatchResponse.nextCaptureResponse();
            captureResponse actualCaptureResponse2 = actualLitleBatchResponse.nextCaptureResponse();
            captureResponse nullCaptureResponse = actualLitleBatchResponse.nextCaptureResponse();

            Assert.AreEqual(123, actualCaptureResponse1.litleTxnId);
            Assert.AreEqual(124, actualCaptureResponse2.litleTxnId);
            Assert.IsNull(nullCaptureResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
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

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            captureGivenAuthResponse mockCaptureGivenAuthResponse1 = new captureGivenAuthResponse();
            mockCaptureGivenAuthResponse1.litleTxnId = 123;
            captureGivenAuthResponse mockCaptureGivenAuthResponse2 = new captureGivenAuthResponse();
            mockCaptureGivenAuthResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextCaptureGivenAuthResponse())
                .Returns(mockCaptureGivenAuthResponse1)
                .Returns(mockCaptureGivenAuthResponse2)
                .Returns((captureGivenAuthResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            captureGivenAuthResponse actualCaptureGivenAuthReponse1 = actualLitleBatchResponse.nextCaptureGivenAuthResponse();
            captureGivenAuthResponse actualCaptureGivenAuthReponse2 = actualLitleBatchResponse.nextCaptureGivenAuthResponse();
            captureGivenAuthResponse nullCaptureGivenAuthReponse = actualLitleBatchResponse.nextCaptureGivenAuthResponse();

            Assert.AreEqual(123, actualCaptureGivenAuthReponse1.litleTxnId);
            Assert.AreEqual(124, actualCaptureGivenAuthReponse2.litleTxnId);
            Assert.IsNull(nullCaptureGivenAuthReponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
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

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            creditResponse mockCreditResponse1 = new creditResponse();
            mockCreditResponse1.litleTxnId = 123;
            creditResponse mockCreditResponse2 = new creditResponse();
            mockCreditResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextCreditResponse())
                .Returns(mockCreditResponse1)
                .Returns(mockCreditResponse2)
                .Returns((creditResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addCredit(credit);
            litleBatchRequest.addCredit(credit);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            creditResponse actualCreditReponse1 = actualLitleBatchResponse.nextCreditResponse();
            creditResponse actualCreditReponse2 = actualLitleBatchResponse.nextCreditResponse();
            creditResponse nullCreditReponse1 = actualLitleBatchResponse.nextCreditResponse();

            Assert.AreEqual(123, actualCreditReponse1.litleTxnId);
            Assert.AreEqual(124, actualCreditReponse2.litleTxnId);
            Assert.IsNull(nullCreditReponse1);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testEcheckCredit()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12;
            echeckcredit.litleTxnId = 123456789101112;

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            echeckCreditResponse mockEcheckCreditResponse1 = new echeckCreditResponse();
            mockEcheckCreditResponse1.litleTxnId = 123;
            echeckCreditResponse mockEcheckCreditResponse2 = new echeckCreditResponse();
            mockEcheckCreditResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextEcheckCreditResponse())
                .Returns(mockEcheckCreditResponse1)
                .Returns(mockEcheckCreditResponse2)
                .Returns((echeckCreditResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addEcheckCredit(echeckcredit);
            litleBatchRequest.addEcheckCredit(echeckcredit);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            echeckCreditResponse actualEcheckCreditResponse1 = actualLitleBatchResponse.nextEcheckCreditResponse();
            echeckCreditResponse actualEcheckCreditResponse2 = actualLitleBatchResponse.nextEcheckCreditResponse();
            echeckCreditResponse nullEcheckCreditResponse = actualLitleBatchResponse.nextEcheckCreditResponse();

            Assert.AreEqual(123, actualEcheckCreditResponse1.litleTxnId);
            Assert.AreEqual(124, actualEcheckCreditResponse2.litleTxnId);
            Assert.IsNull(nullEcheckCreditResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testEcheckRedeposit()
        {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            echeckRedepositResponse mockEcheckRedepositResponse1 = new echeckRedepositResponse();
            mockEcheckRedepositResponse1.litleTxnId = 123;
            echeckRedepositResponse mockEcheckRedepositResponse2 = new echeckRedepositResponse();
            mockEcheckRedepositResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextEcheckRedepositResponse())
                .Returns(mockEcheckRedepositResponse1)
                .Returns(mockEcheckRedepositResponse2)
                .Returns((echeckRedepositResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            echeckRedepositResponse actualEcheckRedepositResponse1 = actualLitleBatchResponse.nextEcheckRedepositResponse();
            echeckRedepositResponse actualEcheckRedepositResponse2 = actualLitleBatchResponse.nextEcheckRedepositResponse();
            echeckRedepositResponse nullEcheckRedepositResponse = actualLitleBatchResponse.nextEcheckRedepositResponse();

            Assert.AreEqual(123, actualEcheckRedepositResponse1.litleTxnId);
            Assert.AreEqual(124, actualEcheckRedepositResponse2.litleTxnId);
            Assert.IsNull(nullEcheckRedepositResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
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

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            echeckSalesResponse mockEcheckSalesResponse1 = new echeckSalesResponse();
            mockEcheckSalesResponse1.litleTxnId = 123;
            echeckSalesResponse mockEcheckSalesResponse2 = new echeckSalesResponse();
            mockEcheckSalesResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextEcheckSalesResponse())
                .Returns(mockEcheckSalesResponse1)
                .Returns(mockEcheckSalesResponse2)
                .Returns((echeckSalesResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addEcheckSale(echecksale);
            litleBatchRequest.addEcheckSale(echecksale);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            echeckSalesResponse actualEcheckSalesResponse1 = actualLitleBatchResponse.nextEcheckSalesResponse();
            echeckSalesResponse actualEcheckSalesResponse2 = actualLitleBatchResponse.nextEcheckSalesResponse();
            echeckSalesResponse nullEcheckSalesResponse = actualLitleBatchResponse.nextEcheckSalesResponse();

            Assert.AreEqual(123, actualEcheckSalesResponse1.litleTxnId);
            Assert.AreEqual(124, actualEcheckSalesResponse2.litleTxnId);
            Assert.IsNull(nullEcheckSalesResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
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

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            echeckVerificationResponse mockEcheckVerificationResponse1 = new echeckVerificationResponse();
            mockEcheckVerificationResponse1.litleTxnId = 123;
            echeckVerificationResponse mockEcheckVerificationResponse2 = new echeckVerificationResponse();
            mockEcheckVerificationResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextEcheckVerificationResponse())
                .Returns(mockEcheckVerificationResponse1)
                .Returns(mockEcheckVerificationResponse2)
                .Returns((echeckVerificationResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addEcheckVerification(echeckverification);
            litleBatchRequest.addEcheckVerification(echeckverification);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            echeckVerificationResponse actualEcheckVerificationResponse1 = actualLitleBatchResponse.nextEcheckVerificationResponse();
            echeckVerificationResponse actualEcheckVerificationResponse2 = actualLitleBatchResponse.nextEcheckVerificationResponse();
            echeckVerificationResponse nullEcheckVerificationResponse = actualLitleBatchResponse.nextEcheckVerificationResponse();

            Assert.AreEqual(123, actualEcheckVerificationResponse1.litleTxnId);
            Assert.AreEqual(124, actualEcheckVerificationResponse2.litleTxnId);
            Assert.IsNull(nullEcheckVerificationResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
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

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            forceCaptureResponse mockForceCaptureResponse1 = new forceCaptureResponse();
            mockForceCaptureResponse1.litleTxnId = 123;
            forceCaptureResponse mockForceCaptureResponse2 = new forceCaptureResponse();
            mockForceCaptureResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextForceCaptureResponse())
                .Returns(mockForceCaptureResponse1)
                .Returns(mockForceCaptureResponse2)
                .Returns((forceCaptureResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addForceCapture(forcecapture);
            litleBatchRequest.addForceCapture(forcecapture);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            forceCaptureResponse actualForceCaptureResponse1 = actualLitleBatchResponse.nextForceCaptureResponse();
            forceCaptureResponse actualForceCaptureResponse2 = actualLitleBatchResponse.nextForceCaptureResponse();
            forceCaptureResponse nullForceCaptureResponse = actualLitleBatchResponse.nextForceCaptureResponse();

            Assert.AreEqual(123, actualForceCaptureResponse1.litleTxnId);
            Assert.AreEqual(124, actualForceCaptureResponse2.litleTxnId);
            Assert.IsNull(nullForceCaptureResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
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

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            saleResponse mockSaleResponse1 = new saleResponse();
            mockSaleResponse1.litleTxnId = 123;
            saleResponse mockSaleResponse2 = new saleResponse();
            mockSaleResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextSaleResponse())
                .Returns(mockSaleResponse1)
                .Returns(mockSaleResponse2)
                .Returns((saleResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addSale(sale);
            litleBatchRequest.addSale(sale);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            saleResponse actualSaleResponse1 = actualLitleBatchResponse.nextSaleResponse();
            saleResponse actualSaleResponse2 = actualLitleBatchResponse.nextSaleResponse();
            saleResponse nullSaleResponse = actualLitleBatchResponse.nextSaleResponse();

            Assert.AreEqual(123, actualSaleResponse1.litleTxnId);
            Assert.AreEqual(124, actualSaleResponse2.litleTxnId);
            Assert.IsNull(nullSaleResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testToken()
        {
            registerTokenRequestType token = new registerTokenRequestType();
            token.orderId = "12344";
            token.accountNumber = "1233456789103801";

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            registerTokenResponse mockRegisterTokenResponse1 = new registerTokenResponse();
            mockRegisterTokenResponse1.litleTxnId = 123;
            registerTokenResponse mockRegisterTokenResponse2 = new registerTokenResponse();
            mockRegisterTokenResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextRegisterTokenResponse())
                .Returns(mockRegisterTokenResponse1)
                .Returns(mockRegisterTokenResponse2)
                .Returns((registerTokenResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addRegisterTokenRequest(token);
            litleBatchRequest.addRegisterTokenRequest(token);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            registerTokenResponse actualRegisterTokenResponse1 = actualLitleBatchResponse.nextRegisterTokenResponse();
            registerTokenResponse actualRegisterTokenResponse2 = actualLitleBatchResponse.nextRegisterTokenResponse();
            registerTokenResponse nullRegisterTokenResponse = actualLitleBatchResponse.nextRegisterTokenResponse();

            Assert.AreEqual(123, actualRegisterTokenResponse1.litleTxnId);
            Assert.AreEqual(124, actualRegisterTokenResponse2.litleTxnId);
            Assert.IsNull(nullRegisterTokenResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
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

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            authorizationResponse mockAuthorizationResponse1 = new authorizationResponse();
            mockAuthorizationResponse1.litleTxnId = 123;
            authorizationResponse mockAuthorizationResponse2 = new authorizationResponse();
            mockAuthorizationResponse2.litleTxnId = 124;

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextAuthorizationResponse())
                .Returns(mockAuthorizationResponse1)
                .Returns(mockAuthorizationResponse2)
                .Returns((authorizationResponse)null);

            litleResponse mockedLitleResponse = mockLitleResponse.Object;
            mockedLitleResponse.message = "Error validating xml data against the schema";
            mockedLitleResponse.response = "1";

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            try
            {
                litle.setCommunication(mockedCommunications);
                litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
                litle.setLitleFile(mockedLitleFile);
                litleBatchRequest litleBatchRequest = new litleBatchRequest();
                litleBatchRequest.setLitleFile(mockedLitleFile);
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
            var mockLitleFile = new Mock<litleFile>();

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            try
            {
                litle.setCommunication(mockedCommunications);
                litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
                litle.setLitleFile(mockedLitleFile);
                litleBatchRequest litleBatchRequest = new litleBatchRequest();
                litleBatchRequest.setLitleFile(mockedLitleFile);
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

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockCommunications = new Mock<Communications>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            var mockLitleFile = new Mock<litleFile>();

            authorizationResponse mockAuthorizationResponse1 = new authorizationResponse();
            mockAuthorizationResponse1.litleTxnId = 123;
            mockAuthorizationResponse1.reportGroup = "Default Report Group";
            authorizationResponse mockAuthorizationResponse2 = new authorizationResponse();
            mockAuthorizationResponse2.litleTxnId = 124;
            mockAuthorizationResponse2.reportGroup = "Default Report Group";

            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextAuthorizationResponse())
                .Returns(mockAuthorizationResponse1)
                .Returns(mockAuthorizationResponse2)
                .Returns((authorizationResponse)null);

            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addAuthorization(authorization);
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            authorizationResponse actualAuthorizationResponse1 = actualLitleBatchResponse.nextAuthorizationResponse();
            authorizationResponse actualAuthorizationResponse2 = actualLitleBatchResponse.nextAuthorizationResponse();
            authorizationResponse nullAuthorizationResponse = actualLitleBatchResponse.nextAuthorizationResponse();

            Assert.AreEqual(123, actualAuthorizationResponse1.litleTxnId);
            Assert.AreEqual("Default Report Group", actualAuthorizationResponse1.reportGroup);
            Assert.AreEqual(124, actualAuthorizationResponse2.litleTxnId);
            Assert.AreEqual("Default Report Group", actualAuthorizationResponse2.reportGroup);
            Assert.IsNull(nullAuthorizationResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testSerialize()
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
            var mockLitleFile = new Mock<litleFile>();

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            litleFile mockedLitleFile = mockLitleFile.Object;

            mockLitleTime.Setup(litleTime => litleTime.getCurrentTime(It.Is<String>(resultFormat => resultFormat == timeFormat))).Returns("01-01-1960_01-22-30-1234_");
            litleTime mockedLitleTime = mockLitleTime.Object;
            litle.setLitleTime(mockedLitleTime);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            string resultFile = litle.Serialize();

            Assert.IsTrue(resultFile.Equals(mockFilePath));
        }
    }
}

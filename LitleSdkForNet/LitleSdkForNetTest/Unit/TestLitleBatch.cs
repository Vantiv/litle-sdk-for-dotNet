using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;
using Moq.Language.Flow;
using System.Xml;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestLitleBatch
    {
        private LitleBatch litle;
        private const string timeFormat = "MM-dd-yyyy_HH-mm-ss-ffff_";
        private const string timeRegex = "[0-1][0-9]-[0-3][0-9]-[0-9]{4}_[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{4}_";
        private const string batchNameRegex = timeRegex + "[A-Z]{8}";
        private const string mockFileName = "TheRainbow.xml";
        private const string mockFilePath = "C:\\Somewhere\\\\Over\\\\" + mockFileName;

        private Mock<litleTime> mockLitleTime;
        private Mock<litleFile> mockLitleFile;
        private Mock<Communications> mockCommunications;
        private Mock<XmlReader> mockXmlReader;

        [TestFixtureSetUp]
        public void setUp()
        {
            mockLitleTime = new Mock<litleTime>();
            mockLitleTime.Setup(litleTime => litleTime.getCurrentTime(It.Is<String>(resultFormat => resultFormat == timeFormat))).Returns("01-01-1960_01-22-30-1234_");

            mockLitleFile = new Mock<litleFile>();
            mockLitleFile.Setup(litleFile => litleFile.createDirectory(It.IsAny<String>()));
            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<litleTime>(), It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);

            mockCommunications = new Mock<Communications>();
        }

        [SetUp]
        public void setUpBeforeEachTest()
        {
            litle = new LitleBatch();

            mockXmlReader = new Mock<XmlReader>();
            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadToFollowing(It.IsAny<String>())).Returns(true).Returns(true).Returns(false);
            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadState).Returns(ReadState.Initial).Returns(ReadState.Interactive).Returns(ReadState.Closed);
        }

        [Test]
        public void testAccountUpdate()
        {
            accountUpdate accountUpdate = new accountUpdate();
            accountUpdate.reportGroup = "Planets";
            accountUpdate.orderId = "12344";
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            accountUpdate.card = card;

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleBatchResponse = new Mock<litleBatchResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            accountUpdateResponse mockAccountUpdateResponse1 = new accountUpdateResponse();
            mockAccountUpdateResponse1.litleTxnId = 123;
            mockAccountUpdateResponse1.response = "000";
            accountUpdateResponse mockAccountUpdateResponse2 = new accountUpdateResponse();
            mockAccountUpdateResponse2.litleTxnId = 124;
            mockAccountUpdateResponse2.response = "000";
            mockLitleBatchResponse.SetupSequence(litleBatchResponse => litleBatchResponse.nextAccountUpdateResponse())
                .Returns(mockAccountUpdateResponse1)
                .Returns(mockAccountUpdateResponse2)
                .Returns((accountUpdateResponse)null);
            litleBatchResponse mockedLitleBatchResponse = mockLitleBatchResponse.Object;

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addAccountUpdate(accountUpdate);
            litleBatchRequest.addAccountUpdate(accountUpdate);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            accountUpdateResponse actualAccountUpdateResponse1 = actualLitleBatchResponse.nextAccountUpdateResponse();
            accountUpdateResponse actualAccountUpdateResponse2 = actualLitleBatchResponse.nextAccountUpdateResponse();
            accountUpdateResponse nullAccountUpdateResponse = actualLitleBatchResponse.nextAccountUpdateResponse();

            Assert.AreSame(mockedLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual(123, actualAccountUpdateResponse1.litleTxnId);
            Assert.AreEqual("000", actualAccountUpdateResponse1.response);
            Assert.AreEqual(124, actualAccountUpdateResponse2.litleTxnId);
            Assert.AreEqual("000", actualAccountUpdateResponse2.response);
            Assert.IsNull(nullAccountUpdateResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
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

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<authorizationResponse id=\"\" reportGroup=\"Planets\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>123</litleTxnId><orderId>123</orderId><response>000</response><responseTime>2013-06-19T19:54:42</responseTime><message>Approved</message><authCode>123457</authCode><fraudResult><avsResult>00</avsResult></fraudResult><tokenResponse><litleToken>1711000103054242</litleToken><tokenResponseCode>802</tokenResponseCode><tokenMessage>Account number was previously registered</tokenMessage><type>VI</type><bin>424242</bin></tokenResponse></authorizationResponse>")
             .Returns("<authorizationResponse id=\"\" reportGroup=\"Planets\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>124</litleTxnId><orderId>124</orderId><response>000</response><responseTime>2013-06-19T19:54:42</responseTime><message>Approved</message><authCode>123457</authCode><fraudResult><avsResult>00</avsResult></fraudResult><tokenResponse><litleToken>1711000103054242</litleToken><tokenResponseCode>802</tokenResponseCode><tokenMessage>Account number was previously registered</tokenMessage><type>VI</type><bin>424242</bin></tokenResponse></authorizationResponse>");

            litleBatchResponse mockLitleBatchResponse = new litleBatchResponse();
            mockLitleBatchResponse.setAuthorizationResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

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

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual(123, actualLitleBatchResponse.nextAuthorizationResponse().litleTxnId);
            Assert.AreEqual(124, actualLitleBatchResponse.nextAuthorizationResponse().litleTxnId);
            Assert.IsNull(actualLitleBatchResponse.nextAuthorizationResponse());

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<authReversalResponse id=\"123\" customerId=\"Customer Id\" reportGroup=\"Auth Reversals\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>123</litleTxnId><orderId>abc123</orderId><response>000</response><responseTime>2011-08-30T13:15:43</responseTime><message>Approved</message></authReversalResponse>")
             .Returns("<authReversalResponse id=\"123\" customerId=\"Customer Id\" reportGroup=\"Auth Reversals\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>124</litleTxnId><orderId>abc123</orderId><response>000</response><responseTime>2011-08-30T13:15:43</responseTime><message>Approved</message></authReversalResponse>");

            litleBatchResponse mockLitleBatchResponse = new litleBatchResponse();
            mockLitleBatchResponse.setAuthReversalResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunications = mockCommunications.Object;
            litle.setCommunication(mockedCommunications);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
              .Returns("<captureResponse id=\"123\" reportGroup=\"RG27\" xmlns=\"http://www.litle.com/schema\"> <litleTxnId>123</litleTxnId> <orderId>12z58743y1</orderId> <response>000</response> <responseTime>2011-09-01T10:24:31</responseTime> <message>message</message> </captureResponse>")
              .Returns("<captureResponse id=\"124\" reportGroup=\"RG27\" xmlns=\"http://www.litle.com/schema\"> <litleTxnId>124</litleTxnId> <orderId>12z58743y1</orderId> <response>000</response> <responseTime>2011-09-01T10:24:31</responseTime> <message>message</message> </captureResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setCaptureResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<captureGivenAuthResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></captureGivenAuthResponse>")
                .Returns("<captureGivenAuthResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></captureGivenAuthResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setCaptureGivenAuthResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<creditResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></creditResponse>")
                .Returns("<creditResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></creditResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setCreditResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckCreditResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></echeckCreditResponse>")
                .Returns("<echeckCreditResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></echeckCreditResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setEcheckCreditResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckRedepositResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></echeckRedepositResponse>")
                .Returns("<echeckRedepositResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></echeckRedepositResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setEcheckRedepositResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckSalesResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></echeckSalesResponse>")
                .Returns("<echeckSalesResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></echeckSalesResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setEcheckSalesResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckVerificationResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></echeckVerificationResponse>")
                .Returns("<echeckVerificationResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></echeckVerificationResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setEcheckVerificationResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<forceCaptureResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></forceCaptureResponse>")
                .Returns("<forceCaptureResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></forceCaptureResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setForceCaptureResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<saleResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></saleResponse>")
                .Returns("<saleResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></saleResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setSaleResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<registerTokenResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></registerTokenResponse>")
                .Returns("<registerTokenResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></registerTokenResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setRegisterTokenResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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
        public void testUpdateCardValidationNumOnToken()
        {
            updateCardValidationNumOnToken updateCardValidationNumOnToken = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken.orderId = "12344";
            updateCardValidationNumOnToken.litleToken = "123";

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<updateCardValidationNumOnTokenResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></updateCardValidationNumOnTokenResponse>")
                .Returns("<updateCardValidationNumOnTokenResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></updateCardValidationNumOnTokenResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setUpdateCardValidationNumOnTokenResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);
            litleBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse actualLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            updateCardValidationNumOnTokenResponse actualUpdateCardValidationNumOnTokenResponse1 = actualLitleBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
            updateCardValidationNumOnTokenResponse actualUpdateCardValidationNumOnTokenResponse2 = actualLitleBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
            updateCardValidationNumOnTokenResponse nullUpdateCardValidationNumOnTokenResponse = actualLitleBatchResponse.nextUpdateCardValidationNumOnTokenResponse();

            Assert.AreEqual(123, actualUpdateCardValidationNumOnTokenResponse1.litleTxnId);
            Assert.AreEqual(124, actualUpdateCardValidationNumOnTokenResponse2.litleTxnId);
            Assert.IsNull(nullUpdateCardValidationNumOnTokenResponse);

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

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

            var mockXml = new Mock<litleXmlSerializer>();

            Communications mockedCommunications = mockCommunications.Object;

            mockXml.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockXml.Object;

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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<authorizationResponse reportGroup=\"Default Report Group\" xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></authorizationResponse>")
                .Returns("<authorizationResponse reportGroup=\"Default Report Group\" xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></authorizationResponse>");

            litleBatchResponse mockedLitleBatchResponse = new litleBatchResponse();
            mockedLitleBatchResponse.setUpdateCardValidationNumOnTokenResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextLitleBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

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

            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsRegex(".*reportGroup=\"Default Report Group\".*", RegexOptions.Singleline)));
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

            litleFile mockedLitleFile = mockLitleFile.Object;

            litleTime mockedLitleTime = mockLitleTime.Object;
            litle.setLitleTime(mockedLitleTime);
            litle.setLitleFile(mockedLitleFile);

            litleBatchRequest litleBatchRequest = new litleBatchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            string resultFile = litle.Serialize();

            Assert.IsTrue(resultFile.Equals(mockFilePath));

            mockLitleFile.Verify(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>()));
        }

        [Test]
        public void testRFRRequest()
        {
            RFRRequest rfrRequest = new RFRRequest();
            rfrRequest.litleSessionId = 123456789;

            var mockBatchXmlReader = new Mock<XmlReader>();
            mockBatchXmlReader.Setup(XmlReader => XmlReader.ReadState).Returns(ReadState.Closed);
            
            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadState).Returns(ReadState.Interactive).Returns(ReadState.Closed);
            mockXmlReader.Setup(XmlReader => XmlReader.ReadOuterXml()).Returns("<RFRResponse response=\"1\" message=\"The account update file is not ready yet. Please try again later.\" xmlns='http://www.litle.com/schema'> </RFRResponse>");

            litleResponse mockedLitleResponse = new litleResponse();
            mockedLitleResponse.setRfrResponseReader(mockXmlReader.Object);
            mockedLitleResponse.setBatchResponseReader(mockBatchXmlReader.Object);

            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();
            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;
            litleTime mockedLitleTime = mockLitleTime.Object;
            Communications mockedCommunications = mockCommunications.Object;

            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockedLitleTime);
            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            rfrRequest.setLitleFile(mockedLitleFile);
            rfrRequest.setLitleTime(mockedLitleTime);

            litle.addRFRRequest(rfrRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle("C:\\RESPONSES\\", batchFileName);
            litleBatchResponse nullLitleBatchResponse = actualLitleResponse.nextLitleBatchResponse();
            RFRResponse actualRFRResponse = actualLitleResponse.nextRFRResponse();
            RFRResponse nullRFRResponse = actualLitleResponse.nextRFRResponse();

            Assert.IsNotNull(actualRFRResponse);
            Assert.AreEqual("1", actualRFRResponse.response);
            Assert.AreEqual("The account update file is not ready yet. Please try again later.", actualRFRResponse.message);
            Assert.IsNull(nullLitleBatchResponse);
            Assert.IsNull(nullRFRResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(mockFilePath, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }
    }
}

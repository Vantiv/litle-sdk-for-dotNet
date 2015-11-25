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
    class TestBatch
    {
        private litleRequest litle;
        private const string timeFormat = "MM-dd-yyyy_HH-mm-ss-ffff_";
        private const string timeRegex = "[0-1][0-9]-[0-3][0-9]-[0-9]{4}_[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{4}_";
        private const string batchNameRegex = timeRegex + "[A-Z]{8}";
        private const string mockFileName = "TheRainbow.xml";
        private const string mockFilePath = "C:\\Somewhere\\Over\\" + mockFileName;

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
            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object)).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendFileToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);

            mockCommunications = new Mock<Communications>();
        }

        [SetUp]
        public void setUpBeforeEachTest()
        {
            litle = new litleRequest();

            mockXmlReader = new Mock<XmlReader>();
            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadToFollowing(It.IsAny<String>())).Returns(true).Returns(true).Returns(false);
            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadState).Returns(ReadState.Initial).Returns(ReadState.Interactive).Returns(ReadState.Closed);
        }

        [Test]
        public void testInitialization()
        {
            Dictionary<String, String> mockConfig = new Dictionary<string, string>();

            mockConfig["url"] = "https://www.mockurl.com";
            mockConfig["reportGroup"] = "Mock Report Group";
            mockConfig["username"] = "mockUser";
            mockConfig["printxml"] = "false";
            mockConfig["timeout"] = "35";
            mockConfig["proxyHost"] = "www.mockproxy.com";
            mockConfig["merchantId"] = "MOCKID";
            mockConfig["password"] = "mockPassword";
            mockConfig["proxyPort"] = "3000";
            mockConfig["sftpUrl"] = "www.mockftp.com";
            mockConfig["sftpUsername"] = "mockFtpUser";
            mockConfig["sftpPassword"] = "mockFtpPassword";
            mockConfig["knownHostsFile"] = "C:\\MockKnownHostsFile";
            mockConfig["onlineBatchUrl"] = "www.mockbatch.com";
            mockConfig["onlineBatchPort"] = "4000";
            mockConfig["requestDirectory"] = "C:\\MockRequests";
            mockConfig["responseDirectory"] = "C:\\MockResponses";

            litle = new litleRequest(mockConfig);

            Assert.AreEqual("C:\\MockRequests\\Requests\\", litle.getRequestDirectory());
            Assert.AreEqual("C:\\MockResponses\\Responses\\", litle.getResponseDirectory());

            Assert.NotNull(litle.getCommunication());
            Assert.NotNull(litle.getLitleTime());
            Assert.NotNull(litle.getLitleFile());
            Assert.NotNull(litle.getLitleXmlSerializer());
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
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<accountUpdateResponse reportGroup=\"Merch01ReportGrp\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>123</litleTxnId><orderId>MERCH01-0002</orderId><response>000</response><responseTime>2010-04-11T15:44:26</responseTime><message>Approved</message></accountUpdateResponse>")
                .Returns("<accountUpdateResponse reportGroup=\"Merch01ReportGrp\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>124</litleTxnId><orderId>MERCH01-0002</orderId><response>000</response><responseTime>2010-04-11T15:44:26</responseTime><message>Approved</message></accountUpdateResponse>");

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setAccountUpdateResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addAccountUpdate(accountUpdate);
            litleBatchRequest.addAccountUpdate(accountUpdate);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            accountUpdateResponse actualAccountUpdateResponse1 = actualLitleBatchResponse.nextAccountUpdateResponse();
            accountUpdateResponse actualAccountUpdateResponse2 = actualLitleBatchResponse.nextAccountUpdateResponse();
            accountUpdateResponse nullAccountUpdateResponse = actualLitleBatchResponse.nextAccountUpdateResponse();

            Assert.AreEqual(123, actualAccountUpdateResponse1.litleTxnId);
            Assert.AreEqual("000", actualAccountUpdateResponse1.response);
            Assert.AreEqual(124, actualAccountUpdateResponse2.litleTxnId);
            Assert.AreEqual("000", actualAccountUpdateResponse2.response);
            Assert.IsNull(nullAccountUpdateResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setAuthorizationResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addAuthorization(authorization);
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual(123, actualLitleBatchResponse.nextAuthorizationResponse().litleTxnId);
            Assert.AreEqual(124, actualLitleBatchResponse.nextAuthorizationResponse().litleTxnId);
            Assert.IsNull(actualLitleBatchResponse.nextAuthorizationResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setAuthReversalResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunications = mockCommunications.Object;
            litle.setCommunication(mockedCommunications);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addAuthReversal(authreversal);
            litleBatchRequest.addAuthReversal(authreversal);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            authReversalResponse actualAuthReversalResponse1 = actualLitleBatchResponse.nextAuthReversalResponse();
            authReversalResponse actualAuthReversalResponse2 = actualLitleBatchResponse.nextAuthReversalResponse();
            authReversalResponse nullAuthReversalResponse = actualLitleBatchResponse.nextAuthReversalResponse();

            Assert.AreEqual(123, actualAuthReversalResponse1.litleTxnId);
            Assert.AreEqual(124, actualAuthReversalResponse2.litleTxnId);
            Assert.IsNull(nullAuthReversalResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setCaptureResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addCapture(capture);
            litleBatchRequest.addCapture(capture);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            captureResponse actualCaptureResponse1 = actualLitleBatchResponse.nextCaptureResponse();
            captureResponse actualCaptureResponse2 = actualLitleBatchResponse.nextCaptureResponse();
            captureResponse nullCaptureResponse = actualLitleBatchResponse.nextCaptureResponse();

            Assert.AreEqual(123, actualCaptureResponse1.litleTxnId);
            Assert.AreEqual(124, actualCaptureResponse2.litleTxnId);
            Assert.IsNull(nullCaptureResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setCaptureGivenAuthResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            captureGivenAuthResponse actualCaptureGivenAuthReponse1 = actualLitleBatchResponse.nextCaptureGivenAuthResponse();
            captureGivenAuthResponse actualCaptureGivenAuthReponse2 = actualLitleBatchResponse.nextCaptureGivenAuthResponse();
            captureGivenAuthResponse nullCaptureGivenAuthReponse = actualLitleBatchResponse.nextCaptureGivenAuthResponse();

            Assert.AreEqual(123, actualCaptureGivenAuthReponse1.litleTxnId);
            Assert.AreEqual(124, actualCaptureGivenAuthReponse2.litleTxnId);
            Assert.IsNull(nullCaptureGivenAuthReponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setCreditResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addCredit(credit);
            litleBatchRequest.addCredit(credit);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            creditResponse actualCreditReponse1 = actualLitleBatchResponse.nextCreditResponse();
            creditResponse actualCreditReponse2 = actualLitleBatchResponse.nextCreditResponse();
            creditResponse nullCreditReponse1 = actualLitleBatchResponse.nextCreditResponse();

            Assert.AreEqual(123, actualCreditReponse1.litleTxnId);
            Assert.AreEqual(124, actualCreditReponse2.litleTxnId);
            Assert.IsNull(nullCreditReponse1);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setEcheckCreditResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addEcheckCredit(echeckcredit);
            litleBatchRequest.addEcheckCredit(echeckcredit);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            echeckCreditResponse actualEcheckCreditResponse1 = actualLitleBatchResponse.nextEcheckCreditResponse();
            echeckCreditResponse actualEcheckCreditResponse2 = actualLitleBatchResponse.nextEcheckCreditResponse();
            echeckCreditResponse nullEcheckCreditResponse = actualLitleBatchResponse.nextEcheckCreditResponse();

            Assert.AreEqual(123, actualEcheckCreditResponse1.litleTxnId);
            Assert.AreEqual(124, actualEcheckCreditResponse2.litleTxnId);
            Assert.IsNull(nullEcheckCreditResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setEcheckRedepositResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            echeckRedepositResponse actualEcheckRedepositResponse1 = actualLitleBatchResponse.nextEcheckRedepositResponse();
            echeckRedepositResponse actualEcheckRedepositResponse2 = actualLitleBatchResponse.nextEcheckRedepositResponse();
            echeckRedepositResponse nullEcheckRedepositResponse = actualLitleBatchResponse.nextEcheckRedepositResponse();

            Assert.AreEqual(123, actualEcheckRedepositResponse1.litleTxnId);
            Assert.AreEqual(124, actualEcheckRedepositResponse2.litleTxnId);
            Assert.IsNull(nullEcheckRedepositResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setEcheckSalesResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addEcheckSale(echecksale);
            litleBatchRequest.addEcheckSale(echecksale);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            echeckSalesResponse actualEcheckSalesResponse1 = actualLitleBatchResponse.nextEcheckSalesResponse();
            echeckSalesResponse actualEcheckSalesResponse2 = actualLitleBatchResponse.nextEcheckSalesResponse();
            echeckSalesResponse nullEcheckSalesResponse = actualLitleBatchResponse.nextEcheckSalesResponse();

            Assert.AreEqual(123, actualEcheckSalesResponse1.litleTxnId);
            Assert.AreEqual(124, actualEcheckSalesResponse2.litleTxnId);
            Assert.IsNull(nullEcheckSalesResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setEcheckVerificationResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addEcheckVerification(echeckverification);
            litleBatchRequest.addEcheckVerification(echeckverification);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            echeckVerificationResponse actualEcheckVerificationResponse1 = actualLitleBatchResponse.nextEcheckVerificationResponse();
            echeckVerificationResponse actualEcheckVerificationResponse2 = actualLitleBatchResponse.nextEcheckVerificationResponse();
            echeckVerificationResponse nullEcheckVerificationResponse = actualLitleBatchResponse.nextEcheckVerificationResponse();

            Assert.AreEqual(123, actualEcheckVerificationResponse1.litleTxnId);
            Assert.AreEqual(124, actualEcheckVerificationResponse2.litleTxnId);
            Assert.IsNull(nullEcheckVerificationResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setForceCaptureResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addForceCapture(forcecapture);
            litleBatchRequest.addForceCapture(forcecapture);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            forceCaptureResponse actualForceCaptureResponse1 = actualLitleBatchResponse.nextForceCaptureResponse();
            forceCaptureResponse actualForceCaptureResponse2 = actualLitleBatchResponse.nextForceCaptureResponse();
            forceCaptureResponse nullForceCaptureResponse = actualLitleBatchResponse.nextForceCaptureResponse();

            Assert.AreEqual(123, actualForceCaptureResponse1.litleTxnId);
            Assert.AreEqual(124, actualForceCaptureResponse2.litleTxnId);
            Assert.IsNull(nullForceCaptureResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setSaleResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addSale(sale);
            litleBatchRequest.addSale(sale);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            saleResponse actualSaleResponse1 = actualLitleBatchResponse.nextSaleResponse();
            saleResponse actualSaleResponse2 = actualLitleBatchResponse.nextSaleResponse();
            saleResponse nullSaleResponse = actualLitleBatchResponse.nextSaleResponse();

            Assert.AreEqual(123, actualSaleResponse1.litleTxnId);
            Assert.AreEqual(124, actualSaleResponse2.litleTxnId);
            Assert.IsNull(nullSaleResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setRegisterTokenResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addRegisterTokenRequest(token);
            litleBatchRequest.addRegisterTokenRequest(token);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            registerTokenResponse actualRegisterTokenResponse1 = actualLitleBatchResponse.nextRegisterTokenResponse();
            registerTokenResponse actualRegisterTokenResponse2 = actualLitleBatchResponse.nextRegisterTokenResponse();
            registerTokenResponse nullRegisterTokenResponse = actualLitleBatchResponse.nextRegisterTokenResponse();

            Assert.AreEqual(123, actualRegisterTokenResponse1.litleTxnId);
            Assert.AreEqual(124, actualRegisterTokenResponse2.litleTxnId);
            Assert.IsNull(nullRegisterTokenResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setUpdateCardValidationNumOnTokenResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);
            litleBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            updateCardValidationNumOnTokenResponse actualUpdateCardValidationNumOnTokenResponse1 = actualLitleBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
            updateCardValidationNumOnTokenResponse actualUpdateCardValidationNumOnTokenResponse2 = actualLitleBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
            updateCardValidationNumOnTokenResponse nullUpdateCardValidationNumOnTokenResponse = actualLitleBatchResponse.nextUpdateCardValidationNumOnTokenResponse();

            Assert.AreEqual(123, actualUpdateCardValidationNumOnTokenResponse1.litleTxnId);
            Assert.AreEqual(124, actualUpdateCardValidationNumOnTokenResponse2.litleTxnId);
            Assert.IsNull(nullUpdateCardValidationNumOnTokenResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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
            var mockLitleBatchResponse = new Mock<batchResponse>();
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
                litle.setLitleTime(mockLitleTime.Object);
                batchRequest litleBatchRequest = new batchRequest();
                litleBatchRequest.setLitleFile(mockedLitleFile);
                litleBatchRequest.setLitleTime(mockLitleTime.Object);

                litleBatchRequest.addAuthorization(authorization);
                litleBatchRequest.addAuthorization(authorization);
                litle.addBatch(litleBatchRequest);

                string batchFileName = litle.sendToLitle();
                litleResponse litleResponse = litle.receiveFromLitle(batchFileName);
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
                litle.setLitleTime(mockLitleTime.Object);
                batchRequest litleBatchRequest = new batchRequest();
                litleBatchRequest.setLitleFile(mockedLitleFile);
                litleBatchRequest.setLitleTime(mockLitleTime.Object);

                litleBatchRequest.addAuthorization(authorization);
                litleBatchRequest.addAuthorization(authorization);
                litle.addBatch(litleBatchRequest);

                string batchFileName = litle.sendToLitle();
                litleResponse litleResponse = litle.receiveFromLitle(batchFileName);
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

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setAuthorizationResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);
            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addAuthorization(authorization);
            litleBatchRequest.addAuthorization(authorization);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            authorizationResponse actualAuthorizationResponse1 = actualLitleBatchResponse.nextAuthorizationResponse();
            authorizationResponse actualAuthorizationResponse2 = actualLitleBatchResponse.nextAuthorizationResponse();
            authorizationResponse nullAuthorizationResponse = actualLitleBatchResponse.nextAuthorizationResponse();

            Assert.AreEqual(123, actualAuthorizationResponse1.litleTxnId);
            Assert.AreEqual("Default Report Group", actualAuthorizationResponse1.reportGroup);
            Assert.AreEqual(124, actualAuthorizationResponse2.litleTxnId);
            Assert.AreEqual("Default Report Group", actualAuthorizationResponse2.reportGroup);
            Assert.IsNull(nullAuthorizationResponse);

            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsRegex(".*reportGroup=\"Default Report Group\".*", RegexOptions.Singleline)));
            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
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

            batchRequest litleBatchRequest = new batchRequest();
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

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse nullLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            RFRResponse actualRFRResponse = actualLitleResponse.nextRFRResponse();
            RFRResponse nullRFRResponse = actualLitleResponse.nextRFRResponse();

            Assert.IsNotNull(actualRFRResponse);
            Assert.AreEqual("1", actualRFRResponse.response);
            Assert.AreEqual("The account update file is not ready yet. Please try again later.", actualRFRResponse.message);
            Assert.IsNull(nullLitleBatchResponse);
            Assert.IsNull(nullRFRResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testCancelSubscription()
        {
            cancelSubscription cancel = new cancelSubscription();
            cancel.subscriptionId = 12345;

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<cancelSubscriptionResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>54321</litleTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04T21:55:14</responseTime><subscriptionId>12345</subscriptionId></cancelSubscriptionResponse>")
                .Returns("<cancelSubscriptionResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>12345</litleTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04T21:55:14</responseTime><subscriptionId>54321</subscriptionId></cancelSubscriptionResponse>");

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setCancelSubscriptionResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addCancelSubscription(cancel);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual("12345", actualLitleBatchResponse.nextCancelSubscriptionResponse().subscriptionId);
            Assert.AreEqual("54321", actualLitleBatchResponse.nextCancelSubscriptionResponse().subscriptionId);
            Assert.IsNull(actualLitleBatchResponse.nextCancelSubscriptionResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testUpdateSubscription()
        {
            updateSubscription update = new updateSubscription();
            update.billingDate = new DateTime(2002, 10, 9);
            contact billToAddress = new contact();
            billToAddress.name = "Greg Dake";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "sdksupport@litle.com";
            update.billToAddress = billToAddress;
            cardType card = new cardType();
            card.number = "4100000000000001";
            card.expDate = "1215";
            card.type = methodOfPaymentTypeEnum.VI;
            update.card = card;
            update.planCode = "abcdefg";
            update.subscriptionId = 12345;

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<updateSubscriptionResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>54321</litleTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04T21:55:14</responseTime><subscriptionId>12345</subscriptionId></updateSubscriptionResponse>")
                .Returns("<updateSubscriptionResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>12345</litleTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04T21:55:14</responseTime><subscriptionId>54321</subscriptionId></updateSubscriptionResponse>");

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setUpdateSubscriptionResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addUpdateSubscription(update);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual("12345", actualLitleBatchResponse.nextUpdateSubscriptionResponse().subscriptionId);
            Assert.AreEqual("54321", actualLitleBatchResponse.nextUpdateSubscriptionResponse().subscriptionId);
            Assert.IsNull(actualLitleBatchResponse.nextUpdateSubscriptionResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testCreatePlan()
        {
            createPlan createPlan = new createPlan();
            createPlan.planCode = "thePlanCode";
            createPlan.name = "theName";
            createPlan.intervalType = intervalType.ANNUAL;
            createPlan.amount = 100;

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<createPlanResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>123</litleTxnId></createPlanResponse>")
                .Returns("<createPlanResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>124</litleTxnId></createPlanResponse>");

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setCreatePlanResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addCreatePlan(createPlan);
            litleBatchRequest.addCreatePlan(createPlan);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual("123", actualLitleBatchResponse.nextCreatePlanResponse().litleTxnId);
            Assert.AreEqual("124", actualLitleBatchResponse.nextCreatePlanResponse().litleTxnId);
            Assert.IsNull(actualLitleBatchResponse.nextCreatePlanResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testUpdatePlan()
        {
            updatePlan updatePlan = new updatePlan();
            updatePlan.planCode = "thePlanCode";
            updatePlan.active = true;

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<updatePlanResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>123</litleTxnId></updatePlanResponse>")
                .Returns("<updatePlanResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>124</litleTxnId></updatePlanResponse>");

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setUpdatePlanResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addUpdatePlan(updatePlan);
            litleBatchRequest.addUpdatePlan(updatePlan);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual("123", actualLitleBatchResponse.nextUpdatePlanResponse().litleTxnId);
            Assert.AreEqual("124", actualLitleBatchResponse.nextUpdatePlanResponse().litleTxnId);
            Assert.IsNull(actualLitleBatchResponse.nextUpdatePlanResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testActivate()
        {
            activate activate = new activate();
            activate.orderId = "theOrderId";
            activate.orderSource = orderSourceType.ecommerce;
            activate.card = new cardType();

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<activateResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" duplicate=\"true\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>123</litleTxnId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></activateResponse>")
                .Returns("<activateResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>124</litleTxnId></activateResponse>");

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setActivateResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addActivate(activate);
            litleBatchRequest.addActivate(activate);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual(123, actualLitleBatchResponse.nextActivateResponse().litleTxnId);
            Assert.AreEqual(124, actualLitleBatchResponse.nextActivateResponse().litleTxnId);
            Assert.IsNull(actualLitleBatchResponse.nextActivateResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testDeactivate()
        {
            deactivate deactivate = new deactivate();
            deactivate.orderId = "theOrderId";
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new cardType();

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<deactivateResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>123</litleTxnId></deactivateResponse>")
                .Returns("<deactivateResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>124</litleTxnId></deactivateResponse>");

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setDeactivateResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addDeactivate(deactivate);
            litleBatchRequest.addDeactivate(deactivate);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual(123, actualLitleBatchResponse.nextDeactivateResponse().litleTxnId);
            Assert.AreEqual(124, actualLitleBatchResponse.nextDeactivateResponse().litleTxnId);
            Assert.IsNull(actualLitleBatchResponse.nextDeactivateResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testLoad()
        {
            load load = new load();
            load.orderId = "theOrderId";
            load.orderSource = orderSourceType.ecommerce;
            load.card = new cardType();

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<loadResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>123</litleTxnId></loadResponse>")
                .Returns("<loadResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>124</litleTxnId></loadResponse>");

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setLoadResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addLoad(load);
            litleBatchRequest.addLoad(load);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual(123, actualLitleBatchResponse.nextLoadResponse().litleTxnId);
            Assert.AreEqual(124, actualLitleBatchResponse.nextLoadResponse().litleTxnId);
            Assert.IsNull(actualLitleBatchResponse.nextLoadResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testUnload()
        {
            unload unload = new unload();
            unload.orderId = "theOrderId";
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new cardType();

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<unloadResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>123</litleTxnId></unloadResponse>")
                .Returns("<unloadResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>124</litleTxnId></unloadResponse>");

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setUnloadResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addUnload(unload);
            litleBatchRequest.addUnload(unload);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual(123, actualLitleBatchResponse.nextUnloadResponse().litleTxnId);
            Assert.AreEqual(124, actualLitleBatchResponse.nextUnloadResponse().litleTxnId);
            Assert.IsNull(actualLitleBatchResponse.nextUnloadResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testBalanceInquiry()
        {
            balanceInquiry balanceInquiry = new balanceInquiry();
            balanceInquiry.orderId = "theOrderId";
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new cardType();

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<balanceInquiryResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>123</litleTxnId></balanceInquiryResponse>")
                .Returns("<balanceInquiryResponse xmlns=\"http://www.litle.com/schema\"><litleTxnId>124</litleTxnId></balanceInquiryResponse>");

            batchResponse mockLitleBatchResponse = new batchResponse();
            mockLitleBatchResponse.setBalanceInquiryResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);

            Communications mockedCommunication = mockCommunications.Object;
            litle.setCommunication(mockedCommunication);

            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);

            litleFile mockedLitleFile = mockLitleFile.Object;
            litle.setLitleFile(mockedLitleFile);

            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addBalanceInquiry(balanceInquiry);
            litleBatchRequest.addBalanceInquiry(balanceInquiry);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();
            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();

            Assert.AreSame(mockLitleBatchResponse, actualLitleBatchResponse);
            Assert.AreEqual(123, actualLitleBatchResponse.nextBalanceInquiryResponse().litleTxnId);
            Assert.AreEqual(124, actualLitleBatchResponse.nextBalanceInquiryResponse().litleTxnId);
            Assert.IsNull(actualLitleBatchResponse.nextBalanceInquiryResponse());

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testEcheckPreNoteSale()
        {
            echeckPreNoteSale echeckPreNoteSale = new echeckPreNoteSale();
            echeckPreNoteSale.orderId = "12345";
            echeckPreNoteSale.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckPreNoteSale.echeck = echeck;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            echeckPreNoteSale.billToAddress = contact;

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckPreNoteSaleResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></echeckPreNoteSaleResponse>")
                .Returns("<echeckPreNoteSaleResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></echeckPreNoteSaleResponse>");

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setEcheckPreNoteSaleResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addEcheckPreNoteSale(echeckPreNoteSale);
            litleBatchRequest.addEcheckPreNoteSale(echeckPreNoteSale);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            echeckPreNoteSaleResponse actualEcheckPreNoteSaleResponse1 = actualLitleBatchResponse.nextEcheckPreNoteSaleResponse();
            echeckPreNoteSaleResponse actualEcheckPreNoteSaleResponse2 = actualLitleBatchResponse.nextEcheckPreNoteSaleResponse();
            echeckPreNoteSaleResponse nullEcheckPreNoteSalesResponse = actualLitleBatchResponse.nextEcheckPreNoteSaleResponse();

            Assert.AreEqual(123, actualEcheckPreNoteSaleResponse1.litleTxnId);
            Assert.AreEqual(124, actualEcheckPreNoteSaleResponse2.litleTxnId);
            Assert.IsNull(nullEcheckPreNoteSalesResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }

        [Test]
        public void testEcheckPreNoteCredit()
        {
            echeckPreNoteCredit echeckPreNoteCredit = new echeckPreNoteCredit();
            echeckPreNoteCredit.orderId = "12345";
            echeckPreNoteCredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.CorpSavings;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckPreNoteCredit.echeck = echeck;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            echeckPreNoteCredit.billToAddress = contact;

            var mockLitleResponse = new Mock<litleResponse>();
            var mockLitleXmlSerializer = new Mock<litleXmlSerializer>();

            mockXmlReader.SetupSequence(XmlReader => XmlReader.ReadOuterXml())
                .Returns("<echeckPreNoteCreditResponse xmlns='http://www.litle.com/schema'><litleTxnId>123</litleTxnId></echeckPreNoteCreditResponse>")
                .Returns("<echeckPreNoteCreditResponse xmlns='http://www.litle.com/schema'><litleTxnId>124</litleTxnId></echeckPreNoteCreditResponse>");

            batchResponse mockedLitleBatchResponse = new batchResponse();
            mockedLitleBatchResponse.setEcheckPreNoteCreditResponseReader(mockXmlReader.Object);

            mockLitleResponse.Setup(litleResponse => litleResponse.nextBatchResponse()).Returns(mockedLitleBatchResponse);
            litleResponse mockedLitleResponse = mockLitleResponse.Object;

            Communications mockedCommunications = mockCommunications.Object;

            mockLitleXmlSerializer.Setup(litleXmlSerializer => litleXmlSerializer.DeserializeObjectFromFile(It.IsAny<String>())).Returns(mockedLitleResponse);
            litleXmlSerializer mockedLitleXmlSerializer = mockLitleXmlSerializer.Object;

            litleFile mockedLitleFile = mockLitleFile.Object;

            litle.setCommunication(mockedCommunications);
            litle.setLitleXmlSerializer(mockedLitleXmlSerializer);
            litle.setLitleFile(mockedLitleFile);
            litle.setLitleTime(mockLitleTime.Object);

            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.setLitleFile(mockedLitleFile);
            litleBatchRequest.setLitleTime(mockLitleTime.Object);
            litleBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCredit);
            litleBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCredit);
            litle.addBatch(litleBatchRequest);

            string batchFileName = litle.sendToLitle();

            litleResponse actualLitleResponse = litle.receiveFromLitle(batchFileName);
            batchResponse actualLitleBatchResponse = actualLitleResponse.nextBatchResponse();
            echeckPreNoteCreditResponse actualEcheckPreNoteCreditResponse1 = actualLitleBatchResponse.nextEcheckPreNoteCreditResponse();
            echeckPreNoteCreditResponse actualEcheckPreNoteCreditResponse2 = actualLitleBatchResponse.nextEcheckPreNoteCreditResponse();
            echeckPreNoteCreditResponse nullEcheckPreNoteCreditsResponse = actualLitleBatchResponse.nextEcheckPreNoteCreditResponse();

            Assert.AreEqual(123, actualEcheckPreNoteCreditResponse1.litleTxnId);
            Assert.AreEqual(124, actualEcheckPreNoteCreditResponse2.litleTxnId);
            Assert.IsNull(nullEcheckPreNoteCreditsResponse);

            mockCommunications.Verify(Communications => Communications.FtpDropOff(It.IsAny<String>(), mockFileName, It.IsAny<Dictionary<String, String>>()));
            mockCommunications.Verify(Communications => Communications.FtpPickUp(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>(), mockFileName));
        }
    }
}

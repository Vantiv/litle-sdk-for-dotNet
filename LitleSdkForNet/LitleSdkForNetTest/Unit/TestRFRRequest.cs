using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;

namespace Litle.Sdk.Test.Unit
{
    [TestClass]
    class TestRFRRequest
    {
        private RFRRequest rfrRequest;

        private const string timeFormat = "MM-dd-yyyy_HH-mm-ss-ffff_";
        private const string timeRegex = "[0-1][0-9]-[0-3][0-9]-[0-9]{4}_[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{4}_";
        private const string batchNameRegex = timeRegex + "[A-Z]{8}";
        private const string mockFileName = "TheRainbow.xml";
        private const string mockFilePath = "C:\\Somewhere\\\\Over\\\\" + mockFileName;

        private Mock<litleFile> mockLitleFile;
        private Mock<litleTime> mockLitleTime;

        [TestInitialize]
        public void setUp()
        {
            mockLitleFile = new Mock<litleFile>();
            mockLitleTime = new Mock<litleTime>();

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object)).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
        }

        [ClassInitialize]
        public void setUpBeforeTest()
        {
            rfrRequest = new RFRRequest();
        }

        [TestMethod]
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

            rfrRequest = new RFRRequest(mockConfig);

            Assert.AreEqual("C:\\MockRequests\\Requests\\", rfrRequest.getRequestDirectory());
            Assert.AreEqual("C:\\MockResponses\\Responses\\", rfrRequest.getResponseDirectory());

            Assert.IsNotNull(rfrRequest.getLitleTime());
            Assert.IsNotNull(rfrRequest.getLitleFile());
        }

        [TestMethod]
        public void testSerialize()
        {
            litleFile mockedLitleFile = mockLitleFile.Object;
            litleTime mockedLitleTime = mockLitleTime.Object;

            rfrRequest.litleSessionId = 123456789;
            rfrRequest.setLitleFile(mockedLitleFile);
            rfrRequest.setLitleTime(mockedLitleTime);

            Assert.AreEqual(mockFilePath, rfrRequest.Serialize());

            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, "\r\n<RFRRequest xmlns=\"http://www.litle.com/schema\">"));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, "\r\n<litleSessionId>123456789</litleSessionId>"));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, "\r\n</RFRRequest>"));
        }
    }
}

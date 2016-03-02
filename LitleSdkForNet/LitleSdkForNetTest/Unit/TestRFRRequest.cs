using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using Moq;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestRFRRequest
    {
        private RFRRequest rfrRequest;

        private const string timeFormat = "MM-dd-yyyy_HH-mm-ss-ffff_";
        private const string timeRegex = "[0-1][0-9]-[0-3][0-9]-[0-9]{4}_[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{4}_";
        private const string batchNameRegex = timeRegex + "[A-Z]{8}";
        private const string mockFileName = "TheRainbow.xml";
        private const string mockFilePath = "C:\\Somewhere\\\\Over\\\\" + mockFileName;

        private Mock<litleFile> mockLitleFile;
        private Mock<litleTime> mockLitleTime;
        private IDictionary<string, StringBuilder> _memoryCache;

        [TestFixtureSetUp]
        public void setUp()
        {
            _memoryCache = new Dictionary<string, StringBuilder>();
            mockLitleFile = new Mock<litleFile>(_memoryCache);
            mockLitleTime = new Mock<litleTime>();

            mockLitleFile.Setup(
                litleFile =>
                    litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                        mockLitleTime.Object)).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<string>()))
                .Returns(mockFilePath);
        }

        [SetUp]
        public void setUpBeforeTest()
        {
            rfrRequest = new RFRRequest(_memoryCache);
        }

        [Test]
        public void testInitialization()
        {
            var mockConfig = new Dictionary<string, string>();

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

            rfrRequest = new RFRRequest(_memoryCache, mockConfig);

            Assert.AreEqual("C:\\MockRequests\\Requests\\", rfrRequest.getRequestDirectory());
            Assert.AreEqual("C:\\MockResponses\\Responses\\", rfrRequest.getResponseDirectory());

            Assert.NotNull(rfrRequest.getLitleTime());
            Assert.NotNull(rfrRequest.getLitleFile());
        }

        [Test]
        public void testSerialize()
        {
            var mockedLitleFile = mockLitleFile.Object;
            var mockedLitleTime = mockLitleTime.Object;

            rfrRequest.litleSessionId = 123456789;
            rfrRequest.setLitleFile(mockedLitleFile);
            rfrRequest.setLitleTime(mockedLitleTime);

            Assert.AreEqual(mockFilePath, rfrRequest.Serialize());

            mockLitleFile.Verify(
                litleFile =>
                    litleFile.AppendLineToFile(mockFilePath, "\r\n<RFRRequest xmlns=\"http://www.litle.com/schema\">"));
            mockLitleFile.Verify(
                litleFile => litleFile.AppendLineToFile(mockFilePath, "\r\n<litleSessionId>123456789</litleSessionId>"));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, "\r\n</RFRRequest>"));
        }

        [Test]
        public void testAccountUpdateFileRequestData()
        {
            var mockConfig = new Dictionary<string, string>();

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

            var accountUpdateFileRequest = new accountUpdateFileRequestData(mockConfig);
            var accountUpdateFileRequestDefault = new accountUpdateFileRequestData();

            Assert.AreEqual(accountUpdateFileRequestDefault.merchantId, Settings.Default.merchantId);
            Assert.AreEqual(accountUpdateFileRequest.merchantId, mockConfig["merchantId"]);
        }
    }
}

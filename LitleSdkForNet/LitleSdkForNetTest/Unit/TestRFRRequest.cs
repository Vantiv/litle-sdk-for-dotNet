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

        [TestFixtureSetUp]
        public void setUp()
        {
            mockLitleFile = new Mock<litleFile>();
            mockLitleTime = new Mock<litleTime>();

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), mockLitleTime.Object, It.IsAny<String>())).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
        }

        [SetUp]
        public void setUpBeforeTest()
        {
            rfrRequest = new RFRRequest();
        }

        [Test]
        public void testSerialize()
        {
            litleFile mockedLitleFile = mockLitleFile.Object;
            litleTime mockedLitleTime = mockLitleTime.Object;

            rfrRequest.litleSessionId = 123456789;
            rfrRequest.setLitleFile(mockedLitleFile);
            rfrRequest.setLitleTime(mockedLitleTime);

            Assert.AreEqual(mockFilePath, rfrRequest.Serialize());

            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, "<RFRRequest>\r\n"));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, "<litleSessionId>123456789</litleSessionId>\r\n"));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, "</RFRRequest>\r\n"));
        }
    }
}

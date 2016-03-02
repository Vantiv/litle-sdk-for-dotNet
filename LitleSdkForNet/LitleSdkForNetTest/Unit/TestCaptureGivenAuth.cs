using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestCaptureGivenAuth
    {
        private LitleOnline litle;
        private IDictionary<string, StringBuilder> _memoryStreams;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _memoryStreams = new Dictionary<string, StringBuilder>();
            litle = new LitleOnline(_memoryStreams);
        }

        [Test]
        public void TestSecondaryAmount()
        {
            var capture = new captureGivenAuth();
            capture.amount = 2;
            capture.secondaryAmount = 1;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestSurchargeAmount()
        {
            var capture = new captureGivenAuth();
            capture.amount = 2;
            capture.surchargeAmount = 1;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            var capture = new captureGivenAuth();
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestDebtRepayment_True()
        {
            var captureGivenAuth = new captureGivenAuth();
            captureGivenAuth.merchantData = new merchantDataType();
            captureGivenAuth.debtRepayment = true;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*</merchantData>\r\n<debtRepayment>true</debtRepayment>\r\n</captureGivenAuth>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.CaptureGivenAuth(captureGivenAuth);
        }

        [Test]
        public void TestDebtRepayment_False()
        {
            var captureGivenAuth = new captureGivenAuth();
            captureGivenAuth.merchantData = new merchantDataType();
            captureGivenAuth.debtRepayment = false;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*</merchantData>\r\n<debtRepayment>false</debtRepayment>\r\n</captureGivenAuth>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.CaptureGivenAuth(captureGivenAuth);
        }

        [Test]
        public void TestDebtRepayment_Optional()
        {
            var captureGivenAuth = new captureGivenAuth();
            captureGivenAuth.merchantData = new merchantDataType();

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*</merchantData>\r\n</captureGivenAuth>.*", RegexOptions.Singleline),
                        It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.CaptureGivenAuth(captureGivenAuth);
        }
    }
}

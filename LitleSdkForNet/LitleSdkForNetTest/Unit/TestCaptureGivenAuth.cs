using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestCaptureGivenAuth
    {
        
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _litle = new LitleOnline();
        }

        [Test]
        public void TestSecondaryAmount()
        {
            var capture = new captureGivenAuth
            {
                amount = 2,
                secondaryAmount = 1,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestSurchargeAmount()
        {
            var capture = new captureGivenAuth
            {
                amount = 2,
                surchargeAmount = 1,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            var capture = new captureGivenAuth
            {
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestDebtRepayment_True()
        {
            var captureGivenAuth = new captureGivenAuth
            {
                merchantData = new merchantDataType(),
                debtRepayment = true
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*</merchantData>\r\n<debtRepayment>true</debtRepayment>\r\n</captureGivenAuth>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.CaptureGivenAuth(captureGivenAuth);
        }

        [Test]
        public void TestDebtRepayment_False()
        {
            var captureGivenAuth = new captureGivenAuth
            {
                merchantData = new merchantDataType(),
                debtRepayment = false
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*</merchantData>\r\n<debtRepayment>false</debtRepayment>\r\n</captureGivenAuth>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.CaptureGivenAuth(captureGivenAuth);
        }

        [Test]
        public void TestDebtRepayment_Optional()
        {
            var captureGivenAuth = new captureGivenAuth {merchantData = new merchantDataType()};

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*</merchantData>\r\n</captureGivenAuth>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.CaptureGivenAuth(captureGivenAuth);
        }

        [Test]
        public void TestProcessingType()
        {
            var capture = new captureGivenAuth
            {
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets",
                processingType = processingType.initialRecurring
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<processingType>initialRecurring</processingType>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestOrigNetworkTxnId()
        {
            var capture = new captureGivenAuth
            {
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets",
                originalNetworkTransactionId = "1234567890"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<originalNetworkTransactionId>1234567890</originalNetworkTransactionId>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.CaptureGivenAuth(capture);
        }

        [Test]
        public void TestOrigTxnAmount()
        {
            var capture = new captureGivenAuth
            {
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets",
                originalTransactionAmount = 1066
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<originalTransactionAmount>1066</originalTransactionAmount>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.CaptureGivenAuth(capture);
        }

    }
}

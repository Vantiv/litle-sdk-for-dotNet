using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestCredit
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
        public void TestActionReasonOnOrphanedRefund()
        {
            var credit = new credit();
            credit.orderId = "12344";
            credit.amount = 2;
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";
            credit.actionReason = "SUSPECT_FRAUD";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*<actionReason>SUSPECT_FRAUD</actionReason>.*", RegexOptions.Singleline),
                        It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Credit(credit);
        }

        [Test]
        public void TestOrderSource_Set()
        {
            var credit = new credit();
            credit.orderId = "12344";
            credit.amount = 2;
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*<credit.*<amount>2</amount>.*<orderSource>ecommerce</orderSource>.*</credit>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Credit(credit);
        }

        [Test]
        public void TestSecondaryAmount_Orphan()
        {
            var credit = new credit();
            credit.amount = 2;
            credit.secondaryAmount = 1;
            credit.orderId = "3";
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<orderId>3</orderId>\r\n<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Credit(credit);
        }

        [Test]
        public void TestSecondaryAmount_Tied()
        {
            var credit = new credit();
            credit.amount = 2;
            credit.secondaryAmount = 1;
            credit.litleTxnId = 3;
            credit.processingInstructions = new processingInstructions();
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<litleTxnId>3</litleTxnId>\r\n<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<process.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Credit(credit);
        }

        [Test]
        public void TestSurchargeAmount_Tied()
        {
            var credit = new credit();
            credit.amount = 2;
            credit.surchargeAmount = 1;
            credit.litleTxnId = 3;
            credit.processingInstructions = new processingInstructions();
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<litleTxnId>3</litleTxnId>\r\n<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<process.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Credit(credit);
        }

        [Test]
        public void TestSurchargeAmount_TiedOptional()
        {
            var credit = new credit();
            credit.amount = 2;
            credit.litleTxnId = 3;
            credit.reportGroup = "Planets";
            credit.processingInstructions = new processingInstructions();

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*<litleTxnId>3</litleTxnId>\r\n<amount>2</amount>\r\n<processi.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Credit(credit);
        }

        [Test]
        public void TestSurchargeAmount_Orphan()
        {
            var credit = new credit();
            credit.amount = 2;
            credit.surchargeAmount = 1;
            credit.orderId = "3";
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<orderId>3</orderId>\r\n<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Credit(credit);
        }

        [Test]
        public void TestSurchargeAmount_OrphanOptional()
        {
            var credit = new credit();
            credit.amount = 2;
            credit.orderId = "3";
            credit.orderSource = orderSourceType.ecommerce;
            credit.reportGroup = "Planets";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<orderId>3</orderId>\r\n<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Credit(credit);
        }

        [Test]
        public void TestPos_Tied()
        {
            var credit = new credit();
            credit.amount = 2;
            credit.pos = new pos();
            credit.pos.terminalId = "abc123";
            credit.litleTxnId = 3;
            credit.reportGroup = "Planets";
            credit.payPalNotes = "notes";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<litleTxnId>3</litleTxnId>\r\n<amount>2</amount>\r\n<pos>\r\n<terminalId>abc123</terminalId></pos>\r\n<payPalNotes>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Credit(credit);
        }

        [Test]
        public void TestPos_TiedOptional()
        {
            var credit = new credit();
            credit.amount = 2;
            credit.litleTxnId = 3;
            credit.reportGroup = "Planets";
            credit.payPalNotes = "notes";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*<litleTxnId>3</litleTxnId>\r\n<amount>2</amount>\r\n<payPalNotes>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Credit(credit);
        }
    }
}

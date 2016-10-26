using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestCapture
    {
        
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _litle = new LitleOnline();
        }

        [Test]
        public void TestSurchargeAmount()
        {
            var capture = new capture
            {
                litleTxnId = 3,
                amount = 2,
                surchargeAmount = 1,
                payPalNotes = "note",
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<payPalNotes>note</payPalNotes>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureResponse><litleTxnId>123</litleTxnId></captureResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Capture(capture);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            var capture = new capture
            {
                litleTxnId = 3,
                amount = 2,
                payPalNotes = "note",
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<payPalNotes>note</payPalNotes>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureResponse><litleTxnId>123</litleTxnId></captureResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Capture(capture);
        }

        [Test]
        public void TestCapture_withPin()
        {
            var capture = new capture
            {
                litleTxnId = 3,
                amount = 2,
                reportGroup = "Planets",
                pin = "1234"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<pin>1234</pin>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureResponse><litleTxnId>123</litleTxnId></captureResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Capture(capture);
        }
    }
}

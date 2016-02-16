using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestEcheckVoid
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
        public void TestFraudFilterOverride()
        {
            var echeckVoid = new echeckVoid();
            echeckVoid.litleTxnId = 123456789;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*<echeckVoid.*<litleTxnId>123456789.*", RegexOptions.Singleline),
                        It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.13' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckVoidResponse><litleTxnId>123</litleTxnId></echeckVoidResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.EcheckVoid(echeckVoid);
        }

        [Test]
        public void simpleForceCaptureWithSecondaryAmount()
        {
            var forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.secondaryAmount = 50;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            forcecapture.card = card;
            var response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

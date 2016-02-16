using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestDepositReversal
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
        public void TestSimple()
        {
            var depositReversal = new depositReversal();
            depositReversal.id = "a";
            depositReversal.reportGroup = "b";
            depositReversal.litleTxnId = "123";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(It.IsRegex(".*<litleTxnId>123</litleTxnId>.*", RegexOptions.Singleline),
                        It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.22' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><depositReversalResponse><litleTxnId>123</litleTxnId></depositReversalResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var response = litle.DepositReversal(depositReversal);
            Assert.AreEqual("123", response.litleTxnId);
        }
    }
}

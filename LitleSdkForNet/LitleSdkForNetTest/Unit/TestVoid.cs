using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestVoid
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
        public void TestRecyclingDataOnVoidResponse()
        {
            voidTxn voidTxn = new voidTxn();
            voidTxn.litleTxnId = 123;
           
            var mock = new Mock<Communications>(new Dictionary<string, StringBuilder>());

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.16' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><voidResponse><litleTxnId>123</litleTxnId><response>000</response><responseTime>2013-01-31T15:48:09</responseTime><postDate>2013-01-31</postDate><message>Approved</message><recycling><creditLitleTxnId>456</creditLitleTxnId></recycling></voidResponse></litleOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleOnlineResponseTransactionResponseVoidResponse response = litle.DoVoid(voidTxn);
            Assert.AreEqual(123, response.litleTxnId);
            Assert.AreEqual(456, response.recycling.creditLitleTxnId);
        }

        [Test]
        public void TestRecyclingDataOnVoidResponseIsOptional()
        {
            voidTxn voidTxn = new voidTxn();
            voidTxn.litleTxnId = 123;

            var mock = new Mock<Communications>(new Dictionary<string, StringBuilder>());

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.16' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><voidResponse><litleTxnId>123</litleTxnId><response>000</response><responseTime>2013-01-31T15:48:09</responseTime><postDate>2013-01-31</postDate><message>Approved</message></voidResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litleOnlineResponseTransactionResponseVoidResponse response = litle.DoVoid(voidTxn);
            Assert.AreEqual(123, response.litleTxnId);
            Assert.IsNull(response.recycling);
        }

    }
}

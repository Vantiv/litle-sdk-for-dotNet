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
    class TestCancelSubscription
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
            cancelSubscription update = new cancelSubscription();
            update.subscriptionId = 12345;
           
            var mock = new Mock<Communications>(new Dictionary<string, StringBuilder>());

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<litleOnlineRequest.*?<cancelSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n</cancelSubscription>\r\n</litleOnlineRequest>.*?.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.20' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><cancelSubscriptionResponse><subscriptionId>12345</subscriptionId></cancelSubscriptionResponse></litleOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.CancelSubscription(update);
        }


    }
}

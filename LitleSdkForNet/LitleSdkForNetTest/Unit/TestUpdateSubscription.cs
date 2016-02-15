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
    class TestUpdateSubscription
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
            updateSubscription update = new updateSubscription();
            update.billingDate = new DateTime(2002, 10, 9);
            contact billToAddress = new contact();
            billToAddress.name = "Greg Dake";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "sdksupport@litle.com";
            update.billToAddress = billToAddress;
            cardType card = new cardType();
            card.number = "4100000000000001";
            card.expDate = "1215";
            card.type = methodOfPaymentTypeEnum.VI;
            update.card = card;
            update.planCode = "abcdefg";
            update.subscriptionId = 12345;
           
            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<litleOnlineRequest.*?<updateSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n<planCode>abcdefg</planCode>\r\n<billToAddress>\r\n<name>Greg Dake</name>.*?</billToAddress>\r\n<card>\r\n<type>VI</type>.*?</card>\r\n<billingDate>2002-10-09</billingDate>\r\n</updateSubscription>\r\n</litleOnlineRequest>.*?.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.20' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><updateSubscriptionResponse ><litleTxnId>456</litleTxnId><response>000</response><message>Approved</message><responseTime>2013-09-04</responseTime><subscriptionId>12345</subscriptionId></updateSubscriptionResponse></litleOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            updateSubscriptionResponse response = litle.UpdateSubscription(update);
            Assert.AreEqual("12345", response.subscriptionId);
            Assert.AreEqual("456", response.litleTxnId);
            Assert.AreEqual("000", response.response);
            Assert.NotNull(response.responseTime);
        }


    }
}

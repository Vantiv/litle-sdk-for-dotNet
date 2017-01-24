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
    class TestGiftCardAuthReversal
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void TestOriginalTransaction()
        {
            giftCardAuthReversal giftCard = new giftCardAuthReversal();
            giftCard.id = "1";
            giftCard.reportGroup = "Planets";
            giftCard.litleTxnId = 123456789;
            giftCardCardType card = new giftCardCardType();
            giftCard.originalRefCode = "abc123";
            giftCard.originalAmount = 500;
            giftCard.originalTxnTime = new DateTime(2017,01,01);
            giftCard.originalSystemTraceId = 123;
            giftCard.originalSequenceNumber = "123456";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<litleTxnId>123456789</litleTxnId>\r\n<originalRefCode>abc123</originalRefCode>\r\n<originalAmount>500</originalAmount>\r\n<originalTxnTime>2017-01-01T00:00:00Z</originalTxnTime>\r\n<originalSystemTraceId>123</originalSystemTraceId>\r\n<originalSequenceNumber>123456</originalSequenceNumber>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><giftCardAuthReversalResponse><litleTxnId>123</litleTxnId></giftCardAuthReversalResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            giftCardAuthReversalResponse giftCardAuthReversalResponse = litle.GiftCardAuthReversal(giftCard);
            Assert.AreEqual(123, giftCardAuthReversalResponse.litleTxnId);
        }

        [Test]
        public void TestGiftCardCardType()
        {
            giftCardAuthReversal giftCard = new giftCardAuthReversal();
            giftCard.id = "1";
            giftCard.reportGroup = "Planets";
            giftCard.litleTxnId = 123456789;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            giftCard.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n</card>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><giftCardAuthReversalResponse><litleTxnId>123</litleTxnId></giftCardAuthReversalResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            giftCardAuthReversalResponse giftCardAuthReversalResponse = litle.GiftCardAuthReversal(giftCard);
            Assert.AreEqual(123, giftCardAuthReversalResponse.litleTxnId);
        }
    }
}

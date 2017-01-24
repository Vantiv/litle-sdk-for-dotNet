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
    class TestGiftCardCapture
    {
        
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void TestSimpleGiftCardCapture()
        {
            giftCardCapture giftCardCapture = new giftCardCapture();
            giftCardCapture.id = "1";
            giftCardCapture.reportGroup = "Planets";
            giftCardCapture.litleTxnId = 123456000;
            giftCardCapture.captureAmount = 106;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            giftCardCapture.card = card;
            giftCardCapture.originalRefCode = "abc123";
            giftCardCapture.originalAmount = 43534345;
            giftCardCapture.originalTxnTime = new DateTime(2017,01,01);

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<litleTxnId>123456000</litleTxnId>\r\n<captureAmount>106</captureAmount>\r\n<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n</card>\r\n<originalRefCode>abc123</originalRefCode>\r\n<originalAmount>43534345</originalAmount>\r\n<originalTxnTime>2017-01-01T00:00:00Z</originalTxnTime>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><giftCardCaptureResponse><litleTxnId>123</litleTxnId></giftCardCaptureResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.GiftCardCapture(giftCardCapture);
        }
    }
}

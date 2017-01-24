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
    class TestGiftCardCredit
    {
        
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void SimpleCreditTxnId()
        {
            giftCardCredit credit = new giftCardCredit();
            credit.id = "1";
            credit.reportGroup = "planets";
            credit.litleTxnId = 123456000;
            credit.creditAmount = 106;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "4100000000000000";
            card.expDate = "1210";
            credit.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<litleTxnId>123456000</litleTxnId>\r\n<creditAmount>106</creditAmount>\r\n<card>\r\n<type>GC</type>\r\n<number>4100000000000000</number>\r\n<expDate>1210</expDate>\r.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.GiftCardCredit(credit);
        }

        [Test]
        public void SimpleCreditOrderId()
        {
            giftCardCredit credit = new giftCardCredit();
            credit.id = "1";
            credit.reportGroup = "planets";
            credit.orderId = "2111";
            credit.creditAmount = 106;
            credit.orderSource = orderSourceType.echeckppd;
            giftCardCardType card = new giftCardCardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "4100000000000000";
            card.expDate = "1210";
            credit.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<orderId>2111</orderId>\r\n<creditAmount>106</creditAmount>\r\n<orderSource>echeckppd</orderSource>\r\n<card>\r\n<type>GC</type>\r\n<number>4100000000000000</number>\r\n<expDate>1210</expDate>\r.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.GiftCardCredit(credit);
        }
    }
}

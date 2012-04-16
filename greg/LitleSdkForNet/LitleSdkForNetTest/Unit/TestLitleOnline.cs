using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestLitleOnline
    {
        
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void TestAuth()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<litleOnlineRequest.*<authorization.*<card>.*<number>4100000000000002</number>.*</card>.*</authorization>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorize = litle.Authorize(authorization);
            Assert.AreEqual(123, authorize.litleTxnId);
        }
            
    }
}

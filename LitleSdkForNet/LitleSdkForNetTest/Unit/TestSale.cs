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
    class TestSale
    {
        
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void TestFraudFilterOverride()
        {
            sale sale = new sale();
            sale.orderId = "12344";
            sale.amount = 2;
            sale.orderSource = orderSourceType.ecommerce;
            sale.reportGroup = "Planets";
            sale.fraudFilterOverride = false;
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>false</fraudFilterOverride>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Sale(sale);
        }            
    }
}

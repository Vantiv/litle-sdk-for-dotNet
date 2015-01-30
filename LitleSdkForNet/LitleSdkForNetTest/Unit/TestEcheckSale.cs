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
    class TestEcheckSale
    {

        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void TestSecondaryAmount()
        {
            echeckSale echeckSale = new echeckSale();
            echeckSale.orderId = "12344";
            echeckSale.amount = 2;
            echeckSale.secondaryAmount = 1;
            echeckSale.orderSource = orderSourceType.ecommerce;            


            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckSalesResponse><litleTxnId>123</litleTxnId></echeckSalesResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            echeckSalesResponse echeckSalesResponse = litle.EcheckSale(echeckSale);

            Assert.NotNull(echeckSalesResponse);
            Assert.AreEqual(123, echeckSalesResponse.litleTxnId);
        }
    }
}

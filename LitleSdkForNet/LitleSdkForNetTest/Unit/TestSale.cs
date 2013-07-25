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

        [Test]
        public void TestSurchargeAmount()
        {
            sale sale = new sale();
            sale.amount = 2;
            sale.surchargeAmount = 1;
            sale.orderSource = orderSourceType.ecommerce;
            sale.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Sale(sale);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            sale sale = new sale();
            sale.amount = 2;
            sale.orderSource = orderSourceType.ecommerce;
            sale.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Sale(sale);
        }

        [Test]
        public void TestRecurringRequest()
        {
            sale sale = new sale();
            sale.card = new cardType();
            sale.card.type = methodOfPaymentTypeEnum.VI;
            sale.card.number = "4100000000000001";
            sale.card.expDate = "1213";
            sale.orderId = "12344";
            sale.amount = 2;
            sale.orderSource = orderSourceType.ecommerce;
            sale.fraudFilterOverride = true;
            sale.recurringRequest = new recurringRequest();
            sale.recurringRequest.subscription = new subscription();
            sale.recurringRequest.subscription.planCode = "abc123";
            sale.recurringRequest.subscription.numberOfPaymentsRemaining = 12;

            var mock = new Mock<Communications>();
            
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n<recurringRequest>\r\n<subscription>\r\n<planCode>abc123</planCode>\r\n<numberOfPaymentsRemaining>12</numberOfPaymentsRemaining>\r\n</subscription>\r\n</recurringRequest>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Sale(sale);
        }

        [Test]
        public void TestRecurringResponse_Full() {
            String xmlResponse = "<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId><recurringResponse><subscriptionId>12</subscriptionId><responseCode>345</responseCode><responseMessage>Foo</responseMessage><recurringTxnId>678</recurringTxnId></recurringResponse></saleResponse></litleOnlineResponse>";
            litleOnlineResponse litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            saleResponse saleResponse = (saleResponse)litleOnlineResponse.Item;

            Assert.AreEqual(123, saleResponse.litleTxnId);
            Assert.AreEqual(12, saleResponse.recurringResponse.subscriptionId);
            Assert.AreEqual("345", saleResponse.recurringResponse.responseCode);
            Assert.AreEqual("Foo", saleResponse.recurringResponse.responseMessage);
            Assert.AreEqual(678, saleResponse.recurringResponse.recurringTxnId);
        }

        [Test]
        public void TestRecurringResponse_NoRecurringTxnId()
        {
            String xmlResponse = "<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId><recurringResponse><subscriptionId>12</subscriptionId><responseCode>345</responseCode><responseMessage>Foo</responseMessage></recurringResponse></saleResponse></litleOnlineResponse>";
            litleOnlineResponse litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            saleResponse saleResponse = (saleResponse)litleOnlineResponse.Item;

            Assert.AreEqual(123, saleResponse.litleTxnId);
            Assert.AreEqual(12, saleResponse.recurringResponse.subscriptionId);
            Assert.AreEqual("345", saleResponse.recurringResponse.responseCode);
            Assert.AreEqual("Foo", saleResponse.recurringResponse.responseMessage);
            Assert.AreEqual(0,saleResponse.recurringResponse.recurringTxnId);
        }

        [Test]
        public void TestRecurringRequest_Optional()
        {
            sale sale = new sale();
            sale.card = new cardType();
            sale.card.type = methodOfPaymentTypeEnum.VI;
            sale.card.number = "4100000000000001";
            sale.card.expDate = "1213";
            sale.orderId = "12344";
            sale.amount = 2;
            sale.orderSource = orderSourceType.ecommerce;
            sale.fraudFilterOverride = true;

            var mock = new Mock<Communications>();
            
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n</sale>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Sale(sale);
        }

        [Test]
        public void Test_LitleInternalRecurringRequest()
        {
            sale sale = new sale();
            sale.card = new cardType();
            sale.card.type = methodOfPaymentTypeEnum.VI;
            sale.card.number = "4100000000000001";
            sale.card.expDate = "1213";
            sale.orderId = "12344";
            sale.amount = 2;
            sale.orderSource = orderSourceType.ecommerce;
            sale.fraudFilterOverride = true;
            sale.litleInternalRecurringRequest = new litleInternalRecurringRequest();
            sale.litleInternalRecurringRequest.subscriptionId = "123";
            sale.litleInternalRecurringRequest.recurringTxnId = "456";

            var mock = new Mock<Communications>();
            
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("<fraudFilterOverride>true</fraudFilterOverride>\r\n<litleInternalRecurringRequest>\r\n<subscriptionId>123</subscriptionId>\r\n<recurringTxnId>456</recurringTxnId>\r\n</litleInternalRecurringRequest>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Sale(sale);
        }

        public void Test_LitleInternalRecurringRequest_Optional()
        {
            sale sale = new sale();
            sale.card = new cardType();
            sale.card.type = methodOfPaymentTypeEnum.VI;
            sale.card.number = "4100000000000001";
            sale.card.expDate = "1213";
            sale.orderId = "12344";
            sale.amount = 2;
            sale.orderSource = orderSourceType.ecommerce;
            sale.fraudFilterOverride = true;

            var mock = new Mock<Communications>();
            
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n</sale>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Sale(sale);
        }
    }
}

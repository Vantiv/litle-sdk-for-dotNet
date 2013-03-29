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
    class TestAuthorization
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
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            auth.fraudFilterOverride = true;
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Authorize(auth);
        }

        [Test]
        public void TestContactShouldSendEmailForEmail_NotZip()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            contact billToAddress = new contact();
            billToAddress.email = "gdake@litle.com";
            billToAddress.zip = "12345";
            auth.billToAddress = billToAddress;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<zip>12345</zip>.*<email>gdake@litle.com</email>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Authorize(auth);
        }

        [Test]
        public void Test3dsAttemptedShouldNotSayItem()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.item3dsAttempted;
            auth.reportGroup = "Planets";
            contact billToAddress = new contact();
            billToAddress.email = "gdake@litle.com";
            billToAddress.zip = "12345";
            auth.billToAddress = billToAddress;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>.*<orderSource>3dsAttempted</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Authorize(auth);
        }

        [Test]
        public void Test3dsAuthenticatedShouldNotSayItem()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.item3dsAuthenticated;
            auth.reportGroup = "Planets";
            contact billToAddress = new contact();
            billToAddress.email = "gdake@litle.com";
            billToAddress.zip = "12345";
            auth.billToAddress = billToAddress;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>.*<orderSource>3dsAuthenticated</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Authorize(auth);
        }

        [Test]
        public void TestSurchargeAmount()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.surchargeAmount = 1;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Authorize(auth);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Authorize(auth);
        }

    }
}

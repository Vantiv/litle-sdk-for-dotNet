using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestEcheckVoid
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
            echeckVoid echeckVoid = new echeckVoid();
            echeckVoid.litleTxnId = 123456789;
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<echeckVoid.*<litleTxnId>123456789.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.13' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckVoidResponse><litleTxnId>123</litleTxnId></echeckVoidResponse></litleOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.EcheckVoid(echeckVoid);
        }

        [Test]
        public void simpleForceCaptureWithSecondaryAmount()
        {
            forceCapture forcecapture = new forceCapture();
            forcecapture.id = "1";
            forcecapture.amount = 106;
            forcecapture.secondaryAmount = 50;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000000";
            card.expDate = "1210";
            forcecapture.card = card;

            var expectedResponse =
                "<litleOnlineResponse xmlns=\"http://www.litle.com/schema\" version=\"10.8\" response=\"0\" message=\"Valid Format\">" +
                "<forceCaptureResponse id=\"1\" reportGroup=\"Default Report Group\">" +
                "<litleTxnId>986922693522351414</litleTxnId>" +
                "<response>000</response>" +
                "<responseTime>2018-06-18T20:20:33.092</responseTime>" +
                "<message>Approved</message>" +
                "</forceCaptureResponse>" +
                "</litleOnlineResponse>";
            
            var mock = new Mock<Communications>();
            mock.Setup(Communications =>
                    Communications.HttpPost(It.IsRegex(".*<forceCapture.*<orderId>12344.*", RegexOptions.Singleline),
                        It.IsAny<Dictionary<String, String>>()))
                .Returns(expectedResponse);
            
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            
            forceCaptureResponse response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

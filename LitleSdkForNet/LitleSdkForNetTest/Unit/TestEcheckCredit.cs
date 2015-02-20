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
    class TestEcheckCredit
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
            echeckCredit echeckCredit = new echeckCredit();
            echeckCredit.orderId = "12344";
            echeckCredit.amount = 2;
            echeckCredit.secondaryAmount = 1;
            echeckCredit.orderSource = orderSourceType.ecommerce;


            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckCreditResponse><litleTxnId>123</litleTxnId></echeckCreditResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            echeckCreditResponse echeckCreditResponse = litle.EcheckCredit(echeckCredit);

            Assert.NotNull(echeckCreditResponse);
            Assert.AreEqual(123, echeckCreditResponse.litleTxnId);
        }
    }
}

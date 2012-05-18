using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestCredit
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void setUp()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void SimpleCreditWithCard()
        {
            credit creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            
            creditObj.card = card;
            
            creditResponse response = litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCreditWithPaypal()
        {
            credit creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "123456";
            creditObj.orderSource = orderSourceType.ecommerce;
            payPal payPalObj = new payPal();
            payPalObj.payerId = "1234";

            creditObj.paypal = payPalObj;

            creditResponse response = litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void PaypalNotes()
        {
            credit creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "123456";
            creditObj.payPalNotes = "Hello";
            creditObj.orderSource = orderSourceType.ecommerce;

            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";

            creditObj.card = card;
            
            creditResponse response = litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ProcessingInstructionAndAmexData()
        {
            credit creditObj = new credit();
            creditObj.amount = 2000;
            creditObj.orderId = "12344";
            creditObj.orderSource = orderSourceType.ecommerce;

            processingInstructions processingInstructionsObj = new processingInstructions();
            processingInstructionsObj.bypassVelocityCheck = true;

            creditObj.processingInstructions = processingInstructionsObj;
            
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";

            creditObj.card = card;

            creditResponse response = litle.Credit(creditObj);
            Assert.AreEqual("Approved", response.message);
        }
            
    }
}

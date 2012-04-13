using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using LitleSdkForNet;

namespace LitleSdkForNetTest.Functional
{
    [TestFixture]
    class TestForceCapture
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void setUp()
        {
            litle = new LitleOnline();
        }


        [Test]
        public void simpleForceCaptureWithCard() {
            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = "106";
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            forcecapture.card = card;
            forceCaptureResponse response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleForceCaptureWithToken() {
            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = "106";
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardTokenType token = new cardTokenType();
            token.litleToken = "123456789101112";
            token.expDate = "1210";
            token.cardValidationNum = "555";
            token.type = methodOfPaymentTypeEnum.VI;
            forcecapture.token = token;
            forceCaptureResponse response = litle.ForceCapture(forcecapture);
            Assert.AreEqual("Approved", response.message); ;
        }
            
    }
}

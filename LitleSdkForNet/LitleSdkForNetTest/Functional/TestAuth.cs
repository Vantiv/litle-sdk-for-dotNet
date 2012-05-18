using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestAuth
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void SimpleAuthWithCard()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            authorization.card = card; //This needs to compile

            customBilling cb = new customBilling();
            cb.phone = "1112223333"; //This needs to compile too            

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void simpleAuthWithPaypal()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "123456";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            payPal paypal = new payPal();
            paypal.payerId = "1234";
            paypal.token = "1234";
            paypal.transactionId = "123456";
            authorization.paypal = paypal; //This needs to compile

            customBilling cb = new customBilling();
            cb.phone = "1112223333"; //This needs to compile too            

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void posWithoutCapabilityAndEntryMode()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            pos pos = new pos();
            pos.cardholderId = posCardholderIdTypeEnum.pin;
            authorization.pos = pos;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card; //This needs to compile

            customBilling cb = new customBilling();
            cb.phone = "1112223333"; //This needs to compile too            

            try
            {
                litle.Authorize(authorization);
                //expected exception;
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
            
    }
}

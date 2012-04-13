using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

        //[Test]
        //public void SimpleTokenWithEcheck()
        //{
        //    LitleOnline lOnlineObj = new LitleOnline();
        //    registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
        //    registerTokenRequest.orderId = "12344";
        //    registerTokenRequest.echeckForToken.accNum = "12344565";
        //    registerTokenRequest.echeckForToken.routingNum = "123476545";
        //    registerTokenRequest.reportGroup = "Planets";
        //    registerTokenResponse rtokenResponse = lOnlineObj.RegisterToken(registerTokenRequest);
        //    StringAssert.AreEqualIgnoringCase("Valid Format", rtokenResponse.message);
        //}

        //[Test]
        //public void TokenEcheckMissingRequiredField()
        //{
        //    LitleOnline lOnlineObj = new LitleOnline();
        //    registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
        //    registerTokenRequest.orderId = "12344";
        //    registerTokenRequest.echeckForToken.routingNum = "123476545";
        //    registerTokenRequest.reportGroup = "Planets";
        //    registerTokenResponse rtokenResponse = lOnlineObj.RegisterToken(registerTokenRequest);
        //    StringAssert.Contains("Error validating xml data against the schema", rtokenResponse.message);
        //}
            

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestSale
    {
        [Test]
        public void SimpleSaleWithCard()
        {
            LitleOnline lOnlineObj = new LitleOnline();
            sale saleObj = new sale();
            saleObj.amount = "106";
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            cardType cardObj = new cardType();
            cardObj.type = methodOfPaymentTypeEnum.VI;
            cardObj.number = "4100000000000002";
            cardObj.expDate = "1210";
            saleObj.card = cardObj;

            saleResponse responseObj = lOnlineObj.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithPayPal()
        {
            LitleOnline lOnlineObj = new LitleOnline();
            sale saleObj = new sale();
            saleObj.amount = "106";
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            payPal payPalObj = new payPal();
            payPalObj.payerId = "1234";
            payPalObj.token = "1234";
            payPalObj.transactionId = "123456";
            saleObj.paypal = payPalObj;
            saleResponse responseObj = lOnlineObj.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }
            
    }
}

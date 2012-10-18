using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestSale
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void setUp()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "65");
            config.Add("merchantId", "101");
            config.Add("password", "TESTCASE");
            config.Add("printxml", "true");
            litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleSaleWithCard()
        {
            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            cardType cardObj = new cardType();
            cardObj.type = methodOfPaymentTypeEnum.VI;
            cardObj.number = "4100000000000000";
            cardObj.expDate = "1210";
            saleObj.card = cardObj;

            saleResponse responseObj = litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithPayPal()
        {
            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            payPal payPalObj = new payPal();
            payPalObj.payerId = "1234";
            payPalObj.token = "1234";
            payPalObj.transactionId = "123456";
            saleObj.paypal = payPalObj;
            saleResponse responseObj = litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }
            
    }
}

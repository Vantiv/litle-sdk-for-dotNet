using System;
using System.Collections.Generic;
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
        public void SimpleAuthWithMpos()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 200;
            authorization.orderSource = orderSourceType.ecommerce;
            mposType mpos = new mposType();
            mpos.ksn = "77853211300008E00016";
            mpos.encryptedTrack = "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD";
            mpos.formatId = "30";
            mpos.track1Status = 0;
            mpos.track2Status = 0;
            authorization.mpos = mpos; //This needs to compile
       

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

        [Test]
        public void trackData()
        {
            authorization authorization = new authorization();
            authorization.id = "AX54321678";
            authorization.reportGroup = "RG27";
            authorization.orderId = "12z58743y1";
            authorization.amount = 12522L;
            authorization.orderSource = orderSourceType.retail;
            contact billToAddress = new contact();
            billToAddress.zip = "95032";
            authorization.billToAddress = billToAddress;
            cardType card = new cardType();
            card.track = "%B40000001^Doe/JohnP^06041...?;40001=0604101064200?";
            authorization.card = card;
            pos pos = new pos();
            pos.capability = posCapabilityTypeEnum.magstripe;
            pos.entryMode = posEntryModeTypeEnum.completeread;
            pos.cardholderId = posCardholderIdTypeEnum.signature;
            authorization.pos = pos;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void testAuthHandleSpecialCharacters()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "<'&\">";
            authorization.orderId = "123456";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            payPal paypal = new payPal();
            paypal.payerId = "1234";
            paypal.token = "1234";
            paypal.transactionId = "123456";
            authorization.paypal = paypal; //This needs to compile

            customBilling cb = new customBilling();
            cb.phone = "<'&\">"; //This needs to compile too            

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

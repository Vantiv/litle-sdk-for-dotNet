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
        private Dictionary<string, string> config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "11.0"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

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

            authorization.originalNetworkTransactionId = "123456789012345678901234567890";
            authorization.originalTransactionAmount = 2500;
            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }
        [Test]
        public void SimpleAuthWithCard_networkTxnId()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "4",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                processingType = processingTypeEnum.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "410080000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" },
                originalNetworkTransactionId = "63225578415568556365452427825"
            };

            var response = litle.Authorize(authorization);
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
        public void AuthWithAmpersand()
        {
            authorization authorization = new authorization();
            authorization.orderId = "1";
            authorization.amount = 10010;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "John & Jane Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010000000009";
            card.expDate = "0112";
             card.cardValidationNum = "349";
             authorization.card = card;
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
        public void simpleAuthWithApplepayAndSecondaryAmount()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "123456";
            authorization.amount = 110;
            authorization.secondaryAmount = 50;
            authorization.orderSource = orderSourceType.applepay;
            applepayType applepay = new applepayType();
            applepayHeaderType applepayHeaderType = new applepayHeaderType();
            applepayHeaderType.applicationData = "454657413164";
            applepayHeaderType.ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            applepayHeaderType.publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            applepayHeaderType.transactionId = "1234";
            applepay.header = applepayHeaderType;
            applepay.data = "user";
            applepay.signature = "sign";
            applepay.version = "12345";
            authorization.applepay = applepay;       

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("Insufficient Funds", response.message);
            Assert.AreEqual("110", response.applepayResponse.transactionAmount);
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

        [Test]
        public void TestNotHavingTheLogFileSettingShouldDefaultItsValueToNull()
        {
            config.Remove("logFile");

            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestNeuterAccountNumsShouldDefaultToFalse()
        {
            config.Remove("neuterAccountNums");

            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestPrintxmlShouldDefaultToFalse()
        {
            config.Remove("printxml");

            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            authorization.card = card;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }
        [Test]
        public void SimpleAuthWithCardAccountFunding()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                processingType = processingTypeEnum.accountFunding,
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = litle.Authorize(authorization);

            Assert.AreEqual("000", response.response);
        }
        [Test]
        public void SimpleAuthWithCardInitialRecurring()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                processingType = processingTypeEnum.initialRecurring,
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = litle.Authorize(authorization);

            Assert.AreEqual("000", response.response);
        }
        [Test]
        public void SimpleAuthWithCardInitialInstallment()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                processingType = processingTypeEnum.initialInstallment,
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = litle.Authorize(authorization);

            Assert.AreEqual("000", response.response);
        }
        [Test]
        public void SimpleAuthWithCardInitialCOF()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                processingType = processingTypeEnum.initialCOF,
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = litle.Authorize(authorization);

            Assert.AreEqual("000", response.response);
        }
        [Test]
        public void SimpleAuthWithCardMerchantInitiatedCOF()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                processingType = processingTypeEnum.merchantInitiatedCOF,
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = litle.Authorize(authorization);

            Assert.AreEqual("000", response.response);
        }
        [Test]
        public void SimpleAuthWithCardCardHolderInitiatedCOF()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                processingType = processingTypeEnum.merchantInitiatedCOF,
                customBilling = new customBilling { phone = "1112223333" }
            };
            var response = litle.Authorize(authorization);

            Assert.AreEqual("000", response.response);
        }
        [Test]
        public void AuthWithChanges8_32And8_33()
        {
            authorization authorization = new authorization();
            authorization.orderId = "1";
            authorization.amount = 10010;
            authorization.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "John & Jane Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            authorization.billToAddress = contact;
            contact contact1 = new contact();
            contact1.name = "John & Jane Smith";
            contact1.addressLine1 = "1 Main St.";
            contact1.city = "Burlington";
            contact1.state = "MA";
            contact1.zip = "01803-3747";
            contact1.country = countryTypeEnum.US;
            contact1.sellerId = "172354";
            contact1.url = "www.google.com";
            authorization.retailerAddress = contact1;
            additionalCOFData additionalCOFData = new additionalCOFData();        
            additionalCOFData.totalPaymentCount = "35";
            additionalCOFData.paymentType = paymentTypeEnum.Fixed_Amount;
            additionalCOFData.uniqueId = "12345wereew233";
            additionalCOFData.frequencyOfMIT = frequencyOfMITEnum.BiWeekly;
            additionalCOFData.validationReference = "re3298rhriw4wrw";
            additionalCOFData.sequenceIndicator = 2;
            authorization.additionalCOFData = additionalCOFData;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010000000009";
            card.expDate = "0112";
            card.cardValidationNum = "349";
            authorization.card = card;
            mposType mpos = new mposType();
            mpos.ksn = "77853211300008E00016";
            mpos.encryptedTrack = "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD";
            mpos.formatId = "30";
            mpos.track1Status = 0;
            mpos.track2Status = 0;
            authorization.mpos = mpos;
            authorization.merchantCategoryCode = "1234";
            authorization.BusinessIndicator = businessIndicatorEnum.consumerBillPayment;
            authorization.crypto = true;
            authorization.authIndicator = authIndicatorEnum.Incremental;
            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void AuthWithAuthIndicatorAndAmount()
        {
            authorization authorization = new authorization();
            authorization.litleTxnId = 1254654654;
            authorization.amount = 1001;
            authorization.authIndicator = authIndicatorEnum.Incremental;          
            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }
    }
}

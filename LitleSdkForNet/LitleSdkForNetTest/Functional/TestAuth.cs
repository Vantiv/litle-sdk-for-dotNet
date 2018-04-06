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
            config = new Dictionary<string, string>();
            config.Add("url", "https://www.testvantivcnp.com/sandbox/new/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "65");
            config.Add("merchantId", "101");
            config.Add("password", "TESTCASE");
            config.Add("printxml", "true");
            config.Add("proxyHost", Properties.Settings.Default.proxyHost);
            config.Add("proxyPort", Properties.Settings.Default.proxyPort);
            config.Add("logFile", Properties.Settings.Default.logFile);
            config.Add("neuterAccountNums", "true");
            litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleAuthWithCard()
        {
            authorization authorization = new authorization();
            authorization.id = "1";
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
            authorization.id = "2";
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
            authorization.id = "rrggbb";
            authorization.orderId = "1985";
            authorization.amount = 10010;
            authorization.reportGroup = "Planets";
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
            authorization.id = "3";
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
        public void SimpleAuthWithAndroidPayWithVI()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "9",
                amount = 106,
                orderSource = orderSourceType.androidpay,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = litle.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("01", response.androidpayResponse.expMonth);
            Assert.AreEqual("2050", response.androidpayResponse.expYear);
            Assert.IsNotEmpty(response.androidpayResponse.cryptogram);
        }
        
        [Test]
        public void SimpleAuthWithAndroidpayWithMC()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.androidpay,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.MC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234",
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("aHR0cHM6Ly93d3cueW91dHViZS5jb20vd2F0Y2g/dj1kUXc0dzlXZ1hjUQ0K", response.androidpayResponse.cryptogram);
            Assert.AreEqual("01", response.androidpayResponse.expMonth);
            Assert.AreEqual("2050", response.androidpayResponse.expYear);
        }
        

        [Test]
        public void simpleAuthWithApplepayAndSecondaryAmountAndWallet()
        {
            authorization authorization = new authorization();
            authorization.id = "4";
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

            wallet wallet = new wallet();
            wallet.walletSourceTypeId = "123";
            wallet.walletSourceType = walletWalletSourceType.MasterPass;
            authorization.wallet = wallet;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("Insufficient Funds", response.message);
            Assert.AreEqual("110", response.applepayResponse.transactionAmount);
        }
        
        [Test]
        public void SimpleAuthWithCard_origTxnIdAndAmount()
        {
            var authorization = new authorization
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "5",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                originalNetworkTransactionId = "123456789012345678901234567890",
                originalTransactionAmount = 2500,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "410000000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void posWithoutCapabilityAndEntryMode()
        {
            authorization authorization = new authorization();
            authorization.id = "5";
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
        public void TestEnhancedAuthResponse()
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
                    number = "4100322311199000",
                    expDate = "1210",
                },
                originalNetworkTransactionId = "123456789123456789123456789",
                originalTransactionAmount = 12,
                processingType = processingTypeEnumType.initialRecurring,
            };

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("63225578415568556365452427825", response.networkTransactionId);
        }
        
        [Test]
        public void TestEnhancedAuthResponseWithNetworkResponse()
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
                    number = "4100822311199000",
                    expDate = "1210",
                },
                originalNetworkTransactionId = "123456789123456789123456789",
                originalTransactionAmount = 12,
                processingType = processingTypeEnumType.initialInstallment,
            };
            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);

            Assert.AreEqual("63225578415568556365452427825", response.networkTransactionId);
            Assert.AreEqual("visa", response.enhancedAuthResponse.networkResponse.endpoint);
            Assert.AreEqual(4, response.enhancedAuthResponse.networkResponse.networkField.fieldNumber);
            Assert.AreEqual("Transaction Amount", response.enhancedAuthResponse.networkResponse.networkField.fieldName);
            Assert.AreEqual("135798642", response.enhancedAuthResponse.networkResponse.networkField.fieldValue);
        }
        
        
        [Test]
        public void TestEnhancedAuthResponseWithProcessingType_initialCOF()
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
                    number = "4100822311199000",
                    expDate = "1210",
                },
                originalNetworkTransactionId = "123456789123456789123456789",
                originalTransactionAmount = 12,
                processingType = processingTypeEnumType.initialCOF,
            };
            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);

            Assert.AreEqual("63225578415568556365452427825", response.networkTransactionId);
            Assert.AreEqual("visa", response.enhancedAuthResponse.networkResponse.endpoint);
            Assert.AreEqual(4, response.enhancedAuthResponse.networkResponse.networkField.fieldNumber);
            Assert.AreEqual("Transaction Amount", response.enhancedAuthResponse.networkResponse.networkField.fieldName);
            Assert.AreEqual("135798642", response.enhancedAuthResponse.networkResponse.networkField.fieldValue);
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
            authorization.id = "6";
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
            authorization.id = "7";
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
            authorization.id = "8";
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
            authorization.id = "9";
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
        public void TestWithAdvancedFraudCheck()
        {
            config.Remove("printxml");

            authorization authorization = new authorization();
            authorization.id = "10";
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            authorization.card = card;
            advancedFraudChecksType advancedFraudChecks = new advancedFraudChecksType();
            advancedFraudChecks.threatMetrixSessionId = "800";
            advancedFraudChecks.customAttribute1 = "testAttribute1";
            advancedFraudChecks.customAttribute2 = "testAttribute2";
            advancedFraudChecks.customAttribute3 = "testAttribute3";
            advancedFraudChecks.customAttribute4 = "testAttribute4";
            advancedFraudChecks.customAttribute5 = "testAttribute5";
            authorization.advancedFraudChecks = advancedFraudChecks;

            authorizationResponse response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }
        
        [Test]
        public void SimpleAuthWithCardPin()
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
                    type = methodOfPaymentTypeEnum.MC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "1234",
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            
            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }
    }
}

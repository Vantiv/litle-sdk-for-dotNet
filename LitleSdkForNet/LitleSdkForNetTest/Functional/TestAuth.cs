using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestAuth
    {
        private LitleOnline _litle;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _config = new Dictionary<string, string>
            {
                {"url", "https://www.testlitle.com/sandbox/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "9.10.0"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };
            
            _litle = new LitleOnline(_config);
        }

        [Test]
        public void SimpleAuthWithCard()
        {
            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "1",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            
            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void SimpleAuthWithMasterCard()
        {
            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "2",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.MC,
                    number = "540000000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.Null(response.networkTransactionId);
        }

        [Test]
        public void SimpleAuthWithCard_CardSuffixResponse()
        {
            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "3",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "410070000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            
            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("123456", response.cardSuffix);
        }

        [Test]
        public void SimpleAuthWithCard_networkTxnId()
        {
            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "4",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.initialInstallment,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "410080000000000000",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("63225578415568556365452427825", response.networkTransactionId);
        }

        [Test]
        public void SimpleAuthWithCard_origTxnIdAndAmount()
        {
            var authorization = new authorization
            {
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
           
            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void SimpleAuthWithMpos()
        {
            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "6",
                amount = 200,
                orderSource = orderSourceType.ecommerce,
                mpos = new mposType
                {
                    ksn = "77853211300008E00016",
                    encryptedTrack =
                    "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD",
                    formatId = "30",
                    track1Status = 0,
                    track2Status = 0
                }
            };

            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void AuthWithAmpersand()
        {
            var authorization = new authorization
            {
                orderId = "7",
                amount = 10010,
                orderSource = orderSourceType.ecommerce,
                billToAddress = new contact
                {
                    name = "John & Jane Smith",
                    addressLine1 = "1 Main St.",
                    city = "Burlington",
                    state = "MA",
                    zip = "01803-3747",
                    country = countryTypeEnum.US
                },
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4457010000000009",
                    expDate = "0112",
                    cardValidationNum = "349"
                }
            };
            
            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void SimpleAuthWithPaypal()
        {
            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "8",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                paypal = new payPal
                {
                    payerId = "1234",
                    token = "1234",
                    transactionId = "123456"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };

            var response = _litle.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleAuthWithAndroidPay()
        {
            var authorization = new authorization
            {
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
            
            var response = _litle.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("01", response.androidpayResponse.expMonth);
            Assert.AreEqual("2050", response.androidpayResponse.expYear);
            Assert.IsNotEmpty(response.androidpayResponse.cryptogram);
        }

        [Test]
        public void simpleAuthWithApplepayAndSecondaryAmountAndWallet_MasterPass()
        {
            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "10",
                amount = 110,
                secondaryAmount = 50,
                orderSource = orderSourceType.applepay,
                applepay = new applepayType
                {
                    data = "user",
                    signature = "sign",
                    version = "12345",
                    header = new applepayHeaderType
                    {
                        applicationData = "454657413164",
                        ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        transactionId = "1234"
                    }
                },
                wallet = new wallet
                {
                    walletSourceTypeId = "123",
                    walletSourceType = walletWalletSourceType.MasterPass
                }
            };

            var response = _litle.Authorize(authorization);
            Assert.AreEqual("Insufficient Funds", response.message);
            Assert.AreEqual("110", response.applepayResponse.transactionAmount);
        }

        [Test]
        public void simpleAuthWithApplepayAndSecondaryAmountAndWallet_VisaCheckout()
        {
            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "11",
                amount = 110,
                secondaryAmount = 50,
                orderSource = orderSourceType.applepay,
                applepay = new applepayType
                {
                    data = "user",
                    signature = "sign",
                    version = "12345",
                    header = new applepayHeaderType
                    {
                        applicationData = "454657413164",
                        ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        transactionId = "1234"
                    }
                },
                wallet = new wallet
                {
                    walletSourceTypeId = "123",
                    walletSourceType = walletWalletSourceType.VisaCheckout
                }
            };

            var response = _litle.Authorize(authorization);
            Assert.AreEqual("Insufficient Funds", response.message);
            Assert.AreEqual("110", response.applepayResponse.transactionAmount);
        }

        [Test]
        public void PosWithoutCapabilityAndEntryMode()
        {
            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "12",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                pos = new pos { cardholderId = posCardholderIdTypeEnum.pin },
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000002",
                    expDate = "1210"
                },
                customBilling = new customBilling { phone = "1112223333" }
            };
            
            try
            {
                _litle.Authorize(authorization);
                Assert.Fail("Exception is expected!");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void TrackData()
        {
            var authorization = new authorization
            {
                id = "AX54321678",
                reportGroup = "RG27",
                orderId = "13",
                amount = 12522L,
                orderSource = orderSourceType.retail,
                billToAddress = new contact { zip = "95032" },
                card = new cardType { track = "%B40000001^Doe/JohnP^06041...?;40001=0604101064200?" },
                pos = new pos
                {
                    capability = posCapabilityTypeEnum.magstripe,
                    entryMode = posEntryModeTypeEnum.completeread,
                    cardholderId = posCardholderIdTypeEnum.signature
                }
            };
           
            var response = _litle.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestAuthHandleSpecialCharacters()
        {
            var authorization = new authorization
            {
                reportGroup = "<'&\">",
                orderId = "14",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                paypal = new payPal
                {
                    payerId = "1234",
                    token = "1234",
                    transactionId = "123456"
                }
            };

            var response = _litle.Authorize(authorization);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void TestNotHavingTheLogFileSettingShouldDefaultItsValueToNull()
        {
            _config.Remove("logFile");

            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "15",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                }
            };
            
            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestNeuterAccountNumsShouldDefaultToFalse()
        {
            _config.Remove("neuterAccountNums");

            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "16",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                }
            };
            
            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestPrintxmlShouldDefaultToFalse()
        {
            _config.Remove("printxml");

            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "17",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                }
            };
            
            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }

        [Test]
        public void TestWithAdvancedFraudCheck()
        {
            _config.Remove("printxml");

            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "18",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "414100000000000000",
                    expDate = "1210"
                },
                advancedFraudChecks = new advancedFraudChecksType
                {
                    threatMetrixSessionId = "800",
                    customAttribute1 = "testAttribute1",
                    customAttribute2 = "testAttribute2",
                    customAttribute3 = "testAttribute3",
                    customAttribute4 = "testAttribute4",
                    customAttribute5 = "testAttribute5"
                }
            };
            
            var response = _litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
        }
    }
}

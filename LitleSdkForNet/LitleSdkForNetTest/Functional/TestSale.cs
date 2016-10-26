using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestSale
    {
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var config = new Dictionary<string, string>
            {
                {"url", "https://www.testlitle.com/sandbox/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "9.10"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };

            _litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleSaleWithCard()
        {
            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };
            
            var responseObj = _litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SaleWithCard_AndroidpayResponse()
        {
            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.androidpay,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                }
            };
           
            var responseObj = _litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
            Assert.AreEqual("01", responseObj.androidpayResponse.expMonth);
            Assert.AreEqual("2050", responseObj.androidpayResponse.expYear);
        }

        [Test]
        public void SaleWithCard_withProcessingTypeAndCardSuffixResponse()
        {
            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                originalNetworkTransactionId = "12345678912345",
                originalTransactionAmount = 1492,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100700000000000",
                    expDate = "1210"
                }
            };
            
            var responseObj = _litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
            Assert.AreEqual("123456", responseObj.cardSuffix);
        }

        [Test]
        public void SaleWithCard_withProcessingTypeAndNetworkTxnId()
        {
            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                originalNetworkTransactionId = "12345678912345",
                originalTransactionAmount = 1492,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100700000000000",
                    expDate = "1210"
                }
            };
            
            var responseObj = _litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
            Assert.AreEqual("63225578415568556365452427825", responseObj.networkTransactionId);
        }

        [Test]
        public void SaleWithMasterCard_withProcessingTypeAndNetworkTxnId()
        {
            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.accountFunding,
                originalNetworkTransactionId = "12345678912345",
                originalTransactionAmount = 1492,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "5400700000000000",
                    expDate = "1210"
                }
            };

            var responseObj = _litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
            Assert.Null(responseObj.networkTransactionId);
        }

        [Test]
        public void SimpleSaleWithMpos()
        {
            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
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

            var responseObj = _litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithPayPal()
        {
            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                paypal = new payPal
                {
                    payerId = "1234",
                    token = "1234",
                    transactionId = "123456"
                }
            };
            
            var responseObj = _litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithApplepayAndSecondaryAmountAndWallet()
        {
            var saleObj = new sale
            {
                amount = 110,
                secondaryAmount = 50,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
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
            
            var responseObj = _litle.Sale(saleObj);
            Assert.AreEqual("Insufficient Funds", responseObj.message);
            Assert.AreEqual("110", responseObj.applepayResponse.transactionAmount);
        }

        [Test]
        public void SimpleSaleWithInvalidFraudCheck()
        {
            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                cardholderAuthentication = new fraudCheckType
                {
                    authenticationValue = "1234567890123456789012345678901234567890123456789012345678907897987987897897987897979797897897979797997798789"
                }
            };
            
            try
            {
                _litle.Sale(saleObj);
                Assert.Fail("Exception expected!");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
    }
}

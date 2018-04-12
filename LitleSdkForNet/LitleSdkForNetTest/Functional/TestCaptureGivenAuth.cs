using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCaptureGivenAuth
    {

        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var config = new Dictionary<string, string>
            {
                {"url", "https://www.testvantivcnp.com/sandbox/new/sandbox/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "8.13"},
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
        public void SimpleCaptureGivenAuthWithCard() {
            var capturegivenauth = new captureGivenAuth
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                }
            };
            
            var response = _litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithMpos()
        {
            var capturegivenauth = new captureGivenAuth
            {
                amount = 500,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                },
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
           
            var response = _litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithToken() {
            var capturegivenauth = new captureGivenAuth
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                token = new cardTokenType
                {
                    litleToken = "123456789101112",
                    expDate = "1210",
                    cardValidationNum = "555",
                    type = methodOfPaymentTypeEnum.VI
                },
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                }
            };
            
            var response = _litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ComplexCaptureGivenAuth() {
            var capturegivenauth = new captureGivenAuth
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "litle.com"
                },
                processingInstructions = new processingInstructions { bypassVelocityCheck = true }
            };
            
            var response = _litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void AuthInfo() {
            var capturegivenauth = new captureGivenAuth
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345,
                    fraudResult = new fraudResult
                    {
                        avsResult = "12",
                        cardValidationResult = "123",
                        authenticationResult = "1",
                        advancedAVSResult = "123"
                    }
                }
            };

            var response = _litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithTokenAndSpecialCharacters()
        {
            var capturegivenauth = new captureGivenAuth
            {
                amount = 106,
                orderId = "<'&\">",
                orderSource = orderSourceType.ecommerce,
                token = new cardTokenType
                {
                    litleToken = "123456789101112",
                    expDate = "1210",
                    cardValidationNum = "555",
                    type = methodOfPaymentTypeEnum.VI
                },
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                }
            };
           
            var response = _litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithSecondaryAmount()
        {
            var capturegivenauth = new captureGivenAuth
            {
                amount = 106,
                secondaryAmount = 50,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                },
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                processingType = processingType.initialCOF
            };

            var response = _litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureGivenAuthWithprocessingTypeOrigNetworkTxnIdAndOrigTxnAmount()
        {
            var capturegivenauth = new captureGivenAuth
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                },
                processingType = processingType.initialRecurring,
                originalTransactionAmount = 42,
                originalNetworkTransactionId = "12345678901234567890"
            };

            var response = _litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCaptureGivenAuth
    {
        private LitleOnline litle;
        private IDictionary<string, StringBuilder> _memoryCache;

        [TestFixtureSetUp]
        public void setUp()
        {
            _memoryCache = new Dictionary<string, StringBuilder>();
            var config = new Dictionary<string, string>();
            config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "5000");
            config.Add("merchantId", "101");
            config.Add("password", "TESTCASE");
            config.Add("printxml", "true");
            config.Add("proxyHost", Settings.Default.proxyHost);
            config.Add("proxyPort", Settings.Default.proxyPort);
            config.Add("logFile", Settings.Default.logFile);
            config.Add("neuterAccountNums", "true");
            litle = new LitleOnline(_memoryCache, config);
        }

        [Test]
        public void simpleCaptureGivenAuthWithCard()
        {
            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000000";
            card.expDate = "1210";
            capturegivenauth.card = card;
            var response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleCaptureGivenAuthWithMpos()
        {
            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 500;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            var mpos = new mposType();
            mpos.ksn = "77853211300008E00016";
            mpos.encryptedTrack =
                "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD";
            mpos.formatId = "30";
            mpos.track1Status = 0;
            mpos.track2Status = 0;
            capturegivenauth.mpos = mpos;
            var response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleCaptureGivenAuthWithToken()
        {
            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            var cardtoken = new cardTokenType();
            cardtoken.litleToken = "123456789101112";
            cardtoken.expDate = "1210";
            cardtoken.cardValidationNum = "555";
            cardtoken.type = methodOfPaymentTypeEnum.VI;
            capturegivenauth.token = cardtoken;
            var response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void complexCaptureGivenAuth()
        {
            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            var contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            capturegivenauth.billToAddress = contact;
            var processinginstructions = new processingInstructions();
            processinginstructions.bypassVelocityCheck = true;
            capturegivenauth.processingInstructions = processinginstructions;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000000";
            card.expDate = "1210";
            capturegivenauth.card = card;
            var response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void authInfo()
        {
            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            var fraudresult = new fraudResult();
            fraudresult.avsResult = "12";
            fraudresult.cardValidationResult = "123";
            fraudresult.authenticationResult = "1";
            fraudresult.advancedAVSResult = "123";
            authInfo.fraudResult = fraudresult;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000000";
            card.expDate = "1210";
            capturegivenauth.card = card;
            var response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleCaptureGivenAuthWithTokenAndSpecialCharacters()
        {
            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "<'&\">";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            var cardtoken = new cardTokenType();
            cardtoken.litleToken = "123456789101112";
            cardtoken.expDate = "1210";
            cardtoken.cardValidationNum = "555";
            cardtoken.type = methodOfPaymentTypeEnum.VI;
            capturegivenauth.token = cardtoken;
            var response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleCaptureGivenAuthWithSecondaryAmount()
        {
            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.secondaryAmount = 50;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000000";
            card.expDate = "1210";
            capturegivenauth.card = card;
            var response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

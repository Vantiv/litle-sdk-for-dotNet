using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestCaptureGivenAuth
    {

        private LitleOnline litle;

        [TestFixtureSetUp]
        public void setUp()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void simpleCaptureGivenAuthWithCard() {
            captureGivenAuth capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = "106";
            capturegivenauth.orderId = "12344";
            authInformation authInfo = new authInformation();
            DateTime authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = "12345";
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            capturegivenauth.card = card;
            captureGivenAuthResponse response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleCaptureGivenAuthWithToken() {
            captureGivenAuth capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = "106";
            capturegivenauth.orderId = "12344";
            authInformation authInfo = new authInformation();
            DateTime authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = "12345L";
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            cardTokenType cardtoken = new cardTokenType();
            cardtoken.litleToken = "123456789101112";
            cardtoken.expDate ="1210";
            cardtoken.cardValidationNum = "555";
            cardtoken.type = methodOfPaymentTypeEnum.VI;
            capturegivenauth.token = cardtoken;
            captureGivenAuthResponse response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void complexCaptureGivenAuth() {
            captureGivenAuth capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = "106";
            capturegivenauth.orderId = "12344";
            authInformation authInfo = new authInformation();
            DateTime authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = "12345";
            capturegivenauth.authInformation = authInfo;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email ="litle.com";
            capturegivenauth.billToAddress = contact;
            processingInstructions processinginstructions = new processingInstructions();
            processinginstructions.bypassVelocityCheck = true;
            capturegivenauth.processingInstructions = processinginstructions;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            capturegivenauth.card = card;
            captureGivenAuthResponse response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void authInfo() {
            captureGivenAuth capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = "106";
            capturegivenauth.orderId = "12344";
            authInformation authInfo = new authInformation();
            DateTime authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = "12345";
            fraudResult fraudresult = new fraudResult();
            fraudresult.avsResult = "12";
            fraudresult.cardValidationResult = "123";
            fraudresult.authenticationResult = "1";
            fraudresult.advancedAVSResult = "123";
            authInfo.fraudResult = fraudresult;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            capturegivenauth.card=card;
            captureGivenAuthResponse response = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual("Approved", response.message);
        }
            
    }
}

using System.Collections.Generic;
using NUnit.Framework;


namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestToken
    {
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var config = new Dictionary<string, string>
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
            _litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleToken()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                orderId = "12344",
                accountNumber = "1233456789103801",
                reportGroup = "Planets"
            };

            var rtokenResponse = _litle.RegisterToken(registerTokenRequest);

            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
        }


        [Test]
        public void SimpleTokenWithPayPage()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                orderId = "12344",
                paypageRegistrationId = "1233456789101112",
                reportGroup = "Planets"
            };

            var rtokenResponse = _litle.RegisterToken(registerTokenRequest);

            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
        }

        [Test]
        public void SimpleTokenWithEcheck()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                orderId = "12344",
                reportGroup = "Planets",
                echeckForToken = new echeckForTokenType
                {
                    accNum = "12344565",
                    routingNum = "123476545"
                }
            };
            
            var rtokenResponse = _litle.RegisterToken(registerTokenRequest);

            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
        }

        [Test]
        public void SimpleTokenWithApplepay()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                orderId = "12344",
                reportGroup = "Planets",
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
                }
            };
            
            var rtokenResponse = _litle.RegisterToken(registerTokenRequest);

            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
            Assert.AreEqual("0", rtokenResponse.applepayResponse.transactionAmount);
        }

        [Test]
        public void TokenEcheckMissingRequiredField()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                orderId = "12344",
                reportGroup = "Planets",
                echeckForToken = new echeckForTokenType { routingNum = "123476545" }
            };
           
            try
            {
                _litle.RegisterToken(registerTokenRequest);
                Assert.Fail("Exception expected!");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void TestSimpleTokenWithNullableTypeField()
        {
            var registerTokenRequest = new registerTokenRequestType
            {
                orderId = "12344",
                accountNumber = "1233456789103801",
                reportGroup = "Planets"
            };

            var rtokenResponse = _litle.RegisterToken(registerTokenRequest);

            StringAssert.AreEqualIgnoringCase("Account number was successfully registered", rtokenResponse.message);
            Assert.IsNull(rtokenResponse.type);
        }
    }
}

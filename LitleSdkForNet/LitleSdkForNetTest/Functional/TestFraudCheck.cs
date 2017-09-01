using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestFraudCheck
    {
        private LitleOnline _litle;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _config = new Dictionary<string, string>
            {
                {"url", "https://www.testvantivcnp.com/sandbox/communicator/online"},
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

            _litle = new LitleOnline(_config);
        }

        [Test]
        public void TestCustomAttribute7TriggeredRules()
        {
            var fraudCheck = new fraudCheck
            {
                id = "1",


                reportGroup = "Planets",
                advancedFraudChecks = new advancedFraudChecksType
                {
                    threatMetrixSessionId = "123",
                    customAttribute1 = "pass",
                    customAttribute2 = "60",
                    customAttribute3 = "7",
                    customAttribute4 = "jkl",
                    customAttribute5 = "mno",
                }
            };

            var fraudCheckResponse = _litle.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual(60, fraudCheckResponse.advancedFraudResults.deviceReputationScore);
            //Assert.AreEqual(7, fraudCheckResponse.advancedFraudResults.triggeredRule.Length);
            Assert.AreEqual("triggered_rule_1", fraudCheckResponse.advancedFraudResults.triggeredRule);
        }

        [Test]
        public void TestFraudCheckWithAddressAndAmount()
        {
            var fraudCheck = new fraudCheck
            {
                id = "1",
                reportGroup = "Planets",
                advancedFraudChecks = new advancedFraudChecksType
                {
                    customAttribute1 = "fail",
                    customAttribute2 = "60",
                    customAttribute3 = "7",
                    customAttribute4 = "jkl",
                    customAttribute5 = "mno",
                    threatMetrixSessionId = "123"
                },
                billToAddress = new contact
                {
                    firstName = "Bob",
                    lastName = "Bagels",
                    addressLine1 = "37 Main Street",
                    city = "Augusta",
                    state = "Wisconsin",
                    zip = "28209"
                },
                shipToAddress = new contact
                {
                    firstName = "P",
                    lastName = "Sherman",
                    addressLine1 = "42 Wallaby Way",
                    city = "Sydney",
                    state = "New South Wales",
                    zip = "2127"
                },
                amount = 51699
            };

            var fraudCheckResponse = _litle.FraudCheck(fraudCheck);
            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("Call Discover", fraudCheckResponse.message);
            Assert.AreEqual("fail", fraudCheckResponse.advancedFraudResults.deviceReviewStatus);

        }
    }
}

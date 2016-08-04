using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestFraudCheck
    {
        private LitleOnline litle;
        private Dictionary<string, string> config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            config = new Dictionary<string, string>();
            config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "5000");
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
        public void TestCustomAttribute7TriggeredRules()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "pass";
            advancedFraudCheck.customAttribute2 = "60";
            advancedFraudCheck.customAttribute3 = "7";
            advancedFraudCheck.customAttribute4 = "jkl";
            advancedFraudCheck.customAttribute5 = "mno";

            fraudCheckResponse fraudCheckResponse = litle.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual(60, fraudCheckResponse.advancedFraudResults.deviceReputationScore);
            //Assert.AreEqual(7, fraudCheckResponse.advancedFraudResults.triggeredRule.Length);
            Assert.AreEqual("triggered_rule_1", fraudCheckResponse.advancedFraudResults.triggeredRule);
        }

        [Test]
        public void TestFraudCheckWithAddressAndAmount()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            contact billToAddress = new contact();
            contact shipToAddresss = new contact();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "fail";
            advancedFraudCheck.customAttribute2 = "60";
            advancedFraudCheck.customAttribute3 = "7";
            advancedFraudCheck.customAttribute4 = "jkl";
            advancedFraudCheck.customAttribute5 = "mno";
            billToAddress.firstName = "Bob";
            billToAddress.lastName = "Bagels";
            billToAddress.addressLine1 = "37 Main Street";
            billToAddress.city = "Augusta";
            billToAddress.state = "Wisconsin";
            billToAddress.zip = "28209";
            shipToAddresss.firstName = "P";
            shipToAddresss.lastName = "Sherman";
            shipToAddresss.addressLine1 = "42 Wallaby Way";
            shipToAddresss.city = "Sydney";
            shipToAddresss.state = "New South Wales";
            shipToAddresss.zip = "2127";
            fraudCheck.amount = 51699;
            fraudCheck.billToAddress = billToAddress;
            fraudCheck.shipToAddress = shipToAddresss;

            fraudCheckResponse fraudCheckResponse = litle.FraudCheck(fraudCheck);
            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("Call Discover", fraudCheckResponse.message);
            Assert.AreEqual("fail", fraudCheckResponse.advancedFraudResults.deviceReviewStatus);

        }
    }
}

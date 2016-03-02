using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckVerification
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
        public void SimpleEcheckVerification()
        {
            var echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;

            var echeckTypeObj = new echeckType();
            echeckTypeObj.accType = echeckAccountTypeEnum.Checking;
            echeckTypeObj.accNum = "12345657890";
            echeckTypeObj.routingNum = "123456789";
            echeckTypeObj.checkNum = "123455";

            var contactObj = new contact();
            contactObj.name = "Bob";
            contactObj.city = "lowell";
            contactObj.state = "MA";
            contactObj.email = "litle.com";

            echeckVerificationObject.echeck = echeckTypeObj;
            echeckVerificationObject.billToAddress = contactObj;

            var response = litle.EcheckVerification(echeckVerificationObject);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckVerificationWithEcheckToken()
        {
            var echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;

            var echeckTokenObj = new echeckTokenType();
            echeckTokenObj.accType = echeckAccountTypeEnum.Checking;
            echeckTokenObj.litleToken = "1234565789012";
            echeckTokenObj.routingNum = "123456789";
            echeckTokenObj.checkNum = "123455";

            var contactObj = new contact();
            contactObj.name = "Bob";
            contactObj.city = "lowell";
            contactObj.state = "MA";
            contactObj.email = "litle.com";

            echeckVerificationObject.token = echeckTokenObj;
            echeckVerificationObject.billToAddress = contactObj;

            var response = litle.EcheckVerification(echeckVerificationObject);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void TestMissingBillingField()
        {
            var echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.reportGroup = "Planets";
            echeckVerificationObject.amount = 123;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;

            var echeckTypeObj = new echeckType();
            echeckTypeObj.accType = echeckAccountTypeEnum.Checking;
            echeckTypeObj.accNum = "12345657890";
            echeckTypeObj.routingNum = "123456789";
            echeckTypeObj.checkNum = "123455";
            echeckVerificationObject.echeck = echeckTypeObj;
            try
            {
                //expected exception;
                var response = litle.EcheckVerification(echeckVerificationObject);
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
    }
}

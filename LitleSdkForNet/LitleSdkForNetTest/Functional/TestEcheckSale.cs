using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckSale
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
        public void SimpleEcheckSaleWithEcheck()
        {
            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;

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

            echeckSaleObj.echeck = echeckTypeObj;
            echeckSaleObj.billToAddress = contactObj;

            var response = litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void NoAmount()
        {
            var echeckSaleObj = new echeckSale();
            echeckSaleObj.reportGroup = "Planets";

            try
            {
                //expected exception;
                var response = litle.EcheckSale(echeckSaleObj);
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void EcheckSaleWithShipTo()
        {
            var echeckSaleObj = new echeckSale();
            echeckSaleObj.reportGroup = "Planets";
            echeckSaleObj.amount = 123456;
            echeckSaleObj.verify = true;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;

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

            echeckSaleObj.echeck = echeckTypeObj;
            echeckSaleObj.billToAddress = contactObj;
            echeckSaleObj.shipToAddress = contactObj;

            var response = litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckSaleWithEcheckToken()
        {
            var echeckSaleObj = new echeckSale();
            echeckSaleObj.reportGroup = "Planets";
            echeckSaleObj.amount = 123456;
            echeckSaleObj.verify = true;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;

            var echeckTokenTypeObj = new echeckTokenType();
            echeckTokenTypeObj.accType = echeckAccountTypeEnum.Checking;
            echeckTokenTypeObj.litleToken = "1234565789012";
            echeckTokenTypeObj.routingNum = "123456789";
            echeckTokenTypeObj.checkNum = "123455";

            var customBillingObj = new customBilling();
            customBillingObj.phone = "123456789";
            customBillingObj.descriptor = "good";

            var contactObj = new contact();
            contactObj.name = "Bob";
            contactObj.city = "lowell";
            contactObj.state = "MA";
            contactObj.email = "litle.com";

            echeckSaleObj.token = echeckTokenTypeObj;
            echeckSaleObj.customBilling = customBillingObj;
            echeckSaleObj.billToAddress = contactObj;

            var response = litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckSaleMissingBilling()
        {
            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;

            var echeckTypeObj = new echeckType();
            echeckTypeObj.accType = echeckAccountTypeEnum.Checking;
            echeckTypeObj.accNum = "12345657890";
            echeckTypeObj.routingNum = "123456789";
            echeckTypeObj.checkNum = "123455";

            echeckSaleObj.echeck = echeckTypeObj;

            try
            {
                //expected exception;
                var response = litle.EcheckSale(echeckSaleObj);
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void SimpleEcheckSale()
        {
            var echeckSaleObj = new echeckSale();
            echeckSaleObj.reportGroup = "Planets";
            echeckSaleObj.litleTxnId = 123456789101112;
            echeckSaleObj.amount = 12;

            var response = litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void SimpleEcheckSaleWithSecondaryAmountWithOrderId()
        {
            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.secondaryAmount = 50;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;

            var echeckTypeObj = new echeckType();
            echeckTypeObj.accType = echeckAccountTypeEnum.CorpSavings;
            echeckTypeObj.accNum = "12345657890";
            echeckTypeObj.routingNum = "123456789";
            echeckTypeObj.checkNum = "123455";

            var contactObj = new contact();
            contactObj.name = "Bob";
            contactObj.city = "lowell";
            contactObj.state = "MA";
            contactObj.email = "litle.com";

            echeckSaleObj.echeck = echeckTypeObj;
            echeckSaleObj.billToAddress = contactObj;

            var response = litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void SimpleEcheckSaleWithSecondaryAmount()
        {
            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.secondaryAmount = 50;
            echeckSaleObj.litleTxnId = 1234565L;
            try
            {
                ////expected exception;
                var response = litle.EcheckSale(echeckSaleObj);
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
    }
}

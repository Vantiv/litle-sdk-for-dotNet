using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Certification
{
    [TestFixture]
    internal class TestCert4Echeck
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
            config.Add("logFile", null);
            config.Add("neuterAccountNums", null);
            config.Add("proxyHost", Settings.Default.proxyHost);
            config.Add("proxyPort", Settings.Default.proxyPort);
            litle = new LitleOnline(_memoryCache, config);
        }

        [Test]
        public void test37()
        {
            var verification = new echeckVerification();
            verification.orderId = "37";
            verification.amount = 3001;
            verification.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            verification.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "10@BC99999";
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.routingNum = "053100300";
            verification.echeck = echeck;

            var response = litle.EcheckVerification(verification);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
        }

        [Test]
        public void test38()
        {
            var verification = new echeckVerification();
            verification.orderId = "38";
            verification.amount = 3002;
            verification.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "John";
            billToAddress.lastName = "Smith";
            billToAddress.phone = "999-999-9999";
            verification.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "1099999999";
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.routingNum = "053000219";
            verification.echeck = echeck;

            var response = litle.EcheckVerification(verification);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void test39()
        {
            var verification = new echeckVerification();
            verification.orderId = "39";
            verification.amount = 3003;
            verification.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "Robert";
            billToAddress.lastName = "Jones";
            billToAddress.companyName = "Good Goods Inc";
            billToAddress.phone = "9999999999";
            verification.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "3099999999";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "053100300";
            verification.echeck = echeck;

            var response = litle.EcheckVerification(verification);
            Assert.AreEqual("950", response.response);
            Assert.AreEqual("Declined - Negative Information on File", response.message);
        }

        [Test]
        public void test40()
        {
            var verification = new echeckVerification();
            verification.orderId = "40";
            verification.amount = 3004;
            verification.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "Peter";
            billToAddress.lastName = "Green";
            billToAddress.companyName = "Green Co";
            billToAddress.phone = "9999999999";
            verification.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "8099999999";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "063102152";
            verification.echeck = echeck;

            var response = litle.EcheckVerification(verification);
            Assert.AreEqual("951", response.response);
            Assert.AreEqual("Absolute Decline", response.message);
        }

        [Test]
        public void test41()
        {
            var sale = new echeckSale();
            sale.orderId = "41";
            sale.amount = 2008;
            sale.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "Mike";
            billToAddress.middleInitial = "J";
            billToAddress.lastName = "Hammer";
            sale.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "10@BC99999";
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.routingNum = "053100300";
            sale.echeck = echeck;

            var response = litle.EcheckSale(sale);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
        }

        [Test]
        public void test42()
        {
            var sale = new echeckSale();
            sale.orderId = "42";
            sale.amount = 2004;
            sale.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "Tom";
            billToAddress.lastName = "Black";
            sale.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "4099999992";
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.routingNum = "211370545";
            sale.echeck = echeck;

            var response = litle.EcheckSale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void test43()
        {
            var sale = new echeckSale();
            sale.orderId = "43";
            sale.amount = 2007;
            sale.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "Peter";
            billToAddress.lastName = "Green";
            billToAddress.companyName = "Green Co";
            sale.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "6099999992";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "211370545";
            sale.echeck = echeck;

            var response = litle.EcheckSale(sale);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void test44()
        {
            var sale = new echeckSale();
            sale.orderId = "44";
            sale.amount = 2009;
            sale.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "Peter";
            billToAddress.lastName = "Green";
            billToAddress.companyName = "Green Co";
            sale.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "9099999992";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "053133052";
            sale.echeck = echeck;

            var response = litle.EcheckSale(sale);
            Assert.AreEqual("900", response.response);
            Assert.AreEqual("Invalid Bank Routing Number", response.message);
        }

        [Test]
        public void test45()
        {
            var credit = new echeckCredit();
            credit.orderId = "45";
            credit.amount = 1001;
            credit.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "John";
            billToAddress.lastName = "Smith";
            credit.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "10@BC99999";
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.routingNum = "053100300";
            credit.echeck = echeck;

            var response = litle.EcheckCredit(credit);
            Assert.AreEqual("301", response.response);
            Assert.AreEqual("Invalid Account Number", response.message);
        }

        [Test]
        public void test46()
        {
            var credit = new echeckCredit();
            credit.orderId = "46";
            credit.amount = 1003;
            credit.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "Robert";
            billToAddress.lastName = "Jones";
            billToAddress.companyName = "Widget Inc";
            credit.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "3099999999";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "063102152";
            credit.echeck = echeck;

            var response = litle.EcheckCredit(credit);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void test47()
        {
            var credit = new echeckCredit();
            credit.orderId = "47";
            credit.amount = 1007;
            credit.orderSource = orderSourceType.telephone;
            var billToAddress = new contact();
            billToAddress.firstName = "Peter";
            billToAddress.lastName = "Green";
            billToAddress.companyName = "Green Co";
            credit.billToAddress = billToAddress;
            var echeck = new echeckType();
            echeck.accNum = "6099999993";
            echeck.accType = echeckAccountTypeEnum.Corporate;
            echeck.routingNum = "211370545";
            credit.echeck = echeck;

            var response = litle.EcheckCredit(credit);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void test48()
        {
            var credit = new echeckCredit();
            credit.litleTxnId = 430000000000000001L;

            var response = litle.EcheckCredit(credit);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void test49()
        {
            var credit = new echeckCredit();
            credit.litleTxnId = 2L;

            var response = litle.EcheckCredit(credit);
            Assert.AreEqual("360", response.response);
            Assert.AreEqual("No transaction found with specified litleTxnId", response.message);
        }
    }
}

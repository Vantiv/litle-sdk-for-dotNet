using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Certification
{
    [TestFixture]
    internal class TestCert2AuthEnhanced
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
        public void test14()
        {
            var authorization = new authorization();
            authorization.orderId = "14";
            authorization.amount = 3000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010200000247";
            card.expDate = "0812";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(fundingSourceTypeEnum.PREPAID, response.enhancedAuthResponse.fundingSource.type);
            Assert.AreEqual("2000", response.enhancedAuthResponse.fundingSource.availableBalance);
            Assert.AreEqual("NO", response.enhancedAuthResponse.fundingSource.reloadable);
            Assert.AreEqual("GIFT", response.enhancedAuthResponse.fundingSource.prepaidCardType);
        }

        [Test]
        public void test15()
        {
            var authorization = new authorization();
            authorization.orderId = "15";
            authorization.amount = 3000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5500000254444445";
            card.expDate = "0312";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(fundingSourceTypeEnum.PREPAID, response.enhancedAuthResponse.fundingSource.type);
            Assert.AreEqual("2000", response.enhancedAuthResponse.fundingSource.availableBalance);
            Assert.AreEqual("YES", response.enhancedAuthResponse.fundingSource.reloadable);
            Assert.AreEqual("PAYROLL", response.enhancedAuthResponse.fundingSource.prepaidCardType);
        }

        [Test]
        public void test16()
        {
            var authorization = new authorization();
            authorization.orderId = "16";
            authorization.amount = 3000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5592106621450897";
            card.expDate = "0312";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(fundingSourceTypeEnum.PREPAID, response.enhancedAuthResponse.fundingSource.type);
            Assert.AreEqual("0", response.enhancedAuthResponse.fundingSource.availableBalance);
            Assert.AreEqual("YES", response.enhancedAuthResponse.fundingSource.reloadable);
            Assert.AreEqual("PAYROLL", response.enhancedAuthResponse.fundingSource.prepaidCardType);
        }

        [Test]
        public void test17()
        {
            var authorization = new authorization();
            authorization.orderId = "17";
            authorization.amount = 3000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5590409551104142";
            card.expDate = "0312";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(fundingSourceTypeEnum.PREPAID, response.enhancedAuthResponse.fundingSource.type);
            Assert.AreEqual("6500", response.enhancedAuthResponse.fundingSource.availableBalance);
            Assert.AreEqual("YES", response.enhancedAuthResponse.fundingSource.reloadable);
            Assert.AreEqual("PAYROLL", response.enhancedAuthResponse.fundingSource.prepaidCardType);
        }

        [Test]
        public void test18()
        {
            var authorization = new authorization();
            authorization.orderId = "18";
            authorization.amount = 3000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5587755665222179";
            card.expDate = "0312";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(fundingSourceTypeEnum.PREPAID, response.enhancedAuthResponse.fundingSource.type);
            Assert.AreEqual("12200", response.enhancedAuthResponse.fundingSource.availableBalance);
            Assert.AreEqual("YES", response.enhancedAuthResponse.fundingSource.reloadable);
            Assert.AreEqual("PAYROLL", response.enhancedAuthResponse.fundingSource.prepaidCardType);
        }

        [Test]
        public void test19()
        {
            var authorization = new authorization();
            authorization.orderId = "19";
            authorization.amount = 3000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5445840176552850";
            card.expDate = "0312";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(fundingSourceTypeEnum.PREPAID, response.enhancedAuthResponse.fundingSource.type);
            Assert.AreEqual("20000", response.enhancedAuthResponse.fundingSource.availableBalance);
            Assert.AreEqual("YES", response.enhancedAuthResponse.fundingSource.reloadable);
            Assert.AreEqual("PAYROLL", response.enhancedAuthResponse.fundingSource.prepaidCardType);
        }

        [Test]
        public void test20()
        {
            var authorization = new authorization();
            authorization.orderId = "20";
            authorization.amount = 3000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5390016478904678";
            card.expDate = "0312";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(fundingSourceTypeEnum.PREPAID, response.enhancedAuthResponse.fundingSource.type);
            Assert.AreEqual("10050", response.enhancedAuthResponse.fundingSource.availableBalance);
            Assert.AreEqual("YES", response.enhancedAuthResponse.fundingSource.reloadable);
            Assert.AreEqual("PAYROLL", response.enhancedAuthResponse.fundingSource.prepaidCardType);
        }

        [Test]
        public void test21()
        {
            var authorization = new authorization();
            authorization.orderId = "21";
            authorization.amount = 5000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010201000246";
            card.expDate = "0912";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(affluenceTypeEnum.AFFLUENT, response.enhancedAuthResponse.affluence);
        }

        [Test]
        public void test22()
        {
            var authorization = new authorization();
            authorization.orderId = "22";
            authorization.amount = 5000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4457010202000245";
            card.expDate = "1111";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(affluenceTypeEnum.MASSAFFLUENT, response.enhancedAuthResponse.affluence);
        }

        [Test]
        public void test23()
        {
            var authorization = new authorization();
            authorization.orderId = "23";
            authorization.amount = 5000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010201000109";
            card.expDate = "0412";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(affluenceTypeEnum.AFFLUENT, response.enhancedAuthResponse.affluence);
        }

        [Test]
        public void test24()
        {
            var authorization = new authorization();
            authorization.orderId = "24";
            authorization.amount = 5000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5112010202000108";
            card.expDate = "0812";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual(affluenceTypeEnum.MASSAFFLUENT, response.enhancedAuthResponse.affluence);
        }

        [Test]
        public void test25()
        {
            var authorization = new authorization();
            authorization.orderId = "25";
            authorization.amount = 5000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100204446270000";
            card.expDate = "1112";
            authorization.card = card;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
            Assert.AreEqual("BRA", response.enhancedAuthResponse.issuerCountry);
        }

        [Test]
        public void test26()
        {
            var authorization = new authorization();
            authorization.orderId = "26";
            authorization.amount = 18698;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5194560012341234";
            card.expDate = "1212";
            authorization.card = card;
            authorization.allowPartialAuth = true;
            var healthcareiias = new healthcareIIAS();
            var healthcareamounts = new healthcareAmounts();
            healthcareamounts.totalHealthcareAmount = 20000;
            healthcareiias.healthcareAmounts = healthcareamounts;
            healthcareiias.IIASFlag = IIASFlagType.Y;
            authorization.healthcareIIAS = healthcareiias;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("341", response.response);
            Assert.AreEqual("Invalid healthcare amounts", response.message);
        }

        [Test]
        public void test27()
        {
            var authorization = new authorization();
            authorization.orderId = "27";
            authorization.amount = 18698;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5194560012341234";
            card.expDate = "1212";
            authorization.card = card;
            authorization.allowPartialAuth = true;
            var healthcareiias = new healthcareIIAS();
            var healthcareamounts = new healthcareAmounts();
            healthcareamounts.totalHealthcareAmount = 15000;
            healthcareamounts.RxAmount = 16000;
            healthcareiias.healthcareAmounts = healthcareamounts;
            healthcareiias.IIASFlag = IIASFlagType.Y;
            authorization.healthcareIIAS = healthcareiias;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("341", response.response);
            Assert.AreEqual("Invalid healthcare amounts", response.message);
        }

        [Test]
        public void test28()
        {
            var authorization = new authorization();
            authorization.orderId = "28";
            authorization.amount = 15000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "5194560012341234";
            card.expDate = "1212";
            authorization.card = card;
            authorization.allowPartialAuth = true;
            var healthcareiias = new healthcareIIAS();
            var healthcareamounts = new healthcareAmounts();
            healthcareamounts.totalHealthcareAmount = 15000;
            healthcareamounts.RxAmount = 3698;
            healthcareiias.healthcareAmounts = healthcareamounts;
            healthcareiias.IIASFlag = IIASFlagType.Y;
            authorization.healthcareIIAS = healthcareiias;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("000", response.response);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void test29()
        {
            var authorization = new authorization();
            authorization.orderId = "29";
            authorization.amount = 18699;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4024720001231239";
            card.expDate = "1212";
            authorization.card = card;
            authorization.allowPartialAuth = true;
            var healthcareiias = new healthcareIIAS();
            var healthcareamounts = new healthcareAmounts();
            healthcareamounts.totalHealthcareAmount = 31000;
            healthcareamounts.RxAmount = 1000;
            healthcareamounts.visionAmount = 19901;
            healthcareamounts.clinicOtherAmount = 9050;
            healthcareamounts.dentalAmount = 1049;
            healthcareiias.healthcareAmounts = healthcareamounts;
            healthcareiias.IIASFlag = IIASFlagType.Y;
            authorization.healthcareIIAS = healthcareiias;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("341", response.response);
            Assert.AreEqual("Invalid healthcare amounts", response.message);
        }

        [Test]
        public void test30()
        {
            var authorization = new authorization();
            authorization.orderId = "30";
            authorization.amount = 20000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4024720001231239";
            card.expDate = "1212";
            authorization.card = card;
            authorization.allowPartialAuth = true;
            var healthcareiias = new healthcareIIAS();
            var healthcareamounts = new healthcareAmounts();
            healthcareamounts.totalHealthcareAmount = 20000;
            healthcareamounts.RxAmount = 1000;
            healthcareamounts.visionAmount = 19901;
            healthcareamounts.clinicOtherAmount = 9050;
            healthcareamounts.dentalAmount = 1049;
            healthcareiias.healthcareAmounts = healthcareamounts;
            healthcareiias.IIASFlag = IIASFlagType.Y;
            authorization.healthcareIIAS = healthcareiias;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("341", response.response);
            Assert.AreEqual("Invalid healthcare amounts", response.message);
        }

        [Test]
        public void test31()
        {
            var authorization = new authorization();
            authorization.orderId = "31";
            authorization.amount = 25000;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4024720001231239";
            card.expDate = "1212";
            authorization.card = card;
            authorization.allowPartialAuth = true;
            var healthcareiias = new healthcareIIAS();
            var healthcareamounts = new healthcareAmounts();
            healthcareamounts.totalHealthcareAmount = 18699;
            healthcareamounts.RxAmount = 1000;
            healthcareamounts.visionAmount = 15099;
            healthcareiias.healthcareAmounts = healthcareamounts;
            healthcareiias.IIASFlag = IIASFlagType.Y;
            authorization.healthcareIIAS = healthcareiias;

            var response = litle.Authorize(authorization);
            Assert.AreEqual("010", response.response);
            Assert.AreEqual("Partially Approved", response.message);
            Assert.AreEqual("18699", response.approvedAmount);
        }
    }
}

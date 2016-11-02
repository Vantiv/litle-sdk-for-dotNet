using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckSale
    {
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var config = new Dictionary<string, string>
            {
                {"url", "https://www.testlitle.com/sandbox/communicator/online"},
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
        public void SimpleEcheckSaleWithEcheck()
        {
            var echeckSaleObj = new echeckSale
            {
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "litle.com"
                }
            };

           var response = _litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void NoAmount()
        {
            var echeckSaleObj = new echeckSale {reportGroup = "Planets"};

            try
            {
                _litle.EcheckSale(echeckSaleObj);
                Assert.Fail("Exception Expected!");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void EcheckSaleWithShipTo()
        {
            var contact = new contact
            {
                name = "Bob",
                city = "lowell",
                state = "MA",
                email = "litle.com"
            };

            var echeckSaleObj = new echeckSale
            {
                reportGroup = "Planets",
                amount = 123456,
                verify = true,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.CorpSavings,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                billToAddress = contact,
                shipToAddress = contact
            };

            var response = _litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckSaleWithEcheckToken()
        {
            var echeckSaleObj = new echeckSale
            {
                reportGroup = "Planets",
                amount = 123456,
                verify = true,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                token = new echeckTokenType
                {
                    accType = echeckAccountTypeEnum.CorpSavings,
                    litleToken = "1234565789012",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                customBilling = new customBilling
                {
                    phone = "123456789",
                    descriptor = "good"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "litle.com"
                }
            };

            var response = _litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckSaleMissingBilling()
        {
            var echeckSaleObj = new echeckSale
            {
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                }
            };

            try
            {
                _litle.EcheckSale(echeckSaleObj);
                Assert.Fail("Exception expected!");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void SimpleEcheckSale()
        {
            var echeckSaleObj = new echeckSale
            {
                reportGroup = "Planets",
                litleTxnId = 123456789101112,
                amount = 12
            };

            var response = _litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void SimpleEcheckSaleWithSecondaryAmountWithOrderId()
        {
            var echeckSaleObj = new echeckSale
            {
                amount = 123456,
                secondaryAmount = 50,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.CorpSavings,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "litle.com"
                }
            };

            var response = _litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void SimpleEcheckSaleWithSecondaryAmount()
        {
            var echeckSaleObj = new echeckSale
            {
                amount = 123456,
                secondaryAmount = 50,
                litleTxnId = 1234565L
            };
            try
            {
                _litle.EcheckSale(echeckSaleObj);
                Assert.Fail("Exception expected!");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
    }
}

using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckSale
    {
        private LitleOnline _litle;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _config = new Dictionary<string, string>
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

            _litle = new LitleOnline(_config);
        }

        [Test]
        public void SimpleEcheckSaleWithEcheck()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
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
        public void EcheckSaleWithLitleTxnId()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                litleTxnId = 1234,
                amount = 123456,
                customBilling = new customBilling
                {
                    phone = "123456789",
                    descriptor = "good",
                },
                customIdentifier = "ident",
                orderId = "12345"
            };

            var response = _litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckSaleWithOrderId()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                orderId = "1",
                verify = true,
                amount = 1234,
                secondaryAmount = 123456,
                orderSource = orderSourceType.ecommerce,

                billToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "litle.com"
                },
                shipToAddress = new contact
                {
                    name = "Bob",
                    city = "lowell",
                    state = "MA",
                    email = "litle.com"
                },
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                customBilling = new customBilling
                {
                    phone = "123456789",
                    descriptor = "good"
                },
                merchantData = new merchantDataType
                {
                    affiliate = "Affiliate",
                    campaign = "campaign",
                    merchantGroupingId = "Merchant Group ID"

                },
                customIdentifier = "ident"
            };

            var response = _litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void NoAmount()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets"
            };


            try
            {
                //expected exception;
                var response = _litle.EcheckSale(echeckSaleObj);
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void EcheckSaleWithShipTo()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                verify = true,
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
                },
                shipToAddress = new contact
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
        public void EcheckSaleWithEcheckToken()
        {
            echeckSale echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                verify = true,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                token = new echeckTokenType
                {
                    accType = echeckAccountTypeEnum.Checking,
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
                },
            };

            var response = _litle.EcheckSale(echeckSaleObj);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckSaleMissingBilling()
        {
            var echeckSaleObj = new echeckSale
            {
                id = "1",
                reportGroup = "Planets",
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
                //expected exception;
                var response = _litle.EcheckSale(echeckSaleObj);
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
                id = "1",
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
                id = "1",
                reportGroup = "Planets",
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
                id = "1",
                reportGroup = "Planets",
                amount = 123456,
                secondaryAmount = 50,
                litleTxnId = 1234565L
            };

            try
            {
                ////expected exception;
                var response = _litle.EcheckSale(echeckSaleObj);
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
    }
}

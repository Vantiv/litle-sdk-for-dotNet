using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckCredit
    {
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void BeforeClass()
        {
            _litle = new LitleOnline();
        }

        [Test]
        public void SimpleEcheckCredit()
        {
            var echeckcredit = new echeckCredit
            {
                amount = 12L,
                litleTxnId = 123456789101112L
            };
            var response = _litle.EcheckCredit(echeckcredit);

            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void NoLitleTxnId()
        {
            var echeckcredit = new echeckCredit();
            try
            {
                _litle.EcheckCredit(echeckcredit);
                Assert.Fail("Expected exception");
            }
            catch (LitleOnlineException e)
            {
                Assert.IsTrue(e.Message.Contains("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void EcheckCreditWithEcheck()
        {
            var echeckcredit = new echeckCredit
            {
                amount = 12L,
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
                    city = "Lowell",
                    state = "MA",
                    email = "litle.com"
                }
            };
            
            var response = _litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void EcheckCreditWithToken()
        {
            var echeckcredit = new echeckCredit
            {
                amount = 12L,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeckToken = new echeckTokenType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    litleToken = "1234565789012",
                    routingNum = "123456789",
                    checkNum = "123455"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "Lowell",
                    state = "MA",
                    email = "litle.com"
                }
            };
            
            var response = _litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void MissingBilling()
        {
            var echeckcredit = new echeckCredit
            {
                amount = 12L,
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
                _litle.EcheckCredit(echeckcredit);
                Assert.Fail("Expected exception");
            }
            catch (LitleOnlineException e)
            {
                Assert.IsTrue(e.Message.Contains("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void EcheckCreditWithSecondaryAmountWithOrderIdAndCcdPaymentInfo()
        {
            var echeckcredit = new echeckCredit
            {
                amount = 12L,
                secondaryAmount = 50,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = new echeckType
                {
                    accType = echeckAccountTypeEnum.Checking,
                    accNum = "12345657890",
                    routingNum = "123456789",
                    checkNum = "123455",
                    ccdPaymentInformation = "9876554"
                },
                billToAddress = new contact
                {
                    name = "Bob",
                    city = "Lowell",
                    state = "MA",
                    email = "litle.com"
                }
            };
            
            var response = _litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void EcheckCreditWithSecondaryAmountWithLitleTxnId()
        {
            var echeckcredit = new echeckCredit
            {
                amount = 12L,
                secondaryAmount = 50,
                litleTxnId = 12345L
            };
            var response = _litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

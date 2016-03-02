using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestEcheckCredit
    {
        private LitleOnline litle;
        private IDictionary<string, StringBuilder> _memoryStreams;

        [TestFixtureSetUp]
        public void beforeClass()
        {
            _memoryStreams = new Dictionary<string, StringBuilder>();
            litle = new LitleOnline(_memoryStreams);
        }

        [Test]
        public void simpleEcheckCredit()
        {
            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.litleTxnId = 123456789101112L;
            var response = litle.EcheckCredit(echeckcredit);

            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void noLitleTxnId()
        {
            var echeckcredit = new echeckCredit();
            try
            {
                litle.EcheckCredit(echeckcredit);
                Assert.Fail("Expected exception");
            }
            catch (LitleOnlineException e)
            {
                Assert.IsTrue(e.Message.Contains("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void echeckCreditWithEcheck()
        {
            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            var billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;
            var response = litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void echeckCreditWithToken()
        {
            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            var echeckToken = new echeckTokenType();
            echeckToken.accType = echeckAccountTypeEnum.Checking;
            echeckToken.litleToken = "1234565789012";
            echeckToken.routingNum = "123456789";
            echeckToken.checkNum = "123455";
            echeckcredit.echeckToken = echeckToken;
            var billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;
            var response = litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void missingBilling()
        {
            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            try
            {
                litle.EcheckCredit(echeckcredit);
                Assert.Fail("Expected exception");
            }
            catch (LitleOnlineException e)
            {
                Assert.IsTrue(e.Message.Contains("Error validating xml data against the schema"));
            }
        }

        [Test]
        public void echeckCreditWithSecondaryAmountWithOrderIdAndCcdPaymentInfo()
        {
            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.secondaryAmount = 50;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeck.ccdPaymentInformation = "9876554";
            echeckcredit.echeck = echeck;
            var billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;
            var response = litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void echeckCreditWithSecondaryAmountWithLitleTxnId()
        {
            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.secondaryAmount = 50;
            echeckcredit.litleTxnId = 12345L;
            var response = litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

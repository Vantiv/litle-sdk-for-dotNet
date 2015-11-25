using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestEcheckCredit
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void beforeClass()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void simpleEcheckCredit()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.litleTxnId = 123456789101112L;
            echeckCreditResponse response = litle.EcheckCredit(echeckcredit);

            Assert.AreEqual("Transaction Received", response.message);
        }

        [Test]
        public void noLitleTxnId()
        {
            echeckCredit echeckcredit = new echeckCredit();
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
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            contact billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;
            echeckCreditResponse response = litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Transaction Received", response.message);
        }

        [Test]
        public void echeckCreditWithToken()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            echeckTokenType echeckToken = new echeckTokenType();
            echeckToken.accType = echeckAccountTypeEnum.Checking;
            echeckToken.litleToken = "1234565789012";
            echeckToken.routingNum = "123456789";
            echeckToken.checkNum = "123455";
            echeckcredit.echeckToken = echeckToken;
            contact billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;
            echeckCreditResponse response = litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Transaction Received", response.message);
        }

        [Test]
        public void missingBilling()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
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
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.secondaryAmount = 50;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeck.ccdPaymentInformation = "9876554";
            echeckcredit.echeck = echeck;
            contact billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;
            echeckCreditResponse response = litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Transaction Received", response.message);
        }

        [Test]
        public void echeckCreditWithSecondaryAmountWithLitleTxnId()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.secondaryAmount = 50;
            echeckcredit.litleTxnId = 12345L;
            echeckCreditResponse response = litle.EcheckCredit(echeckcredit);
            Assert.AreEqual("Transaction Received", response.message);
        }
    }
}

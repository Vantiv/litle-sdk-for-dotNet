using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestEcheckVerification
    {
        [Test]
        public void SimpleEcheckVerification()
        {
            LitleOnline lOnlineObj = new LitleOnline();

            echeckVerification echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            
            echeckType echeckTypeObj = new echeckType();
            echeckTypeObj.accType = echeckAccountTypeEnum.Checking;
            echeckTypeObj.accNum = "12345657890";
            echeckTypeObj.routingNum = "123456789";
            echeckTypeObj.checkNum = "123455";
            
            contact contactObj = new contact();
            contactObj.name = "Bob";
            contactObj.city = "lowell";
            contactObj.state = "MA";
            contactObj.email = "litle.com";

            echeckVerificationObject.echeck = echeckTypeObj;
            echeckVerificationObject.billToAddress = contactObj;

            echeckVerificationResponse response = lOnlineObj.EcheckVerification(echeckVerificationObject);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void EcheckVerificationWithEcheckToken()
        {
            LitleOnline lOnlineObj = new LitleOnline();

            echeckVerification echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;

            echeckTokenType echeckTokenObj = new echeckTokenType();
            echeckTokenObj.accType = echeckAccountTypeEnum.Checking;
            echeckTokenObj.litleToken = "1234565789012";
            echeckTokenObj.routingNum = "123456789";
            echeckTokenObj.checkNum = "123455";

            contact contactObj = new contact();
            contactObj.name = "Bob";
            contactObj.city = "lowell";
            contactObj.state = "MA";
            contactObj.email = "litle.com";

            echeckVerificationObject.token = echeckTokenObj;
            echeckVerificationObject.billToAddress = contactObj;

            echeckVerificationResponse response = lOnlineObj.EcheckVerification(echeckVerificationObject);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }

        [Test]
        public void TestMissingBillingField()
        {
            LitleOnline lOnlineObj = new LitleOnline();

            echeckVerification echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.reportGroup = "Planets";
            echeckVerificationObject.amount = 123;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;

            echeckType echeckTypeObj = new echeckType();
            echeckTypeObj.accType = echeckAccountTypeEnum.Checking;
            echeckTypeObj.accNum = "12345657890";
            echeckTypeObj.routingNum = "123456789";
            echeckTypeObj.checkNum = "123455";
            echeckVerificationObject.echeck = echeckTypeObj;
            try
            {
                //expected exception;
                echeckVerificationResponse response = lOnlineObj.EcheckVerification(echeckVerificationObject);
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
    }
}

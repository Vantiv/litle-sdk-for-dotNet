﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestClass]
    public class TestEcheckVerification
    {
        private LitleOnline litle;

        [TestInitialize]
        public void setUp()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "65");
            config.Add("merchantId", "101");
            config.Add("password", "TESTCASE");
            config.Add("printxml", "true");

            config.Add("logFile", Properties.Settings.Default.logFile);
            config.Add("neuterAccountNums", "true");
            litle = new LitleOnline(config);
        }

        [TestMethod]
        public void SimpleEcheckVerification()
        {
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

            echeckVerificationResponse response = litle.EcheckVerification(echeckVerificationObject);
            StringAssert.Equals("Approved", response.message);
        }

        [TestMethod]
        public void EcheckVerificationWithEcheckToken()
        {
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

            echeckVerificationResponse response = litle.EcheckVerification(echeckVerificationObject);
            StringAssert.Equals("Approved", response.message);
        }

        [TestMethod]
        public void TestMissingBillingField()
        {
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
                echeckVerificationResponse response = litle.EcheckVerification(echeckVerificationObject);
            }
            catch (LitleOnlineException e)
            {
                Assert.IsTrue(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
    }
}

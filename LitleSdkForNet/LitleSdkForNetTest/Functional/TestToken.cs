using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestClass]
    public class TestToken
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
        public void SimpleToken()
        {
            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";
            registerTokenResponse rtokenResponse = litle.RegisterToken(registerTokenRequest);
            StringAssert.Equals("Account number was successfully registered", rtokenResponse.message);
        }


        [TestMethod]
        public void SimpleTokenWithPayPage()
        {
            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.paypageRegistrationId = "1233456789101112";
            registerTokenRequest.reportGroup = "Planets";
            registerTokenResponse rtokenResponse = litle.RegisterToken(registerTokenRequest);
            StringAssert.Equals("Account number was successfully registered", rtokenResponse.message);
        }

        [TestMethod]
        public void SimpleTokenWithEcheck()
        {
            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            echeckForTokenType echeckObj = new echeckForTokenType();
            echeckObj.accNum = "12344565";
            echeckObj.routingNum = "123476545";
            registerTokenRequest.echeckForToken = echeckObj;
            registerTokenRequest.reportGroup = "Planets";
            registerTokenResponse rtokenResponse = litle.RegisterToken(registerTokenRequest);
            StringAssert.Equals("Account number was successfully registered", rtokenResponse.message);
        }

        [TestMethod]
        public void TokenEcheckMissingRequiredField()
        {
            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            echeckForTokenType echeckObj = new echeckForTokenType();
            echeckObj.routingNum = "123476545";
            registerTokenRequest.echeckForToken = echeckObj;
            registerTokenRequest.reportGroup = "Planets";
            try
            {
                //expected exception;
                registerTokenResponse rtokenResponse = litle.RegisterToken(registerTokenRequest);
            }
            catch (LitleOnlineException e)
            {
                Assert.IsTrue(e.Message.StartsWith("Error validating xml data against the schema"));
            }
        }
            
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestClass]
    public class TestAuthReversal
    {
        private LitleOnline litle;

        [TestInitialize]
        public void SetUpLitle()
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
        public void SimpleAuthReversal()
        {
            authReversal reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            authReversalResponse response = litle.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
            
        [TestMethod]
        public void testAuthReversalHandleSpecialCharacters()
        {
            authReversal reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "<'&\">";

            authReversalResponse response = litle.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
    }
    }
}

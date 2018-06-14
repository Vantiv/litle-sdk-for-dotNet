using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestAuthReversal
    {
        private LitleOnline litle;
        
        private Dictionary<string, string> config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "10.0"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };
            litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleAuthReversal()
        {
            authReversal reversal = new authReversal();
            reversal.id = "1";
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            authReversalResponse response = litle.AuthReversal(reversal);
            Assert.AreEqual("Transaction Received", response.message);
        }
            
        [Test]
        public void testAuthReversalHandleSpecialCharacters()
        {
            authReversal reversal = new authReversal();
            reversal.id = "1";
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "<'&\">";

            authReversalResponse response = litle.AuthReversal(reversal);
            Assert.AreEqual("Transaction Received", response.message);
    }
    }
}

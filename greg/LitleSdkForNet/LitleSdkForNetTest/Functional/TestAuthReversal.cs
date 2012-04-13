using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestAuthReversal
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void SimpleAuthReversal()
        {
            authReversal reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = "106";
            reversal.payPalNotes = "Notes";

            authReversalResponse response = litle.AuthReversal(reversal);
            Assert.AreEqual("Approved", response.message);
        }
            
    }
}

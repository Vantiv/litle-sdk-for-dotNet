using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestXmlFieldsSerializer
    {
        
        //private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
         //   litle = new LitleOnline();
        }

        [Test]
        public void TestRecurringRequest_Full()
        {
            recurringRequest request = new recurringRequest();
            request.subscription = new subscription();
            request.subscription.planCode = "123abc";
            request.subscription.numberOfPayments = 10;
            request.subscription.startDate = new DateTime(2013, 7, 25);
            request.subscription.amount = 102;

            String xml = request.Serialize();
            System.Text.RegularExpressions.Match match = Regex.Match(xml,"<subscription>\r\n<planCode>123abc</planCode>\r\n<numberOfPayments>10</numberOfPayments>\r\n<startDate>2013-07-25</startDate>\r\n<amount>102</amount>\r\n</subscription>");
            Assert.IsTrue(match.Success, xml);
        }

        [Test]
        public void TestRecurringResponse_OnlyRequired()
        {
            recurringRequest request = new recurringRequest();
            request.subscription = new subscription();
            request.subscription.planCode = "123abc";
 
            String xml = request.Serialize();
            System.Text.RegularExpressions.Match match = Regex.Match(xml, "<subscription>\r\n<planCode>123abc</planCode>\r\n</subscription>");
            Assert.IsTrue(match.Success, xml);
        }



    }
}

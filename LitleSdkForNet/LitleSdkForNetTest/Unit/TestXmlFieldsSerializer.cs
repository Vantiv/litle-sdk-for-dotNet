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

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
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

        [Test]
        public void TestUpdateSubscription_Full()
        {
            updateSubscription update = new updateSubscription();
            update.billingDate = new DateTime(2002, 10, 9);
            contact billToAddress = new contact();
            billToAddress.name = "Greg Dake";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "sdksupport@litle.com";
            update.billToAddress = billToAddress;
            cardType card = new cardType();
            card.number = "4100000000000001";
            card.expDate = "1215";
            card.type = methodOfPaymentTypeEnum.VI;
            update.card = card;
            update.planCode = "abcdefg";
            update.subscriptionId = 12345;

            String actual = update.Serialize();
            String expected = "\r\n<updateSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n<planCode>abcdefg</planCode>\r\n<billToAddress>\r\n<name>Greg Dake</name>\r\n<city>Lowell</city>\r\n<state>MA</state>\r\n<email>sdksupport@litle.com</email>\r\n</billToAddress>\r\n<card>\r\n<type>VI</type>\r\n<number>4100000000000001</number>\r\n<expDate>1215</expDate>\r\n</card>\r\n<billingDate>2002-10-09</billingDate>\r\n</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSubscription_OnlyRequired()
        {
            updateSubscription update = new updateSubscription();
            update.subscriptionId = 12345;

            String actual = update.Serialize();
            String expected = "\r\n<updateSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCancelSubscription_Full()
        {
            cancelSubscription cancel = new cancelSubscription();
            cancel.subscriptionId = 12345;

            String actual = cancel.Serialize();
            String expected = "\r\n<cancelSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n</cancelSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testCancelSubscription_OnlyRequired()
        {
            cancelSubscription update = new cancelSubscription();
            update.subscriptionId = 12345;

            String actual = update.Serialize();
            String expected = "\r\n<cancelSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n</cancelSubscription>";
            Assert.AreEqual(expected, actual);
        }



    }
}

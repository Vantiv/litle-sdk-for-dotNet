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
        public void TestRecurringRequest_OnlyRequired()
        {
            recurringRequest request = new recurringRequest();
            request.subscription = new subscription();
            request.subscription.planCode = "123abc";

            String xml = request.Serialize();
            System.Text.RegularExpressions.Match match = Regex.Match(xml, "<subscription>\r\n<planCode>123abc</planCode>\r\n</subscription>");
            Assert.IsTrue(match.Success, xml);
        }

        [Test]
        public void TestSubscription_CanContainCreateDiscounts()
        {
            subscription subscription = new subscription();
            subscription.planCode = "123abc";

            createDiscount cd1 = new createDiscount();
            cd1.discountCode = "1";
            cd1.name = "cheaper";
            cd1.amount = 200;
            cd1.startDate = new DateTime(2013, 9, 5);
            cd1.endDate = new DateTime(2013, 9, 6);

            createDiscount cd2 = new createDiscount();
            cd2.discountCode = "2";
            cd2.name = "cheap";
            cd2.amount = 100;
            cd2.startDate = new DateTime(2013, 9, 3);
            cd2.endDate = new DateTime(2013, 9, 4);

            subscription.createDiscounts.Add(cd1);
            subscription.createDiscounts.Add(cd2);

            String actual = subscription.Serialize();
            String expected = @"
<planCode>123abc</planCode>
<createDiscount>
<discountCode>1</discountCode>
<name>cheaper</name>
<amount>200</amount>
<startDate>2013-09-05</startDate>
<endDate>2013-09-06</endDate>
</createDiscount>
<createDiscount>
<discountCode>2</discountCode>
<name>cheap</name>
<amount>100</amount>
<startDate>2013-09-03</startDate>
<endDate>2013-09-04</endDate>
</createDiscount>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSubscription_CanContainCreateAddOns()
        {
            subscription subscription = new subscription();
            subscription.planCode = "123abc";

            createAddOn cao1 = new createAddOn();
            cao1.addOnCode = "1";
            cao1.name = "addOn1";
            cao1.amount = 100;
            cao1.startDate = new DateTime(2013, 9, 5);
            cao1.endDate = new DateTime(2013, 9, 6);

            createAddOn cao2 = new createAddOn();
            cao2.addOnCode = "2";
            cao2.name = "addOn2";
            cao2.amount = 200;
            cao2.startDate = new DateTime(2013, 9, 4);
            cao2.endDate = new DateTime(2013, 9, 5);

            subscription.createAddOns.Add(cao1);
            subscription.createAddOns.Add(cao2);

            String actual = subscription.Serialize();
            String expected = @"
<planCode>123abc</planCode>
<createAddOn>
<addOnCode>1</addOnCode>
<name>addOn1</name>
<amount>100</amount>
<startDate>2013-09-05</startDate>
<endDate>2013-09-06</endDate>
</createAddOn>
<createAddOn>
<addOnCode>2</addOnCode>
<name>addOn2</name>
<amount>200</amount>
<startDate>2013-09-04</startDate>
<endDate>2013-09-05</endDate>
</createAddOn>";
            Assert.AreEqual(expected, actual);
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
        public void testUpdateSubscription_CanContainCreateDiscounts()
        {
            createDiscount cd1 = new createDiscount();
            cd1.discountCode = "1";
            cd1.name = "cheaper";
            cd1.amount = 200;
            cd1.startDate = new DateTime(2013, 9, 5);
            cd1.endDate = new DateTime(2013, 9, 6);

            createDiscount cd2 = new createDiscount();
            cd2.discountCode = "2";
            cd2.name = "cheap";
            cd2.amount = 100;
            cd2.startDate = new DateTime(2013, 9, 3);
            cd2.endDate = new DateTime(2013, 9, 4);

            updateSubscription update = new updateSubscription();
            update.subscriptionId = 1;
            update.createDiscounts.Add(cd1);
            update.createDiscounts.Add(cd2);

            String actual = update.Serialize();
            String expected = @"
<updateSubscription>
<subscriptionId>1</subscriptionId>
<createDiscount>
<discountCode>1</discountCode>
<name>cheaper</name>
<amount>200</amount>
<startDate>2013-09-05</startDate>
<endDate>2013-09-06</endDate>
</createDiscount>
<createDiscount>
<discountCode>2</discountCode>
<name>cheap</name>
<amount>100</amount>
<startDate>2013-09-03</startDate>
<endDate>2013-09-04</endDate>
</createDiscount>
</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSubscription_CanContainUpdateDiscounts()
        {
            updateDiscount ud1 = new updateDiscount();
            ud1.discountCode = "1";
            ud1.name = "cheaper";
            ud1.amount = 200;
            ud1.startDate = new DateTime(2013, 9, 5);
            ud1.endDate = new DateTime(2013, 9, 6);

            updateDiscount ud2 = new updateDiscount();
            ud2.discountCode = "2";
            ud2.name = "cheap";
            ud2.amount = 100;
            ud2.startDate = new DateTime(2013, 9, 3);
            ud2.endDate = new DateTime(2013, 9, 4);

            updateSubscription update = new updateSubscription();
            update.subscriptionId = 1;
            update.updateDiscounts.Add(ud1);
            update.updateDiscounts.Add(ud2);

            String actual = update.Serialize();
            String expected = @"
<updateSubscription>
<subscriptionId>1</subscriptionId>
<updateDiscount>
<discountCode>1</discountCode>
<name>cheaper</name>
<amount>200</amount>
<startDate>2013-09-05</startDate>
<endDate>2013-09-06</endDate>
</updateDiscount>
<updateDiscount>
<discountCode>2</discountCode>
<name>cheap</name>
<amount>100</amount>
<startDate>2013-09-03</startDate>
<endDate>2013-09-04</endDate>
</updateDiscount>
</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSubscription_CanContainDeleteDiscounts()
        {
            deleteDiscount dd1 = new deleteDiscount();
            dd1.discountCode = "1";

            deleteDiscount dd2 = new deleteDiscount();
            dd2.discountCode = "2";

            updateSubscription update = new updateSubscription();
            update.subscriptionId = 1;
            update.deleteDiscounts.Add(dd1);
            update.deleteDiscounts.Add(dd2);

            String actual = update.Serialize();
            String expected = @"
<updateSubscription>
<subscriptionId>1</subscriptionId>
<deleteDiscount>
<discountCode>1</discountCode>
</deleteDiscount>
<deleteDiscount>
<discountCode>2</discountCode>
</deleteDiscount>
</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSubscription_CanContainCreateAddOns()
        {
            createAddOn cao1 = new createAddOn();
            cao1.addOnCode = "1";
            cao1.name = "addOn1";
            cao1.amount = 100;
            cao1.startDate = new DateTime(2013, 9, 5);
            cao1.endDate = new DateTime(2013, 9, 6);

            createAddOn cao2 = new createAddOn();
            cao2.addOnCode = "2";
            cao2.name = "addOn2";
            cao2.amount = 200;
            cao2.startDate = new DateTime(2013, 9, 4);
            cao2.endDate = new DateTime(2013, 9, 5);

            updateSubscription update = new updateSubscription();
            update.subscriptionId = 1;
            update.createAddOns.Add(cao1);
            update.createAddOns.Add(cao2);

            String actual = update.Serialize();
            String expected = @"
<updateSubscription>
<subscriptionId>1</subscriptionId>
<createAddOn>
<addOnCode>1</addOnCode>
<name>addOn1</name>
<amount>100</amount>
<startDate>2013-09-05</startDate>
<endDate>2013-09-06</endDate>
</createAddOn>
<createAddOn>
<addOnCode>2</addOnCode>
<name>addOn2</name>
<amount>200</amount>
<startDate>2013-09-04</startDate>
<endDate>2013-09-05</endDate>
</createAddOn>
</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSubscription_CanContainUpdateAddOns()
        {
            updateAddOn uao1 = new updateAddOn();
            uao1.addOnCode = "1";
            uao1.name = "addOn1";
            uao1.amount = 100;
            uao1.startDate = new DateTime(2013, 9, 5);
            uao1.endDate = new DateTime(2013, 9, 6);

            updateAddOn uao2 = new updateAddOn();
            uao2.addOnCode = "2";
            uao2.name = "addOn2";
            uao2.amount = 200;
            uao2.startDate = new DateTime(2013, 9, 4);
            uao2.endDate = new DateTime(2013, 9, 5);

            updateSubscription update = new updateSubscription();
            update.subscriptionId = 1;
            update.updateAddOns.Add(uao1);
            update.updateAddOns.Add(uao2);

            String actual = update.Serialize();
            String expected = @"
<updateSubscription>
<subscriptionId>1</subscriptionId>
<updateAddOn>
<addOnCode>1</addOnCode>
<name>addOn1</name>
<amount>100</amount>
<startDate>2013-09-05</startDate>
<endDate>2013-09-06</endDate>
</updateAddOn>
<updateAddOn>
<addOnCode>2</addOnCode>
<name>addOn2</name>
<amount>200</amount>
<startDate>2013-09-04</startDate>
<endDate>2013-09-05</endDate>
</updateAddOn>
</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSubscription_CanContainDeleteAddOns()
        {
            deleteAddOn dao1 = new deleteAddOn();
            dao1.addOnCode = "1";

            deleteAddOn dao2 = new deleteAddOn();
            dao2.addOnCode = "2";

            updateSubscription update = new updateSubscription();
            update.subscriptionId = 1;
            update.deleteAddOns.Add(dao1);
            update.deleteAddOns.Add(dao2);

            String actual = update.Serialize();
            String expected = @"
<updateSubscription>
<subscriptionId>1</subscriptionId>
<deleteAddOn>
<addOnCode>1</addOnCode>
</deleteAddOn>
<deleteAddOn>
<addOnCode>2</addOnCode>
</deleteAddOn>
</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSubscription_CanContainToken()
        {
            updateSubscription update = new updateSubscription();
            update.subscriptionId = 1;
            update.token = new cardTokenType();
            update.token.litleToken = "123456";

            String actual = update.Serialize();
            String expected = @"
<updateSubscription>
<subscriptionId>1</subscriptionId>
<token>
<litleToken>123456</litleToken>
</token>
</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSubscription_CanContainPaypage()
        {
            updateSubscription update = new updateSubscription();
            update.subscriptionId = 1;
            update.paypage = new cardPaypageType();
            update.paypage.paypageRegistrationId = "abc123";

            String actual = update.Serialize();
            String expected = @"
<updateSubscription>
<subscriptionId>1</subscriptionId>
<paypage>
<paypageRegistrationId>abc123</paypageRegistrationId>
</paypage>
</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void TestCancelSubscription_Full()
        {
            cancelSubscription cancel = new cancelSubscription();
            cancel.subscriptionId = 12345;

            String actual = cancel.Serialize();
            String expected = @"
<cancelSubscription>
<subscriptionId>12345</subscriptionId>
</cancelSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testCancelSubscription_OnlyRequired()
        {
            cancelSubscription update = new cancelSubscription();
            update.subscriptionId = 12345;

            String actual = update.Serialize();
            String expected = @"
<cancelSubscription>
<subscriptionId>12345</subscriptionId>
</cancelSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testActivate_Full()
        {
            activate activate = new activate();
            activate.orderId = "12345";
            activate.amount = 200;
            activate.orderSource = orderSourceType.ecommerce;
            activate.id = "theId";
            activate.reportGroup = "theReportGroup";
            activate.card = new cardType();

            String actual = activate.Serialize();
            String expected = @"
<activate id=""theId"" reportGroup=""theReportGroup"">
<orderId>12345</orderId>
<amount>200</amount>
<orderSource>ecommerce</orderSource>
<card>
<type>MC</type>
</card>
</activate>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testActivate_VirtualGiftCard()
        {
            activate activate = new activate();
            activate.orderId = "12345";
            activate.amount = 200;
            activate.orderSource = orderSourceType.ecommerce;
            activate.id = "theId";
            activate.reportGroup = "theReportGroup";
            activate.virtualGiftCard = new virtualGiftCardType();

            String actual = activate.Serialize();
            String expected = @"
<activate id=""theId"" reportGroup=""theReportGroup"">
<orderId>12345</orderId>
<amount>200</amount>
<orderSource>ecommerce</orderSource>
<virtualGiftCard>
</virtualGiftCard>
</activate>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testVirtualGiftCard_Full()
        {
            virtualGiftCardType virtualGiftCard = new virtualGiftCardType();
            virtualGiftCard.accountNumberLength = 16;
            virtualGiftCard.giftCardBin = "123456";

            String actual = virtualGiftCard.Serialize();
            String expected = @"
<accountNumberLength>16</accountNumberLength>
<giftCardBin>123456</giftCardBin>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testDeactivate_Full()
        {
            deactivate deactivate = new deactivate();
            deactivate.orderId = "12345";
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new cardType();
            deactivate.id = "theId";
            deactivate.reportGroup = "theReportGroup";

            String actual = deactivate.Serialize();
            String expected = @"
<deactivate id=""theId"" reportGroup=""theReportGroup"">
<orderId>12345</orderId>
<orderSource>ecommerce</orderSource>
<card>
<type>MC</type>
</card>
</deactivate>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testDeactivate_OnlyRequired()
        {
            deactivate deactivate = new deactivate();
            deactivate.orderId = "12345";
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new cardType();
            deactivate.id = "theId";
            deactivate.reportGroup = "theReportGroup";

            String actual = deactivate.Serialize();
            String expected = @"
<deactivate id=""theId"" reportGroup=""theReportGroup"">
<orderId>12345</orderId>
<orderSource>ecommerce</orderSource>
<card>
<type>MC</type>
</card>
</deactivate>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testLoad_Full()
        {
            load load = new load();
            load.orderId = "12345";
            load.amount = 200;
            load.orderSource = orderSourceType.ecommerce;
            load.card = new cardType();
            load.id = "theId";
            load.reportGroup = "theReportGroup";

            String actual = load.Serialize();
            String expected = @"
<load id=""theId"" reportGroup=""theReportGroup"">
<orderId>12345</orderId>
<amount>200</amount>
<orderSource>ecommerce</orderSource>
<card>
<type>MC</type>
</card>
</load>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testLoad_OnlyRequired()
        {
            load load = new load();
            load.orderId = "12345";
            load.amount = 200;
            load.orderSource = orderSourceType.ecommerce;
            load.card = new cardType();
            load.id = "theId";
            load.reportGroup = "theReportGroup";

            String actual = load.Serialize();
            String expected = @"
<load id=""theId"" reportGroup=""theReportGroup"">
<orderId>12345</orderId>
<amount>200</amount>
<orderSource>ecommerce</orderSource>
<card>
<type>MC</type>
</card>
</load>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUnload_Full()
        {
            unload unload = new unload();
            unload.orderId = "12345";
            unload.amount = 200;
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new cardType();
            unload.id = "theId";
            unload.reportGroup = "theReportGroup";

            String actual = unload.Serialize();
            String expected = @"
<unload id=""theId"" reportGroup=""theReportGroup"">
<orderId>12345</orderId>
<amount>200</amount>
<orderSource>ecommerce</orderSource>
<card>
<type>MC</type>
</card>
</unload>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUnload_OnlyRequired()
        {
            unload unload = new unload();
            unload.orderId = "12345";
            unload.amount = 200;
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new cardType();
            unload.id = "theId";
            unload.reportGroup = "theReportGroup";

            String actual = unload.Serialize();
            String expected = @"
<unload id=""theId"" reportGroup=""theReportGroup"">
<orderId>12345</orderId>
<amount>200</amount>
<orderSource>ecommerce</orderSource>
<card>
<type>MC</type>
</card>
</unload>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testBalanceInquiry_Full()
        {
            balanceInquiry balanceInquiry = new balanceInquiry();
            balanceInquiry.orderId = "12345";
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new cardType();
            balanceInquiry.id = "theId";
            balanceInquiry.reportGroup = "theReportGroup";

            String actual = balanceInquiry.Serialize();
            String expected = @"
<balanceInquiry id=""theId"" reportGroup=""theReportGroup"">
<orderId>12345</orderId>
<orderSource>ecommerce</orderSource>
<card>
<type>MC</type>
</card>
</balanceInquiry>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testBalanceInquiry_OnlyRequired()
        {
            balanceInquiry balanceInquiry = new balanceInquiry();
            balanceInquiry.orderId = "12345";
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new cardType();
            balanceInquiry.id = "theId";
            balanceInquiry.reportGroup = "theReportGroup";

            String actual = balanceInquiry.Serialize();
            String expected = @"
<balanceInquiry id=""theId"" reportGroup=""theReportGroup"">
<orderId>12345</orderId>
<orderSource>ecommerce</orderSource>
<card>
<type>MC</type>
</card>
</balanceInquiry>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCreatePlan_Full()
        {
            createPlan create = new createPlan();
            create.planCode = "abc";
            create.name = "thePlan";
            create.description = "theDescription";
            create.intervalType = intervalType.ANNUAL;
            create.amount = 100;
            create.numberOfPayments = 3;
            create.trialNumberOfIntervals = 2;
            create.trialIntervalType = trialIntervalType.MONTH;
            create.active = true;

            String actual = create.Serialize();
            String expected = @"
<createPlan>
<planCode>abc</planCode>
<name>thePlan</name>
<description>theDescription</description>
<intervalType>ANNUAL</intervalType>
<amount>100</amount>
<numberOfPayments>3</numberOfPayments>
<trialNumberOfIntervals>2</trialNumberOfIntervals>
<trialIntervalType>MONTH</trialIntervalType>
<active>true</active>
</createPlan>";
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void TestCreatePlan_OnlyRequired()
        {
            createPlan create = new createPlan();
            create.planCode = "abc";
            create.name = "thePlan";
            create.intervalType = intervalType.ANNUAL;
            create.amount = 100;

            String actual = create.Serialize();
            String expected = @"
<createPlan>
<planCode>abc</planCode>
<name>thePlan</name>
<intervalType>ANNUAL</intervalType>
<amount>100</amount>
</createPlan>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestUpdatePlan_Full()
        {
            updatePlan update = new updatePlan();
            update.planCode = "abc";
            update.active = true;

            String actual = update.Serialize();
            String expected = @"
<updatePlan>
<planCode>abc</planCode>
<active>true</active>
</updatePlan>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TesLitleInternalRecurringRequestMustContainFinalPayment()
        {
            litleInternalRecurringRequest litleInternalRecurringRequest = new litleInternalRecurringRequest();
            litleInternalRecurringRequest.subscriptionId = "123";
            litleInternalRecurringRequest.recurringTxnId = "456";
            litleInternalRecurringRequest.finalPayment = true;

            String actual = litleInternalRecurringRequest.Serialize();
            String expected = @"
<subscriptionId>123</subscriptionId>
<recurringTxnId>456</recurringTxnId>
<finalPayment>true</finalPayment>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCreateDiscount_Full()
        {
            createDiscount cd = new createDiscount();
            cd.discountCode = "1";
            cd.name = "cheaper";
            cd.amount = 200;
            cd.startDate = new DateTime(2013, 9, 5);
            cd.endDate = new DateTime(2013, 9, 6);

            String actual = cd.Serialize();
            String expected = @"
<discountCode>1</discountCode>
<name>cheaper</name>
<amount>200</amount>
<startDate>2013-09-05</startDate>
<endDate>2013-09-06</endDate>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestUpdateDiscount_Full()
        {
            updateDiscount ud = new updateDiscount();
            ud.discountCode = "1";
            ud.name = "cheaper";
            ud.amount = 200;
            ud.startDate = new DateTime(2013, 9, 5);
            ud.endDate = new DateTime(2013, 9, 6);

            String actual = ud.Serialize();
            String expected = @"
<discountCode>1</discountCode>
<name>cheaper</name>
<amount>200</amount>
<startDate>2013-09-05</startDate>
<endDate>2013-09-06</endDate>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestUpdateDiscount_OnlyRequired()
        {
            updateDiscount ud = new updateDiscount();
            ud.discountCode = "1";

            String actual = ud.Serialize();
            String expected = @"
<discountCode>1</discountCode>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDeleteDiscount()
        {
            deleteDiscount ud = new deleteDiscount();
            ud.discountCode = "1";

            String actual = ud.Serialize();
            String expected = @"
<discountCode>1</discountCode>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCreateAddOn()
        {
            createAddOn cao = new createAddOn();
            cao.addOnCode = "1";
            cao.name = "addOn1";
            cao.amount = 100;
            cao.startDate = new DateTime(2013, 9, 5);
            cao.endDate = new DateTime(2013, 9, 6);

            String actual = cao.Serialize();
            String expected = @"
<addOnCode>1</addOnCode>
<name>addOn1</name>
<amount>100</amount>
<startDate>2013-09-05</startDate>
<endDate>2013-09-06</endDate>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestUpdateAddOn_Full()
        {
            updateAddOn uao = new updateAddOn();
            uao.addOnCode = "1";
            uao.name = "addOn1";
            uao.amount = 100;
            uao.startDate = new DateTime(2013, 9, 5);
            uao.endDate = new DateTime(2013, 9, 6);

            String actual = uao.Serialize();
            String expected = @"
<addOnCode>1</addOnCode>
<name>addOn1</name>
<amount>100</amount>
<startDate>2013-09-05</startDate>
<endDate>2013-09-06</endDate>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestUpdateAddOn_OnlyRequired()
        {
            updateAddOn uao = new updateAddOn();
            uao.addOnCode = "1";

            String actual = uao.Serialize();
            String expected = @"
<addOnCode>1</addOnCode>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDeleteAddOn()
        {
            deleteAddOn dao = new deleteAddOn();
            dao.addOnCode = "1";

            String actual = dao.Serialize();
            String expected = @"
<addOnCode>1</addOnCode>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestEmptyMethodOfPayment()
        {
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.Item;
            card.number = "4100000000000001";
            card.expDate = "1250";

            String actual = card.Serialize();
            String expected = @"
<type></type>
<number>4100000000000001</number>
<expDate>1250</expDate>";
            Assert.AreEqual(expected, actual);
        }
    }
}

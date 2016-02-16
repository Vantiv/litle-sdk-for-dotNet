using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestXmlFieldsSerializer
    {
        [TestFixtureSetUp]
        public void SetUpLitle()
        {
        }

        [Test]
        public void TestRecurringRequest_Full()
        {
            var request = new recurringRequest();
            request.subscription = new subscription();
            request.subscription.planCode = "123abc";
            request.subscription.numberOfPayments = 10;
            request.subscription.startDate = new DateTime(2013, 7, 25);
            request.subscription.amount = 102;

            var xml = request.Serialize();
            var match = Regex.Match(xml,
                "<subscription>\r\n<planCode>123abc</planCode>\r\n<numberOfPayments>10</numberOfPayments>\r\n<startDate>2013-07-25</startDate>\r\n<amount>102</amount>\r\n</subscription>");
            Assert.IsTrue(match.Success, xml);
        }

        [Test]
        public void TestRecurringRequest_OnlyRequired()
        {
            var request = new recurringRequest();
            request.subscription = new subscription();
            request.subscription.planCode = "123abc";

            var xml = request.Serialize();
            var match = Regex.Match(xml, "<subscription>\r\n<planCode>123abc</planCode>\r\n</subscription>");
            Assert.IsTrue(match.Success, xml);
        }

        [Test]
        public void TestSubscription_CanContainCreateDiscounts()
        {
            var subscription = new subscription();
            subscription.planCode = "123abc";

            var cd1 = new createDiscount();
            cd1.discountCode = "1";
            cd1.name = "cheaper";
            cd1.amount = 200;
            cd1.startDate = new DateTime(2013, 9, 5);
            cd1.endDate = new DateTime(2013, 9, 6);

            var cd2 = new createDiscount();
            cd2.discountCode = "2";
            cd2.name = "cheap";
            cd2.amount = 100;
            cd2.startDate = new DateTime(2013, 9, 3);
            cd2.endDate = new DateTime(2013, 9, 4);

            subscription.createDiscounts.Add(cd1);
            subscription.createDiscounts.Add(cd2);

            var actual = subscription.Serialize();
            var expected = @"
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
            var subscription = new subscription();
            subscription.planCode = "123abc";

            var cao1 = new createAddOn();
            cao1.addOnCode = "1";
            cao1.name = "addOn1";
            cao1.amount = 100;
            cao1.startDate = new DateTime(2013, 9, 5);
            cao1.endDate = new DateTime(2013, 9, 6);

            var cao2 = new createAddOn();
            cao2.addOnCode = "2";
            cao2.name = "addOn2";
            cao2.amount = 200;
            cao2.startDate = new DateTime(2013, 9, 4);
            cao2.endDate = new DateTime(2013, 9, 5);

            subscription.createAddOns.Add(cao1);
            subscription.createAddOns.Add(cao2);

            var actual = subscription.Serialize();
            var expected = @"
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
            var update = new updateSubscription();
            update.billingDate = new DateTime(2002, 10, 9);
            var billToAddress = new contact();
            billToAddress.name = "Greg Dake";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "sdksupport@litle.com";
            update.billToAddress = billToAddress;
            var card = new cardType();
            card.number = "4100000000000001";
            card.expDate = "1215";
            card.type = methodOfPaymentTypeEnum.VI;
            update.card = card;
            update.planCode = "abcdefg";
            update.subscriptionId = 12345;

            var actual = update.Serialize();
            var expected =
                "\r\n<updateSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n<planCode>abcdefg</planCode>\r\n<billToAddress>\r\n<name>Greg Dake</name>\r\n<city>Lowell</city>\r\n<state>MA</state>\r\n<email>sdksupport@litle.com</email>\r\n</billToAddress>\r\n<card>\r\n<type>VI</type>\r\n<number>4100000000000001</number>\r\n<expDate>1215</expDate>\r\n</card>\r\n<billingDate>2002-10-09</billingDate>\r\n</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSubscription_OnlyRequired()
        {
            var update = new updateSubscription();
            update.subscriptionId = 12345;

            var actual = update.Serialize();
            var expected = "\r\n<updateSubscription>\r\n<subscriptionId>12345</subscriptionId>\r\n</updateSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUpdateSubscription_CanContainCreateDiscounts()
        {
            var cd1 = new createDiscount();
            cd1.discountCode = "1";
            cd1.name = "cheaper";
            cd1.amount = 200;
            cd1.startDate = new DateTime(2013, 9, 5);
            cd1.endDate = new DateTime(2013, 9, 6);

            var cd2 = new createDiscount();
            cd2.discountCode = "2";
            cd2.name = "cheap";
            cd2.amount = 100;
            cd2.startDate = new DateTime(2013, 9, 3);
            cd2.endDate = new DateTime(2013, 9, 4);

            var update = new updateSubscription();
            update.subscriptionId = 1;
            update.createDiscounts.Add(cd1);
            update.createDiscounts.Add(cd2);

            var actual = update.Serialize();
            var expected = @"
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
            var ud1 = new updateDiscount();
            ud1.discountCode = "1";
            ud1.name = "cheaper";
            ud1.amount = 200;
            ud1.startDate = new DateTime(2013, 9, 5);
            ud1.endDate = new DateTime(2013, 9, 6);

            var ud2 = new updateDiscount();
            ud2.discountCode = "2";
            ud2.name = "cheap";
            ud2.amount = 100;
            ud2.startDate = new DateTime(2013, 9, 3);
            ud2.endDate = new DateTime(2013, 9, 4);

            var update = new updateSubscription();
            update.subscriptionId = 1;
            update.updateDiscounts.Add(ud1);
            update.updateDiscounts.Add(ud2);

            var actual = update.Serialize();
            var expected = @"
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
            var dd1 = new deleteDiscount();
            dd1.discountCode = "1";

            var dd2 = new deleteDiscount();
            dd2.discountCode = "2";

            var update = new updateSubscription();
            update.subscriptionId = 1;
            update.deleteDiscounts.Add(dd1);
            update.deleteDiscounts.Add(dd2);

            var actual = update.Serialize();
            var expected = @"
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
            var cao1 = new createAddOn();
            cao1.addOnCode = "1";
            cao1.name = "addOn1";
            cao1.amount = 100;
            cao1.startDate = new DateTime(2013, 9, 5);
            cao1.endDate = new DateTime(2013, 9, 6);

            var cao2 = new createAddOn();
            cao2.addOnCode = "2";
            cao2.name = "addOn2";
            cao2.amount = 200;
            cao2.startDate = new DateTime(2013, 9, 4);
            cao2.endDate = new DateTime(2013, 9, 5);

            var update = new updateSubscription();
            update.subscriptionId = 1;
            update.createAddOns.Add(cao1);
            update.createAddOns.Add(cao2);

            var actual = update.Serialize();
            var expected = @"
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
            var uao1 = new updateAddOn();
            uao1.addOnCode = "1";
            uao1.name = "addOn1";
            uao1.amount = 100;
            uao1.startDate = new DateTime(2013, 9, 5);
            uao1.endDate = new DateTime(2013, 9, 6);

            var uao2 = new updateAddOn();
            uao2.addOnCode = "2";
            uao2.name = "addOn2";
            uao2.amount = 200;
            uao2.startDate = new DateTime(2013, 9, 4);
            uao2.endDate = new DateTime(2013, 9, 5);

            var update = new updateSubscription();
            update.subscriptionId = 1;
            update.updateAddOns.Add(uao1);
            update.updateAddOns.Add(uao2);

            var actual = update.Serialize();
            var expected = @"
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
            var dao1 = new deleteAddOn();
            dao1.addOnCode = "1";

            var dao2 = new deleteAddOn();
            dao2.addOnCode = "2";

            var update = new updateSubscription();
            update.subscriptionId = 1;
            update.deleteAddOns.Add(dao1);
            update.deleteAddOns.Add(dao2);

            var actual = update.Serialize();
            var expected = @"
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
            var update = new updateSubscription();
            update.subscriptionId = 1;
            update.token = new cardTokenType();
            update.token.litleToken = "123456";

            var actual = update.Serialize();
            var expected = @"
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
            var update = new updateSubscription();
            update.subscriptionId = 1;
            update.paypage = new cardPaypageType();
            update.paypage.paypageRegistrationId = "abc123";

            var actual = update.Serialize();
            var expected = @"
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
            var cancel = new cancelSubscription();
            cancel.subscriptionId = 12345;

            var actual = cancel.Serialize();
            var expected = @"
<cancelSubscription>
<subscriptionId>12345</subscriptionId>
</cancelSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testCancelSubscription_OnlyRequired()
        {
            var update = new cancelSubscription();
            update.subscriptionId = 12345;

            var actual = update.Serialize();
            var expected = @"
<cancelSubscription>
<subscriptionId>12345</subscriptionId>
</cancelSubscription>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testActivate_Full()
        {
            var activate = new activate();
            activate.orderId = "12345";
            activate.amount = 200;
            activate.orderSource = orderSourceType.ecommerce;
            activate.id = "theId";
            activate.reportGroup = "theReportGroup";
            activate.card = new cardType();

            var actual = activate.Serialize();
            var expected = @"
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
            var activate = new activate();
            activate.orderId = "12345";
            activate.amount = 200;
            activate.orderSource = orderSourceType.ecommerce;
            activate.id = "theId";
            activate.reportGroup = "theReportGroup";
            activate.virtualGiftCard = new virtualGiftCardType();

            var actual = activate.Serialize();
            var expected = @"
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
            var virtualGiftCard = new virtualGiftCardType();
            virtualGiftCard.accountNumberLength = 16;
            virtualGiftCard.giftCardBin = "123456";

            var actual = virtualGiftCard.Serialize();
            var expected = @"
<accountNumberLength>16</accountNumberLength>
<giftCardBin>123456</giftCardBin>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testDeactivate_Full()
        {
            var deactivate = new deactivate();
            deactivate.orderId = "12345";
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new cardType();
            deactivate.id = "theId";
            deactivate.reportGroup = "theReportGroup";

            var actual = deactivate.Serialize();
            var expected = @"
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
            var deactivate = new deactivate();
            deactivate.orderId = "12345";
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new cardType();
            deactivate.id = "theId";
            deactivate.reportGroup = "theReportGroup";

            var actual = deactivate.Serialize();
            var expected = @"
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
            var load = new load();
            load.orderId = "12345";
            load.amount = 200;
            load.orderSource = orderSourceType.ecommerce;
            load.card = new cardType();
            load.id = "theId";
            load.reportGroup = "theReportGroup";

            var actual = load.Serialize();
            var expected = @"
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
            var load = new load();
            load.orderId = "12345";
            load.amount = 200;
            load.orderSource = orderSourceType.ecommerce;
            load.card = new cardType();
            load.id = "theId";
            load.reportGroup = "theReportGroup";

            var actual = load.Serialize();
            var expected = @"
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
            var unload = new unload();
            unload.orderId = "12345";
            unload.amount = 200;
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new cardType();
            unload.id = "theId";
            unload.reportGroup = "theReportGroup";

            var actual = unload.Serialize();
            var expected = @"
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
            var unload = new unload();
            unload.orderId = "12345";
            unload.amount = 200;
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new cardType();
            unload.id = "theId";
            unload.reportGroup = "theReportGroup";

            var actual = unload.Serialize();
            var expected = @"
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
            var balanceInquiry = new balanceInquiry();
            balanceInquiry.orderId = "12345";
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new cardType();
            balanceInquiry.id = "theId";
            balanceInquiry.reportGroup = "theReportGroup";

            var actual = balanceInquiry.Serialize();
            var expected = @"
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
            var balanceInquiry = new balanceInquiry();
            balanceInquiry.orderId = "12345";
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new cardType();
            balanceInquiry.id = "theId";
            balanceInquiry.reportGroup = "theReportGroup";

            var actual = balanceInquiry.Serialize();
            var expected = @"
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
            var create = new createPlan();
            create.planCode = "abc";
            create.name = "thePlan";
            create.description = "theDescription";
            create.intervalType = intervalType.ANNUAL;
            create.amount = 100;
            create.numberOfPayments = 3;
            create.trialNumberOfIntervals = 2;
            create.trialIntervalType = trialIntervalType.MONTH;
            create.active = true;

            var actual = create.Serialize();
            var expected = @"
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
            var create = new createPlan();
            create.planCode = "abc";
            create.name = "thePlan";
            create.intervalType = intervalType.ANNUAL;
            create.amount = 100;

            var actual = create.Serialize();
            var expected = @"
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
            var update = new updatePlan();
            update.planCode = "abc";
            update.active = true;

            var actual = update.Serialize();
            var expected = @"
<updatePlan>
<planCode>abc</planCode>
<active>true</active>
</updatePlan>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TesLitleInternalRecurringRequestMustContainFinalPayment()
        {
            var litleInternalRecurringRequest = new litleInternalRecurringRequest();
            litleInternalRecurringRequest.subscriptionId = "123";
            litleInternalRecurringRequest.recurringTxnId = "456";
            litleInternalRecurringRequest.finalPayment = true;

            var actual = litleInternalRecurringRequest.Serialize();
            var expected = @"
<subscriptionId>123</subscriptionId>
<recurringTxnId>456</recurringTxnId>
<finalPayment>true</finalPayment>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCreateDiscount_Full()
        {
            var cd = new createDiscount();
            cd.discountCode = "1";
            cd.name = "cheaper";
            cd.amount = 200;
            cd.startDate = new DateTime(2013, 9, 5);
            cd.endDate = new DateTime(2013, 9, 6);

            var actual = cd.Serialize();
            var expected = @"
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
            var ud = new updateDiscount();
            ud.discountCode = "1";
            ud.name = "cheaper";
            ud.amount = 200;
            ud.startDate = new DateTime(2013, 9, 5);
            ud.endDate = new DateTime(2013, 9, 6);

            var actual = ud.Serialize();
            var expected = @"
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
            var ud = new updateDiscount();
            ud.discountCode = "1";

            var actual = ud.Serialize();
            var expected = @"
<discountCode>1</discountCode>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDeleteDiscount()
        {
            var ud = new deleteDiscount();
            ud.discountCode = "1";

            var actual = ud.Serialize();
            var expected = @"
<discountCode>1</discountCode>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestCreateAddOn()
        {
            var cao = new createAddOn();
            cao.addOnCode = "1";
            cao.name = "addOn1";
            cao.amount = 100;
            cao.startDate = new DateTime(2013, 9, 5);
            cao.endDate = new DateTime(2013, 9, 6);

            var actual = cao.Serialize();
            var expected = @"
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
            var uao = new updateAddOn();
            uao.addOnCode = "1";
            uao.name = "addOn1";
            uao.amount = 100;
            uao.startDate = new DateTime(2013, 9, 5);
            uao.endDate = new DateTime(2013, 9, 6);

            var actual = uao.Serialize();
            var expected = @"
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
            var uao = new updateAddOn();
            uao.addOnCode = "1";

            var actual = uao.Serialize();
            var expected = @"
<addOnCode>1</addOnCode>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestDeleteAddOn()
        {
            var dao = new deleteAddOn();
            dao.addOnCode = "1";

            var actual = dao.Serialize();
            var expected = @"
<addOnCode>1</addOnCode>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testDepositReversal_Full()
        {
            var depositReversal = new depositReversal();
            depositReversal.id = "theId";
            depositReversal.reportGroup = "theReportGroup";
            depositReversal.customerId = "theCustomerId";
            depositReversal.litleTxnId = "123";

            var actual = depositReversal.Serialize();
            var expected = @"
<depositReversal id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>123</litleTxnId>
</depositReversal>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testRefundReversal_Full()
        {
            var refundReversal = new refundReversal();
            refundReversal.id = "theId";
            refundReversal.reportGroup = "theReportGroup";
            refundReversal.customerId = "theCustomerId";
            refundReversal.litleTxnId = "123";

            var actual = refundReversal.Serialize();
            var expected = @"
<refundReversal id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>123</litleTxnId>
</refundReversal>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testActivateReversal_Full()
        {
            var activateReversal = new activateReversal();
            activateReversal.id = "theId";
            activateReversal.reportGroup = "theReportGroup";
            activateReversal.customerId = "theCustomerId";
            activateReversal.litleTxnId = "123";

            var actual = activateReversal.Serialize();
            var expected = @"
<activateReversal id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>123</litleTxnId>
</activateReversal>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testDeactivateReversal_Full()
        {
            var deactivateReversal = new deactivateReversal();
            deactivateReversal.id = "theId";
            deactivateReversal.reportGroup = "theReportGroup";
            deactivateReversal.customerId = "theCustomerId";
            deactivateReversal.litleTxnId = "123";

            var actual = deactivateReversal.Serialize();
            var expected = @"
<deactivateReversal id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>123</litleTxnId>
</deactivateReversal>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testLoadReversal_Full()
        {
            var loadReversal = new loadReversal();
            loadReversal.id = "theId";
            loadReversal.reportGroup = "theReportGroup";
            loadReversal.customerId = "theCustomerId";
            loadReversal.litleTxnId = "123";

            var actual = loadReversal.Serialize();
            var expected = @"
<loadReversal id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>123</litleTxnId>
</loadReversal>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testUnloadReversal_Full()
        {
            var unloadReversal = new unloadReversal();
            unloadReversal.id = "theId";
            unloadReversal.reportGroup = "theReportGroup";
            unloadReversal.customerId = "theCustomerId";
            unloadReversal.litleTxnId = "123";

            var actual = unloadReversal.Serialize();
            var expected = @"
<unloadReversal id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>123</litleTxnId>
</unloadReversal>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void testSpecialCharacters_RefundReversal()
        {
            var refundReversal = new refundReversal();
            refundReversal.id = "theId";
            refundReversal.reportGroup = "<'&\">";
            refundReversal.customerId = "theCustomerId";
            refundReversal.litleTxnId = "123";

            var actual = refundReversal.Serialize();
            var expected = @"
<refundReversal id=""theId"" customerId=""theCustomerId"" reportGroup=""&lt;&apos;&amp;&quot;&gt;"">
<litleTxnId>123</litleTxnId>
</refundReversal>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestEmptyMethodOfPayment()
        {
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.Item;
            card.number = "4100000000000001";
            card.expDate = "1250";

            var actual = card.Serialize();
            var expected = @"
<type></type>
<number>4100000000000001</number>
<expDate>1250</expDate>";
            Assert.AreEqual(expected, actual);
        }
    }
}

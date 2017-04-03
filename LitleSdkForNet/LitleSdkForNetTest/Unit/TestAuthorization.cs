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
    class TestAuthorization
    {

        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void TestFraudFilterOverride()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            auth.fraudFilterOverride = true;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestContactShouldSendEmailForEmail_NotZip()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            contact billToAddress = new contact();
            billToAddress.email = "gdake@litle.com";
            billToAddress.zip = "12345";
            auth.billToAddress = billToAddress;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<zip>12345</zip>.*<email>gdake@litle.com</email>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void Test3dsAttemptedShouldNotSayItem()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.item3dsAttempted;
            auth.reportGroup = "Planets";
            contact billToAddress = new contact();
            billToAddress.email = "gdake@litle.com";
            billToAddress.zip = "12345";
            auth.billToAddress = billToAddress;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>.*<orderSource>3dsAttempted</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void Test3dsAuthenticatedShouldNotSayItem()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.item3dsAuthenticated;
            auth.reportGroup = "Planets";
            contact billToAddress = new contact();
            billToAddress.email = "gdake@litle.com";
            billToAddress.zip = "12345";
            auth.billToAddress = billToAddress;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>.*<orderSource>3dsAuthenticated</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestSecondaryAmount()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.secondaryAmount = 1;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestSurchargeAmount()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.surchargeAmount = 1;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestMethodOfPaymentAllowsGiftCard()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.reportGroup = "Planets";
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.GC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            auth.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n</card>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestMethodOfPaymentApplepayAndWallet()
        {
            authorization auth = new authorization();
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.applepay;
            auth.reportGroup = "Planets";
            applepayType applepay = new applepayType();
            applepayHeaderType applepayHeaderType = new applepayHeaderType();
            applepayHeaderType.applicationData = "454657413164";
            applepayHeaderType.ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            applepayHeaderType.publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            applepayHeaderType.transactionId = "1234";
            applepay.header = applepayHeaderType;
            applepay.data = "user";
            applepay.signature = "sign";
            applepay.version = "1";
            auth.applepay = applepay;

            wallet wallet = new wallet();
            wallet.walletSourceTypeId = "123";
            auth.wallet = wallet;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<litleOnlineRequest.*?<authorization.*?<orderSource>applepay</orderSource>.*?<applepay>.*?<data>user</data>.*?</applepay>.*?<wallet>.*?<walletSourceTypeId>123</walletSourceTypeId>.*?</wallet>.*?</authorization>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestRecurringRequest()
        {
            authorization auth = new authorization();
            auth.card = new cardType();
            auth.card.type = methodOfPaymentTypeEnum.VI;
            auth.card.number = "4100000000000001";
            auth.card.expDate = "1213";
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.fraudFilterOverride = true;
            auth.recurringRequest = new recurringRequest();
            auth.recurringRequest.subscription = new subscription();
            auth.recurringRequest.subscription.planCode = "abc123";
            auth.recurringRequest.subscription.numberOfPayments = 12;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n<recurringRequest>\r\n<subscription>\r\n<planCode>abc123</planCode>\r\n<numberOfPayments>12</numberOfPayments>\r\n</subscription>\r\n</recurringRequest>\r\n</authorization>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestDebtRepayment()
        {
            authorization auth = new authorization();
            auth.card = new cardType();
            auth.card.type = methodOfPaymentTypeEnum.VI;
            auth.card.number = "4100000000000001";
            auth.card.expDate = "1213";
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.fraudFilterOverride = true;
            auth.debtRepayment = true;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n<debtRepayment>true</debtRepayment>\r\n</authorization>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestRecurringResponse_Full()
        {
            String xmlResponse = "<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId><recurringResponse><subscriptionId>12</subscriptionId><responseCode>345</responseCode><responseMessage>Foo</responseMessage><recurringTxnId>678</recurringTxnId></recurringResponse></authorizationResponse></litleOnlineResponse>";
            litleOnlineResponse litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            authorizationResponse authorizationResponse = (authorizationResponse)litleOnlineResponse.authorizationResponse;

            Assert.AreEqual(123, authorizationResponse.litleTxnId);
            Assert.AreEqual(12, authorizationResponse.recurringResponse.subscriptionId);
            Assert.AreEqual("345", authorizationResponse.recurringResponse.responseCode);
            Assert.AreEqual("Foo", authorizationResponse.recurringResponse.responseMessage);
            Assert.AreEqual(678, authorizationResponse.recurringResponse.recurringTxnId);
        }

        [Test]
        public void TestRecurringResponse_NoRecurringTxnId()
        {
            String xmlResponse = "<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId><recurringResponse><subscriptionId>12</subscriptionId><responseCode>345</responseCode><responseMessage>Foo</responseMessage></recurringResponse></authorizationResponse></litleOnlineResponse>";
            litleOnlineResponse litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            authorizationResponse authorizationResponse = (authorizationResponse)litleOnlineResponse.authorizationResponse;

            Assert.AreEqual(123, authorizationResponse.litleTxnId);
            Assert.AreEqual(12, authorizationResponse.recurringResponse.subscriptionId);
            Assert.AreEqual("345", authorizationResponse.recurringResponse.responseCode);
            Assert.AreEqual("Foo", authorizationResponse.recurringResponse.responseMessage);
            Assert.AreEqual(0, authorizationResponse.recurringResponse.recurringTxnId);
        }

        [Test]
        public void TestSimpleAuthWithFraudCheck()
        {
            authorization auth = new authorization();
            auth.card = new cardType();
            auth.card.type = methodOfPaymentTypeEnum.VI;
            auth.card.number = "4100000000000001";
            auth.card.expDate = "1213";
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.cardholderAuthentication = new fraudCheckType();
            auth.cardholderAuthentication.customerIpAddress = "192.168.1.1";

            String expectedResult = @"
<authorization id="""" reportGroup="""">
<orderId>12344</orderId>
<amount>2</amount>
<orderSource>ecommerce</orderSource>
<card>
<type>VI</type>
<number>4100000000000001</number>
<expDate>1213</expDate>
</card>
<cardholderAuthentication>
<customerIpAddress>192.168.1.1</customerIpAddress>
</cardholderAuthentication>
</authorization>";

            Assert.AreEqual(Regex.Replace(expectedResult, @"\s+", String.Empty), Regex.Replace(auth.Serialize(), @"\s+", String.Empty));

            var mock = new Mock<Communications>();
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<authorization id=\".*>.*<customerIpAddress>192.168.1.1</customerIpAddress>.*</authorization>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Authorize(auth);

            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestSimpleAuthWithBillMeLaterRequest()
        {
            authorization auth = new authorization();
            auth.card = new cardType();
            auth.card.type = methodOfPaymentTypeEnum.VI;
            auth.card.number = "4100000000000001";
            auth.card.expDate = "1213";
            auth.orderId = "12344";
            auth.amount = 2;
            auth.orderSource = orderSourceType.ecommerce;
            auth.billMeLaterRequest = new billMeLaterRequest();
            auth.billMeLaterRequest.virtualAuthenticationKeyData = "Data";
            auth.billMeLaterRequest.virtualAuthenticationKeyPresenceIndicator = "Presence";

            String expectedResult = @"
<authorization id="""" reportGroup="""">
<orderId>12344</orderId>
<amount>2</amount>
<orderSource>ecommerce</orderSource>
<card>
<type>VI</type>
<number>4100000000000001</number>
<expDate>1213</expDate>
</card>
<billMeLaterRequest>
<virtualAuthenticationKeyPresenceIndicator>Presence</virtualAuthenticationKeyPresenceIndicator>
<virtualAuthenticationKeyData>Data</virtualAuthenticationKeyData>
</billMeLaterRequest>
</authorization>";

            Assert.AreEqual(Regex.Replace(expectedResult, @"\s+", String.Empty), Regex.Replace(auth.Serialize(), @"\s+", String.Empty));

            var mock = new Mock<Communications>();
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<authorization id=\".*>.*<billMeLaterRequest>\r\n<virtualAuthenticationKeyPresenceIndicator>Presence</virtualAuthenticationKeyPresenceIndicator>\r\n<virtualAuthenticationKeyData>Data</virtualAuthenticationKeyData>\r\n</billMeLaterRequest>.*</authorization>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Authorize(auth);

            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestAuthWithAdvancedFraud()
        {
            authorization auth = new authorization();
            auth.orderId = "123";
            auth.amount = 10;
            auth.advancedFraudChecks = new advancedFraudChecksType();
            auth.advancedFraudChecks.threatMetrixSessionId = "800";
            auth.advancedFraudChecks.customAttribute1 = "testAttribute1";
            auth.advancedFraudChecks.customAttribute2 = "testAttribute2";
            auth.advancedFraudChecks.customAttribute3 = "testAttribute3";
            auth.advancedFraudChecks.customAttribute4 = "testAttribute4";
            auth.advancedFraudChecks.customAttribute5 = "testAttribute5";


            String expectedResult = @"
<authorization id="""" reportGroup="""">
<orderId>123</orderId>
<amount>10</amount>
<advancedFraudChecks>
<threatMetrixSessionId>800</threatMetrixSessionId>
<customAttribute1>testAttribute1</customAttribute1>
<customAttribute2>testAttribute2</customAttribute2>
<customAttribute3>testAttribute3</customAttribute3>
<customAttribute4>testAttribute4</customAttribute4>
<customAttribute5>testAttribute5</customAttribute5>
</advancedFraudChecks>
</authorization>";
            string test = auth.Serialize();
            Assert.AreEqual(Regex.Replace(expectedResult, @"\s+", String.Empty), Regex.Replace(auth.Serialize(), @"\s+", String.Empty));

            var mock = new Mock<Communications>();
            mock.Setup(Communications => Communications.HttpPost(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.23' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><orderId>123</orderId><fraudResult><advancedFraudResults><deviceReviewStatus>\"ReviewStatus\"</deviceReviewStatus><deviceReputationScore>800</deviceReputationScore></advancedFraudResults></fraudResult></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual("123", authorizationResponse.orderId);
        }

        [Test]
        public void TestAdvancedFraudResponse()
        {
            String xmlResponse = @"<litleOnlineResponse version='8.23' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'>
<authorizationResponse>
<litleTxnId>123</litleTxnId>
<fraudResult>
<advancedFraudResults>
<deviceReviewStatus>ReviewStatus</deviceReviewStatus>
<deviceReputationScore>800</deviceReputationScore>
<triggeredRule>rule triggered</triggeredRule>
<triggeredRule>rule triggered 2</triggeredRule>
</advancedFraudResults>
</fraudResult>
</authorizationResponse>
</litleOnlineResponse>";

            litleOnlineResponse litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            authorizationResponse authorizationResponse = (authorizationResponse)litleOnlineResponse.authorizationResponse;


            Assert.AreEqual(123, authorizationResponse.litleTxnId);
            Assert.NotNull(authorizationResponse.fraudResult);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.AreEqual("ReviewStatus", authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual(800, authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual("rule triggered", authorizationResponse.fraudResult.advancedFraudResults.triggeredRule);
        }

        [Test]
        public void TestAuthWithPosCatLevelEnum()
        {
            authorization auth = new authorization();
            auth.pos = new pos();
            auth.orderId = "ABC123";
            auth.amount = 98700;
            auth.pos.catLevel = posCatLevelEnum.selfservice;

            String expectedResult = @"
<authorization id="""" reportGroup="""">
<orderId>ABC123</orderId>
<amount>98700</amount>
<pos>
<catLevel>self service</catLevel>
</pos>
</authorization>";

            Assert.AreEqual(Regex.Replace(expectedResult, @"\s+", String.Empty), Regex.Replace(auth.Serialize(), @"\s+", String.Empty));

            var mock = new Mock<Communications>();
            mock.Setup(Communications => Communications.HttpPost(It.IsAny<String>(), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.23' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestRecycleEngineActive()
        {
            String xmlResponse = @"<litleOnlineResponse version='8.23' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'>
<authorizationResponse>
<litleTxnId>123</litleTxnId>
<fraudResult>
<advancedFraudResults>
<deviceReviewStatus>ReviewStatus</deviceReviewStatus>
<deviceReputationScore>800</deviceReputationScore>
<triggeredRule>rule triggered</triggeredRule>
</advancedFraudResults>
</fraudResult>
<recycling>
<recycleEngineActive>1</recycleEngineActive>
</recycling>
</authorizationResponse>
</litleOnlineResponse>";

            litleOnlineResponse litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            authorizationResponse authorizationResponse = (authorizationResponse)litleOnlineResponse.authorizationResponse;


            Assert.AreEqual(123, authorizationResponse.litleTxnId);
            Assert.NotNull(authorizationResponse.fraudResult);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.AreEqual("ReviewStatus", authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual(800, authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual("rule triggered", authorizationResponse.fraudResult.advancedFraudResults.triggeredRule);
            Assert.AreEqual(true, authorizationResponse.recycling.recycleEngineActive);
        }

        [Test]
        public void TestOriginalTransaction()
        {
            authorization auth = new authorization();
            auth.originalNetworkTransactionId = "123456789";
            auth.originalTransactionAmount = 12;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*?<originalNetworkTransactionId>123456789</originalNetworkTransactionId>.*?<originalTransactionAmount>12</originalTransactionAmount>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestOriginalTransactionWithPin()
        {
            authorization auth = new authorization();
            auth.originalNetworkTransactionId = "123456789";
            auth.originalTransactionAmount = 12;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.MC;
            card.number = "414100000000000000";
            card.expDate = "1210";
            card.pin = "1234";
            auth.card = card;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<card>\r\n<type>MC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n<pin>1234</pin>\r\n</card>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
               .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            authorizationResponse authorizationResponse = litle.Authorize(auth);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

    }
}

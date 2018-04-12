using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestAuthorization
    {

        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _litle = new LitleOnline();
        }

        [Test]
        public void TestFraudFilterOverride()
        {
            var auth = new authorization
            {
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets",
                fraudFilterOverride = true
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestContactShouldSendEmailForEmail_NotZip()
        {
            var auth = new authorization
            {
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets",
                billToAddress = new contact
                {
                    email = "gdake@litle.com",
                    zip = "12345"
                }
            };
            
            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<zip>12345</zip>.*<email>gdake@litle.com</email>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void Test3DsAttemptedShouldNotSayItem()
        {
            var auth = new authorization
            {
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.item3dsAttempted,
                reportGroup = "Planets",
                billToAddress = new contact
                {
                    email = "gdake@litle.com",
                    zip = "12345"
                }
            };
           
            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>.*<orderSource>3dsAttempted</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void Test3DsAuthenticatedShouldNotSayItem()
        {
            var auth = new authorization
            {
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.item3dsAuthenticated,
                reportGroup = "Planets",
                billToAddress = new contact
                {
                    email = "gdake@litle.com",
                    zip = "12345"
                }
            };
            
            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>.*<orderSource>3dsAuthenticated</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestSecondaryAmount()
        {
            var auth = new authorization
            {
                orderId = "12344",
                amount = 2,
                secondaryAmount = 1,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestSurchargeAmount()
        {
            var auth = new authorization
            {
                orderId = "12344",
                amount = 2,
                surchargeAmount = 1,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            var auth = new authorization
            {
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestMethodOfPaymentAllowsGiftCard()
        {
            var auth = new authorization
            {
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets",
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.GC,
                    number = "414100000000000000",
                    expDate = "1210",
                    pin = "9876"
                }
            };
            
            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<card>\r\n<type>GC</type>\r\n<number>414100000000000000</number>\r\n<expDate>1210</expDate>\r\n<pin>9876</pin>\r\n</card>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestMethodOfPaymentApplepayAndWallet()
        {
            var auth = new authorization
            {
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.applepay,
                reportGroup = "Planets",
                applepay = new applepayType
                {
                    data = "user",
                    signature = "sign",
                    version = "1",
                    header = new applepayHeaderType
                    {
                        applicationData = "454657413164",
                        ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        transactionId = "1234"
                    }
                },
                wallet = new wallet { walletSourceTypeId = "123" }
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*?<litleOnlineRequest.*?<authorization.*?<orderSource>applepay</orderSource>.*?<applepay>.*?<data>user</data>.*?</applepay>.*?<wallet>.*?<walletSourceTypeId>123</walletSourceTypeId>.*?</wallet>.*?</authorization>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestRecurringRequest()
        {
            var auth = new authorization
            {
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1213"
                },
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                fraudFilterOverride = true,
                recurringRequest = new recurringRequest
                {
                    subscription = new subscription
                    {
                        planCode = "abc123",
                        numberOfPayments = 12
                    }
                }
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n<recurringRequest>\r\n<subscription>\r\n<planCode>abc123</planCode>\r\n<numberOfPayments>12</numberOfPayments>\r\n</subscription>\r\n</recurringRequest>\r\n</authorization>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestDebtRepayment()
        {
            var auth = new authorization
            {
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1213"
                },
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                fraudFilterOverride = true,
                debtRepayment = true
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n<debtRepayment>true</debtRepayment>\r\n</authorization>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestRecurringResponse_Full()
        {
            var xmlResponse = "<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId><recurringResponse><subscriptionId>12</subscriptionId><responseCode>345</responseCode><responseMessage>Foo</responseMessage><recurringTxnId>678</recurringTxnId></recurringResponse></authorizationResponse></litleOnlineResponse>";
            var litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            var authorizationResponse = litleOnlineResponse.authorizationResponse;

            Assert.AreEqual(123, authorizationResponse.litleTxnId);
            Assert.AreEqual(12, authorizationResponse.recurringResponse.subscriptionId);
            Assert.AreEqual("345", authorizationResponse.recurringResponse.responseCode);
            Assert.AreEqual("Foo", authorizationResponse.recurringResponse.responseMessage);
            Assert.AreEqual(678, authorizationResponse.recurringResponse.recurringTxnId);
        }

        [Test]
        public void TestRecurringResponse_NoRecurringTxnId()
        {
            var xmlResponse = "<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId><recurringResponse><subscriptionId>12</subscriptionId><responseCode>345</responseCode><responseMessage>Foo</responseMessage></recurringResponse></authorizationResponse></litleOnlineResponse>";
            var litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            var authorizationResponse = litleOnlineResponse.authorizationResponse;

            Assert.AreEqual(123, authorizationResponse.litleTxnId);
            Assert.AreEqual(12, authorizationResponse.recurringResponse.subscriptionId);
            Assert.AreEqual("345", authorizationResponse.recurringResponse.responseCode);
            Assert.AreEqual("Foo", authorizationResponse.recurringResponse.responseMessage);
            Assert.AreEqual(0, authorizationResponse.recurringResponse.recurringTxnId);
        }

        [Test]
        public void TestSimpleAuthWithFraudCheck()
        {
            var auth = new authorization
            {
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1213"
                },
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                cardholderAuthentication = new fraudCheckType {customerIpAddress = "192.168.1.1"}
            };

            const string expectedResult = @"
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

            Assert.AreEqual(expectedResult, auth.Serialize());

            var mock = new Mock<Communications>();
            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<authorization id=\".*>.*<customerIpAddress>192.168.1.1</customerIpAddress>.*</authorization>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Authorize(auth);

            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestSimpleAuthWithBillMeLaterRequest()
        {
            var auth = new authorization
            {
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000001",
                    expDate = "1213"
                },
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                billMeLaterRequest = new billMeLaterRequest
                {
                    virtualAuthenticationKeyData = "Data",
                    virtualAuthenticationKeyPresenceIndicator = "Presence"
                }
            };

            const string expectedResult = @"
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

            Assert.AreEqual(expectedResult, auth.Serialize());

            var mock = new Mock<Communications>();
            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<authorization id=\".*>.*<billMeLaterRequest>\r\n<virtualAuthenticationKeyPresenceIndicator>Presence</virtualAuthenticationKeyPresenceIndicator>\r\n<virtualAuthenticationKeyData>Data</virtualAuthenticationKeyData>\r\n</billMeLaterRequest>.*</authorization>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Authorize(auth);

            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestAuthWithAdvancedFraud()
        {
            var auth = new authorization
            {
                orderId = "123",
                amount = 10,
                advancedFraudChecks = new advancedFraudChecksType
                {
                    threatMetrixSessionId = "800",
                    customAttribute1 = "testAttribute1",
                    customAttribute2 = "testAttribute2",
                    customAttribute3 = "testAttribute3",
                    customAttribute4 = "testAttribute4",
                    customAttribute5 = "testAttribute5"
                }
            };


            const string expectedResult = @"
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
            
            Assert.AreEqual(expectedResult, auth.Serialize());

            var mock = new Mock<Communications>();
            mock.Setup(communications => communications.HttpPost(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><orderId>123</orderId><fraudResult><advancedFraudResults><deviceReviewStatus>\"ReviewStatus\"</deviceReviewStatus><deviceReputationScore>800</deviceReputationScore></advancedFraudResults></fraudResult></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual("123", authorizationResponse.orderId);
        }

        [Test]
        public void TestAdvancedFraudResponse()
        {
            const string xmlResponse = @"<litleOnlineResponse version='9.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'>
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

            var litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            var authorizationResponse = litleOnlineResponse.authorizationResponse;


            Assert.AreEqual(123, authorizationResponse.litleTxnId);
            Assert.NotNull(authorizationResponse.fraudResult);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.AreEqual("ReviewStatus", authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual(800, authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual("rule triggered", authorizationResponse.fraudResult.advancedFraudResults.triggeredRule[0]);
            Assert.AreEqual("rule triggered 2", authorizationResponse.fraudResult.advancedFraudResults.triggeredRule[1]);
        }

        [Test]
        public void TestAuthWithPosCatLevelEnum()
        {
            var auth = new authorization
            {
                pos = new pos{catLevel = posCatLevelEnum.selfservice },
                orderId = "ABC123",
                amount = 98700
            };

            const string expectedResult = @"
<authorization id="""" reportGroup="""">
<orderId>ABC123</orderId>
<amount>98700</amount>
<pos>
<catLevel>self service</catLevel>
</pos>
</authorization>";

            Assert.AreEqual(expectedResult, auth.Serialize());

            var mock = new Mock<Communications>();
            mock.Setup(communications => communications.HttpPost(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            var authorizationResponse = _litle.Authorize(auth);

            Assert.NotNull(authorizationResponse);
            Assert.AreEqual(123, authorizationResponse.litleTxnId);
        }

        [Test]
        public void TestRecycleEngineActive()
        {
            const string xmlResponse = @"<litleOnlineResponse version='9.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'>
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

            var litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            var authorizationResponse = litleOnlineResponse.authorizationResponse;


            Assert.AreEqual(123, authorizationResponse.litleTxnId);
            Assert.NotNull(authorizationResponse.fraudResult);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.AreEqual("ReviewStatus", authorizationResponse.fraudResult.advancedFraudResults.deviceReviewStatus);
            Assert.NotNull(authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual(800, authorizationResponse.fraudResult.advancedFraudResults.deviceReputationScore);
            Assert.AreEqual("rule triggered", authorizationResponse.fraudResult.advancedFraudResults.triggeredRule[0]);
            Assert.AreEqual(true, authorizationResponse.recycling.recycleEngineActive);
        }

        [Test]
        public void TestAndroidpay()
        {
            var auth = new authorization
            {
                amount = 2,
                secondaryAmount = 1,
                orderSource = orderSourceType.androidpay,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>androidpay</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse><message>Approved</message><androidpayResponse><cryptogram>aHR0cHM6Ly93d3cueW91dHViZS5jb20vd2F0Y2g/dj1kUXc0dzlXZ1hjUQ0K</cryptogram><expMonth>01</expMonth><expYear>2050</expYear></androidpayResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Authorize(auth);
        }

        [Test]
        public void TestOrigTxnIdAndOrigAmount()
        {
            var auth = new authorization
            {
                amount = 584,
                orderSource = orderSourceType.ecommerce,
                originalNetworkTransactionId = "1234567890123456978901234567890",
                originalTransactionAmount = 6478,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>584</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<originalNetworkTransactionId>1234567890123456978901234567890</originalNetworkTransactionId>\r\n<originalTransactionAmount>6478</originalTransactionAmount>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId>><message>Approved</message></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Authorize(auth);
        }

        [Test]
        public void TestProcessingType()
        {
            var auth = new authorization
            {
                amount = 584,
                orderSource = orderSourceType.ecommerce,
                processingType = processingType.initialInstallment,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>584</amount>\r\n<orderSource>ecommerce</orderSource>\r\n<processingType>initialInstallment</processingType>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId>><message>Approved</message></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Authorize(auth);
        }

    }
}

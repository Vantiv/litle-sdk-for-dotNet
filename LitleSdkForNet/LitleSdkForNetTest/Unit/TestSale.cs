using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestSale
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
            var sale = new sale
            {
                orderId = "12344",
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets",
                fraudFilterOverride = false
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>false</fraudFilterOverride>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");
     
            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        [Test]
        public void TestSurchargeAmount()
        {
            var sale = new sale
            {
                amount = 2,
                surchargeAmount = 1,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        [Test]
        public void TestSurchargeAmount_Optional()
        {
            var sale = new sale
            {
                amount = 2,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        [Test]
        public void TestRecurringRequest()
        {
            var sale = new sale
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
            
            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n<recurringRequest>\r\n<subscription>\r\n<planCode>abc123</planCode>\r\n<numberOfPayments>12</numberOfPayments>\r\n</subscription>\r\n</recurringRequest>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        [Test]
        public void TestRecurringResponse_Full() {
            var xmlResponse = "<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId><recurringResponse><subscriptionId>12</subscriptionId><responseCode>345</responseCode><responseMessage>Foo</responseMessage><recurringTxnId>678</recurringTxnId></recurringResponse></saleResponse></litleOnlineResponse>";
            var litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            var saleResponse = litleOnlineResponse.saleResponse;

            Assert.AreEqual(123, saleResponse.litleTxnId);
            Assert.AreEqual(12, saleResponse.recurringResponse.subscriptionId);
            Assert.AreEqual("345", saleResponse.recurringResponse.responseCode);
            Assert.AreEqual("Foo", saleResponse.recurringResponse.responseMessage);
            Assert.AreEqual(678, saleResponse.recurringResponse.recurringTxnId);
        }

        [Test]
        public void TestRecurringResponse_NoRecurringTxnId()
        {
            var xmlResponse = "<litleOnlineResponse version='8.18' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId><recurringResponse><subscriptionId>12</subscriptionId><responseCode>345</responseCode><responseMessage>Foo</responseMessage></recurringResponse></saleResponse></litleOnlineResponse>";
            var litleOnlineResponse = LitleOnline.DeserializeObject(xmlResponse);
            var saleResponse = litleOnlineResponse.saleResponse;

            Assert.AreEqual(123, saleResponse.litleTxnId);
            Assert.AreEqual(12, saleResponse.recurringResponse.subscriptionId);
            Assert.AreEqual("345", saleResponse.recurringResponse.responseCode);
            Assert.AreEqual("Foo", saleResponse.recurringResponse.responseMessage);
            Assert.AreEqual(0,saleResponse.recurringResponse.recurringTxnId);
        }

        [Test]
        public void TestRecurringRequest_Optional()
        {
            var sale = new sale
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
                fraudFilterOverride = true
            };

            var mock = new Mock<Communications>();
            
            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n</sale>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        [Test]
        public void Test_LitleInternalRecurringRequest()
        {
            var sale = new sale
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
                litleInternalRecurringRequest = new litleInternalRecurringRequest
                {
                    subscriptionId = "123",
                    recurringTxnId = "456"
                }
            };

            var mock = new Mock<Communications>();
            
            mock.Setup(communications => communications.HttpPost(It.IsRegex("<fraudFilterOverride>true</fraudFilterOverride>\r\n<litleInternalRecurringRequest>\r\n<subscriptionId>123</subscriptionId>\r\n<recurringTxnId>456</recurringTxnId>\r\n</litleInternalRecurringRequest>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        public void Test_LitleInternalRecurringRequest_Optional()
        {
            var sale = new sale
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
                fraudFilterOverride = true
            };

            var mock = new Mock<Communications>();
            
            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<fraudFilterOverride>true</fraudFilterOverride>\r\n</sale>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        [Test]
        public void TestDebtRepayment_True()
        {
            var sale = new sale
            {
                litleInternalRecurringRequest = new litleInternalRecurringRequest(),
                debtRepayment = true
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*</litleInternalRecurringRequest>\r\n<debtRepayment>true</debtRepayment>\r\n</sale>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        [Test]
        public void TestDebtRepayment_False()
        {
            var sale = new sale
            {
                litleInternalRecurringRequest = new litleInternalRecurringRequest(),
                debtRepayment = false
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*</litleInternalRecurringRequest>\r\n<debtRepayment>false</debtRepayment>\r\n</sale>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        [Test]
        public void TestDebtRepayment_Optional()
        {
            var sale = new sale {litleInternalRecurringRequest = new litleInternalRecurringRequest()};

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*</litleInternalRecurringRequest>\r\n</sale>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        [Test]
        public void TestSecondaryAmount()
        {
            var sale = new sale
            {
                amount = 2,
                secondaryAmount = 1,
                orderSource = orderSourceType.ecommerce,
                reportGroup = "Planets"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<secondaryAmount>1</secondaryAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }

        [Test]
        public void TestApplepayAndWallet()
        {
            var sale = new sale {applepay = new applepayType()};
            var applepayHeaderType = new applepayHeaderType
            {
                applicationData = "454657413164",
                ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                transactionId = "1234"
            };
            sale.applepay.header = applepayHeaderType;
            sale.applepay.data = "user";
            sale.applepay.signature = "sign";
            sale.applepay.version = "1";
            sale.orderId = "12344";
            sale.amount = 2;
            sale.orderSource = orderSourceType.ecommerce;
            var wallet = new wallet {walletSourceTypeId = "123"};
            sale.wallet = wallet;

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*?<litleOnlineRequest.*?<sale.*?<applepay>.*?<data>user</data>.*?</applepay>.*?<walletSourceTypeId>123</walletSourceTypeId>.*?</wallet>.*?</sale>.*?", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.Sale(sale);
        }
    }
}

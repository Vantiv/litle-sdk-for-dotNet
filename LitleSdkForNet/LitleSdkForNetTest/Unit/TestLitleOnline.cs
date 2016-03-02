using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestLitleOnline
    {
        private LitleOnline litle;
        private IDictionary<string, StringBuilder> _memoryStreams;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _memoryStreams = new Dictionary<string, StringBuilder>();
            litle = new LitleOnline(_memoryStreams);
        }

        [Test]
        public void TestAuth()
        {
            var authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<litleOnlineRequest.*<authorization.*<card>.*<number>4100000000000002</number>.*</card>.*</authorization>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var authorize = litle.Authorize(authorization);
            Assert.AreEqual(123, authorize.litleTxnId);
        }

        [Test]
        public void testAuthReversal()
        {
            var authreversal = new authReversal();
            authreversal.litleTxnId = 12345678000;
            authreversal.amount = 106;
            authreversal.payPalNotes = "Notes";


            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<authReversal.*?<litleTxnId>12345678000</litleTxnId>.*?</authReversal>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authReversalResponse><litleTxnId>123</litleTxnId></authReversalResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var authreversalresponse = litle.AuthReversal(authreversal);
            Assert.AreEqual(123, authreversalresponse.litleTxnId);
        }

        [Test]
        public void testCapture()
        {
            var caputure = new capture();
            caputure.litleTxnId = 123456000;
            caputure.amount = 106;
            caputure.payPalNotes = "Notes";


            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<capture.*?<litleTxnId>123456000</litleTxnId>.*?</capture>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureResponse><litleTxnId>123</litleTxnId></captureResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var captureresponse = litle.Capture(caputure);
            Assert.AreEqual(123, captureresponse.litleTxnId);
        }

        [Test]
        public void testCaptureGivenAuth()
        {
            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.orderId = "12344";
            capturegivenauth.amount = 106;
            var authinfo = new authInformation();
            authinfo.authDate = new DateTime(2002, 10, 9);
            authinfo.authCode = "543216";
            authinfo.authAmount = 12345;
            capturegivenauth.authInformation = authinfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            capturegivenauth.card = card;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<captureGivenAuth.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</captureGivenAuth>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureGivenAuthResponse><litleTxnId>123</litleTxnId></captureGivenAuthResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var caputregivenauthresponse = litle.CaptureGivenAuth(capturegivenauth);
            Assert.AreEqual(123, caputregivenauthresponse.litleTxnId);
        }

        [Test]
        public void testCredit()
        {
            var credit = new credit();
            credit.orderId = "12344";
            credit.amount = 106;
            credit.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            credit.card = card;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<credit.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</credit>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><creditResponse><litleTxnId>123</litleTxnId></creditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var creditresponse = litle.Credit(credit);
            Assert.AreEqual(123, creditresponse.litleTxnId);
        }

        [Test]
        public void testEcheckCredit()
        {
            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12;
            echeckcredit.litleTxnId = 123456789101112;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<echeckCredit.*?<litleTxnId>123456789101112</litleTxnId>.*?</echeckCredit>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckCreditResponse><litleTxnId>123</litleTxnId></echeckCreditResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var echeckcreditresponse = litle.EcheckCredit(echeckcredit);
            Assert.AreEqual(123, echeckcreditresponse.litleTxnId);
        }

        [Test]
        public void testEcheckRedeposit()
        {
            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<echeckRedeposit.*?<litleTxnId>123456</litleTxnId>.*?</echeckRedeposit>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckRedepositResponse><litleTxnId>123</litleTxnId></echeckRedepositResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var echeckredepositresponse = litle.EcheckRedeposit(echeckredeposit);
            Assert.AreEqual(123, echeckredepositresponse.litleTxnId);
        }

        [Test]
        public void testEcheckSale()
        {
            var echecksale = new echeckSale();
            echecksale.orderId = "12345";
            echecksale.amount = 123456;
            echecksale.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echecksale.echeck = echeck;
            var contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            echecksale.billToAddress = contact;


            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<echeckSale.*?<echeck>.*?<accNum>12345657890</accNum>.*?</echeck>.*?</echeckSale>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckSalesResponse><litleTxnId>123</litleTxnId></echeckSalesResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var echecksaleresponse = litle.EcheckSale(echecksale);
            Assert.AreEqual(123, echecksaleresponse.litleTxnId);
        }

        [Test]
        public void testEcheckVerification()
        {
            var echeckverification = new echeckVerification();
            echeckverification.orderId = "12345";
            echeckverification.amount = 123456;
            echeckverification.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckverification.echeck = echeck;
            var contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            echeckverification.billToAddress = contact;


            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<echeckVerification.*?<echeck>.*?<accNum>12345657890</accNum>.*?</echeck>.*?</echeckVerification>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckVerificationResponse><litleTxnId>123</litleTxnId></echeckVerificationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var echeckverificaitonresponse = litle.EcheckVerification(echeckverification);
            Assert.AreEqual(123, echeckverificaitonresponse.litleTxnId);
        }

        [Test]
        public void testForceCapture()
        {
            var forcecapture = new forceCapture();
            forcecapture.orderId = "12344";
            forcecapture.amount = 106;
            forcecapture.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            forcecapture.card = card;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<forceCapture.*?<card>.*?<number>4100000000000001</number>.*?</card>.*?</forceCapture>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><forceCaptureResponse><litleTxnId>123</litleTxnId></forceCaptureResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var forcecaptureresponse = litle.ForceCapture(forcecapture);
            Assert.AreEqual(123, forcecaptureresponse.litleTxnId);
        }

        [Test]
        public void testSale()
        {
            var sale = new sale();
            sale.orderId = "12344";
            sale.amount = 106;
            sale.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            sale.card = card;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<sale.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</sale>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><saleResponse><litleTxnId>123</litleTxnId></saleResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var saleresponse = litle.Sale(sale);
            Assert.AreEqual(123, saleresponse.litleTxnId);
        }

        [Test]
        public void testToken()
        {
            var token = new registerTokenRequestType();
            token.orderId = "12344";
            token.accountNumber = "1233456789103801";


            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<registerTokenRequest.*?<accountNumber>1233456789103801</accountNumber>.*?</registerTokenRequest>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><registerTokenResponse><litleTxnId>123</litleTxnId></registerTokenResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var registertokenresponse = litle.RegisterToken(token);
            Assert.AreEqual(123, registertokenresponse.litleTxnId);
            Assert.IsNull(registertokenresponse.type);
        }

        [Test]
        public void testActivate()
        {
            var activate = new activate();
            activate.orderId = "2";
            activate.orderSource = orderSourceType.ecommerce;
            activate.card = new cardType();

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*?<litleOnlineRequest.*?<activate.*?<orderId>2</orderId>.*?</activate>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><activateResponse><litleTxnId>123</litleTxnId></activateResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var activateResponse = litle.Activate(activate);
            Assert.AreEqual("123", activateResponse.litleTxnId);
        }

        [Test]
        public void testDeactivate()
        {
            var deactivate = new deactivate();
            deactivate.orderId = "2";
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new cardType();

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*?<litleOnlineRequest.*?<deactivate.*?<orderId>2</orderId>.*?</deactivate>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><deactivateResponse><litleTxnId>123</litleTxnId></deactivateResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var deactivateResponse = litle.Deactivate(deactivate);
            Assert.AreEqual("123", deactivateResponse.litleTxnId);
        }

        [Test]
        public void testLoad()
        {
            var load = new load();
            load.orderId = "2";
            load.orderSource = orderSourceType.ecommerce;
            load.card = new cardType();

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*?<litleOnlineRequest.*?<load.*?<orderId>2</orderId>.*?</load>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><loadResponse><litleTxnId>123</litleTxnId></loadResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var loadResponse = litle.Load(load);
            Assert.AreEqual("123", loadResponse.litleTxnId);
        }

        [Test]
        public void testUnload()
        {
            var unload = new unload();
            unload.orderId = "2";
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new cardType();

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(".*?<litleOnlineRequest.*?<unload.*?<orderId>2</orderId>.*?</unload>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><unloadResponse><litleTxnId>123</litleTxnId></unloadResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var unloadResponse = litle.Unload(unload);
            Assert.AreEqual("123", unloadResponse.litleTxnId);
        }

        [Test]
        public void testBalanceInquiry()
        {
            var balanceInquiry = new balanceInquiry();
            balanceInquiry.orderId = "2";
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new cardType();

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<balanceInquiry.*?<orderId>2</orderId>.*?</balanceInquiry>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><balanceInquiryResponse><litleTxnId>123</litleTxnId></balanceInquiryResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var balanceInquiryResponse = litle.BalanceInquiry(balanceInquiry);
            Assert.AreEqual("123", balanceInquiryResponse.litleTxnId);
        }

        [Test]
        public void testCreatePlan()
        {
            var createPlan = new createPlan();
            createPlan.planCode = "theCode";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<createPlan.*?<planCode>theCode</planCode>.*?</createPlan>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><createPlanResponse><planCode>theCode</planCode></createPlanResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var createPlanResponse = litle.CreatePlan(createPlan);
            Assert.AreEqual("theCode", createPlanResponse.planCode);
        }

        [Test]
        public void testUpdatePlan()
        {
            var updatePlan = new updatePlan();
            updatePlan.planCode = "theCode";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<updatePlan.*?<planCode>theCode</planCode>.*?</updatePlan>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.21' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><updatePlanResponse><planCode>theCode</planCode></updatePlanResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var updatePlanResponse = litle.UpdatePlan(updatePlan);
            Assert.AreEqual("theCode", updatePlanResponse.planCode);
        }

        [Test]
        public void testLitleOnlineException()
        {
            var authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<authorization.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='1' message='Error validating xml data against the schema' xmlns='http://www.litle.com/schema'><authorizationResponse><litleTxnId>123</litleTxnId></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            try
            {
                litle.Authorize(authorization);
            }
            catch (LitleOnlineException e)
            {
                Assert.AreEqual("Error validating xml data against the schema", e.Message);
            }
        }

        [Test]
        public void testInvalidOperationException()
        {
            var authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<authorization.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("no xml");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            try
            {
                litle.Authorize(authorization);
            }
            catch (LitleOnlineException e)
            {
                Assert.AreEqual("Error validating xml data against the schema", e.Message);
            }
        }

        [Test]
        public void testDefaultReportGroup()
        {
            var authorization = new authorization();
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*?<litleOnlineRequest.*?<authorization.*? reportGroup=\"Default Report Group\">.*?<card>.*?<number>4100000000000002</number>.*?</card>.*?</authorization>.*?",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><authorizationResponse reportGroup='Default Report Group'></authorizationResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var authorize = litle.Authorize(authorization);
            Assert.AreEqual("Default Report Group", authorize.reportGroup);
        }

        [Test]
        public void testSetMerchantSdk()
        {
        }
    }
}

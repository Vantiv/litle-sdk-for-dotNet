using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestBatchRequest
    {
        private batchRequest _batchRequest;
        private const string MockFileName = "TheRainbow.xml";
        private const string MockFilePath = "C:\\Somewhere\\\\Over\\\\" + MockFileName;

        private Mock<litleFile> _mockLitleFile;
        private Mock<litleTime> _mockLitleTime;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _mockLitleFile = new Mock<litleFile>();
            _mockLitleTime = new Mock<litleTime>();

            _mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object)).Returns(MockFilePath);
            _mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(MockFilePath, It.IsAny<string>())).Returns(MockFilePath);
        }

        [SetUp]
        public void BeforeTestSetup()
        {
            _batchRequest = new batchRequest();
            _batchRequest.setLitleFile(_mockLitleFile.Object);
            _batchRequest.setLitleTime(_mockLitleTime.Object);
        }

        [Test]
        public void TestBatchRequestContainsMerchantSdkAttribute()
        {
            var mockConfig = new Dictionary<string, string>();

            mockConfig["merchantId"] = "01234";
            mockConfig["requestDirectory"] = "C:\\MockRequests";
            mockConfig["responseDirectory"] = "C:\\MockResponses";

            _batchRequest = new batchRequest(mockConfig);

            var actual = _batchRequest.generateXmlHeader();
            const string expected = @"
<batchRequest id=""""
merchantSdk=""DotNet;9.12.2""
merchantId=""01234"">
";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestInitialization()
        {
            var mockConfig = new Dictionary<string, string>();

            mockConfig["url"] = "https://www.mockurl.com";
            mockConfig["reportGroup"] = "Mock Report Group";
            mockConfig["username"] = "mockUser";
            mockConfig["printxml"] = "false";
            mockConfig["timeout"] = "35";
            mockConfig["proxyHost"] = "www.mockproxy.com";
            mockConfig["merchantId"] = "MOCKID";
            mockConfig["password"] = "mockPassword";
            mockConfig["proxyPort"] = "3000";
            mockConfig["sftpUrl"] = "www.mockftp.com";
            mockConfig["sftpUsername"] = "mockFtpUser";
            mockConfig["sftpPassword"] = "mockFtpPassword";
            mockConfig["knownHostsFile"] = "C:\\MockKnownHostsFile";
            mockConfig["onlineBatchUrl"] = "www.mockbatch.com";
            mockConfig["onlineBatchPort"] = "4000";
            mockConfig["requestDirectory"] = "C:\\MockRequests";
            mockConfig["responseDirectory"] = "C:\\MockResponses";

            _batchRequest = new batchRequest(mockConfig);

            Assert.AreEqual("C:\\MockRequests\\Requests\\", _batchRequest.getRequestDirectory());
            Assert.AreEqual("C:\\MockResponses\\Responses\\", _batchRequest.getResponseDirectory());

            Assert.NotNull(_batchRequest.getLitleTime());
            Assert.NotNull(_batchRequest.getLitleFile());
        }

        [Test]
        public void TestAddAuthorization()
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

            _batchRequest.addAuthorization(authorization);

            Assert.AreEqual(1, _batchRequest.getNumAuthorization());
            Assert.AreEqual(authorization.amount, _batchRequest.getSumOfAuthorization());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, authorization.Serialize()));
        }

        [Test]
        public void TestAddAccountUpdate()
        {
            var accountUpdate = new accountUpdate();
            accountUpdate.reportGroup = "Planets";
            accountUpdate.orderId = "12344";
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            accountUpdate.card = card;

            _batchRequest.addAccountUpdate(accountUpdate);

            Assert.AreEqual(1, _batchRequest.getNumAccountUpdates());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, accountUpdate.Serialize()));
        }

        [Test]
        public void TestAuthReversal()
        {
            var authreversal = new authReversal();
            authreversal.litleTxnId = 12345678000;
            authreversal.amount = 106;
            authreversal.payPalNotes = "Notes";

            _batchRequest.addAuthReversal(authreversal);

            Assert.AreEqual(1, _batchRequest.getNumAuthReversal());
            Assert.AreEqual(authreversal.amount, _batchRequest.getSumOfAuthReversal());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, authreversal.Serialize()));
        }

        [Test]
        public void TestCapture()
        {
            var capture = new capture();
            capture.litleTxnId = 12345678000;
            capture.amount = 106;

            _batchRequest.addCapture(capture);

            Assert.AreEqual(1, _batchRequest.getNumCapture());
            Assert.AreEqual(capture.amount, _batchRequest.getSumOfCapture());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, capture.Serialize()));
        }

        [Test]
        public void TestCaptureGivenAuth()
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

            _batchRequest.addCaptureGivenAuth(capturegivenauth);

            Assert.AreEqual(1, _batchRequest.getNumCaptureGivenAuth());
            Assert.AreEqual(capturegivenauth.amount, _batchRequest.getSumOfCaptureGivenAuth());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, capturegivenauth.Serialize()));
        }

        [Test]
        public void TestCredit()
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

            _batchRequest.addCredit(credit);

            Assert.AreEqual(1, _batchRequest.getNumCredit());
            Assert.AreEqual(credit.amount, _batchRequest.getSumOfCredit());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, credit.Serialize()));
        }

        [Test]
        public void TestEcheckCredit()
        {
            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12;
            echeckcredit.litleTxnId = 123456789101112;

            _batchRequest.addEcheckCredit(echeckcredit);

            Assert.AreEqual(1, _batchRequest.getNumEcheckCredit());
            Assert.AreEqual(echeckcredit.amount, _batchRequest.getSumOfEcheckCredit());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, echeckcredit.Serialize()));
        }

        [Test]
        public void TestEcheckRedeposit()
        {
            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;

            _batchRequest.addEcheckRedeposit(echeckredeposit);

            Assert.AreEqual(1, _batchRequest.getNumEcheckRedeposit());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, echeckredeposit.Serialize()));
        }

        [Test]
        public void TestEcheckSale()
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

            _batchRequest.addEcheckSale(echecksale);

            Assert.AreEqual(1, _batchRequest.getNumEcheckSale());
            Assert.AreEqual(echecksale.amount, _batchRequest.getSumOfEcheckSale());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, echecksale.Serialize()));
        }

        [Test]
        public void TestEcheckVerification()
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

            _batchRequest.addEcheckVerification(echeckverification);

            Assert.AreEqual(1, _batchRequest.getNumEcheckVerification());
            Assert.AreEqual(echeckverification.amount, _batchRequest.getSumOfEcheckVerification());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, echeckverification.Serialize()));
        }

        [Test]
        public void TestForceCapture()
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

            _batchRequest.addForceCapture(forcecapture);

            Assert.AreEqual(1, _batchRequest.getNumForceCapture());
            Assert.AreEqual(forcecapture.amount, _batchRequest.getSumOfForceCapture());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, forcecapture.Serialize()));
        }

        [Test]
        public void TestSale()
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

            _batchRequest.addSale(sale);

            Assert.AreEqual(1, _batchRequest.getNumSale());
            Assert.AreEqual(sale.amount, _batchRequest.getSumOfSale());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, sale.Serialize()));
        }

        [Test]
        public void TestToken()
        {
            var token = new registerTokenRequestType();
            token.orderId = "12344";
            token.accountNumber = "1233456789103801";

            _batchRequest.addRegisterTokenRequest(token);

            Assert.AreEqual(1, _batchRequest.getNumRegisterTokenRequest());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, token.Serialize()));
        }

        [Test]
        public void TestUpdateCardValidationNumOnToken()
        {
            var updateCardValidationNumOnToken = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken.orderId = "12344";
            updateCardValidationNumOnToken.litleToken = "123";

            _batchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);

            Assert.AreEqual(1, _batchRequest.getNumUpdateCardValidationNumOnToken());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, updateCardValidationNumOnToken.Serialize()));
        }

        [Test]
        public void TestUpdateSubscription()
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

            _batchRequest.addUpdateSubscription(update);

            Assert.AreEqual(1, _batchRequest.getNumUpdateSubscriptions());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, update.Serialize()));
        }

        [Test]
        public void TestCreatePlan()
        {
            var createPlan = new createPlan();

            _batchRequest.addCreatePlan(createPlan);

            Assert.AreEqual(1, _batchRequest.getNumCreatePlans());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, createPlan.Serialize()));
        }

        [Test]
        public void TestUpdatePlan()
        {
            var updatePlan = new updatePlan();

            _batchRequest.addUpdatePlan(updatePlan);

            Assert.AreEqual(1, _batchRequest.getNumUpdatePlans());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, updatePlan.Serialize()));
        }

        [Test]
        public void TestActivate()
        {
            var activate = new activate();
            activate.amount = 500;
            activate.orderSource = orderSourceType.ecommerce;
            activate.card = new cardType();

            _batchRequest.addActivate(activate);

            Assert.AreEqual(1, _batchRequest.getNumActivates());
            Assert.AreEqual(500, _batchRequest.getActivateAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, activate.Serialize()));
        }

        [Test]
        public void TestDeactivate()
        {
            var deactivate = new deactivate();
            deactivate.orderSource = orderSourceType.ecommerce;
            deactivate.card = new cardType();

            _batchRequest.addDeactivate(deactivate);

            Assert.AreEqual(1, _batchRequest.getNumDeactivates());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, deactivate.Serialize()));
        }

        [Test]
        public void TestLoad()
        {
            var load = new load();
            load.amount = 600;
            load.orderSource = orderSourceType.ecommerce;
            load.card = new cardType();

            _batchRequest.addLoad(load);

            Assert.AreEqual(1, _batchRequest.getNumLoads());
            Assert.AreEqual(600, _batchRequest.getLoadAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, load.Serialize()));
        }

        [Test]
        public void TestUnload()
        {
            var unload = new unload();
            unload.amount = 700;
            unload.orderSource = orderSourceType.ecommerce;
            unload.card = new cardType();

            _batchRequest.addUnload(unload);

            Assert.AreEqual(1, _batchRequest.getNumUnloads());
            Assert.AreEqual(700, _batchRequest.getUnloadAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, unload.Serialize()));
        }

        [Test]
        public void TestBalanceInquiry()
        {
            var balanceInquiry = new balanceInquiry();
            balanceInquiry.orderSource = orderSourceType.ecommerce;
            balanceInquiry.card = new cardType();

            _batchRequest.addBalanceInquiry(balanceInquiry);

            Assert.AreEqual(1, _batchRequest.getNumBalanceInquiries());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, balanceInquiry.Serialize()));
        }

        [Test]
        public void TestCancelSubscription()
        {
            var cancel = new cancelSubscription();
            cancel.subscriptionId = 12345;

            _batchRequest.addCancelSubscription(cancel);

            Assert.AreEqual(1, _batchRequest.getNumCancelSubscriptions());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, cancel.Serialize()));
        }

        [Test]
        public void TestAddEcheckPreNoteSale()
        {
            var echeckPreNoteSale = new echeckPreNoteSale();
            echeckPreNoteSale.orderId = "12345";
            echeckPreNoteSale.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckPreNoteSale.echeck = echeck;
            var contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            echeckPreNoteSale.billToAddress = contact;

            _batchRequest.addEcheckPreNoteSale(echeckPreNoteSale);

            Assert.AreEqual(1, _batchRequest.getNumEcheckPreNoteSale());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, echeckPreNoteSale.Serialize()));
        }

        [Test]
        public void TestAddEcheckPreNoteCredit()
        {
            var echeckPreNoteCredit = new echeckPreNoteCredit();
            echeckPreNoteCredit.orderId = "12345";
            echeckPreNoteCredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckPreNoteCredit.echeck = echeck;
            var contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            echeckPreNoteCredit.billToAddress = contact;

            _batchRequest.addEcheckPreNoteCredit(echeckPreNoteCredit);

            Assert.AreEqual(1, _batchRequest.getNumEcheckPreNoteCredit());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, echeckPreNoteCredit.Serialize()));
        }

        [Test]
        public void TestAddSubmerchantCredit()
        {
            var submerchantCredit = new submerchantCredit();
            submerchantCredit.fundingSubmerchantId = "123456";
            submerchantCredit.submerchantName = "merchant";
            submerchantCredit.fundsTransferId = "123467";
            submerchantCredit.amount = 106L;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            submerchantCredit.accountInfo = echeck;

            _batchRequest.addSubmerchantCredit(submerchantCredit);

            Assert.AreEqual(1, _batchRequest.getNumSubmerchantCredit());
            Assert.AreEqual(106L, _batchRequest.getSubmerchantCreditAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, submerchantCredit.Serialize()));
        }

        [Test]
        public void TestAddPayFacCredit()
        {
            var payFacCredit = new payFacCredit();
            payFacCredit.fundingSubmerchantId = "123456";
            payFacCredit.fundsTransferId = "123467";
            payFacCredit.amount = 107L;

            _batchRequest.addPayFacCredit(payFacCredit);

            Assert.AreEqual(1, _batchRequest.getNumPayFacCredit());
            Assert.AreEqual(107L, _batchRequest.getPayFacCreditAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, payFacCredit.Serialize()));
        }

        [Test]
        public void TestAddReserveCredit()
        {
            var reserveCredit = new reserveCredit();
            reserveCredit.fundingSubmerchantId = "123456";
            reserveCredit.fundsTransferId = "123467";
            reserveCredit.amount = 107L;

            _batchRequest.addReserveCredit(reserveCredit);

            Assert.AreEqual(1, _batchRequest.getNumReserveCredit());
            Assert.AreEqual(107L, _batchRequest.getReserveCreditAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, reserveCredit.Serialize()));
        }

        [Test]
        public void TestAddVendorCredit()
        {
            var vendorCredit = new vendorCredit();
            vendorCredit.fundingSubmerchantId = "123456";
            vendorCredit.vendorName = "merchant";
            vendorCredit.fundsTransferId = "123467";
            vendorCredit.amount = 106L;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            vendorCredit.accountInfo = echeck;

            _batchRequest.addVendorCredit(vendorCredit);

            Assert.AreEqual(1, _batchRequest.getNumVendorCredit());
            Assert.AreEqual(106L, _batchRequest.getVendorCreditAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, vendorCredit.Serialize()));
        }

        [Test]
        public void TestAddPhysicalCheckCredit()
        {
            var physicalCheckCredit = new physicalCheckCredit();
            physicalCheckCredit.fundingSubmerchantId = "123456";
            physicalCheckCredit.fundsTransferId = "123467";
            physicalCheckCredit.amount = 107L;

            _batchRequest.addPhysicalCheckCredit(physicalCheckCredit);

            Assert.AreEqual(1, _batchRequest.getNumPhysicalCheckCredit());
            Assert.AreEqual(107L, _batchRequest.getPhysicalCheckCreditAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, physicalCheckCredit.Serialize()));
        }

        [Test]
        public void TestAddSubmerchantDebit()
        {
            var submerchantDebit = new submerchantDebit();
            submerchantDebit.fundingSubmerchantId = "123456";
            submerchantDebit.submerchantName = "merchant";
            submerchantDebit.fundsTransferId = "123467";
            submerchantDebit.amount = 106L;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            submerchantDebit.accountInfo = echeck;

            _batchRequest.addSubmerchantDebit(submerchantDebit);

            Assert.AreEqual(1, _batchRequest.getNumSubmerchantDebit());
            Assert.AreEqual(106L, _batchRequest.getSubmerchantDebitAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, submerchantDebit.Serialize()));
        }

        [Test]
        public void TestAddPayFacDebit()
        {
            var payFacDebit = new payFacDebit();
            payFacDebit.fundingSubmerchantId = "123456";
            payFacDebit.fundsTransferId = "123467";
            payFacDebit.amount = 107L;

            _batchRequest.addPayFacDebit(payFacDebit);

            Assert.AreEqual(1, _batchRequest.getNumPayFacDebit());
            Assert.AreEqual(107L, _batchRequest.getPayFacDebitAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, payFacDebit.Serialize()));
        }

        [Test]
        public void TestAddReserveDebit()
        {
            var reserveDebit = new reserveDebit();
            reserveDebit.fundingSubmerchantId = "123456";
            reserveDebit.fundsTransferId = "123467";
            reserveDebit.amount = 107L;

            _batchRequest.addReserveDebit(reserveDebit);

            Assert.AreEqual(1, _batchRequest.getNumReserveDebit());
            Assert.AreEqual(107L, _batchRequest.getReserveDebitAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, reserveDebit.Serialize()));
        }

        [Test]
        public void TestAddVendorDebit()
        {
            var vendorDebit = new vendorDebit();
            vendorDebit.fundingSubmerchantId = "123456";
            vendorDebit.vendorName = "merchant";
            vendorDebit.fundsTransferId = "123467";
            vendorDebit.amount = 106L;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            vendorDebit.accountInfo = echeck;

            _batchRequest.addVendorDebit(vendorDebit);

            Assert.AreEqual(1, _batchRequest.getNumVendorDebit());
            Assert.AreEqual(106L, _batchRequest.getVendorDebitAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, vendorDebit.Serialize()));
        }

        [Test]
        public void TestAddPhysicalCheckDebit()
        {
            var physicalCheckDebit = new physicalCheckDebit();
            physicalCheckDebit.fundingSubmerchantId = "123456";
            physicalCheckDebit.fundsTransferId = "123467";
            physicalCheckDebit.amount = 107L;

            _batchRequest.addPhysicalCheckDebit(physicalCheckDebit);

            Assert.AreEqual(1, _batchRequest.getNumPhysicalCheckDebit());
            Assert.AreEqual(107L, _batchRequest.getPhysicalCheckDebitAmount());

            _mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), _mockLitleTime.Object));
            _mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(MockFilePath, physicalCheckDebit.Serialize()));
        }
    }
}

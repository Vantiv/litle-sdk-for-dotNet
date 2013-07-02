using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;
using Moq.Language.Flow;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestBatchRequest
    {
        private batchRequest batchRequest;
        private const string timeFormat = "MM-dd-yyyy_HH-mm-ss-ffff_";
        private const string timeRegex = "[0-1][0-9]-[0-3][0-9]-[0-9]{4}_[0-9]{2}-[0-9]{2}-[0-9]{2}-[0-9]{4}_";
        private const string batchNameRegex = timeRegex + "[A-Z]{8}";
        private const string mockFileName = "TheRainbow.xml";
        private const string mockFilePath = "C:\\Somewhere\\\\Over\\\\" + mockFileName;

        private Mock<litleFile> mockLitleFile;
        private Mock<litleTime> mockLitleTime;

        [TestFixtureSetUp]
        public void setUp()
        {
            mockLitleFile = new Mock<litleFile>();
            mockLitleTime = new Mock<litleTime>();

            mockLitleFile.Setup(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object)).Returns(mockFilePath);
            mockLitleFile.Setup(litleFile => litleFile.AppendLineToFile(mockFilePath, It.IsAny<String>())).Returns(mockFilePath);
        }

        [SetUp]
        public void beforeTestSetup()
        {
            batchRequest = new batchRequest();
            batchRequest.setLitleFile(mockLitleFile.Object);
            batchRequest.setLitleTime(mockLitleTime.Object);
        }

        [Test]
        public void testAddAuthorization()
        {
            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            authorization.card = card;

            batchRequest.addAuthorization(authorization);

            Assert.AreEqual(1, batchRequest.getNumAuthorization());
            Assert.AreEqual(authorization.amount, batchRequest.getSumOfAuthorization());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, authorization.Serialize()));
        }

        [Test]
        public void testAddAccountUpdate()
        {
            accountUpdate accountUpdate = new accountUpdate();
            accountUpdate.reportGroup = "Planets";
            accountUpdate.orderId = "12344";
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            accountUpdate.card = card;

            batchRequest.addAccountUpdate(accountUpdate);

            Assert.AreEqual(1, batchRequest.getNumAccountUpdates());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, accountUpdate.Serialize()));
        }

        [Test]
        public void testAuthReversal()
        {
            authReversal authreversal = new authReversal();
            authreversal.litleTxnId = 12345678000;
            authreversal.amount = 106;
            authreversal.payPalNotes = "Notes";

            batchRequest.addAuthReversal(authreversal);

            Assert.AreEqual(1, batchRequest.getNumAuthReversal());
            Assert.AreEqual(authreversal.amount, batchRequest.getSumOfAuthReversal());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, authreversal.Serialize()));
        }

        [Test]
        public void testCapture()
        {
            capture capture = new capture();
            capture.litleTxnId = 12345678000;
            capture.amount = 106;

            batchRequest.addCapture(capture);

            Assert.AreEqual(1, batchRequest.getNumCapture());
            Assert.AreEqual(capture.amount, batchRequest.getSumOfCapture());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, capture.Serialize()));
        }

        [Test]
        public void testCaptureGivenAuth()
        {
            captureGivenAuth capturegivenauth = new captureGivenAuth();
            capturegivenauth.orderId = "12344";
            capturegivenauth.amount = 106;
            authInformation authinfo = new authInformation();
            authinfo.authDate = new DateTime(2002, 10, 9);
            authinfo.authCode = "543216";
            authinfo.authAmount = 12345;
            capturegivenauth.authInformation = authinfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            capturegivenauth.card = card;

            batchRequest.addCaptureGivenAuth(capturegivenauth);

            Assert.AreEqual(1, batchRequest.getNumCaptureGivenAuth());
            Assert.AreEqual(capturegivenauth.amount, batchRequest.getSumOfCaptureGivenAuth());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, capturegivenauth.Serialize()));
        }

        [Test]
        public void testCredit()
        {
            credit credit = new credit();
            credit.orderId = "12344";
            credit.amount = 106;
            credit.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            credit.card = card;

            batchRequest.addCredit(credit);

            Assert.AreEqual(1, batchRequest.getNumCredit());
            Assert.AreEqual(credit.amount, batchRequest.getSumOfCredit());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, credit.Serialize()));
        }

        [Test]
        public void testEcheckCredit()
        {
            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12;
            echeckcredit.litleTxnId = 123456789101112;

            batchRequest.addEcheckCredit(echeckcredit);

            Assert.AreEqual(1, batchRequest.getNumEcheckCredit());
            Assert.AreEqual(echeckcredit.amount, batchRequest.getSumOfEcheckCredit());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, echeckcredit.Serialize()));
        }

        [Test]
        public void testEcheckRedeposit()
        {
            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;

            batchRequest.addEcheckRedeposit(echeckredeposit);

            Assert.AreEqual(1, batchRequest.getNumEcheckRedeposit());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, echeckredeposit.Serialize()));
        }

        [Test]
        public void testEcheckSale()
        {
            echeckSale echecksale = new echeckSale();
            echecksale.orderId = "12345";
            echecksale.amount = 123456;
            echecksale.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echecksale.echeck = echeck;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            echecksale.billToAddress = contact;

            batchRequest.addEcheckSale(echecksale);

            Assert.AreEqual(1, batchRequest.getNumEcheckSale());
            Assert.AreEqual(echecksale.amount, batchRequest.getSumOfEcheckSale());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, echecksale.Serialize()));
        }

        [Test]
        public void testEcheckVerification()
        {
            echeckVerification echeckverification = new echeckVerification();
            echeckverification.orderId = "12345";
            echeckverification.amount = 123456;
            echeckverification.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckverification.echeck = echeck;
            contact contact = new contact();
            contact.name = "Bob";
            contact.city = "lowell";
            contact.state = "MA";
            contact.email = "litle.com";
            echeckverification.billToAddress = contact;

            batchRequest.addEcheckVerification(echeckverification);

            Assert.AreEqual(1, batchRequest.getNumEcheckVerification());
            Assert.AreEqual(echeckverification.amount, batchRequest.getSumOfEcheckVerification());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, echeckverification.Serialize()));
        }

        [Test]
        public void testForceCapture()
        {
            forceCapture forcecapture = new forceCapture();
            forcecapture.orderId = "12344";
            forcecapture.amount = 106;
            forcecapture.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            forcecapture.card = card;

            batchRequest.addForceCapture(forcecapture);

            Assert.AreEqual(1, batchRequest.getNumForceCapture());
            Assert.AreEqual(forcecapture.amount, batchRequest.getSumOfForceCapture());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, forcecapture.Serialize()));
        }

        [Test]
        public void testSale()
        {
            sale sale = new sale();
            sale.orderId = "12344";
            sale.amount = 106;
            sale.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000002";
            card.expDate = "1210";
            sale.card = card;

            batchRequest.addSale(sale);

            Assert.AreEqual(1, batchRequest.getNumSale());
            Assert.AreEqual(sale.amount, batchRequest.getSumOfSale());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, sale.Serialize()));
        }

        [Test]
        public void testToken()
        {
            registerTokenRequestType token = new registerTokenRequestType();
            token.orderId = "12344";
            token.accountNumber = "1233456789103801";

            batchRequest.addRegisterTokenRequest(token);

            Assert.AreEqual(1, batchRequest.getNumRegisterTokenRequest());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, token.Serialize()));
        }

        [Test]
        public void testUpdateCardValidationNumOnToken()
        {
            updateCardValidationNumOnToken updateCardValidationNumOnToken = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken.orderId = "12344";
            updateCardValidationNumOnToken.litleToken = "123";

            batchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);

            Assert.AreEqual(1, batchRequest.getNumUpdateCardValidationNumOnToken());

            mockLitleFile.Verify(litleFile => litleFile.createRandomFile(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), mockLitleTime.Object));
            mockLitleFile.Verify(litleFile => litleFile.AppendLineToFile(mockFilePath, updateCardValidationNumOnToken.Serialize()));
        } 
    }
}

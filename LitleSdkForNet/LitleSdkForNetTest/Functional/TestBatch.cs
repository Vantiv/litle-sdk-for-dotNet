using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using System.IO;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestBatch
    {
        private litleRequest _litle;
        private Dictionary<string, string> _invalidConfig;
        private Dictionary<string, string> _invalidSftpConfig;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _invalidConfig = new Dictionary<string, string>();
            _invalidConfig["url"] = Properties.Settings.Default.url;
            _invalidConfig["reportGroup"] = Properties.Settings.Default.reportGroup;
            _invalidConfig["username"] = "badUsername";
            _invalidConfig["printxml"] = Properties.Settings.Default.printxml;
            _invalidConfig["timeout"] = Properties.Settings.Default.timeout;
            _invalidConfig["proxyHost"] = Properties.Settings.Default.proxyHost;
            _invalidConfig["merchantId"] = Properties.Settings.Default.merchantId;
            _invalidConfig["password"] = "badPassword";
            _invalidConfig["proxyPort"] = Properties.Settings.Default.proxyPort;
            _invalidConfig["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            _invalidConfig["sftpUsername"] = Properties.Settings.Default.sftpUsername;
            _invalidConfig["sftpPassword"] = Properties.Settings.Default.sftpPassword;
            _invalidConfig["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
            _invalidConfig["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            _invalidConfig["responseDirectory"] = Properties.Settings.Default.responseDirectory;
           

            _invalidSftpConfig = new Dictionary<string, string>();
            _invalidSftpConfig["url"] = Properties.Settings.Default.url;
            _invalidSftpConfig["reportGroup"] = Properties.Settings.Default.reportGroup;
            _invalidSftpConfig["username"] = Properties.Settings.Default.username;
            _invalidSftpConfig["printxml"] = Properties.Settings.Default.printxml;
            _invalidSftpConfig["timeout"] = Properties.Settings.Default.timeout;
            _invalidSftpConfig["proxyHost"] = Properties.Settings.Default.proxyHost;
            _invalidSftpConfig["merchantId"] = Properties.Settings.Default.merchantId;
            _invalidSftpConfig["password"] = Properties.Settings.Default.password;
            _invalidSftpConfig["proxyPort"] = Properties.Settings.Default.proxyPort;
            _invalidSftpConfig["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            _invalidSftpConfig["sftpUsername"] = "badSftpUsername";
            _invalidSftpConfig["sftpPassword"] = "badSftpPassword";
            _invalidSftpConfig["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
            _invalidSftpConfig["requestDirectory"] = Properties.Settings.Default.requestDirectory;
            _invalidSftpConfig["responseDirectory"] = Properties.Settings.Default.responseDirectory;

        }

        [SetUp]
        public void SetUpBeforeTest()
        {
            _litle = new litleRequest();
        }

        [Test]
        public void SimpleBatch()
        {
            var litleBatchRequest = new batchRequest();

            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce
            };
            var card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4100000000000001",
                expDate = "1210"
            };
            authorization.card = card;
            authorization.id = "id";

            litleBatchRequest.addAuthorization(authorization);

            var authorization2 = new authorization();
            authorization2.reportGroup = "Planets";
            authorization2.orderId = "12345";
            authorization2.amount = 106;
            authorization2.orderSource = orderSourceType.ecommerce;
            var card2 = new cardType();
            card2.type = methodOfPaymentTypeEnum.VI;
            card2.number = "4242424242424242";
            card2.expDate = "1210";
            authorization2.card = card2;
            authorization2.id = "id";

            litleBatchRequest.addAuthorization(authorization2);
            
            var reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";
            reversal.id = "id";

            litleBatchRequest.addAuthReversal(reversal);

            var reversal2 = new authReversal();
            reversal2.litleTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes"; 
            reversal2.id = "id";

            litleBatchRequest.addAuthReversal(reversal2);
            
            var giftCardAuthReversal = new giftCardAuthReversal();
            giftCardAuthReversal.id = "id";
            giftCardAuthReversal.litleTxnId = 12345678000L;
            var giftCardCardTypeAuthReversal = new giftCardCardType();
            giftCardCardTypeAuthReversal.type = methodOfPaymentTypeEnum.GC;
            giftCardCardTypeAuthReversal.number = "4100000000000001";
            giftCardCardTypeAuthReversal.expDate = "1210";
            giftCardAuthReversal.card = giftCardCardTypeAuthReversal;
            giftCardAuthReversal.originalRefCode = "123456";
            giftCardAuthReversal.originalAmount = 1000;
            giftCardAuthReversal.originalTxnTime = DateTime.Now;
            giftCardAuthReversal.originalSystemTraceId = 123;
            giftCardAuthReversal.originalSequenceNumber = "123456";

            litleBatchRequest.addGiftCardAuthReversal(giftCardAuthReversal);

            var capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";
            capture.id = "id";

            litleBatchRequest.addCapture(capture);

            var capture2 = new capture();
            capture2.litleTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";
            capture2.id = "id";

            litleBatchRequest.addCapture(capture2);
            
            var giftCardCapture = new giftCardCapture();
            giftCardCapture.id = "id";
            giftCardCapture.litleTxnId = 12345678000L;
            giftCardCapture.captureAmount = 123456;
            var giftCardCardTypeCapture = new giftCardCardType();
            giftCardCardTypeCapture.type = methodOfPaymentTypeEnum.GC;
            giftCardCardTypeCapture.number = "4100000000000001";
            giftCardCardTypeCapture.expDate = "1210";
            giftCardCapture.card = giftCardCardTypeCapture;
            giftCardCapture.originalRefCode = "123456";
            giftCardCapture.originalAmount = 1000;
            giftCardCapture.originalTxnTime = DateTime.Now;

            litleBatchRequest.addGiftCardCapture(giftCardCapture);

            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            capturegivenauth.card = card;
            capturegivenauth.id = "id";

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);

            var capturegivenauth2 = new captureGivenAuth();
            capturegivenauth2.amount = 106;
            capturegivenauth2.orderId = "12344";
            var authInfo2 = new authInformation();
            authDate = new DateTime(2003, 10, 9);
            authInfo2.authDate = authDate;
            authInfo2.authCode = "543216";
            authInfo2.authAmount = 12345;
            capturegivenauth2.authInformation = authInfo;
            capturegivenauth2.orderSource = orderSourceType.ecommerce;
            capturegivenauth2.card = card2;
            capturegivenauth2.id = "id";

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;
            creditObj.id = "id";

            litleBatchRequest.addCredit(creditObj);

            var creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;
            creditObj2.id = "id";

            litleBatchRequest.addCredit(creditObj2);
            
            var giftCardCredit = new giftCardCredit();
            giftCardCredit.id = "id";
            giftCardCredit.litleTxnId = 12345678000L;
            giftCardCredit.creditAmount = 123456;
            var giftCardCardTypeCredit = new giftCardCardType();
            giftCardCardTypeCredit.type = methodOfPaymentTypeEnum.GC;
            giftCardCardTypeCredit.number = "4100000000000001";
            giftCardCardTypeCredit.expDate = "1210";
            giftCardCredit.card = giftCardCardTypeCredit;
            giftCardCredit.orderId = "123456";
            giftCardCredit.orderSource = orderSourceType.ecommerce;

            litleBatchRequest.addGiftCardCredit(giftCardCredit);

            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "1099999903";
            echeck.routingNum = "011201995";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            var billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;
            echeckcredit.id = "id";

            litleBatchRequest.addEcheckCredit(echeckcredit);

            var echeckcredit2 = new echeckCredit();
            echeckcredit2.amount = 12L;
            echeckcredit2.orderId = "12346";
            echeckcredit2.orderSource = orderSourceType.ecommerce;
            var echeck2 = new echeckType();
            echeck2.accType = echeckAccountTypeEnum.Checking;
            echeck2.accNum = "1099999903";
            echeck2.routingNum = "011201995";
            echeck2.checkNum = "123456";
            echeckcredit2.echeck = echeck2;
            var billToAddress2 = new contact();
            billToAddress2.name = "Mike";
            billToAddress2.city = "Lowell";
            billToAddress2.state = "MA";
            billToAddress2.email = "litle.com";
            echeckcredit2.billToAddress = billToAddress2;
            echeckcredit2.id = "id";

            litleBatchRequest.addEcheckCredit(echeckcredit2);

            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;
            echeckredeposit.id = "id";

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);

            var echeckredeposit2 = new echeckRedeposit();
            echeckredeposit2.litleTxnId = 123457;
            echeckredeposit2.echeck = echeck2;
            echeckredeposit2.id = "id";

            litleBatchRequest.addEcheckRedeposit(echeckredeposit2);

            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;
            echeckSaleObj.id = "id";

            litleBatchRequest.addEcheckSale(echeckSaleObj);

            var echeckSaleObj2 = new echeckSale();
            echeckSaleObj2.amount = 123456;
            echeckSaleObj2.orderId = "12346";
            echeckSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckSaleObj2.echeck = echeck2;
            echeckSaleObj2.billToAddress = billToAddress2;
            echeckSaleObj2.id = "id";

            litleBatchRequest.addEcheckSale(echeckSaleObj2);

            var echeckPreNoteSaleObj1 = new echeckPreNoteSale();
            echeckPreNoteSaleObj1.orderId = "12345";
            echeckPreNoteSaleObj1.orderSource = orderSourceType.ecommerce;
            echeckPreNoteSaleObj1.echeck = echeck;
            echeckPreNoteSaleObj1.billToAddress = billToAddress;
            echeckPreNoteSaleObj1.id = "id";

            litleBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleObj1);

            var echeckPreNoteSaleObj2 = new echeckPreNoteSale();
            echeckPreNoteSaleObj2.orderId = "12345";
            echeckPreNoteSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckPreNoteSaleObj2.echeck = echeck2;
            echeckPreNoteSaleObj2.billToAddress = billToAddress2;
            echeckPreNoteSaleObj2.id = "id";

            litleBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleObj2);

            var echeckPreNoteCreditObj1 = new echeckPreNoteCredit();
            echeckPreNoteCreditObj1.orderId = "12345";
            echeckPreNoteCreditObj1.orderSource = orderSourceType.ecommerce;
            echeckPreNoteCreditObj1.echeck = echeck;
            echeckPreNoteCreditObj1.billToAddress = billToAddress;
            echeckPreNoteCreditObj1.id = "id";

            litleBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditObj1);

            var echeckPreNoteCreditObj2 = new echeckPreNoteCredit();
            echeckPreNoteCreditObj2.orderId = "12345";
            echeckPreNoteCreditObj2.orderSource = orderSourceType.ecommerce;
            echeckPreNoteCreditObj2.echeck = echeck2;
            echeckPreNoteCreditObj2.billToAddress = billToAddress2;
            echeckPreNoteCreditObj2.id = "id";

            var echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;
            echeckVerificationObject.id = "id";

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);

            var echeckVerificationObject2 = new echeckVerification();
            echeckVerificationObject2.amount = 123456;
            echeckVerificationObject2.orderId = "12346";
            echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject2.echeck = echeck2;
            echeckVerificationObject2.billToAddress = billToAddress2; 
            echeckVerificationObject2.id = "id";

            litleBatchRequest.addEcheckVerification(echeckVerificationObject2);

            var forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;
            forcecapture.id = "id";

            litleBatchRequest.addForceCapture(forcecapture);

            var forcecapture2 = new forceCapture();
            forcecapture2.amount = 106;
            forcecapture2.orderId = "12345";
            forcecapture2.orderSource = orderSourceType.ecommerce;
            forcecapture2.card = card2;
            forcecapture2.id = "id";

            litleBatchRequest.addForceCapture(forcecapture2);

            var saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;
            saleObj.id = "id";

            litleBatchRequest.addSale(saleObj);

            var saleObj2 = new sale();
            saleObj2.amount = 106;
            saleObj2.litleTxnId = 123456;
            saleObj2.orderId = "12345";
            saleObj2.orderSource = orderSourceType.ecommerce;
            saleObj2.card = card2;
            saleObj2.id = "id";

            litleBatchRequest.addSale(saleObj2);

            var registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";
            registerTokenRequest.id = "id";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            var registerTokenRequest2 = new registerTokenRequestType();
            registerTokenRequest2.orderId = "12345";
            registerTokenRequest2.accountNumber = "1233456789103801";
            registerTokenRequest2.reportGroup = "Planets";
            registerTokenRequest2.id = "id";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            var updateCardValidationNumOnToken = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken.orderId = "12344";
            updateCardValidationNumOnToken.cardValidationNum = "123";
            updateCardValidationNumOnToken.litleToken = "4100000000000001";
            updateCardValidationNumOnToken.id = "id";

            litleBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);

            var updateCardValidationNumOnToken2 = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken2.orderId = "12345";
            updateCardValidationNumOnToken2.cardValidationNum = "123";
            updateCardValidationNumOnToken2.litleToken = "4242424242424242";
            updateCardValidationNumOnToken2.id = "id";

            litleBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken2);
            _litle.addBatch(litleBatchRequest);

            var batchName = _litle.sendToLitle();

            _litle.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var litleResponse = _litle.receiveFromLitle(batchName);

            Assert.NotNull(litleResponse);
            Assert.AreEqual("0", litleResponse.response);
            Assert.AreEqual("Valid Format", litleResponse.message);

            var litleBatchResponse = litleResponse.nextBatchResponse();
            while (litleBatchResponse != null)
            {
                var authorizationResponse = litleBatchResponse.nextAuthorizationResponse();
                while (authorizationResponse != null)
                {
                    Assert.AreEqual("000", authorizationResponse.response);

                    authorizationResponse = litleBatchResponse.nextAuthorizationResponse();
                }

                var authReversalResponse = litleBatchResponse.nextAuthReversalResponse();
                while (authReversalResponse != null)
                {
                    Assert.AreEqual("000", authReversalResponse.response);

                    authReversalResponse = litleBatchResponse.nextAuthReversalResponse();
                }

                var giftCardAuthReversalResponse = litleBatchResponse.nextGiftCardAuthReversalResponse();
                while (giftCardAuthReversalResponse != null)
                {
                    Assert.NotNull(giftCardAuthReversalResponse.response);

                    giftCardAuthReversalResponse = litleBatchResponse.nextGiftCardAuthReversalResponse();
                }

                var captureResponse = litleBatchResponse.nextCaptureResponse();
                while (captureResponse != null)
                {
                    Assert.AreEqual("000", captureResponse.response);

                    captureResponse = litleBatchResponse.nextCaptureResponse();
                }

                var giftCardCaptureResponse = litleBatchResponse.nextGiftCardCaptureResponse();
                while (giftCardCaptureResponse != null)
                {
                    Assert.NotNull(giftCardCaptureResponse.response);

                    giftCardCaptureResponse = litleBatchResponse.nextGiftCardCaptureResponse();
                }


                var captureGivenAuthResponse = litleBatchResponse.nextCaptureGivenAuthResponse();
                while (captureGivenAuthResponse != null)
                {
                    Assert.AreEqual("000", captureGivenAuthResponse.response);

                    captureGivenAuthResponse = litleBatchResponse.nextCaptureGivenAuthResponse();
                }

                var creditResponse = litleBatchResponse.nextCreditResponse();
                while (creditResponse != null)
                {
                    Assert.AreEqual("000", creditResponse.response);

                    creditResponse = litleBatchResponse.nextCreditResponse();
                }

                var giftCardCreditResponse = litleBatchResponse.nextGiftCardCreditResponse();
                while (giftCardCreditResponse != null)
                {
                    Assert.NotNull(giftCardCreditResponse.response);

                    giftCardCreditResponse = litleBatchResponse.nextGiftCardCreditResponse();
                }

                var echeckCreditResponse = litleBatchResponse.nextEcheckCreditResponse();
                while (echeckCreditResponse != null)
                {
                    Assert.AreEqual("000", echeckCreditResponse.response);

                    echeckCreditResponse = litleBatchResponse.nextEcheckCreditResponse();
                }

                var echeckRedepositResponse = litleBatchResponse.nextEcheckRedepositResponse();
                while (echeckRedepositResponse != null)
                {
                    Assert.AreEqual("000", echeckRedepositResponse.response);

                    echeckRedepositResponse = litleBatchResponse.nextEcheckRedepositResponse();
                }

                var echeckSalesResponse = litleBatchResponse.nextEcheckSalesResponse();
                while (echeckSalesResponse != null)
                {
                    Assert.AreEqual("000", echeckSalesResponse.response);

                    echeckSalesResponse = litleBatchResponse.nextEcheckSalesResponse();
                }

                var echeckPreNoteSaleResponse = litleBatchResponse.nextEcheckPreNoteSaleResponse();
                while (echeckPreNoteSaleResponse != null)
                {
                    Assert.AreEqual("000", echeckPreNoteSaleResponse.response);

                    echeckPreNoteSaleResponse = litleBatchResponse.nextEcheckPreNoteSaleResponse();
                }

                var echeckPreNoteCreditResponse = litleBatchResponse.nextEcheckPreNoteCreditResponse();
                while (echeckPreNoteCreditResponse != null)
                {
                    Assert.AreEqual("000", echeckPreNoteCreditResponse.response);

                    echeckPreNoteCreditResponse = litleBatchResponse.nextEcheckPreNoteCreditResponse();
                }

                var echeckVerificationResponse = litleBatchResponse.nextEcheckVerificationResponse();
                while (echeckVerificationResponse != null)
                {
                    Assert.AreEqual("957", echeckVerificationResponse.response);

                    echeckVerificationResponse = litleBatchResponse.nextEcheckVerificationResponse();
                }

                var forceCaptureResponse = litleBatchResponse.nextForceCaptureResponse();
                while (forceCaptureResponse != null)
                {
                    Assert.AreEqual("000", forceCaptureResponse.response);

                    forceCaptureResponse = litleBatchResponse.nextForceCaptureResponse();
                }

                var registerTokenResponse = litleBatchResponse.nextRegisterTokenResponse();
                while (registerTokenResponse != null)
                {
                    Assert.AreEqual("820", registerTokenResponse.response);

                    registerTokenResponse = litleBatchResponse.nextRegisterTokenResponse();
                }

                var saleResponse = litleBatchResponse.nextSaleResponse();
                while (saleResponse != null)
                {
                    Assert.AreEqual("000", saleResponse.response);

                    saleResponse = litleBatchResponse.nextSaleResponse();
                }

                var updateCardValidationNumOnTokenResponse = litleBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
                while (updateCardValidationNumOnTokenResponse != null)
                {
                    Assert.AreEqual("823", updateCardValidationNumOnTokenResponse.response);

                    updateCardValidationNumOnTokenResponse = litleBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
                }

                litleBatchResponse = litleResponse.nextBatchResponse();
            }
        }

        [Test]
        public void AccountUpdateBatch()
        {
            var litleBatchRequest = new batchRequest();

            var accountUpdate1 = new accountUpdate();
            accountUpdate1.orderId = "1111";
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            accountUpdate1.card = card;
            accountUpdate1.id = "id";

            litleBatchRequest.addAccountUpdate(accountUpdate1);

            var accountUpdate2 = new accountUpdate();
            accountUpdate2.orderId = "1112";
            accountUpdate2.card = card;
            accountUpdate2.id = "id";

            litleBatchRequest.addAccountUpdate(accountUpdate2);

            _litle.addBatch(litleBatchRequest);
            var batchName = _litle.sendToLitle();

            _litle.blockAndWaitForResponse(batchName, estimatedResponseTime(0, 1 * 2));

            var litleResponse = _litle.receiveFromLitle(batchName);

            Assert.NotNull(litleResponse);
            Assert.AreEqual("0", litleResponse.response);
            Assert.AreEqual("Valid Format", litleResponse.message);

            var litleBatchResponse = litleResponse.nextBatchResponse();
            while (litleBatchResponse != null)
            {
                var accountUpdateResponse = litleBatchResponse.nextAccountUpdateResponse();
                while (accountUpdateResponse != null)
                {
                    Assert.AreEqual("301", accountUpdateResponse.response);

                    accountUpdateResponse = litleBatchResponse.nextAccountUpdateResponse();
                }
                litleBatchResponse = litleResponse.nextBatchResponse();
            }
        }

        [Test]
        public void RFRBatch()
        {
            var litleBatchRequest = new batchRequest();
            litleBatchRequest.id = "1234567A";

            var accountUpdate1 = new accountUpdate();
            accountUpdate1.orderId = "1111";
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4242424242424242";
            card.expDate = "1210";
            accountUpdate1.card = card;
            accountUpdate1.id = "id";

            litleBatchRequest.addAccountUpdate(accountUpdate1);

            var accountUpdate2 = new accountUpdate();
            accountUpdate2.orderId = "1112";
            accountUpdate2.card = card;
            accountUpdate2.id = "id";

            litleBatchRequest.addAccountUpdate(accountUpdate2);
            _litle.addBatch(litleBatchRequest);

            var batchName = _litle.sendToLitle();
            _litle.blockAndWaitForResponse(batchName, estimatedResponseTime(0, 1 * 2));
            var litleResponse = _litle.receiveFromLitle(batchName);

            Assert.NotNull(litleResponse);

            var litleBatchResponse = litleResponse.nextBatchResponse();
            Assert.NotNull(litleBatchResponse);
            while (litleBatchResponse != null)
            {
                var accountUpdateResponse = litleBatchResponse.nextAccountUpdateResponse();
                Assert.NotNull(accountUpdateResponse);
                while (accountUpdateResponse != null)
                {
                    Assert.AreEqual("000", accountUpdateResponse.response);

                    accountUpdateResponse = litleBatchResponse.nextAccountUpdateResponse();
                }
                litleBatchResponse = litleResponse.nextBatchResponse();
            }

            var litleRfr = new litleRequest();
            var rfrRequest = new RFRRequest();
            var accountUpdateFileRequestData = new accountUpdateFileRequestData();
            accountUpdateFileRequestData.merchantId = Properties.Settings.Default.merchantId;
            accountUpdateFileRequestData.postDay = DateTime.Now;
            rfrRequest.accountUpdateFileRequestData = accountUpdateFileRequestData;

            litleRfr.addRFRRequest(rfrRequest);

            var rfrBatchName = litleRfr.sendToLitle();
            
            try
            {
                _litle.blockAndWaitForResponse(rfrBatchName, 120000);
                var litleRfrResponse = _litle.receiveFromLitle(rfrBatchName);
                Assert.NotNull(litleRfrResponse);
                var rfrResponse = litleRfrResponse.nextRFRResponse();
                Assert.NotNull(rfrResponse);
                while (rfrResponse != null)
                {
                    Assert.AreEqual("1", rfrResponse.response);
                    Assert.AreEqual("The account update file is not ready yet.  Please try again later.", rfrResponse.message);
                    rfrResponse = litleResponse.nextRFRResponse();
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        [Test]
        public void NullBatchData()
        {
            var litleBatchRequest = new batchRequest();

            var authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            authorization.card = card;

            litleBatchRequest.addAuthorization(authorization);
            try
            {
                litleBatchRequest.addAuthorization(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal);
            try
            {
                litleBatchRequest.addAuthReversal(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            litleBatchRequest.addCapture(capture);
            try
            {
                litleBatchRequest.addCapture(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            capturegivenauth.card = card;

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);
            try
            {
                litleBatchRequest.addCaptureGivenAuth(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;

            litleBatchRequest.addCredit(creditObj);
            try
            {
                litleBatchRequest.addCredit(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "011201995";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            var billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;

            litleBatchRequest.addEcheckCredit(echeckcredit);
            try
            {
                litleBatchRequest.addEcheckCredit(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            try
            {
                litleBatchRequest.addEcheckRedeposit(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;

            litleBatchRequest.addEcheckSale(echeckSaleObj);
            try
            {
                litleBatchRequest.addEcheckSale(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);
            try
            {
                litleBatchRequest.addEcheckVerification(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;

            litleBatchRequest.addForceCapture(forcecapture);
            try
            {
                litleBatchRequest.addForceCapture(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;

            litleBatchRequest.addSale(saleObj);
            try
            {
                litleBatchRequest.addSale(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);
            try
            {
                litleBatchRequest.addRegisterTokenRequest(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            try
            {
                _litle.addBatch(litleBatchRequest);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void InvalidCredientialsBatch()
        {
            var litleIC = new litleRequest(_invalidConfig);

            var litleBatchRequest = new batchRequest();

            var authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            authorization.card = card; //This needs to compile      

            litleBatchRequest.addAuthorization(authorization);

            var authorization2 = new authorization();
            authorization2.reportGroup = "Planets";
            authorization2.orderId = "12345";
            authorization2.amount = 106;
            authorization2.orderSource = orderSourceType.ecommerce;
            var card2 = new cardType();
            card2.type = methodOfPaymentTypeEnum.VI;
            card2.number = "4242424242424242";
            card2.expDate = "1210";
            authorization2.card = card2; //This needs to compile

            litleBatchRequest.addAuthorization(authorization2);

            var reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal);

            var reversal2 = new authReversal();
            reversal2.litleTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal2);

            var capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            litleBatchRequest.addCapture(capture);

            var capture2 = new capture();
            capture2.litleTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";

            litleBatchRequest.addCapture(capture2);

            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            capturegivenauth.card = card;

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);

            var capturegivenauth2 = new captureGivenAuth();
            capturegivenauth2.amount = 106;
            capturegivenauth2.orderId = "12344";
            var authInfo2 = new authInformation();
            authDate = new DateTime(2003, 10, 9);
            authInfo2.authDate = authDate;
            authInfo2.authCode = "543216";
            authInfo2.authAmount = 12345;
            capturegivenauth2.authInformation = authInfo;
            capturegivenauth2.orderSource = orderSourceType.ecommerce;
            capturegivenauth2.card = card2;

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;

            litleBatchRequest.addCredit(creditObj);

            var creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;

            litleBatchRequest.addCredit(creditObj2);

            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "1099999903";
            echeck.routingNum = "011201995";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            var billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;

            litleBatchRequest.addEcheckCredit(echeckcredit);

            var echeckcredit2 = new echeckCredit();
            echeckcredit2.amount = 12L;
            echeckcredit2.orderId = "12346";
            echeckcredit2.orderSource = orderSourceType.ecommerce;
            var echeck2 = new echeckType();
            echeck2.accType = echeckAccountTypeEnum.Checking;
            echeck2.accNum = "1099999903";
            echeck2.routingNum = "011201995";
            echeck2.checkNum = "123456";
            echeckcredit2.echeck = echeck2;
            var billToAddress2 = new contact();
            billToAddress2.name = "Mike";
            billToAddress2.city = "Lowell";
            billToAddress2.state = "MA";
            billToAddress2.email = "litle.com";
            echeckcredit2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckCredit(echeckcredit2);

            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);

            var echeckredeposit2 = new echeckRedeposit();
            echeckredeposit2.litleTxnId = 123457;
            echeckredeposit2.echeck = echeck2;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit2);

            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;

            litleBatchRequest.addEcheckSale(echeckSaleObj);

            var echeckSaleObj2 = new echeckSale();
            echeckSaleObj2.amount = 123456;
            echeckSaleObj2.orderId = "12346";
            echeckSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckSaleObj2.echeck = echeck2;
            echeckSaleObj2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckSale(echeckSaleObj2);

            var echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);

            var echeckVerificationObject2 = new echeckVerification();
            echeckVerificationObject2.amount = 123456;
            echeckVerificationObject2.orderId = "12346";
            echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject2.echeck = echeck2;
            echeckVerificationObject2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject2);

            var forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;

            litleBatchRequest.addForceCapture(forcecapture);

            var forcecapture2 = new forceCapture();
            forcecapture2.amount = 106;
            forcecapture2.orderId = "12345";
            forcecapture2.orderSource = orderSourceType.ecommerce;
            forcecapture2.card = card2;

            litleBatchRequest.addForceCapture(forcecapture2);

            var saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;

            litleBatchRequest.addSale(saleObj);

            var saleObj2 = new sale();
            saleObj2.amount = 106;
            saleObj2.litleTxnId = 123456;
            saleObj2.orderId = "12345";
            saleObj2.orderSource = orderSourceType.ecommerce;
            saleObj2.card = card2;

            litleBatchRequest.addSale(saleObj2);

            var registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            var registerTokenRequest2 = new registerTokenRequestType();
            registerTokenRequest2.orderId = "12345";
            registerTokenRequest2.accountNumber = "1233456789103801";
            registerTokenRequest2.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            litleIC.addBatch(litleBatchRequest);

            var batchName = litleIC.sendToLitle();

            litleIC.blockAndWaitForResponse(batchName, 60*1000*5);

            try
            {
                var litleResponse = litleIC.receiveFromLitle(batchName);
                Assert.Fail("Fail to throw a connection exception");
            }
            catch (LitleOnlineException e)
            {
                Assert.AreEqual("Error occured while attempting to retrieve and save the file from SFTP", e.Message);
            }
        }

        [Test]
        public void InvalidSftpCredientialsBatch()
        {
            var litleIsc = new litleRequest(_invalidSftpConfig);

            var litleBatchRequest = new batchRequest();

            var authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            authorization.card = card; //This needs to compile      

            litleBatchRequest.addAuthorization(authorization);

            var authorization2 = new authorization();
            authorization2.reportGroup = "Planets";
            authorization2.orderId = "12345";
            authorization2.amount = 106;
            authorization2.orderSource = orderSourceType.ecommerce;
            var card2 = new cardType();
            card2.type = methodOfPaymentTypeEnum.VI;
            card2.number = "4242424242424242";
            card2.expDate = "1210";
            authorization2.card = card2; //This needs to compile

            litleBatchRequest.addAuthorization(authorization2);

            var reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal);

            var reversal2 = new authReversal();
            reversal2.litleTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal2);

            var capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            litleBatchRequest.addCapture(capture);

            var capture2 = new capture();
            capture2.litleTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";

            litleBatchRequest.addCapture(capture2);

            var capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            var authInfo = new authInformation();
            var authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            capturegivenauth.card = card;

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);

            var capturegivenauth2 = new captureGivenAuth();
            capturegivenauth2.amount = 106;
            capturegivenauth2.orderId = "12344";
            var authInfo2 = new authInformation();
            authDate = new DateTime(2003, 10, 9);
            authInfo2.authDate = authDate;
            authInfo2.authCode = "543216";
            authInfo2.authAmount = 12345;
            capturegivenauth2.authInformation = authInfo;
            capturegivenauth2.orderSource = orderSourceType.ecommerce;
            capturegivenauth2.card = card2;

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            var creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;

            litleBatchRequest.addCredit(creditObj);

            var creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;

            litleBatchRequest.addCredit(creditObj2);

            var echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            var echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "1099999903";
            echeck.routingNum = "011201995";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            var billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;

            litleBatchRequest.addEcheckCredit(echeckcredit);

            var echeckcredit2 = new echeckCredit();
            echeckcredit2.amount = 12L;
            echeckcredit2.orderId = "12346";
            echeckcredit2.orderSource = orderSourceType.ecommerce;
            var echeck2 = new echeckType();
            echeck2.accType = echeckAccountTypeEnum.Checking;
            echeck2.accNum = "1099999903";
            echeck2.routingNum = "011201995";
            echeck2.checkNum = "123456";
            echeckcredit2.echeck = echeck2;
            var billToAddress2 = new contact();
            billToAddress2.name = "Mike";
            billToAddress2.city = "Lowell";
            billToAddress2.state = "MA";
            billToAddress2.email = "litle.com";
            echeckcredit2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckCredit(echeckcredit2);

            var echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);

            var echeckredeposit2 = new echeckRedeposit();
            echeckredeposit2.litleTxnId = 123457;
            echeckredeposit2.echeck = echeck2;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit2);

            var echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;

            litleBatchRequest.addEcheckSale(echeckSaleObj);

            var echeckSaleObj2 = new echeckSale();
            echeckSaleObj2.amount = 123456;
            echeckSaleObj2.orderId = "12346";
            echeckSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckSaleObj2.echeck = echeck2;
            echeckSaleObj2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckSale(echeckSaleObj2);

            var echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);

            var echeckVerificationObject2 = new echeckVerification();
            echeckVerificationObject2.amount = 123456;
            echeckVerificationObject2.orderId = "12346";
            echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject2.echeck = echeck2;
            echeckVerificationObject2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject2);

            var forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;

            litleBatchRequest.addForceCapture(forcecapture);

            var forcecapture2 = new forceCapture();
            forcecapture2.amount = 106;
            forcecapture2.orderId = "12345";
            forcecapture2.orderSource = orderSourceType.ecommerce;
            forcecapture2.card = card2;

            litleBatchRequest.addForceCapture(forcecapture2);

            var saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;

            litleBatchRequest.addSale(saleObj);

            var saleObj2 = new sale();
            saleObj2.amount = 106;
            saleObj2.litleTxnId = 123456;
            saleObj2.orderId = "12345";
            saleObj2.orderSource = orderSourceType.ecommerce;
            saleObj2.card = card2;

            litleBatchRequest.addSale(saleObj2);

            var registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            var registerTokenRequest2 = new registerTokenRequestType();
            registerTokenRequest2.orderId = "12345";
            registerTokenRequest2.accountNumber = "1233456789103801";
            registerTokenRequest2.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            litleIsc.addBatch(litleBatchRequest);

            try
            {
                var batchName = litleIsc.sendToLitle();
                Assert.Fail("Fail to throw a connection exception");
            }
            catch (LitleOnlineException e)
            {
                Assert.AreSame("Error occured while attempting to establish an SFTP connection", e.Message);
            }
        }

        [Test]
        public void SimpleBatchWithSpecialCharacters()
        {
            var litleBatchRequest = new batchRequest();

            var authorization = new authorization();
            authorization.reportGroup = "<ReportGroup>";
            authorization.orderId = "12344&'\"";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            var card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            authorization.card = card;
            authorization.id = "id";

            litleBatchRequest.addAuthorization(authorization);

            _litle.addBatch(litleBatchRequest);

            var batchName = _litle.sendToLitle();

            _litle.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            var litleResponse = _litle.receiveFromLitle(batchName);

            Assert.NotNull(litleResponse);
            Assert.AreEqual("0", litleResponse.response);
            Assert.AreEqual("Valid Format", litleResponse.message);

            var litleBatchResponse = litleResponse.nextBatchResponse();
            while (litleBatchResponse != null)
            {
                var authorizationResponse = litleBatchResponse.nextAuthorizationResponse();
                while (authorizationResponse != null)
                {
                    Assert.AreEqual("000", authorizationResponse.response);

                    authorizationResponse = litleBatchResponse.nextAuthorizationResponse();
                }

                litleBatchResponse = litleResponse.nextBatchResponse();
            }
        }

        private int estimatedResponseTime(int numAuthsAndSales, int numRest)
        {
            return (int)(5 * 60 * 1000 + 2.5 * 1000 + numAuthsAndSales * (1 / 5) * 1000 + numRest * (1 / 50) * 1000) * 5;
        }
    }
}

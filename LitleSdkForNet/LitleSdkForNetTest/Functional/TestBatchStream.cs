using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using System.IO;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestBatchStream
    {
        private litleRequest litle;
        private Dictionary<String, String> invalidConfig;
        private Dictionary<String, String> invalidSftpConfig;

        [TestFixtureSetUp]
        public void setUp()
        {
            invalidConfig = new Dictionary<String, String>();
            invalidConfig["url"] = Properties.Settings.Default.url;
            invalidConfig["reportGroup"] = Properties.Settings.Default.reportGroup;
            invalidConfig["username"] = "badUsername";
            invalidConfig["printxml"] = Properties.Settings.Default.printxml;
            invalidConfig["timeout"] = Properties.Settings.Default.timeout;
            invalidConfig["proxyHost"] = Properties.Settings.Default.proxyHost;
            invalidConfig["merchantId"] = Properties.Settings.Default.merchantId;
            invalidConfig["password"] = "badPassword";
            invalidConfig["proxyPort"] = Properties.Settings.Default.proxyPort;
            invalidConfig["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            invalidConfig["sftpUsername"] = Properties.Settings.Default.sftpUrl;
            invalidConfig["sftpPassword"] = Properties.Settings.Default.sftpPassword;
            invalidConfig["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
            

            invalidSftpConfig = new Dictionary<String, String>();
            invalidSftpConfig["url"] = Properties.Settings.Default.url;
            invalidSftpConfig["reportGroup"] = Properties.Settings.Default.reportGroup;
            invalidSftpConfig["username"] = Properties.Settings.Default.username;
            invalidSftpConfig["printxml"] = Properties.Settings.Default.printxml;
            invalidSftpConfig["timeout"] = Properties.Settings.Default.timeout;
            invalidSftpConfig["proxyHost"] = Properties.Settings.Default.proxyHost;
            invalidSftpConfig["merchantId"] = Properties.Settings.Default.merchantId;
            invalidSftpConfig["password"] = Properties.Settings.Default.password;
            invalidSftpConfig["proxyPort"] = Properties.Settings.Default.proxyPort;
            invalidSftpConfig["sftpUrl"] = Properties.Settings.Default.sftpUrl;
            invalidSftpConfig["sftpUsername"] = "badSftpUsername";
            invalidSftpConfig["sftpPassword"] = "badSftpPassword";
            invalidSftpConfig["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
            
        }

        [SetUp]
        public void setUpBeforeTest()
        {
            litle = new litleRequest();
        }

        /*[Test]
        public void SimpleBatch()
        {
            batchRequest litleBatchRequest = new batchRequest();

            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            authorization.card = card;
            authorization.id = "id";

            litleBatchRequest.addAuthorization(authorization);

            authorization authorization2 = new authorization();
            authorization2.reportGroup = "Planets";
            authorization2.orderId = "12345";
            authorization2.amount = 106;
            authorization2.orderSource = orderSourceType.ecommerce;
            cardType card2 = new cardType();
            card2.type = methodOfPaymentTypeEnum.VI;
            card2.number = "4242424242424242";
            card2.expDate = "1210";
            authorization2.card = card2;
            authorization2.id = "id";

            litleBatchRequest.addAuthorization(authorization2);

            authReversal reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";
            reversal.id = "id";

            litleBatchRequest.addAuthReversal(reversal);

            authReversal reversal2 = new authReversal();
            reversal2.litleTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes";
            reversal2.id = "id";

            litleBatchRequest.addAuthReversal(reversal2);

            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";
            capture.id = "id";

            litleBatchRequest.addCapture(capture);

            capture capture2 = new capture();
            capture2.litleTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";
            capture2.id = "id";

            litleBatchRequest.addCapture(capture2);

            captureGivenAuth capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            authInformation authInfo = new authInformation();
            DateTime authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            capturegivenauth.card = card;
            capturegivenauth.id = "id";

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);

            captureGivenAuth capturegivenauth2 = new captureGivenAuth();
            capturegivenauth2.amount = 106;
            capturegivenauth2.orderId = "12344";
            authInformation authInfo2 = new authInformation();
            authDate = new DateTime(2003, 10, 9);
            authInfo2.authDate = authDate;
            authInfo2.authCode = "543216";
            authInfo2.authAmount = 12345;
            capturegivenauth2.authInformation = authInfo;
            capturegivenauth2.orderSource = orderSourceType.ecommerce;
            capturegivenauth2.card = card2;
            capturegivenauth2.id = "id";

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            credit creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;
            creditObj.id = "id";

            litleBatchRequest.addCredit(creditObj);

            credit creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;
            creditObj2.id = "id";

            litleBatchRequest.addCredit(creditObj2);

            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "1099999903";
            echeck.routingNum = "011201995";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            contact billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;
            echeckcredit.id = "id";

            litleBatchRequest.addEcheckCredit(echeckcredit);

            echeckCredit echeckcredit2 = new echeckCredit();
            echeckcredit2.amount = 12L;
            echeckcredit2.orderId = "12346";
            echeckcredit2.orderSource = orderSourceType.ecommerce;
            echeckType echeck2 = new echeckType();
            echeck2.accType = echeckAccountTypeEnum.Checking;
            echeck2.accNum = "1099999903";
            echeck2.routingNum = "011201995";
            echeck2.checkNum = "123456";
            echeckcredit2.echeck = echeck2;
            contact billToAddress2 = new contact();
            billToAddress2.name = "Mike";
            billToAddress2.city = "Lowell";
            billToAddress2.state = "MA";
            billToAddress2.email = "litle.com";
            echeckcredit2.billToAddress = billToAddress2;
            echeckcredit2.id = "id";

            litleBatchRequest.addEcheckCredit(echeckcredit2);

            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;
            echeckredeposit.id = "id";

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);

            echeckRedeposit echeckredeposit2 = new echeckRedeposit();
            echeckredeposit2.litleTxnId = 123457;
            echeckredeposit2.echeck = echeck2;
            echeckredeposit2.id = "id";

            litleBatchRequest.addEcheckRedeposit(echeckredeposit2);

            echeckSale echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;
            echeckSaleObj.id = "id";

            litleBatchRequest.addEcheckSale(echeckSaleObj);

            echeckSale echeckSaleObj2 = new echeckSale();
            echeckSaleObj2.amount = 123456;
            echeckSaleObj2.orderId = "12346";
            echeckSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckSaleObj2.echeck = echeck2;
            echeckSaleObj2.billToAddress = billToAddress2;
            echeckSaleObj2.id = "id";

            litleBatchRequest.addEcheckSale(echeckSaleObj2);

            echeckPreNoteSale echeckPreNoteSaleObj1 = new echeckPreNoteSale();
            echeckPreNoteSaleObj1.orderId = "12345";
            echeckPreNoteSaleObj1.orderSource = orderSourceType.ecommerce;
            echeckPreNoteSaleObj1.echeck = echeck;
            echeckPreNoteSaleObj1.billToAddress = billToAddress;
            echeckPreNoteSaleObj1.id = "id";

            litleBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleObj1);

            echeckPreNoteSale echeckPreNoteSaleObj2 = new echeckPreNoteSale();
            echeckPreNoteSaleObj2.orderId = "12345";
            echeckPreNoteSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckPreNoteSaleObj2.echeck = echeck2;
            echeckPreNoteSaleObj2.billToAddress = billToAddress2;
            echeckPreNoteSaleObj2.id = "id";

            litleBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleObj2);

            echeckPreNoteCredit echeckPreNoteCreditObj1 = new echeckPreNoteCredit();
            echeckPreNoteCreditObj1.orderId = "12345";
            echeckPreNoteCreditObj1.orderSource = orderSourceType.ecommerce;
            echeckPreNoteCreditObj1.echeck = echeck;
            echeckPreNoteCreditObj1.billToAddress = billToAddress;
            echeckPreNoteCreditObj1.id = "id";

            litleBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditObj1);

            echeckPreNoteCredit echeckPreNoteCreditObj2 = new echeckPreNoteCredit();
            echeckPreNoteCreditObj2.orderId = "12345";
            echeckPreNoteCreditObj2.orderSource = orderSourceType.ecommerce;
            echeckPreNoteCreditObj2.echeck = echeck2;
            echeckPreNoteCreditObj2.billToAddress = billToAddress2;
            echeckPreNoteCreditObj2.id = "id";

            litleBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditObj2);

            echeckVerification echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;
            echeckVerificationObject.id = "id";

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);

            echeckVerification echeckVerificationObject2 = new echeckVerification();
            echeckVerificationObject2.amount = 123456;
            echeckVerificationObject2.orderId = "12346";
            echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject2.echeck = echeck2;
            echeckVerificationObject2.billToAddress = billToAddress2;
            echeckVerificationObject2.id = "id";

            litleBatchRequest.addEcheckVerification(echeckVerificationObject2);

            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;
            forcecapture.id = "id";

            litleBatchRequest.addForceCapture(forcecapture);

            forceCapture forcecapture2 = new forceCapture();
            forcecapture2.amount = 106;
            forcecapture2.orderId = "12345";
            forcecapture2.orderSource = orderSourceType.ecommerce;
            forcecapture2.card = card2;
            forcecapture2.id = "id";

            litleBatchRequest.addForceCapture(forcecapture2);

            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;
            saleObj.id = "id";

            litleBatchRequest.addSale(saleObj);

            sale saleObj2 = new sale();
            saleObj2.amount = 106;
            saleObj2.litleTxnId = 123456;
            saleObj2.orderId = "12345";
            saleObj2.orderSource = orderSourceType.ecommerce;
            saleObj2.card = card2;
            saleObj2.id = "id";

            litleBatchRequest.addSale(saleObj2);

            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";
            registerTokenRequest.id = "id";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            registerTokenRequestType registerTokenRequest2 = new registerTokenRequestType();
            registerTokenRequest2.orderId = "12345";
            registerTokenRequest2.accountNumber = "1233456789103801";
            registerTokenRequest2.reportGroup = "Planets";
            registerTokenRequest2.id = "id";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            updateCardValidationNumOnToken updateCardValidationNumOnToken = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken.orderId = "12344";
            updateCardValidationNumOnToken.cardValidationNum = "123";
            updateCardValidationNumOnToken.litleToken = "4100000000000001";
            updateCardValidationNumOnToken.id = "id";

            litleBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);

            updateCardValidationNumOnToken updateCardValidationNumOnToken2 = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken2.orderId = "12345";
            updateCardValidationNumOnToken2.cardValidationNum = "123";
            updateCardValidationNumOnToken2.litleToken = "4242424242424242";
            updateCardValidationNumOnToken2.id = "id";

            litleBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken2);
            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitleWithStream();

            Assert.NotNull(litleResponse);
            Assert.AreEqual("0", litleResponse.response);
            Assert.AreEqual("Valid Format", litleResponse.message);

            batchResponse litleBatchResponse = litleResponse.nextBatchResponse();
            while (litleBatchResponse != null)
            {
                authorizationResponse authorizationResponse = litleBatchResponse.nextAuthorizationResponse();
                while (authorizationResponse != null)
                {
                    Assert.AreEqual("000", authorizationResponse.response);

                    authorizationResponse = litleBatchResponse.nextAuthorizationResponse();
                }

                authReversalResponse authReversalResponse = litleBatchResponse.nextAuthReversalResponse();
                while (authReversalResponse != null)
                {
                    Assert.AreEqual("001", authReversalResponse.response);

                    authReversalResponse = litleBatchResponse.nextAuthReversalResponse();
                }

                captureResponse captureResponse = litleBatchResponse.nextCaptureResponse();
                while (captureResponse != null)
                {
                    Assert.AreEqual("001", captureResponse.response);

                    captureResponse = litleBatchResponse.nextCaptureResponse();
                }

                captureGivenAuthResponse captureGivenAuthResponse = litleBatchResponse.nextCaptureGivenAuthResponse();
                while (captureGivenAuthResponse != null)
                {
                    Assert.AreEqual("001", captureGivenAuthResponse.response);

                    captureGivenAuthResponse = litleBatchResponse.nextCaptureGivenAuthResponse();
                }

                creditResponse creditResponse = litleBatchResponse.nextCreditResponse();
                while (creditResponse != null)
                {
                    Assert.AreEqual("001", creditResponse.response);

                    creditResponse = litleBatchResponse.nextCreditResponse();
                }

                echeckCreditResponse echeckCreditResponse = litleBatchResponse.nextEcheckCreditResponse();
                while (echeckCreditResponse != null)
                {
                    Assert.AreEqual("001", echeckCreditResponse.response);

                    echeckCreditResponse = litleBatchResponse.nextEcheckCreditResponse();
                }

                echeckRedepositResponse echeckRedepositResponse = litleBatchResponse.nextEcheckRedepositResponse();
                while (echeckRedepositResponse != null)
                {
                    Assert.AreEqual("001", echeckRedepositResponse.response);

                    echeckRedepositResponse = litleBatchResponse.nextEcheckRedepositResponse();
                }

                echeckSalesResponse echeckSalesResponse = litleBatchResponse.nextEcheckSalesResponse();
                while (echeckSalesResponse != null)
                {
                    Assert.AreEqual("000", echeckSalesResponse.response);

                    echeckSalesResponse = litleBatchResponse.nextEcheckSalesResponse();
                }

                echeckPreNoteSaleResponse echeckPreNoteSaleResponse = litleBatchResponse.nextEcheckPreNoteSaleResponse();
                while (echeckPreNoteSaleResponse != null)
                {
                    Assert.AreEqual("000", echeckPreNoteSaleResponse.response);

                    echeckPreNoteSaleResponse = litleBatchResponse.nextEcheckPreNoteSaleResponse();
                }

                echeckPreNoteCreditResponse echeckPreNoteCreditResponse = litleBatchResponse.nextEcheckPreNoteCreditResponse();
                while (echeckPreNoteCreditResponse != null)
                {
                    Assert.AreEqual("000", echeckPreNoteCreditResponse.response);

                    echeckPreNoteCreditResponse = litleBatchResponse.nextEcheckPreNoteCreditResponse();
                }

                echeckVerificationResponse echeckVerificationResponse = litleBatchResponse.nextEcheckVerificationResponse();
                while (echeckVerificationResponse != null)
                {
                    Assert.AreEqual("957", echeckVerificationResponse.response);

                    echeckVerificationResponse = litleBatchResponse.nextEcheckVerificationResponse();
                }

                forceCaptureResponse forceCaptureResponse = litleBatchResponse.nextForceCaptureResponse();
                while (forceCaptureResponse != null)
                {
                    Assert.AreEqual("000", forceCaptureResponse.response);

                    forceCaptureResponse = litleBatchResponse.nextForceCaptureResponse();
                }

                registerTokenResponse registerTokenResponse = litleBatchResponse.nextRegisterTokenResponse();
                while (registerTokenResponse != null)
                {
                    Assert.AreEqual("820", registerTokenResponse.response);

                    registerTokenResponse = litleBatchResponse.nextRegisterTokenResponse();
                }

                saleResponse saleResponse = litleBatchResponse.nextSaleResponse();
                while (saleResponse != null)
                {
                    Assert.AreEqual("000", saleResponse.response);

                    saleResponse = litleBatchResponse.nextSaleResponse();
                }

                litleBatchResponse = litleResponse.nextBatchResponse();
            }
        }

        [Test]
        public void accountUpdateBatch()
        {
            batchRequest litleBatchRequest = new batchRequest();

            accountUpdate accountUpdate1 = new accountUpdate();
            accountUpdate1.orderId = "1111";
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            accountUpdate1.card = card;
            accountUpdate1.id = "id";

            litleBatchRequest.addAccountUpdate(accountUpdate1);

            accountUpdate accountUpdate2 = new accountUpdate();
            accountUpdate2.orderId = "1112";
            accountUpdate2.card = card;
            accountUpdate2.id = "id";

            litleBatchRequest.addAccountUpdate(accountUpdate2);

            litle.addBatch(litleBatchRequest);
            litleResponse litleResponse = litle.sendToLitleWithStream();

            Assert.NotNull(litleResponse);
            Assert.AreEqual("0", litleResponse.response);
            Assert.AreEqual("Valid Format", litleResponse.message);

            batchResponse litleBatchResponse = litleResponse.nextBatchResponse();
            while (litleBatchResponse != null)
            {
                accountUpdateResponse accountUpdateResponse = litleBatchResponse.nextAccountUpdateResponse();
                Assert.NotNull(accountUpdateResponse);
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
            batchRequest litleBatchRequest = new batchRequest();
            litleBatchRequest.id = "1234567A";

            accountUpdate accountUpdate1 = new accountUpdate();
            accountUpdate1.orderId = "1111";
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4242424242424242";
            card.expDate = "1210";
            accountUpdate1.card = card;
            accountUpdate1.id = "id";

            litleBatchRequest.addAccountUpdate(accountUpdate1);

            accountUpdate accountUpdate2 = new accountUpdate();
            accountUpdate2.orderId = "1112";
            accountUpdate2.card = card;
            accountUpdate2.id = "id";

            litleBatchRequest.addAccountUpdate(accountUpdate2);

            litle.addBatch(litleBatchRequest);
            litleResponse litleResponse = litle.sendToLitleWithStream();

            Assert.NotNull(litleResponse);

            batchResponse litleBatchResponse = litleResponse.nextBatchResponse();
            Assert.NotNull(litleBatchResponse);
            while (litleBatchResponse != null)
            {
                accountUpdateResponse accountUpdateResponse = litleBatchResponse.nextAccountUpdateResponse();
                Assert.NotNull(accountUpdateResponse);
                while (accountUpdateResponse != null)
                {
                    Assert.AreEqual("000", accountUpdateResponse.response);

                    accountUpdateResponse = litleBatchResponse.nextAccountUpdateResponse();
                }
                litleBatchResponse = litleResponse.nextBatchResponse();
            }

            litleRequest litleRfr = new litleRequest();
            RFRRequest rfrRequest = new RFRRequest();
            accountUpdateFileRequestData accountUpdateFileRequestData = new accountUpdateFileRequestData();
            accountUpdateFileRequestData.merchantId = Properties.Settings.Default.merchantId;
            accountUpdateFileRequestData.postDay = DateTime.Now;
            rfrRequest.accountUpdateFileRequestData = accountUpdateFileRequestData;

            litleRfr.addRFRRequest(rfrRequest);            

            try
            {
                litleResponse litleRfrResponse = litleRfr.sendToLitleWithStream();
                Assert.NotNull(litleRfrResponse);

                RFRResponse rfrResponse = litleRfrResponse.nextRFRResponse();
                Assert.NotNull(rfrResponse);
                while (rfrResponse != null)
                {
                    Assert.AreEqual("1", rfrResponse.response);
                    Assert.AreEqual("The account update file is not ready yet.  Please try again later.", rfrResponse.message);
                    rfrResponse = litleResponse.nextRFRResponse();
                }
            }
            catch (Exception)
            {
            }
        }

        [Test]
        public void nullBatchData()
        {
            batchRequest litleBatchRequest = new batchRequest();

            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "414100000000000000";
            card.expDate = "1210";
            authorization.card = card; //This needs to compile      
            authorization.id = "id";

            litleBatchRequest.addAuthorization(authorization);
            try
            {
                litleBatchRequest.addAuthorization(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            authReversal reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";
            reversal.id = "id";

            litleBatchRequest.addAuthReversal(reversal);
            try
            {
                litleBatchRequest.addAuthReversal(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";
            capture.id = "id";

            litleBatchRequest.addCapture(capture);
            try
            {
                litleBatchRequest.addCapture(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            captureGivenAuth capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            authInformation authInfo = new authInformation();
            DateTime authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            capturegivenauth.card = card;
            capturegivenauth.id = "id";

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);
            try
            {
                litleBatchRequest.addCaptureGivenAuth(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            credit creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;
            creditObj.id = "id";

            litleBatchRequest.addCredit(creditObj);
            try
            {
                litleBatchRequest.addCredit(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "12345657890";
            echeck.routingNum = "011201995";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            contact billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;
            echeckcredit.id = "id";

            litleBatchRequest.addEcheckCredit(echeckcredit);
            try
            {
                litleBatchRequest.addEcheckCredit(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;
            echeckredeposit.id = "id";

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            try
            {
                litleBatchRequest.addEcheckRedeposit(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            echeckSale echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;
            echeckSaleObj.id = "id";

            litleBatchRequest.addEcheckSale(echeckSaleObj);
            try
            {
                litleBatchRequest.addEcheckSale(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            echeckVerification echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;
            echeckVerificationObject.id = "id";

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);
            try
            {
                litleBatchRequest.addEcheckVerification(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;
            forcecapture.id = "id";

            litleBatchRequest.addForceCapture(forcecapture);
            try
            {
                litleBatchRequest.addForceCapture(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;
            saleObj.id = "id";

            litleBatchRequest.addSale(saleObj);
            try
            {
                litleBatchRequest.addSale(null);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";
            registerTokenRequest.id = "id";

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
                litle.addBatch(litleBatchRequest);
            }
            catch (System.NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void InvalidCredientialsBatch()
        {
            batchRequest litleBatchRequest = new batchRequest();

            authorization authorization = new authorization();
            authorization.reportGroup = "Planets";
            authorization.orderId = "12344";
            authorization.amount = 106;
            authorization.orderSource = orderSourceType.ecommerce;
            cardType card = new cardType();
            card.type = methodOfPaymentTypeEnum.VI;
            card.number = "4100000000000001";
            card.expDate = "1210";
            authorization.card = card;
            authorization.id = "id";

            litleBatchRequest.addAuthorization(authorization);

            authorization authorization2 = new authorization();
            authorization2.reportGroup = "Planets";
            authorization2.orderId = "12345";
            authorization2.amount = 106;
            authorization2.orderSource = orderSourceType.ecommerce;
            cardType card2 = new cardType();
            card2.type = methodOfPaymentTypeEnum.VI;
            card2.number = "4242424242424242";
            card2.expDate = "1210";
            authorization2.card = card2;
            authorization2.id = "id";

            litleBatchRequest.addAuthorization(authorization2);

            authReversal reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";
            reversal.id = "id";

            litleBatchRequest.addAuthReversal(reversal);

            authReversal reversal2 = new authReversal();
            reversal2.litleTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes";
            reversal2.id = "id";

            litleBatchRequest.addAuthReversal(reversal2);

            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";
            capture.id = "id";

            litleBatchRequest.addCapture(capture);

            capture capture2 = new capture();
            capture2.litleTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";
            capture2.id = "id";

            litleBatchRequest.addCapture(capture2);

            captureGivenAuth capturegivenauth = new captureGivenAuth();
            capturegivenauth.amount = 106;
            capturegivenauth.orderId = "12344";
            authInformation authInfo = new authInformation();
            DateTime authDate = new DateTime(2002, 10, 9);
            authInfo.authDate = authDate;
            authInfo.authCode = "543216";
            authInfo.authAmount = 12345;
            capturegivenauth.authInformation = authInfo;
            capturegivenauth.orderSource = orderSourceType.ecommerce;
            capturegivenauth.card = card;
            capturegivenauth.id = "id";

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);

            captureGivenAuth capturegivenauth2 = new captureGivenAuth();
            capturegivenauth2.amount = 106;
            capturegivenauth2.orderId = "12344";
            authInformation authInfo2 = new authInformation();
            authDate = new DateTime(2003, 10, 9);
            authInfo2.authDate = authDate;
            authInfo2.authCode = "543216";
            authInfo2.authAmount = 12345;
            capturegivenauth2.authInformation = authInfo;
            capturegivenauth2.orderSource = orderSourceType.ecommerce;
            capturegivenauth2.card = card2;
            capturegivenauth2.id = "id";


            litleBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            credit creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;
            creditObj.id = "id";

            litleBatchRequest.addCredit(creditObj);

            credit creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;
            creditObj2.id = "id";

            litleBatchRequest.addCredit(creditObj2);

            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "1099999903";
            echeck.routingNum = "011201995";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            contact billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;
            echeckcredit.id = "id";

            litleBatchRequest.addEcheckCredit(echeckcredit);

            echeckCredit echeckcredit2 = new echeckCredit();
            echeckcredit2.amount = 12L;
            echeckcredit2.orderId = "12346";
            echeckcredit2.orderSource = orderSourceType.ecommerce;
            echeckType echeck2 = new echeckType();
            echeck2.accType = echeckAccountTypeEnum.Checking;
            echeck2.accNum = "1099999903";
            echeck2.routingNum = "011201995";
            echeck2.checkNum = "123456";
            echeckcredit2.echeck = echeck2;
            contact billToAddress2 = new contact();
            billToAddress2.name = "Mike";
            billToAddress2.city = "Lowell";
            billToAddress2.state = "MA";
            billToAddress2.email = "litle.com";
            echeckcredit2.billToAddress = billToAddress2;
            echeckcredit2.id = "id";

            litleBatchRequest.addEcheckCredit(echeckcredit2);

            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;
            echeckredeposit.id = "id";

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);

            echeckRedeposit echeckredeposit2 = new echeckRedeposit();
            echeckredeposit2.litleTxnId = 123457;
            echeckredeposit2.echeck = echeck2;
            echeckredeposit2.id = "id";

            litleBatchRequest.addEcheckRedeposit(echeckredeposit2);

            echeckSale echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;
            echeckSaleObj.id = "id";

            litleBatchRequest.addEcheckSale(echeckSaleObj);

            echeckSale echeckSaleObj2 = new echeckSale();
            echeckSaleObj2.amount = 123456;
            echeckSaleObj2.orderId = "12346";
            echeckSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckSaleObj2.echeck = echeck2;
            echeckSaleObj2.billToAddress = billToAddress2;
            echeckSaleObj2.id = "id";

            litleBatchRequest.addEcheckSale(echeckSaleObj2);

            echeckVerification echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;
            echeckVerificationObject.id = "id";

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);

            echeckVerification echeckVerificationObject2 = new echeckVerification();
            echeckVerificationObject2.amount = 123456;
            echeckVerificationObject2.orderId = "12346";
            echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject2.echeck = echeck2;
            echeckVerificationObject2.billToAddress = billToAddress2;
            echeckVerificationObject2.id = "id";

            litleBatchRequest.addEcheckVerification(echeckVerificationObject2);

            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;
            forcecapture.id = "id";

            litleBatchRequest.addForceCapture(forcecapture);

            forceCapture forcecapture2 = new forceCapture();
            forcecapture2.amount = 106;
            forcecapture2.orderId = "12345";
            forcecapture2.orderSource = orderSourceType.ecommerce;
            forcecapture2.card = card2;
            forcecapture2.id = "id";

            litleBatchRequest.addForceCapture(forcecapture2);

            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;
            saleObj.id = "id";

            litleBatchRequest.addSale(saleObj);

            sale saleObj2 = new sale();
            saleObj2.amount = 106;
            saleObj2.litleTxnId = 123456;
            saleObj2.orderId = "12345";
            saleObj2.orderSource = orderSourceType.ecommerce;
            saleObj2.card = card2;
            saleObj2.id = "id";

            litleBatchRequest.addSale(saleObj2);

            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";
            registerTokenRequest.id = "id";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            registerTokenRequestType registerTokenRequest2 = new registerTokenRequestType();
            registerTokenRequest2.orderId = "12345";
            registerTokenRequest2.accountNumber = "1233456789103801";
            registerTokenRequest2.reportGroup = "Planets";
            registerTokenRequest.id = "id";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            litle.addBatch(litleBatchRequest);

            try
            {
                litleResponse litleResponse = litle.sendToLitleWithStream();
            }
            catch (LitleOnlineException e)
            {
                Assert.AreEqual("Error establishing a network connection", e.Message);
            }
        }

        [Test]
        public void EcheckPreNoteTestAll()
        {
            batchRequest litleBatchRequest = new batchRequest();

            contact billToAddress = new contact();
            billToAddress.name = "Mike";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";

            echeckType echeckSuccess = new echeckType();
            echeckSuccess.accType = echeckAccountTypeEnum.Corporate;
            echeckSuccess.accNum = "1092969901";
            echeckSuccess.routingNum = "011075150";
            echeckSuccess.checkNum = "123456";
    

            echeckType echeckRoutErr = new echeckType();
            echeckRoutErr.accType = echeckAccountTypeEnum.Checking;
            echeckRoutErr.accNum = "6099999992";
            echeckRoutErr.routingNum = "053133052";
            echeckRoutErr.checkNum = "123457";
   

            echeckType echeckAccErr = new echeckType();
            echeckAccErr.accType = echeckAccountTypeEnum.Corporate;
            echeckAccErr.accNum = "10@2969901";
            echeckAccErr.routingNum = "011100012";
            echeckAccErr.checkNum = "123458";
        

            echeckPreNoteSale echeckPreNoteSaleSuccess = new echeckPreNoteSale();
            echeckPreNoteSaleSuccess.orderId = "000";
            echeckPreNoteSaleSuccess.orderSource = orderSourceType.ecommerce;
            echeckPreNoteSaleSuccess.echeck = echeckSuccess;
            echeckPreNoteSaleSuccess.billToAddress = billToAddress;
            echeckPreNoteSaleSuccess.id = "id";
            litleBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleSuccess);

            echeckPreNoteSale echeckPreNoteSaleRoutErr = new echeckPreNoteSale();
            echeckPreNoteSaleRoutErr.orderId = "900";
            echeckPreNoteSaleRoutErr.orderSource = orderSourceType.ecommerce;
            echeckPreNoteSaleRoutErr.echeck = echeckRoutErr;
            echeckPreNoteSaleRoutErr.billToAddress = billToAddress;
            echeckPreNoteSaleRoutErr.id = "id";
            litleBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleRoutErr);

            echeckPreNoteSale echeckPreNoteSaleAccErr = new echeckPreNoteSale();
            echeckPreNoteSaleAccErr.orderId = "301";
            echeckPreNoteSaleAccErr.orderSource = orderSourceType.ecommerce;
            echeckPreNoteSaleAccErr.echeck = echeckAccErr;
            echeckPreNoteSaleAccErr.billToAddress = billToAddress;
            echeckPreNoteSaleAccErr.id = "id";
            litleBatchRequest.addEcheckPreNoteSale(echeckPreNoteSaleAccErr);

            echeckPreNoteCredit echeckPreNoteCreditSuccess = new echeckPreNoteCredit();
            echeckPreNoteCreditSuccess.orderId = "000";
            echeckPreNoteCreditSuccess.orderSource = orderSourceType.ecommerce;
            echeckPreNoteCreditSuccess.echeck = echeckSuccess;
            echeckPreNoteCreditSuccess.billToAddress = billToAddress;
            echeckPreNoteCreditSuccess.id = "id";
            litleBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditSuccess);

            echeckPreNoteCredit echeckPreNoteCreditRoutErr = new echeckPreNoteCredit();
            echeckPreNoteCreditRoutErr.orderId = "900";
            echeckPreNoteCreditRoutErr.orderSource = orderSourceType.ecommerce;
            echeckPreNoteCreditRoutErr.echeck = echeckRoutErr;
            echeckPreNoteCreditRoutErr.billToAddress = billToAddress;
            echeckPreNoteCreditRoutErr.id = "id";
            litleBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditRoutErr);

            echeckPreNoteCredit echeckPreNoteCreditAccErr = new echeckPreNoteCredit();
            echeckPreNoteCreditAccErr.orderId = "301";
            echeckPreNoteCreditAccErr.orderSource = orderSourceType.ecommerce;
            echeckPreNoteCreditAccErr.echeck = echeckAccErr;
            echeckPreNoteCreditAccErr.billToAddress = billToAddress;
            echeckPreNoteCreditAccErr.id = "id";
            litleBatchRequest.addEcheckPreNoteCredit(echeckPreNoteCreditAccErr);

            litle.addBatch(litleBatchRequest);

            litleResponse litleResponse = litle.sendToLitleWithStream();

            Assert.NotNull(litleResponse);
            Assert.AreEqual("0", litleResponse.response);
            Assert.AreEqual("Valid Format", litleResponse.message);

            batchResponse litleBatchResponse = litleResponse.nextBatchResponse();
            while (litleBatchResponse != null)
            {
                echeckPreNoteSaleResponse echeckPreNoteSaleResponse = litleBatchResponse.nextEcheckPreNoteSaleResponse();
                while (echeckPreNoteSaleResponse != null)
                {

                    echeckPreNoteSaleResponse = litleBatchResponse.nextEcheckPreNoteSaleResponse();
                }

                echeckPreNoteCreditResponse echeckPreNoteCreditResponse = litleBatchResponse.nextEcheckPreNoteCreditResponse();
                while (echeckPreNoteCreditResponse != null)
                {

                    echeckPreNoteCreditResponse = litleBatchResponse.nextEcheckPreNoteCreditResponse();
                }

                litleBatchResponse = litleResponse.nextBatchResponse();
            }
        }*/

//        [Test]
//        public void PFIFInstructionTxnTest()
//        {
//            
//            Dictionary<string, string> configOverride = new Dictionary<string, string>();
//            configOverride["url"] = Properties.Settings.Default.url;
//            configOverride["reportGroup"] = Properties.Settings.Default.reportGroup;
//            configOverride["username"] = "SDKV10";
//            configOverride["printxml"] = Properties.Settings.Default.printxml;
//            configOverride["timeout"] = Properties.Settings.Default.timeout;
//            configOverride["proxyHost"] = Properties.Settings.Default.proxyHost;
//            configOverride["merchantId"] = "0180-xml10";
//            configOverride["password"] = "x3Km7hd";
//            configOverride["proxyPort"] = Properties.Settings.Default.proxyPort;
//            configOverride["sftpUrl"] = Properties.Settings.Default.sftpUrl;
//            configOverride["sftpUsername"] = Properties.Settings.Default.sftpUsername;
//            configOverride["sftpPassword"] = Properties.Settings.Default.sftpPassword;
//            configOverride["knownHostsFile"] = Properties.Settings.Default.knownHostsFile;
//            configOverride["onlineBatchUrl"] = Properties.Settings.Default.onlineBatchUrl;
//            configOverride["onlineBatchPort"] = Properties.Settings.Default.onlineBatchPort;
//            configOverride["requestDirectory"] = Properties.Settings.Default.requestDirectory;
//            configOverride["responseDirectory"] = Properties.Settings.Default.responseDirectory;
//
//            litleRequest litleOverride = new litleRequest(configOverride);
//
//            batchRequest litleBatchRequest = new batchRequest(configOverride);
//
//            echeckType echeck = new echeckType();
//            echeck.accType = echeckAccountTypeEnum.Corporate;
//            echeck.accNum = "1092969901";
//            echeck.routingNum = "011075150";
//            echeck.checkNum = "123455";
//  
//
//            submerchantCredit submerchantCredit = new submerchantCredit();
//            submerchantCredit.fundingSubmerchantId = "123456";
//            submerchantCredit.submerchantName = "merchant";
//            submerchantCredit.fundsTransferId = "123467";
//            submerchantCredit.amount = 106L;
//            submerchantCredit.accountInfo = echeck;
//            submerchantCredit.id = "id";
//            litleBatchRequest.addSubmerchantCredit(submerchantCredit);
//
//            payFacCredit payFacCredit = new payFacCredit();
//            payFacCredit.fundingSubmerchantId = "123456";
//            payFacCredit.fundsTransferId = "123467";
//            payFacCredit.amount = 107L;
//            payFacCredit.id = "id";
//            litleBatchRequest.addPayFacCredit(payFacCredit);
//
//            reserveCredit reserveCredit = new reserveCredit();
//            reserveCredit.fundingSubmerchantId = "123456";
//            reserveCredit.fundsTransferId = "123467";
//            reserveCredit.amount = 107L;
//            reserveCredit.id = "id";
//            litleBatchRequest.addReserveCredit(reserveCredit);
//
//            vendorCredit vendorCredit = new vendorCredit();
//            vendorCredit.fundingSubmerchantId = "123456";
//            vendorCredit.vendorName = "merchant";
//            vendorCredit.fundsTransferId = "123467";
//            vendorCredit.amount = 106L;
//            vendorCredit.accountInfo = echeck;
//            vendorCredit.id = "id";
//            litleBatchRequest.addVendorCredit(vendorCredit);
//
//            physicalCheckCredit physicalCheckCredit = new physicalCheckCredit();
//            physicalCheckCredit.fundingSubmerchantId = "123456";
//            physicalCheckCredit.fundsTransferId = "123467";
//            physicalCheckCredit.amount = 107L;
//            physicalCheckCredit.id = "id";
//            litleBatchRequest.addPhysicalCheckCredit(physicalCheckCredit);
//
//            submerchantDebit submerchantDebit = new submerchantDebit();
//            submerchantDebit.fundingSubmerchantId = "123456";
//            submerchantDebit.submerchantName = "merchant";
//            submerchantDebit.fundsTransferId = "123467";
//            submerchantDebit.amount = 106L;
//            submerchantDebit.accountInfo = echeck;
//            submerchantDebit.id = "id";
//            litleBatchRequest.addSubmerchantDebit(submerchantDebit);
//
//            payFacDebit payFacDebit = new payFacDebit();
//            payFacDebit.fundingSubmerchantId = "123456";
//            payFacDebit.fundsTransferId = "123467";
//            payFacDebit.amount = 107L;
//            payFacDebit.id = "id";
//            litleBatchRequest.addPayFacDebit(payFacDebit);
//
//            reserveDebit reserveDebit = new reserveDebit();
//            reserveDebit.fundingSubmerchantId = "123456";
//            reserveDebit.fundsTransferId = "123467";
//            reserveDebit.amount = 107L;
//            reserveDebit.id = "id";
//            litleBatchRequest.addReserveDebit(reserveDebit);
//
//            vendorDebit vendorDebit = new vendorDebit();
//            vendorDebit.fundingSubmerchantId = "123456";
//            vendorDebit.vendorName = "merchant";
//            vendorDebit.fundsTransferId = "123467";
//            vendorDebit.amount = 106L;
//            vendorDebit.accountInfo = echeck;
//            vendorDebit.id = "id";
//            litleBatchRequest.addVendorDebit(vendorDebit);
//
//            physicalCheckDebit physicalCheckDebit = new physicalCheckDebit();
//            physicalCheckDebit.fundingSubmerchantId = "123456";
//            physicalCheckDebit.fundsTransferId = "123467";
//            physicalCheckDebit.amount = 107L;
//            physicalCheckDebit.id = "id";
//            litleBatchRequest.addPhysicalCheckDebit(physicalCheckDebit);
//
//            litleOverride.addBatch(litleBatchRequest);
//
//            litleResponse litleResponse = litleOverride.sendToLitleWithStream();
//
//            Assert.NotNull(litleResponse);
//            Assert.AreEqual("0", litleResponse.response);
//            Assert.AreEqual("Valid Format", litleResponse.message);
//
//            batchResponse litleBatchResponse = litleResponse.nextBatchResponse();
//            while (litleBatchResponse != null)
//            {
//                submerchantCreditResponse submerchantCreditResponse = litleBatchResponse.nextSubmerchantCreditResponse();
//                while (submerchantCreditResponse != null)
//                {
//                    Assert.AreEqual("000", submerchantCreditResponse.response);
//                    submerchantCreditResponse = litleBatchResponse.nextSubmerchantCreditResponse();
//                }
//
//                payFacCreditResponse payFacCreditResponse = litleBatchResponse.nextPayFacCreditResponse();
//                while (payFacCreditResponse != null)
//                {
//                    Assert.AreEqual("000", payFacCreditResponse.response);
//                    payFacCreditResponse = litleBatchResponse.nextPayFacCreditResponse();
//                }
//
//                vendorCreditResponse vendorCreditResponse = litleBatchResponse.nextVendorCreditResponse();
//                while (vendorCreditResponse != null)
//                {
//                    Assert.AreEqual("000", vendorCreditResponse.response);
//                    vendorCreditResponse = litleBatchResponse.nextVendorCreditResponse();
//                }
//
//                reserveCreditResponse reserveCreditResponse = litleBatchResponse.nextReserveCreditResponse();
//                while (reserveCreditResponse != null)
//                {
//                    Assert.AreEqual("000", reserveCreditResponse.response);
//                    reserveCreditResponse = litleBatchResponse.nextReserveCreditResponse();
//                }
//
//                physicalCheckCreditResponse physicalCheckCreditResponse = litleBatchResponse.nextPhysicalCheckCreditResponse();
//                while (physicalCheckCreditResponse != null)
//                {
//                    Assert.AreEqual("000", physicalCheckCreditResponse.response);
//                    physicalCheckCreditResponse = litleBatchResponse.nextPhysicalCheckCreditResponse();
//                }
//
//                submerchantDebitResponse submerchantDebitResponse = litleBatchResponse.nextSubmerchantDebitResponse();
//                while (submerchantDebitResponse != null)
//                {
//                    Assert.AreEqual("000", submerchantDebitResponse.response);
//                    submerchantDebitResponse = litleBatchResponse.nextSubmerchantDebitResponse();
//                }
//
//                payFacDebitResponse payFacDebitResponse = litleBatchResponse.nextPayFacDebitResponse();
//                while (payFacDebitResponse != null)
//                {
//                    Assert.AreEqual("000", payFacDebitResponse.response);
//                    payFacDebitResponse = litleBatchResponse.nextPayFacDebitResponse();
//                }
//
//                vendorDebitResponse vendorDebitResponse = litleBatchResponse.nextVendorDebitResponse();
//                while (vendorDebitResponse != null)
//                {
//                    Assert.AreEqual("000", vendorDebitResponse.response);
//                    vendorDebitResponse = litleBatchResponse.nextVendorDebitResponse();
//                }
//
//                reserveDebitResponse reserveDebitResponse = litleBatchResponse.nextReserveDebitResponse();
//                while (reserveDebitResponse != null)
//                {
//                    Assert.AreEqual("000", reserveDebitResponse.response);
//                    reserveDebitResponse = litleBatchResponse.nextReserveDebitResponse();
//                }
//
//                physicalCheckDebitResponse physicalCheckDebitResponse = litleBatchResponse.nextPhysicalCheckDebitResponse();
//                while (physicalCheckDebitResponse != null)
//                {
//                    Assert.AreEqual("000", physicalCheckDebitResponse.response);
//                    physicalCheckDebitResponse = litleBatchResponse.nextPhysicalCheckDebitResponse();
//                }
//
//                litleBatchResponse = litleResponse.nextBatchResponse();
//            }
//        }
    }
}

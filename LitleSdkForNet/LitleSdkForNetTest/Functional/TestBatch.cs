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

        [Test]
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

            litleBatchRequest.addAuthorization(authorization2);

            authReversal reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal);

            authReversal reversal2 = new authReversal();
            reversal2.litleTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal2);

            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            litleBatchRequest.addCapture(capture);

            capture capture2 = new capture();
            capture2.litleTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";

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

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            credit creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;

            litleBatchRequest.addCredit(creditObj);

            credit creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;

            litleBatchRequest.addCredit(creditObj2);

            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "1099999903";
            echeck.routingNum = "114567895";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            contact billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;

            litleBatchRequest.addEcheckCredit(echeckcredit);

            echeckCredit echeckcredit2 = new echeckCredit();
            echeckcredit2.amount = 12L;
            echeckcredit2.orderId = "12346";
            echeckcredit2.orderSource = orderSourceType.ecommerce;
            echeckType echeck2 = new echeckType();
            echeck2.accType = echeckAccountTypeEnum.Checking;
            echeck2.accNum = "1099999903";
            echeck2.routingNum = "114567895";
            echeck2.checkNum = "123456";
            echeckcredit2.echeck = echeck2;
            contact billToAddress2 = new contact();
            billToAddress2.name = "Mike";
            billToAddress2.city = "Lowell";
            billToAddress2.state = "MA";
            billToAddress2.email = "litle.com";
            echeckcredit2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckCredit(echeckcredit2);

            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);

            echeckRedeposit echeckredeposit2 = new echeckRedeposit();
            echeckredeposit2.litleTxnId = 123457;
            echeckredeposit2.echeck = echeck2;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit2);

            echeckSale echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;

            litleBatchRequest.addEcheckSale(echeckSaleObj);

            echeckSale echeckSaleObj2 = new echeckSale();
            echeckSaleObj2.amount = 123456;
            echeckSaleObj2.orderId = "12346";
            echeckSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckSaleObj2.echeck = echeck2;
            echeckSaleObj2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckSale(echeckSaleObj2);

            echeckVerification echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);

            echeckVerification echeckVerificationObject2 = new echeckVerification();
            echeckVerificationObject2.amount = 123456;
            echeckVerificationObject2.orderId = "12346";
            echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject2.echeck = echeck2;
            echeckVerificationObject2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject2);

            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;

            litleBatchRequest.addForceCapture(forcecapture);

            forceCapture forcecapture2 = new forceCapture();
            forcecapture2.amount = 106;
            forcecapture2.orderId = "12345";
            forcecapture2.orderSource = orderSourceType.ecommerce;
            forcecapture2.card = card2;

            litleBatchRequest.addForceCapture(forcecapture2);

            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;

            litleBatchRequest.addSale(saleObj);

            sale saleObj2 = new sale();
            saleObj2.amount = 106;
            saleObj2.litleTxnId = 123456;
            saleObj2.orderId = "12345";
            saleObj2.orderSource = orderSourceType.ecommerce;
            saleObj2.card = card2;

            litleBatchRequest.addSale(saleObj2);

            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            registerTokenRequestType registerTokenRequest2 = new registerTokenRequestType();
            registerTokenRequest2.orderId = "12345";
            registerTokenRequest2.accountNumber = "1233456789103801";
            registerTokenRequest2.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            updateCardValidationNumOnToken updateCardValidationNumOnToken = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken.orderId = "12344";
            updateCardValidationNumOnToken.cardValidationNum = "123";
            updateCardValidationNumOnToken.litleToken = "4100000000000001";

            litleBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken);

            updateCardValidationNumOnToken updateCardValidationNumOnToken2 = new updateCardValidationNumOnToken();
            updateCardValidationNumOnToken2.orderId = "12345";
            updateCardValidationNumOnToken2.cardValidationNum = "123";
            updateCardValidationNumOnToken2.litleToken = "4242424242424242";

            litleBatchRequest.addUpdateCardValidationNumOnToken(updateCardValidationNumOnToken2);
            litle.addBatch(litleBatchRequest);

            string batchName = litle.sendToLitle();

            litle.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            litleResponse litleResponse = litle.receiveFromLitle(batchName);

            Assert.NotNull(litleResponse);
            Assert.AreEqual("0", litleResponse.response);
            Assert.AreEqual("Valid Format", litleResponse.message);

            batchResponse litleBatchResponse = litleResponse.nextLitleBatchResponse();
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
                    Assert.AreEqual("360", authReversalResponse.response);

                    authReversalResponse = litleBatchResponse.nextAuthReversalResponse();
                }

                captureResponse captureResponse = litleBatchResponse.nextCaptureResponse();
                while (captureResponse != null)
                {
                    Assert.AreEqual("360", captureResponse.response);

                    captureResponse = litleBatchResponse.nextCaptureResponse();
                }

                captureGivenAuthResponse captureGivenAuthResponse = litleBatchResponse.nextCaptureGivenAuthResponse();
                while (captureGivenAuthResponse != null)
                {
                    Assert.AreEqual("000", captureGivenAuthResponse.response);

                    captureGivenAuthResponse = litleBatchResponse.nextCaptureGivenAuthResponse();
                }

                creditResponse creditResponse = litleBatchResponse.nextCreditResponse();
                while (creditResponse != null)
                {
                    Assert.AreEqual("000", creditResponse.response);

                    creditResponse = litleBatchResponse.nextCreditResponse();
                }

                echeckCreditResponse echeckCreditResponse = litleBatchResponse.nextEcheckCreditResponse();
                while (echeckCreditResponse != null)
                {
                    Assert.AreEqual("000", echeckCreditResponse.response);

                    echeckCreditResponse = litleBatchResponse.nextEcheckCreditResponse();
                }

                echeckRedepositResponse echeckRedepositResponse = litleBatchResponse.nextEcheckRedepositResponse();
                while (echeckRedepositResponse != null)
                {
                    Assert.AreEqual("360", echeckRedepositResponse.response);

                    echeckRedepositResponse = litleBatchResponse.nextEcheckRedepositResponse();
                }

                echeckSalesResponse echeckSalesResponse = litleBatchResponse.nextEcheckSalesResponse();
                while (echeckSalesResponse != null)
                {
                    Assert.AreEqual("000", echeckSalesResponse.response);

                    echeckSalesResponse = litleBatchResponse.nextEcheckSalesResponse();
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

                updateCardValidationNumOnTokenResponse updateCardValidationNumOnTokenResponse = litleBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
                while (updateCardValidationNumOnTokenResponse != null)
                {
                    Assert.AreEqual("823", updateCardValidationNumOnTokenResponse.response);

                    updateCardValidationNumOnTokenResponse = litleBatchResponse.nextUpdateCardValidationNumOnTokenResponse();
                }

                litleBatchResponse = litleResponse.nextLitleBatchResponse();
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

            litleBatchRequest.addAccountUpdate(accountUpdate1);

            accountUpdate accountUpdate2 = new accountUpdate();
            accountUpdate2.orderId = "1112";
            accountUpdate2.card = card;

            litleBatchRequest.addAccountUpdate(accountUpdate2);

            litle.addBatch(litleBatchRequest);
            string batchName = litle.sendToLitle();

            litle.blockAndWaitForResponse(batchName, estimatedResponseTime(0, 1 * 2));

            litleResponse litleResponse = litle.receiveFromLitle(batchName);

            Assert.NotNull(litleResponse);
            Assert.AreEqual("0", litleResponse.response);
            Assert.AreEqual("Valid Format", litleResponse.message);

            batchResponse litleBatchResponse = litleResponse.nextLitleBatchResponse();
            while (litleBatchResponse != null)
            {
                accountUpdateResponse accountUpdateResponse = litleBatchResponse.nextAccountUpdateResponse();
                while (accountUpdateResponse != null)
                {
                    Assert.AreEqual("301", accountUpdateResponse.response);

                    accountUpdateResponse = litleBatchResponse.nextAccountUpdateResponse();
                }
                litleBatchResponse = litleResponse.nextLitleBatchResponse();
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

            litleBatchRequest.addAccountUpdate(accountUpdate1);

            accountUpdate accountUpdate2 = new accountUpdate();
            accountUpdate2.orderId = "1112";
            accountUpdate2.card = card;

            litleBatchRequest.addAccountUpdate(accountUpdate2);
            litle.addBatch(litleBatchRequest);

            string batchName = litle.sendToLitle();
            litle.blockAndWaitForResponse(batchName, estimatedResponseTime(0, 1 * 2));
            litleResponse litleResponse = litle.receiveFromLitle(batchName);

            Assert.NotNull(litleResponse);

            batchResponse litleBatchResponse = litleResponse.nextLitleBatchResponse();
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
                litleBatchResponse = litleResponse.nextLitleBatchResponse();
            }

            litleRequest litleRfr = new litleRequest();
            RFRRequest rfrRequest = new RFRRequest();
            accountUpdateFileRequestData accountUpdateFileRequestData = new accountUpdateFileRequestData();
            accountUpdateFileRequestData.merchantId = Properties.Settings.Default.merchantId;
            accountUpdateFileRequestData.postDay = DateTime.Now;
            rfrRequest.accountUpdateFileRequestData = accountUpdateFileRequestData;

            litleRfr.addRFRRequest(rfrRequest);

            string rfrBatchName = litle.sendToLitle();
            litle.blockAndWaitForResponse(rfrBatchName, estimatedResponseTime(0, 1 * 2));
            litleResponse litleRfrResponse = litle.receiveFromLitle(rfrBatchName);

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

            authReversal reversal = new authReversal();
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

            capture capture = new capture();
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
            echeck.routingNum = "123456789";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            contact billToAddress = new contact();
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

            echeckRedeposit echeckredeposit = new echeckRedeposit();
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

            echeckSale echeckSaleObj = new echeckSale();
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

            echeckVerification echeckVerificationObject = new echeckVerification();
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

            forceCapture forcecapture = new forceCapture();
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

            sale saleObj = new sale();
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

            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
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
            authorization.card = card; //This needs to compile      

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
            authorization2.card = card2; //This needs to compile

            litleBatchRequest.addAuthorization(authorization2);

            authReversal reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal);

            authReversal reversal2 = new authReversal();
            reversal2.litleTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal2);

            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            litleBatchRequest.addCapture(capture);

            capture capture2 = new capture();
            capture2.litleTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";

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

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            credit creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;

            litleBatchRequest.addCredit(creditObj);

            credit creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;

            litleBatchRequest.addCredit(creditObj2);

            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "1099999903";
            echeck.routingNum = "114567895";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            contact billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;

            litleBatchRequest.addEcheckCredit(echeckcredit);

            echeckCredit echeckcredit2 = new echeckCredit();
            echeckcredit2.amount = 12L;
            echeckcredit2.orderId = "12346";
            echeckcredit2.orderSource = orderSourceType.ecommerce;
            echeckType echeck2 = new echeckType();
            echeck2.accType = echeckAccountTypeEnum.Checking;
            echeck2.accNum = "1099999903";
            echeck2.routingNum = "114567895";
            echeck2.checkNum = "123456";
            echeckcredit2.echeck = echeck2;
            contact billToAddress2 = new contact();
            billToAddress2.name = "Mike";
            billToAddress2.city = "Lowell";
            billToAddress2.state = "MA";
            billToAddress2.email = "litle.com";
            echeckcredit2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckCredit(echeckcredit2);

            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);

            echeckRedeposit echeckredeposit2 = new echeckRedeposit();
            echeckredeposit2.litleTxnId = 123457;
            echeckredeposit2.echeck = echeck2;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit2);

            echeckSale echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;

            litleBatchRequest.addEcheckSale(echeckSaleObj);

            echeckSale echeckSaleObj2 = new echeckSale();
            echeckSaleObj2.amount = 123456;
            echeckSaleObj2.orderId = "12346";
            echeckSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckSaleObj2.echeck = echeck2;
            echeckSaleObj2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckSale(echeckSaleObj2);

            echeckVerification echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);

            echeckVerification echeckVerificationObject2 = new echeckVerification();
            echeckVerificationObject2.amount = 123456;
            echeckVerificationObject2.orderId = "12346";
            echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject2.echeck = echeck2;
            echeckVerificationObject2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject2);

            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;

            litleBatchRequest.addForceCapture(forcecapture);

            forceCapture forcecapture2 = new forceCapture();
            forcecapture2.amount = 106;
            forcecapture2.orderId = "12345";
            forcecapture2.orderSource = orderSourceType.ecommerce;
            forcecapture2.card = card2;

            litleBatchRequest.addForceCapture(forcecapture2);

            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;

            litleBatchRequest.addSale(saleObj);

            sale saleObj2 = new sale();
            saleObj2.amount = 106;
            saleObj2.litleTxnId = 123456;
            saleObj2.orderId = "12345";
            saleObj2.orderSource = orderSourceType.ecommerce;
            saleObj2.card = card2;

            litleBatchRequest.addSale(saleObj2);

            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            registerTokenRequestType registerTokenRequest2 = new registerTokenRequestType();
            registerTokenRequest2.orderId = "12345";
            registerTokenRequest2.accountNumber = "1233456789103801";
            registerTokenRequest2.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            litle.addBatch(litleBatchRequest);

            string batchName = litle.sendToLitle();

            litle.blockAndWaitForResponse(batchName, estimatedResponseTime(2 * 2, 10 * 2));

            try
            {
                litleResponse litleResponse = litle.receiveFromLitle(batchName);
            }
            catch (LitleOnlineException e)
            {
                Assert.AreEqual("Error occured while attempting to retrieve and save the file from SFTP", e.Message);
            }
        }

        [Test]
        public void InvalidSftpCredientialsBatch()
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
            authorization.card = card; //This needs to compile      

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
            authorization2.card = card2; //This needs to compile

            litleBatchRequest.addAuthorization(authorization2);

            authReversal reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal);

            authReversal reversal2 = new authReversal();
            reversal2.litleTxnId = 12345678900L;
            reversal2.amount = 106;
            reversal2.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal2);

            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            litleBatchRequest.addCapture(capture);

            capture capture2 = new capture();
            capture2.litleTxnId = 123456700;
            capture2.amount = 106;
            capture2.payPalNotes = "Notes";

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

            litleBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            credit creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;

            litleBatchRequest.addCredit(creditObj);

            credit creditObj2 = new credit();
            creditObj2.amount = 106;
            creditObj2.orderId = "2111";
            creditObj2.orderSource = orderSourceType.ecommerce;
            creditObj2.card = card2;

            litleBatchRequest.addCredit(creditObj2);

            echeckCredit echeckcredit = new echeckCredit();
            echeckcredit.amount = 12L;
            echeckcredit.orderId = "12345";
            echeckcredit.orderSource = orderSourceType.ecommerce;
            echeckType echeck = new echeckType();
            echeck.accType = echeckAccountTypeEnum.Checking;
            echeck.accNum = "1099999903";
            echeck.routingNum = "114567895";
            echeck.checkNum = "123455";
            echeckcredit.echeck = echeck;
            contact billToAddress = new contact();
            billToAddress.name = "Bob";
            billToAddress.city = "Lowell";
            billToAddress.state = "MA";
            billToAddress.email = "litle.com";
            echeckcredit.billToAddress = billToAddress;

            litleBatchRequest.addEcheckCredit(echeckcredit);

            echeckCredit echeckcredit2 = new echeckCredit();
            echeckcredit2.amount = 12L;
            echeckcredit2.orderId = "12346";
            echeckcredit2.orderSource = orderSourceType.ecommerce;
            echeckType echeck2 = new echeckType();
            echeck2.accType = echeckAccountTypeEnum.Checking;
            echeck2.accNum = "1099999903";
            echeck2.routingNum = "114567895";
            echeck2.checkNum = "123456";
            echeckcredit2.echeck = echeck2;
            contact billToAddress2 = new contact();
            billToAddress2.name = "Mike";
            billToAddress2.city = "Lowell";
            billToAddress2.state = "MA";
            billToAddress2.email = "litle.com";
            echeckcredit2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckCredit(echeckcredit2);

            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);

            echeckRedeposit echeckredeposit2 = new echeckRedeposit();
            echeckredeposit2.litleTxnId = 123457;
            echeckredeposit2.echeck = echeck2;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit2);

            echeckSale echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;

            litleBatchRequest.addEcheckSale(echeckSaleObj);

            echeckSale echeckSaleObj2 = new echeckSale();
            echeckSaleObj2.amount = 123456;
            echeckSaleObj2.orderId = "12346";
            echeckSaleObj2.orderSource = orderSourceType.ecommerce;
            echeckSaleObj2.echeck = echeck2;
            echeckSaleObj2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckSale(echeckSaleObj2);

            echeckVerification echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);

            echeckVerification echeckVerificationObject2 = new echeckVerification();
            echeckVerificationObject2.amount = 123456;
            echeckVerificationObject2.orderId = "12346";
            echeckVerificationObject2.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject2.echeck = echeck2;
            echeckVerificationObject2.billToAddress = billToAddress2;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject2);

            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;

            litleBatchRequest.addForceCapture(forcecapture);

            forceCapture forcecapture2 = new forceCapture();
            forcecapture2.amount = 106;
            forcecapture2.orderId = "12345";
            forcecapture2.orderSource = orderSourceType.ecommerce;
            forcecapture2.card = card2;

            litleBatchRequest.addForceCapture(forcecapture2);

            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;

            litleBatchRequest.addSale(saleObj);

            sale saleObj2 = new sale();
            saleObj2.amount = 106;
            saleObj2.litleTxnId = 123456;
            saleObj2.orderId = "12345";
            saleObj2.orderSource = orderSourceType.ecommerce;
            saleObj2.card = card2;

            litleBatchRequest.addSale(saleObj2);

            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            registerTokenRequestType registerTokenRequest2 = new registerTokenRequestType();
            registerTokenRequest2.orderId = "12345";
            registerTokenRequest2.accountNumber = "1233456789103801";
            registerTokenRequest2.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            litle.addBatch(litleBatchRequest);

            try
            {
                string batchName = litle.sendToLitle();
            }
            catch (LitleOnlineException e)
            {
                Assert.AreSame("Error occured while attempting to establish an SFTP connection", e.Message);
            }
        }

        private int estimatedResponseTime(int numAuthsAndSales, int numRest)
        {
            return (int)(5 * 60 * 1000 + 2.5 * 1000 + numAuthsAndSales * (1 / 5) * 1000 + numRest * (1 / 50) * 1000);
        }
    }
}

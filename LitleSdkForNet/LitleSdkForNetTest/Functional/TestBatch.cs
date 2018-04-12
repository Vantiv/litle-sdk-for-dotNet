using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestBatch
    {
        private litleRequest _litle;
        private Dictionary<string, string> _invalidConfig;
        private Dictionary<string, string> _invalidSftpConfig;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _invalidConfig = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", Properties.Settings.Default.reportGroup},
                {"username", "badUsername"},
                {"printxml", Properties.Settings.Default.printxml},
                {"timeout", Properties.Settings.Default.timeout},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"merchantId", Properties.Settings.Default.merchantId},
                {"password", "badPassword"},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"sftpUrl", Properties.Settings.Default.sftpUrl},
                {"sftpUsername", Properties.Settings.Default.sftpUsername},
                {"sftpPassword", Properties.Settings.Default.sftpPassword},
                {"knownHostsFile", Properties.Settings.Default.knownHostsFile},
                {"requestDirectory", Properties.Settings.Default.requestDirectory},
                {"responseDirectory", Properties.Settings.Default.responseDirectory}
            };

            _invalidSftpConfig = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", Properties.Settings.Default.reportGroup},
                {"username", Properties.Settings.Default.username},
                {"printxml", Properties.Settings.Default.printxml},
                {"timeout", Properties.Settings.Default.timeout},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"merchantId", Properties.Settings.Default.merchantId},
                {"password", Properties.Settings.Default.password},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"sftpUrl", Properties.Settings.Default.sftpUrl},
                {"sftpUsername", "badSftpUsername"},
                {"sftpPassword", "badSftpPassword"},
                {"knownHostsFile", Properties.Settings.Default.knownHostsFile},
                {"requestDirectory", Properties.Settings.Default.requestDirectory},
                {"responseDirectory", Properties.Settings.Default.responseDirectory}
            };
            
        }

        [SetUp]
        public void SetUpBeforeTest()
        {
            _litle = new litleRequest();
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
            echeck.routingNum = "011201995";
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
            echeck2.routingNum = "011201995";
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
            _litle.addBatch(litleBatchRequest);

            string batchName = _litle.sendToLitle();

            _litle.blockAndWaitForResponse(batchName, EstimatedResponseTime(2 * 2, 10 * 2));

            litleResponse litleResponse = _litle.receiveFromLitle(batchName);

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

                litleBatchResponse = litleResponse.nextBatchResponse();
            }
        }

        [Test]
        public void AccountUpdateBatch()
        {
            var litleBatchRequest = new batchRequest();

            var card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "414100000000000000",
                expDate = "1210"
            };

            var accountUpdate1 = new accountUpdate
            {
                orderId = "1111",
                card = card
            };
            
            litleBatchRequest.addAccountUpdate(accountUpdate1);

            var accountUpdate2 = new accountUpdate
            {
                orderId = "1112",
                card = card
            };

            litleBatchRequest.addAccountUpdate(accountUpdate2);

            _litle.addBatch(litleBatchRequest);
            var batchName = _litle.sendToLitle();

            _litle.blockAndWaitForResponse(batchName, EstimatedResponseTime(0, 1 * 2));

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
        public void RfrBatch()
        {
            var litleBatchRequest = new batchRequest {id = "1234567A"};

            var card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4242424242424242",
                expDate = "1210"
            };

            var accountUpdate1 = new accountUpdate
            {
                orderId = "1111",
                card = card
            };
            
            litleBatchRequest.addAccountUpdate(accountUpdate1);

            var accountUpdate2 = new accountUpdate
            {
                orderId = "1112",
                card = card
            };

            litleBatchRequest.addAccountUpdate(accountUpdate2);
            _litle.addBatch(litleBatchRequest);

            var batchName = _litle.sendToLitle();
            _litle.blockAndWaitForResponse(batchName, EstimatedResponseTime(0, 1 * 2));
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
            var rfrRequest = new RFRRequest
            {
                accountUpdateFileRequestData = new accountUpdateFileRequestData
                {
                    merchantId = Properties.Settings.Default.merchantId,
                    postDay = DateTime.Now
                }
            };
            
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
                Console.WriteLine("Exception handled <" + e.Message + ">");
            }
        }

        [Test]
        public void NullBatchData()
        {
            var litleBatchRequest = new batchRequest();

            var card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "414100000000000000",
                expDate = "1210"
            };
            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addAuthorization(authorization);
            try
            {
                litleBatchRequest.addAuthorization(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var reversal = new authReversal
            {
                litleTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "Notes"
            };

            litleBatchRequest.addAuthReversal(reversal);
            try
            {
                litleBatchRequest.addAuthReversal(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var capture = new capture
            {
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes"
            };

            litleBatchRequest.addCapture(capture);
            try
            {
                litleBatchRequest.addCapture(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var capturegivenauth = new captureGivenAuth
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                authInformation = new authInformation
                {
                    authDate = new DateTime(2002, 10, 9),
                    authCode = "543216",
                    authAmount = 12345
                },
                card = card
            };
            
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);
            try
            {
                litleBatchRequest.addCaptureGivenAuth(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var creditObj = new credit
            {
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addCredit(creditObj);
            try
            {
                litleBatchRequest.addCredit(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeck = new echeckType
            {
                accType = echeckAccountTypeEnum.Checking,
                accNum = "12345657890",
                routingNum = "011201995",
                checkNum = "123455"
            };

            var billToAddress = new contact
            {
                name = "Bob",
                city = "Lowell",
                state = "MA",
                email = "litle.com"
            };

            var echeckcredit = new echeckCredit
            {
                amount = 12L,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };
            
            litleBatchRequest.addEcheckCredit(echeckcredit);
            try
            {
                litleBatchRequest.addEcheckCredit(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckredeposit = new echeckRedeposit
            {
                litleTxnId = 123456,
                echeck = echeck
            };

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            try
            {
                litleBatchRequest.addEcheckRedeposit(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckSaleObj = new echeckSale
            {
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };

            litleBatchRequest.addEcheckSale(echeckSaleObj);
            try
            {
                litleBatchRequest.addEcheckSale(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var echeckVerificationObject = new echeckVerification
            {
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);
            try
            {
                litleBatchRequest.addEcheckVerification(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var forcecapture = new forceCapture
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addForceCapture(forcecapture);
            try
            {
                litleBatchRequest.addForceCapture(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addSale(saleObj);
            try
            {
                litleBatchRequest.addSale(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            var registerTokenRequest = new registerTokenRequestType
            {
                orderId = "12344",
                accountNumber = "1233456789103801",
                reportGroup = "Planets"
            };

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);
            try
            {
                litleBatchRequest.addRegisterTokenRequest(null);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

            try
            {
                _litle.addBatch(litleBatchRequest);
            }
            catch (NullReferenceException e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void InvalidCredientialsBatch()
        {
            var litleIc = new litleRequest(_invalidConfig);

            var litleBatchRequest = new batchRequest();

            var card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4100000000000001",
                expDate = "1210"
            };

            var card2 = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4242424242424242",
                expDate = "1210"
            };

            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = card
            };
            
            litleBatchRequest.addAuthorization(authorization);

            var authorization2 = new authorization
            {
                reportGroup = "Planets",
                orderId = "12345",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = card2
            };
            
          litleBatchRequest.addAuthorization(authorization2);

            var reversal = new authReversal
            {
                litleTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "Notes"
            };

            litleBatchRequest.addAuthReversal(reversal);

            var reversal2 = new authReversal
            {
                litleTxnId = 12345678900L,
                amount = 106,
                payPalNotes = "Notes"
            };

            litleBatchRequest.addAuthReversal(reversal2);

            var capture = new capture
            {
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes"
            };

            litleBatchRequest.addCapture(capture);

            var capture2 = new capture
            {
                litleTxnId = 123456700,
                amount = 106,
                payPalNotes = "Notes"
            };

            litleBatchRequest.addCapture(capture2);

            var authInfo = new authInformation
            {
                authDate = new DateTime(2002, 10, 9),
                authCode = "543216",
                authAmount = 12345
            };

            var capturegivenauth = new captureGivenAuth
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                authInformation = authInfo,
                card = card
            };
            
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);

            var capturegivenauth2 = new captureGivenAuth
            {
                amount = 106,
                orderId = "12344",
                authInformation = authInfo,
                orderSource = orderSourceType.ecommerce,
                card = card2
            };
            
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            var creditObj = new credit
            {
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addCredit(creditObj);

            var creditObj2 = new credit
            {
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                card = card2
            };

            litleBatchRequest.addCredit(creditObj2);

            var echeck = new echeckType
            {
                accType = echeckAccountTypeEnum.Checking,
                accNum = "1099999903",
                routingNum = "011201995",
                checkNum = "123455"
            };

            var billToAddress = new contact
            {
                name = "Bob",
                city = "Lowell",
                state = "MA",
                email = "litle.com"
            };

            var echeckcredit = new echeckCredit
            {
                amount = 12L,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };

            litleBatchRequest.addEcheckCredit(echeckcredit);

            var echeck2 = new echeckType
            {
                accType = echeckAccountTypeEnum.Checking,
                accNum = "1099999903",
                routingNum = "011201995",
                checkNum = "123456"
            };

            var billToAddress2 = new contact
            {
                name = "Mike",
                city = "Lowell",
                state = "MA",
                email = "litle.com"
            };

            var echeckcredit2 = new echeckCredit
            {
                amount = 12L,
                orderId = "12346",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck2,
                billToAddress = billToAddress2
            };

            litleBatchRequest.addEcheckCredit(echeckcredit2);

            var echeckredeposit = new echeckRedeposit
            {
                litleTxnId = 123456,
                echeck = echeck
            };

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);

            var echeckredeposit2 = new echeckRedeposit
            {
                litleTxnId = 123457,
                echeck = echeck2
            };

            litleBatchRequest.addEcheckRedeposit(echeckredeposit2);

            var echeckSaleObj = new echeckSale
            {
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };

            litleBatchRequest.addEcheckSale(echeckSaleObj);

            var echeckSaleObj2 = new echeckSale
            {
                amount = 123456,
                orderId = "12346",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck2,
                billToAddress = billToAddress2
            };

            litleBatchRequest.addEcheckSale(echeckSaleObj2);

            var echeckVerificationObject = new echeckVerification
            {
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);

            var echeckVerificationObject2 = new echeckVerification
            {
                amount = 123456,
                orderId = "12346",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck2,
                billToAddress = billToAddress2
            };

            litleBatchRequest.addEcheckVerification(echeckVerificationObject2);

            var forcecapture = new forceCapture
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addForceCapture(forcecapture);

            var forcecapture2 = new forceCapture
            {
                amount = 106,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                card = card2
            };

            litleBatchRequest.addForceCapture(forcecapture2);

            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addSale(saleObj);

            var saleObj2 = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                card = card2
            };

            litleBatchRequest.addSale(saleObj2);

            var registerTokenRequest = new registerTokenRequestType
            {
                orderId = "12344",
                accountNumber = "1233456789103801",
                reportGroup = "Planets"
            };

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            var registerTokenRequest2 = new registerTokenRequestType
            {
                orderId = "12345",
                accountNumber = "1233456789103801",
                reportGroup = "Planets"
            };

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            litleIc.addBatch(litleBatchRequest);

            var batchName = litleIc.sendToLitle();

            litleIc.blockAndWaitForResponse(batchName, 60*1000*5);

            try
            {
                litleIc.receiveFromLitle(batchName);
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

            var card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4100000000000001",
                expDate = "1210"
            };

            var authorization = new authorization
            {
                reportGroup = "Planets",
                orderId = "12344",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addAuthorization(authorization);

            var card2 = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4242424242424242",
                expDate = "1210"
            };

            var authorization2 = new authorization
            {
                reportGroup = "Planets",
                orderId = "12345",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = card2
            };

            litleBatchRequest.addAuthorization(authorization2);

            var reversal = new authReversal
            {
                litleTxnId = 12345678000L,
                amount = 106,
                payPalNotes = "Notes"
            };

            litleBatchRequest.addAuthReversal(reversal);

            var reversal2 = new authReversal
            {
                litleTxnId = 12345678900L,
                amount = 106,
                payPalNotes = "Notes"
            };

            litleBatchRequest.addAuthReversal(reversal2);

            var capture = new capture
            {
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes"
            };

            litleBatchRequest.addCapture(capture);

            var capture2 = new capture
            {
                litleTxnId = 123456700,
                amount = 106,
                payPalNotes = "Notes"
            };

            var authInfo = new authInformation
            {
                authDate = new DateTime(2002, 10, 9),
                authCode = "543216",
                authAmount = 12345
            };

            litleBatchRequest.addCapture(capture2);

            var capturegivenauth = new captureGivenAuth
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                authInformation = authInfo,
                card = card
            };
            
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth);

            var authInfo2 = new authInformation
            {
                authDate = new DateTime(2003, 10, 9),
                authCode = "543216",
                authAmount = 12345
            };

            var capturegivenauth2 = new captureGivenAuth
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                authInformation = authInfo2,
                card = card2
            };
            
            litleBatchRequest.addCaptureGivenAuth(capturegivenauth2);

            var creditObj = new credit
            {
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addCredit(creditObj);

            var creditObj2 = new credit
            {
                amount = 106,
                orderId = "2111",
                orderSource = orderSourceType.ecommerce,
                card = card2
            };

            litleBatchRequest.addCredit(creditObj2);

            var echeck = new echeckType
            {
                accType = echeckAccountTypeEnum.Checking,
                accNum = "1099999903",
                routingNum = "011201995",
                checkNum = "123455"
            };

            var billToAddress = new contact
            {
                name = "Bob",
                city = "Lowell",
                state = "MA",
                email = "litle.com"
            };

            var echeckcredit = new echeckCredit
            {
                amount = 12L,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };

            litleBatchRequest.addEcheckCredit(echeckcredit);

            var echeck2 = new echeckType
            {
                accType = echeckAccountTypeEnum.Checking,
                accNum = "1099999903",
                routingNum = "011201995",
                checkNum = "123456"
            };

            var billToAddress2 = new contact
            {
                name = "Mike",
                city = "Lowell",
                state = "MA",
                email = "litle.com"
            };

            var echeckcredit2 = new echeckCredit
            {
                amount = 12L,
                orderId = "12346",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck2,
                billToAddress = billToAddress2
            };
            
            litleBatchRequest.addEcheckCredit(echeckcredit2);

            var echeckredeposit = new echeckRedeposit
            {
                litleTxnId = 123456,
                echeck = echeck
            };

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);

            var echeckredeposit2 = new echeckRedeposit
            {
                litleTxnId = 123457,
                echeck = echeck2
            };

            litleBatchRequest.addEcheckRedeposit(echeckredeposit2);

            var echeckSaleObj = new echeckSale
            {
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };

            litleBatchRequest.addEcheckSale(echeckSaleObj);

            var echeckSaleObj2 = new echeckSale
            {
                amount = 123456,
                orderId = "12346",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck2,
                billToAddress = billToAddress2
            };

            litleBatchRequest.addEcheckSale(echeckSaleObj2);

            var echeckVerificationObject = new echeckVerification
            {
                amount = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck,
                billToAddress = billToAddress
            };

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);

            var echeckVerificationObject2 = new echeckVerification
            {
                amount = 123456,
                orderId = "12346",
                orderSource = orderSourceType.ecommerce,
                echeck = echeck2,
                billToAddress = billToAddress2
            };

            litleBatchRequest.addEcheckVerification(echeckVerificationObject2);

            var forcecapture = new forceCapture
            {
                amount = 106,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addForceCapture(forcecapture);

            var forcecapture2 = new forceCapture
            {
                amount = 106,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                card = card2
            };

            litleBatchRequest.addForceCapture(forcecapture2);

            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = card
            };

            litleBatchRequest.addSale(saleObj);

            var saleObj2 = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                orderId = "12345",
                orderSource = orderSourceType.ecommerce,
                card = card2
            };

            litleBatchRequest.addSale(saleObj2);

            var registerTokenRequest = new registerTokenRequestType
            {
                orderId = "12344",
                accountNumber = "1233456789103801",
                reportGroup = "Planets"
            };

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);

            var registerTokenRequest2 = new registerTokenRequestType
            {
                orderId = "12345",
                accountNumber = "1233456789103801",
                reportGroup = "Planets"
            };

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest2);

            litleIsc.addBatch(litleBatchRequest);

            try
            {
                litleIsc.sendToLitle();
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

            var card = new cardType
            {
                type = methodOfPaymentTypeEnum.VI,
                number = "4100000000000001",
                expDate = "1210"
            };

            var authorization = new authorization
            {
                reportGroup = "<ReportGroup>",
                orderId = "12344&'\"",
                amount = 106,
                orderSource = orderSourceType.ecommerce,
                card = card
            };
            
            litleBatchRequest.addAuthorization(authorization);

            _litle.addBatch(litleBatchRequest);

            var batchName = _litle.sendToLitle();

            _litle.blockAndWaitForResponse(batchName, EstimatedResponseTime(2 * 2, 10 * 2));

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

        private static int EstimatedResponseTime(int numAuthsAndSales, int numRest)
        {
            return (int)(5 * 60 * 1000 + 2.5 * 1000 + numAuthsAndSales * (1 / 5) * 1000 + numRest * (1 / 50) * 1000) * 5;
        }
    }
}

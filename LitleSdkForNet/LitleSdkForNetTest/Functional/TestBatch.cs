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
        private string responseDir;
        private LitleBatch litle;

        [TestFixtureSetUp]
        public void setUp()
        {
            //Dictionary<string, string> config = new Dictionary<string, string>();
            //config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            //config.Add("reportGroup", "Default Report Group");
            //config.Add("username", "DOTNET");
            //config.Add("version", "8.13");
            //config.Add("timeout", "65");
            //config.Add("merchantId", "101");
            //config.Add("password", "TESTCASE");
            //config.Add("printxml", "true");

            litle = new LitleBatch();

            string currentPath = Environment.CurrentDirectory.ToString();
            string parentPath = Directory.GetParent(currentPath).ToString();
            responseDir = parentPath + "\\Responses\\";
        }

        [Test]
        public void SimpleBatch()
        {
            litleBatchRequest litleBatchRequest = new litleBatchRequest();

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

            litle.blockAndWaitForResponse(batchName, estimatedResponseTime(2*2, 10*2));

            litleResponse litleResponse = litle.receiveFromLitle(responseDir + batchName, batchName);

            Assert.NotNull(litleResponse);
            Assert.AreEqual("0", litleResponse.response);
            Assert.AreEqual("Valid Format", litleResponse.message);
            //Assert.AreEqual(24, litleResponse.listOfLitleBatchResponse[0]);
        }



        [Test]
        public void nullBatchData()
        {
            litleBatchRequest litleBatchRequest = new litleBatchRequest();

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

            litleBatchRequest.addAuthorization(authorization);
            litleBatchRequest.addAuthorization(null);

            authReversal reversal = new authReversal();
            reversal.litleTxnId = 12345678000L;
            reversal.amount = 106;
            reversal.payPalNotes = "Notes";

            litleBatchRequest.addAuthReversal(reversal);
            litleBatchRequest.addAuthReversal(null);

            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            litleBatchRequest.addCapture(capture);
            litleBatchRequest.addCapture(null);

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
            litleBatchRequest.addCaptureGivenAuth(null);

            credit creditObj = new credit();
            creditObj.amount = 106;
            creditObj.orderId = "2111";
            creditObj.orderSource = orderSourceType.ecommerce;
            creditObj.card = card;

            litleBatchRequest.addCredit(creditObj);
            litleBatchRequest.addCredit(null);

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
            litleBatchRequest.addEcheckCredit(null);

            echeckRedeposit echeckredeposit = new echeckRedeposit();
            echeckredeposit.litleTxnId = 123456;
            echeckredeposit.echeck = echeck;

            litleBatchRequest.addEcheckRedeposit(echeckredeposit);
            litleBatchRequest.addEcheckRedeposit(null);

            echeckSale echeckSaleObj = new echeckSale();
            echeckSaleObj.amount = 123456;
            echeckSaleObj.orderId = "12345";
            echeckSaleObj.orderSource = orderSourceType.ecommerce;
            echeckSaleObj.echeck = echeck;
            echeckSaleObj.billToAddress = billToAddress;

            litleBatchRequest.addEcheckSale(echeckSaleObj);
            litleBatchRequest.addEcheckSale(null);

            echeckVerification echeckVerificationObject = new echeckVerification();
            echeckVerificationObject.amount = 123456;
            echeckVerificationObject.orderId = "12345";
            echeckVerificationObject.orderSource = orderSourceType.ecommerce;
            echeckVerificationObject.echeck = echeck;
            echeckVerificationObject.billToAddress = billToAddress;

            litleBatchRequest.addEcheckVerification(echeckVerificationObject);
            litleBatchRequest.addEcheckVerification(null);

            forceCapture forcecapture = new forceCapture();
            forcecapture.amount = 106;
            forcecapture.orderId = "12344";
            forcecapture.orderSource = orderSourceType.ecommerce;
            forcecapture.card = card;

            litleBatchRequest.addForceCapture(forcecapture);
            litleBatchRequest.addForceCapture(null);

            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            saleObj.card = card;

            litleBatchRequest.addSale(saleObj);
            litleBatchRequest.addSale(null);

            registerTokenRequestType registerTokenRequest = new registerTokenRequestType();
            registerTokenRequest.orderId = "12344";
            registerTokenRequest.accountNumber = "1233456789103801";
            registerTokenRequest.reportGroup = "Planets";

            litleBatchRequest.addRegisterTokenRequest(registerTokenRequest);
            litleBatchRequest.addRegisterTokenRequest(null);

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
            litleBatchRequest litleBatchRequest = new litleBatchRequest();

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

            litleResponse litleResponse = litle.receiveFromLitle(responseDir + batchName, batchName);

            Assert.NotNull(litleResponse);
            Assert.AreEqual("1", litleResponse.response);
            //Assert.AreEqual("Valid Format", litleResponse.message);
        }


        private int estimatedResponseTime(int numAuthsAndSales, int numRest)
        {
            return (int)(5 * 60 * 1000 + 2.5 * 1000 + numAuthsAndSales * (1 / 5) * 1000 + numRest * (1 / 50) * 1000);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using LitleSdkForNet;

namespace LitleSdkForNetTest.Certification
{
    [TestFixture]
    class TestCert3AuthReversal
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void setUp()
        {
            litle = new LitleOnline();
        }

	    [Test]
	    public void test32() {
		    authorization auth = new authorization();
		    auth.orderId = "32";
		    auth.amount = 10010;
		    auth.orderSource = orderSourceType.ecommerce;
		    contact billToAddress = new contact();
		    billToAddress.name = "John Smith";
		    billToAddress.addressLine1 = "1 Main St.";
		    billToAddress.city = "Burlington";
		    billToAddress.state = "MA";
		    billToAddress.zip = "01803-3747";
		    billToAddress.country = countryTypeEnum.US;
		    auth.billToAddress = billToAddress;
		    cardType card = new cardType();
		    card.number = "4457010000000009";
		    card.expDate = "0112";
		    card.cardValidationNum = "349";
		    card.type = methodOfPaymentTypeEnum.VI;
		    auth.card = card;

		    authorizationResponse authorizeResponse = litle.Authorize(auth);
		    Assert.AreEqual("000", authorizeResponse.response);
		    Assert.AreEqual("Approved", authorizeResponse.message);
		    Assert.AreEqual("11111 ", authorizeResponse.authCode);
		    Assert.AreEqual("01", authorizeResponse.fraudResult.avsResult);
		    Assert.AreEqual("M", authorizeResponse.fraudResult.cardValidationResult);

		    capture capture = new capture();
		    capture.litleTxnId = authorizeResponse.litleTxnId;
		    capture.amount = 5005;
		    captureResponse captureResponse = litle.Capture(capture);
		    Assert.AreEqual(captureResponse.message, "000", captureResponse.response);
		    Assert.AreEqual(captureResponse.message, "Approved", captureResponse.message);

		    authReversal reversal = new authReversal();
		    reversal.litleTxnId = authorizeResponse.litleTxnId;
		    authReversalResponse reversalResponse = litle.AuthReversal(reversal);
		    Assert.AreEqual(reversalResponse.message, "111", reversalResponse.response);
		    Assert.AreEqual(reversalResponse.message, "authorization amount has already been depleted", reversalResponse.message);
	    }

	    [Test]
	    public void test33() {
		    authorization auth = new authorization();
		    auth.orderId = "33";
		    auth.amount = 20020;
		    auth.orderSource = orderSourceType.ecommerce;
		    contact billToAddress = new contact();
		    billToAddress.name = "Mike J. Hammer";
		    billToAddress.addressLine1 = "2 Main St.";
		    billToAddress.addressLine2 = "Apt. 222";
		    billToAddress.city = "Riverside";
		    billToAddress.state = "RI";
		    billToAddress.zip = "02915";
		    billToAddress.country = countryTypeEnum.US;
		    auth.billToAddress = billToAddress;
		    cardType card = new cardType();
		    card.number = "5112010000000003";
		    card.expDate = "0212";
		    card.cardValidationNum = "261";
		    card.type = methodOfPaymentTypeEnum.MC;
		    auth.card = card;
		    fraudCheckType fraud = new fraudCheckType();
		    fraud.authenticationValue = "BwABBJQ1AgAAAAAgJDUCAAAAAAA=";
		    auth.cardholderAuthentication = fraud;

		    authorizationResponse authorizeResponse = litle.Authorize(auth);
		    Assert.AreEqual("000", authorizeResponse.response);
		    Assert.AreEqual("Approved", authorizeResponse.message);
		    Assert.AreEqual("22222", authorizeResponse.authCode);
		    Assert.AreEqual("10", authorizeResponse.fraudResult.avsResult);
		    Assert.AreEqual("M", authorizeResponse.fraudResult.cardValidationResult);

		    authReversal reversal = new authReversal();
		    reversal.litleTxnId = authorizeResponse.litleTxnId;
		    authReversalResponse reversalResponse = litle.AuthReversal(reversal);
		    Assert.AreEqual(reversalResponse.message, "000", reversalResponse.response);
		    Assert.AreEqual(reversalResponse.message, "Approved", reversalResponse.message);
	    }

	    [Test]
	    public void test34() {
		    authorization auth = new authorization();
		    auth.orderId = "34";
		    auth.amount = 30030;
		    auth.orderSource = orderSourceType.ecommerce;
		    contact billToAddress = new contact();
		    billToAddress.name = "Eileen Jones";
		    billToAddress.addressLine1 = "3 Main St.";
		    billToAddress.city = "Bloomfield";
		    billToAddress.state = "CT";
		    billToAddress.zip = "06002";
		    billToAddress.country = countryTypeEnum.US;
		    auth.billToAddress = billToAddress;
		    cardType card = new cardType();
		    card.number = "6011010000000003";
		    card.expDate = "0312";
		    card.cardValidationNum = "758";
		    card.type = methodOfPaymentTypeEnum.DI;
		    auth.card = card;

		    authorizationResponse authorizeResponse = litle.Authorize(auth);
		    Assert.AreEqual("000", authorizeResponse.response);
		    Assert.AreEqual("Approved", authorizeResponse.message);
		    Assert.AreEqual("33333", authorizeResponse.authCode);
		    Assert.AreEqual("10", authorizeResponse.fraudResult.avsResult);
		    Assert.AreEqual("M", authorizeResponse.fraudResult.cardValidationResult);

		    authReversal reversal = new authReversal();
		    reversal.litleTxnId = authorizeResponse.litleTxnId;
		    authReversalResponse reversalResponse = litle.AuthReversal(reversal);
		    Assert.AreEqual(reversalResponse.message, "000", reversalResponse.response);
		    Assert.AreEqual(reversalResponse.message, "Approved", reversalResponse.message);
	    }

	    [Test]
	    public void test35() {
		    authorization auth = new authorization();
		    auth.orderId = "35";
		    auth.amount = 40040;
		    auth.orderSource = orderSourceType.ecommerce;
		    contact billToAddress = new contact();
		    billToAddress.name = "Bob Black";
		    billToAddress.addressLine1 = "4 Main St.";
		    billToAddress.city = "Laurel";
		    billToAddress.state = "MD";
		    billToAddress.zip = "20708";
		    billToAddress.country = countryTypeEnum.US;
		    auth.billToAddress = billToAddress;
		    cardType card = new cardType();
		    card.number = "375001000000005";
		    card.expDate = "0412";
		    card.type = methodOfPaymentTypeEnum.AX;
		    auth.card = card;

		    authorizationResponse authorizeResponse = litle.Authorize(auth);
		    Assert.AreEqual("000", authorizeResponse.response);
		    Assert.AreEqual("Approved", authorizeResponse.message);
		    Assert.AreEqual("44444", authorizeResponse.authCode);
		    Assert.AreEqual("12", authorizeResponse.fraudResult.avsResult);

		    capture capture = new capture();
		    capture.litleTxnId = authorizeResponse.litleTxnId;
		    capture.amount = 20020;
		    captureResponse captureResponse = litle.Capture(capture);
		    Assert.AreEqual(captureResponse.message, "000", captureResponse.response);
		    Assert.AreEqual(captureResponse.message, "Approved", captureResponse.message);

		    authReversal reversal = new authReversal();
		    reversal.litleTxnId = authorizeResponse.litleTxnId;
		    reversal.amount = 20020;
		    authReversalResponse reversalResponse = litle.AuthReversal(reversal);
		    Assert.AreEqual(reversalResponse.message, "000", reversalResponse.response);
		    Assert.AreEqual(reversalResponse.message, "Approved", reversalResponse.message);
	    }

	    [Test]
	    public void test36() {
		    authorization auth = new authorization();
		    auth.orderId = "36";
		    auth.amount = 20500;
		    auth.orderSource = orderSourceType.ecommerce;
		    cardType card = new cardType();
		    card.number = "375000026600004";
		    card.expDate = "0512";
		    card.type = methodOfPaymentTypeEnum.AX;
		    auth.card = card;

		    authorizationResponse authorizeResponse = litle.Authorize(auth);
		    Assert.AreEqual("000", authorizeResponse.response);
		    Assert.AreEqual("Approved", authorizeResponse.message);

		    authReversal reversal = new authReversal();
		    reversal.litleTxnId = authorizeResponse.litleTxnId;
		    reversal.amount = 10000;
		    authReversalResponse reversalResponse = litle.AuthReversal(reversal);
		    Assert.AreEqual(reversalResponse.message, "336", reversalResponse.response);
		    Assert.AreEqual(reversalResponse.message, "Reversal Amount does not match authorization amount", reversalResponse.message);
	    }

            
    }
}

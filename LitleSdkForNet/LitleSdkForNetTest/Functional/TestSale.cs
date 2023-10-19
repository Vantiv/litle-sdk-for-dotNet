﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestSale
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void setUp()
        {
            Dictionary<string, string> config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "11.0"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };
            litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleSaleWithTaxTypeIdentifier()
        {
            var saleObj = new sale
            {
                amount = 106,
                litleTxnId = 123456,
                id = "1",
                orderId = "12344",
                orderSource = orderSourceType.ecommerce,
                card = new cardType
                {
                    type = methodOfPaymentTypeEnum.VI,
                    number = "4100000000000000",
                    expDate = "1210"
                },
                enhancedData = new enhancedData
                {
                    customerReference = "000000008110801",
                    salesTax = 23,
                    deliveryType = enhancedDataDeliveryType.DIG,
                    taxExempt = false,
                    detailTaxes = new List<detailTax>()


                }
            };

            var myDetailTax = new detailTax();
            myDetailTax.taxIncludedInTotal = true;
            myDetailTax.taxAmount = 23;
            myDetailTax.taxTypeIdentifier = taxTypeIdentifierEnum.Item00;
            myDetailTax.cardAcceptorTaxId = "58-1942497";
            saleObj.enhancedData.detailTaxes.Add(myDetailTax);

            var responseObj = litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithCard()
        {
            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            cardType cardObj = new cardType();
            cardObj.type = methodOfPaymentTypeEnum.VI;
            cardObj.number = "4100000000000000";
            cardObj.expDate = "1210";
            saleObj.card = cardObj;

            saleResponse responseObj = litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithMpos()
        {
            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            mposType mpos = new mposType();
            mpos.ksn = "77853211300008E00016";
            mpos.encryptedTrack = "CASE1E185EADD6AFE78C9A214B21313DCD836FDD555FBE3A6C48D141FE80AB9172B963265AFF72111895FE415DEDA162CE8CB7AC4D91EDB611A2AB756AA9CB1A000000000000000000000000000000005A7AAF5E8885A9DB88ECD2430C497003F2646619A2382FFF205767492306AC804E8E64E8EA6981DD";
            mpos.formatId = "30";
            mpos.track1Status = 0;
            mpos.track2Status = 0; ;
            saleObj.mpos = mpos;

            saleResponse responseObj = litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithPayPal()
        {
            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            payPal payPalObj = new payPal();
            payPalObj.payerId = "1234";
            payPalObj.token = "1234";
            payPalObj.transactionId = "123456";
            saleObj.paypal = payPalObj;
            saleResponse responseObj = litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }

        [Test]
        public void SimpleSaleWithApplepayAndSecondaryAmountAndWallet()
        {
            sale saleObj = new sale();
            saleObj.amount = 110;
            saleObj.secondaryAmount = 50;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            applepayType applepay = new applepayType();
            applepayHeaderType applepayHeaderType = new applepayHeaderType();
            applepayHeaderType.applicationData = "454657413164";
            applepayHeaderType.ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            applepayHeaderType.publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            applepayHeaderType.transactionId = "1234";
            applepay.header = applepayHeaderType;
            applepay.data = "user";
            applepay.signature = "sign";
            applepay.version = "12345";
            saleObj.applepay = applepay;
            wallet wallet = new wallet();
            wallet.walletSourceTypeId = "123";
            wallet.walletSourceType = walletWalletSourceType.MasterPass;
            saleObj.wallet = wallet;
            saleResponse responseObj = litle.Sale(saleObj);
            Assert.AreEqual("Insufficient Funds", responseObj.message);
            Assert.AreEqual("110", responseObj.applepayResponse.transactionAmount);
        }
        [Test]
        public void SimpleSaleWithChanges8_32And8_33()
        {
            sale saleObj = new sale();
            saleObj.amount = 106;
            saleObj.litleTxnId = 123456;
            saleObj.orderId = "12344";
            saleObj.orderSource = orderSourceType.ecommerce;
            contact contact = new contact();
            contact.name = "John & Jane Smith";
            contact.addressLine1 = "1 Main St.";
            contact.city = "Burlington";
            contact.state = "MA";
            contact.zip = "01803-3747";
            contact.country = countryTypeEnum.US;
            saleObj.retailerAddress = contact;
            additionalCOFData additionalCOFData = new additionalCOFData();
            additionalCOFData.totalPaymentCount = "35";
            additionalCOFData.paymentType = paymentTypeEnum.Fixed_Amount;
            additionalCOFData.uniqueId = "12345wereew233";
            additionalCOFData.frequencyOfMIT = frequencyOfMITEnum.BiWeekly;
            additionalCOFData.validationReference = "re3298rhriw4wrw";
            additionalCOFData.sequenceIndicator = 2;
            saleObj.additionalCOFData = additionalCOFData;
            cardType cardObj = new cardType();
            cardObj.type = methodOfPaymentTypeEnum.VI;
            cardObj.number = "4100000000000000";
            cardObj.expDate = "1210";
            saleObj.card = cardObj;
            saleObj.merchantCategoryCode = "1234";
            saleObj.BusinessIndicator = businessIndicatorEnum.consumerBillPayment;
            saleObj.crypto = true;
            saleObj.foreignRetailerIndicator = foreignRetailerIndicatorEnum.F;
            saleResponse responseObj = litle.Sale(saleObj);
            StringAssert.AreEqualIgnoringCase("Approved", responseObj.message);
        }


    }
}

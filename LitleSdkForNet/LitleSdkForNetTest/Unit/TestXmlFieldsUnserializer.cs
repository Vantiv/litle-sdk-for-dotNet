using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestXmlFieldsUnserializer
    {

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
        }

        [Test]
        public void TestAuthorizationResponseContainsGiftCardResponse()
        {
            String xml = "<authorizationResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></authorizationResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(authorizationResponse));
            StringReader reader = new StringReader(xml);
            authorizationResponse authorizationResponse = (authorizationResponse)serializer.Deserialize(reader);

            Assert.NotNull(authorizationResponse.giftCardResponse);
        }

        // CES: Commenting this out because AuthReversal no longer uses giftCardResponse
        
        /*
        [Test]
        public void TestAuthReversalResponseContainsGiftCardResponse()
        {
            String xml = "<authReversalResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></authReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(authReversalResponse));
            StringReader reader = new StringReader(xml);
            authReversalResponse authReversalResponse = (authReversalResponse)serializer.Deserialize(reader);

            Assert.NotNull(authReversalResponse.giftCardResponse);
        }
        */

        [Test]
        public void TestgiftCardAuthReversalResponseContainsGiftCardResponse()
        {
            String xml = "<giftCardAuthReversalResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></giftCardAuthReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(giftCardAuthReversalResponse));
            StringReader reader = new StringReader(xml);
            giftCardAuthReversalResponse giftCardAuthReversalResponse = (giftCardAuthReversalResponse)serializer.Deserialize(reader);

            Assert.NotNull(giftCardAuthReversalResponse.giftCardResponse);
        }

        // CES: Commenting this out because captureResponse no longer uses giftCardResponse
        // I will add a test for giftCardCapture
        /*
        [Test]
        public void TestCaptureResponseContainsGiftCardResponse()
        {
            String xml = "<captureResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></captureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureResponse));
            StringReader reader = new StringReader(xml);
            captureResponse captureResponse = (captureResponse)serializer.Deserialize(reader);

            Assert.NotNull(captureResponse.giftCardResponse);
        }
        */

        [Test]
        public void TestCaptureResponseContainsFraudResult()
        {
            String xml = "<captureResponse xmlns=\"http://www.litle.com/schema\"><fraudResult></fraudResult></captureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureResponse));
            StringReader reader = new StringReader(xml);
            captureResponse captureResponse = (captureResponse)serializer.Deserialize(reader);

            Assert.NotNull(captureResponse.fraudResult);
        }

        [Test]
        public void TestForceCaptureResponseContainsGiftCardResponse()
        {
            String xml = "<forceCaptureResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></forceCaptureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(forceCaptureResponse));
            StringReader reader = new StringReader(xml);
            forceCaptureResponse forceCaptureResponse = (forceCaptureResponse)serializer.Deserialize(reader);

            Assert.NotNull(forceCaptureResponse.giftCardResponse);
        }

        [Test]
        public void TestForceCaptureResponseContainsFraudResult()
        {
            String xml = "<forceCaptureResponse xmlns=\"http://www.litle.com/schema\"><fraudResult></fraudResult></forceCaptureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(forceCaptureResponse));
            StringReader reader = new StringReader(xml);
            forceCaptureResponse forceCaptureResponse = (forceCaptureResponse)serializer.Deserialize(reader);

            Assert.NotNull(forceCaptureResponse.fraudResult);
        }

        [Test]
        public void TestCaptureGivenAuthResponseContainsGiftCardResponse()
        {
            String xml = "<captureGivenAuthResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></captureGivenAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureGivenAuthResponse));
            StringReader reader = new StringReader(xml);
            captureGivenAuthResponse captureGivenAuthResponse = (captureGivenAuthResponse)serializer.Deserialize(reader);

            Assert.NotNull(captureGivenAuthResponse.giftCardResponse);
        }

        [Test]
        public void TestCaptureGivenAuthResponseContainsFraudResult()
        {
            String xml = "<captureGivenAuthResponse xmlns=\"http://www.litle.com/schema\"><fraudResult></fraudResult></captureGivenAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureGivenAuthResponse));
            StringReader reader = new StringReader(xml);
            captureGivenAuthResponse captureGivenAuthResponse = (captureGivenAuthResponse)serializer.Deserialize(reader);

            Assert.NotNull(captureGivenAuthResponse.fraudResult);
        }

        [Test]
        public void TestSaleResponseContainsGiftCardResponse()
        {
            String xml = "<saleResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></saleResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(saleResponse));
            StringReader reader = new StringReader(xml);
            saleResponse saleResponse = (saleResponse)serializer.Deserialize(reader);

            Assert.NotNull(saleResponse.giftCardResponse);
        }

        // Gift card response is now its own class
        //[Test]
        //public void TestCreditResponseContainsGiftCardResponse()
        //{
        //    String xml = "<creditResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></creditResponse>";
        //    XmlSerializer serializer = new XmlSerializer(typeof(creditResponse));
        //    StringReader reader = new StringReader(xml);
        //    creditResponse creditResponse = (creditResponse)serializer.Deserialize(reader);

        //    Assert.NotNull(creditResponse.giftCardResponse);
        //}

        [Test]
        public void TestCreditResponseContainsFraudResult()
        {
            String xml = "<creditResponse xmlns=\"http://www.litle.com/schema\"><fraudResult></fraudResult></creditResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(creditResponse));
            StringReader reader = new StringReader(xml);
            creditResponse creditResponse = (creditResponse)serializer.Deserialize(reader);

            Assert.NotNull(creditResponse.fraudResult);
        }

        [Test]
        public void TestActivateResponse()
        {
            String xml = "<activateResponse reportGroup=\"A\" id=\"3\" customerId=\"4\"  xmlns=\"http://www.litle.com/schema\"><response>000</response><litleTxnId>1</litleTxnId><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></activateResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(activateResponse));
            StringReader reader = new StringReader(xml);
            activateResponse activateResponse = (activateResponse)serializer.Deserialize(reader);

            Assert.AreEqual("A", activateResponse.reportGroup);
            Assert.AreEqual("4", activateResponse.customerId);
            Assert.AreEqual(1, activateResponse.litleTxnId);
            Assert.AreEqual("000", activateResponse.response);
            Assert.AreEqual(new DateTime(2013,9,5,14,23,45), activateResponse.responseTime);
            Assert.AreEqual(new DateTime(2013,9,5), activateResponse.postDate);
            Assert.AreEqual("Approved", activateResponse.message);
            Assert.NotNull(activateResponse.fraudResult);
            Assert.NotNull(activateResponse.giftCardResponse);
        }

        [Test]
        public void TestLoadResponse()
        {
            String xml = "<loadResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" duplicate=\"true\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>1</litleTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></loadResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(loadResponse));
            StringReader reader = new StringReader(xml);
            loadResponse loadResponse = (loadResponse)serializer.Deserialize(reader);

            Assert.AreEqual("A", loadResponse.reportGroup);
            Assert.AreEqual("3", loadResponse.id);
            Assert.AreEqual("4", loadResponse.customerId);
            Assert.AreEqual(1, loadResponse.litleTxnId);
            Assert.AreEqual("000", loadResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), loadResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), loadResponse.postDate);
            Assert.AreEqual("Approved", loadResponse.message);
            Assert.NotNull(loadResponse.fraudResult);
            Assert.NotNull(loadResponse.giftCardResponse);
        }

        [Test]
        public void TestUnloadResponse()
        {
            String xml = "<unloadResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" duplicate=\"true\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>1</litleTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></unloadResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(unloadResponse));
            StringReader reader = new StringReader(xml);
            unloadResponse unloadResponse = (unloadResponse)serializer.Deserialize(reader);

            Assert.AreEqual("A", unloadResponse.reportGroup);
            Assert.AreEqual("4", unloadResponse.customerId);
            Assert.AreEqual(1, unloadResponse.litleTxnId);
            Assert.AreEqual("000", unloadResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), unloadResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), unloadResponse.postDate);
            Assert.AreEqual("Approved", unloadResponse.message);
            Assert.NotNull(unloadResponse.fraudResult);
            Assert.NotNull(unloadResponse.giftCardResponse);
        }

        [Test]
        public void TestGiftCardResponse()
        {
            String xml = "<balanceInquiryResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" xmlns=\"http://www.litle.com/schema\"><giftCardResponse><availableBalance>1</availableBalance><beginningBalance>2</beginningBalance><endingBalance>3</endingBalance><cashBackAmount>4</cashBackAmount></giftCardResponse></balanceInquiryResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(balanceInquiryResponse));
            StringReader reader = new StringReader(xml);
            balanceInquiryResponse balanceInquiryResponse = (balanceInquiryResponse)serializer.Deserialize(reader);
            giftCardResponse giftCardResponse = balanceInquiryResponse.giftCardResponse;

            Assert.AreEqual("1", giftCardResponse.availableBalance);
            Assert.AreEqual("2", giftCardResponse.beginningBalance);
            Assert.AreEqual("3", giftCardResponse.endingBalance);
            Assert.AreEqual("4", giftCardResponse.cashBackAmount);
        }

        [Test]
        public void TestBalanceInquiryResponse()
        {
            String xml = "<balanceInquiryResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>1</litleTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></balanceInquiryResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(balanceInquiryResponse));
            StringReader reader = new StringReader(xml);
            balanceInquiryResponse balanceInquiryResponse = (balanceInquiryResponse)serializer.Deserialize(reader);

            Assert.AreEqual("A", balanceInquiryResponse.reportGroup);
            Assert.AreEqual("3", balanceInquiryResponse.id);
            Assert.AreEqual("4", balanceInquiryResponse.customerId);
            Assert.AreEqual(1, balanceInquiryResponse.litleTxnId);
            Assert.AreEqual("000", balanceInquiryResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), balanceInquiryResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), balanceInquiryResponse.postDate);
            Assert.AreEqual("Approved", balanceInquiryResponse.message);
            Assert.NotNull(balanceInquiryResponse.fraudResult);
            Assert.NotNull(balanceInquiryResponse.giftCardResponse);
        }

        [Test]
        public void TestDeactivateResponse()
        {
            String xml = "<deactivateResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>1</litleTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></deactivateResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(deactivateResponse));
            StringReader reader = new StringReader(xml);
            deactivateResponse deactivateResponse = (deactivateResponse)serializer.Deserialize(reader);

            Assert.AreEqual("A", deactivateResponse.reportGroup);
            Assert.AreEqual("3", deactivateResponse.id);
            Assert.AreEqual("4", deactivateResponse.customerId);
            Assert.AreEqual(1, deactivateResponse.litleTxnId);
            Assert.AreEqual("000", deactivateResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), deactivateResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), deactivateResponse.postDate);
            Assert.AreEqual("Approved", deactivateResponse.message);
            Assert.NotNull(deactivateResponse.fraudResult);
            Assert.NotNull(deactivateResponse.giftCardResponse);
        }

        [Test]
        public void TestCreatePlanResponse()
        {
            String xml = @"
<createPlanResponse xmlns=""http://www.litle.com/schema"">
<litleTxnId>1</litleTxnId>
<response>000</response>
<message>Approved</message>
<responseTime>2013-09-05T14:23:45</responseTime>
<planCode>thePlan</planCode>
</createPlanResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(createPlanResponse));
            StringReader reader = new StringReader(xml);
            createPlanResponse createPlanResponse = (createPlanResponse)serializer.Deserialize(reader);

            Assert.AreEqual("1", createPlanResponse.litleTxnId);
            Assert.AreEqual("000", createPlanResponse.response);
            Assert.AreEqual("Approved", createPlanResponse.message);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), createPlanResponse.responseTime);
            Assert.AreEqual("thePlan", createPlanResponse.planCode);
        }

        [Test]
        public void TestUpdatePlanResponse()
        {
            String xml = @"
<updatePlanResponse xmlns=""http://www.litle.com/schema"">
<litleTxnId>1</litleTxnId>
<response>000</response>
<message>Approved</message>
<responseTime>2013-09-05T14:23:45</responseTime>
<planCode>thePlan</planCode>
</updatePlanResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(updatePlanResponse));
            StringReader reader = new StringReader(xml);
            updatePlanResponse updatePlanResponse = (updatePlanResponse)serializer.Deserialize(reader);

            Assert.AreEqual("1", updatePlanResponse.litleTxnId);
            Assert.AreEqual("000", updatePlanResponse.response);
            Assert.AreEqual("Approved", updatePlanResponse.message);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), updatePlanResponse.responseTime);
            Assert.AreEqual("thePlan", updatePlanResponse.planCode);
        }

        [Test]
        public void TestUpdateSubscriptionResponseCanContainTokenResponse()
        {
            String xml = @"
<updateSubscriptionResponse xmlns=""http://www.litle.com/schema"">
<litleTxnId>1</litleTxnId>
<response>000</response>
<message>Approved</message>
<responseTime>2013-09-05T14:23:45</responseTime>
<subscriptionId>123</subscriptionId>
<tokenResponse>
<litleToken>123456</litleToken>
</tokenResponse>
</updateSubscriptionResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(updateSubscriptionResponse));
            StringReader reader = new StringReader(xml);
            updateSubscriptionResponse updateSubscriptionResponse = (updateSubscriptionResponse)serializer.Deserialize(reader);
            Assert.AreEqual("123", updateSubscriptionResponse.subscriptionId);
            Assert.AreEqual("123456", updateSubscriptionResponse.tokenResponse.litleToken);
        }

        [Test]
        public void TestEnhancedAuthResponseCanContainVirtualAccountNumber()
        {
            String xml = @"
<enhancedAuthResponse xmlns=""http://www.litle.com/schema"">
<virtualAccountNumber>true</virtualAccountNumber>
</enhancedAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(enhancedAuthResponse));
            StringReader reader = new StringReader(xml);
            enhancedAuthResponse enhancedAuthResponse = (enhancedAuthResponse)serializer.Deserialize(reader);
            Assert.IsTrue(enhancedAuthResponse.virtualAccountNumber);
        }

        [Test]
        public void TestEnhancedAuthResponseWithCardProductType()
        {
            String xml = @"
<enhancedAuthResponse xmlns=""http://www.litle.com/schema"">
<virtualAccountNumber>true</virtualAccountNumber>
<cardProductType>COMMERCIAL</cardProductType>
</enhancedAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(enhancedAuthResponse));
            StringReader reader = new StringReader(xml);
            enhancedAuthResponse enhancedAuthResponse = (enhancedAuthResponse)serializer.Deserialize(reader);
            Assert.IsTrue(enhancedAuthResponse.virtualAccountNumber);
            Assert.AreEqual(cardProductTypeEnum.COMMERCIAL, enhancedAuthResponse.cardProductType);
        }

        [Test]
        public void TestEnhancedAuthResponseWithNullableEnumFields()
        {
            String xml = @"
<enhancedAuthResponse xmlns=""http://www.litle.com/schema"">
<virtualAccountNumber>1</virtualAccountNumber>
</enhancedAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(enhancedAuthResponse));
            StringReader reader = new StringReader(xml);
            enhancedAuthResponse enhancedAuthResponse = (enhancedAuthResponse)serializer.Deserialize(reader);
            Assert.IsTrue(enhancedAuthResponse.virtualAccountNumber);
            Assert.IsNull(enhancedAuthResponse.cardProductType);
            Assert.IsNull(enhancedAuthResponse.affluence);
        }

        //        [Test]
        //        public void TestAuthReversalResponseCanContainGiftCardResponse()
        //        {
        //            String xml = @"
        //<authReversalResponse xmlns=""http://www.litle.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
        //<litleTxnId>1</litleTxnId>
        //<orderId>2</orderId>
        //<response>000</response>
        //<responseTime>2013-09-05T14:23:45</responseTime>
        //<postDate>2013-09-05</postDate>
        //<message>Foo</message>
        //<giftCardResponse>
        //<availableBalance>5</availableBalance>
        //</giftCardResponse>
        //</authReversalResponse>";
        //            XmlSerializer serializer = new XmlSerializer(typeof(authReversalResponse));
        //            StringReader reader = new StringReader(xml);
        //            authReversalResponse authReversalResponse = (authReversalResponse)serializer.Deserialize(reader);
        //            Assert.AreEqual("theId", authReversalResponse.id);
        //            Assert.AreEqual("theCustomerId", authReversalResponse.customerId);
        //            Assert.AreEqual("theReportGroup", authReversalResponse.reportGroup);
        //            Assert.AreEqual(1, authReversalResponse.litleTxnId);
        //            Assert.AreEqual("000", authReversalResponse.response);
        //            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), authReversalResponse.responseTime);
        //            Assert.AreEqual(new DateTime(2013, 9, 5), authReversalResponse.postDate);
        //            Assert.AreEqual("Foo", authReversalResponse.message);
        //            Assert.AreEqual("5", authReversalResponse.giftCardResponse.availableBalance);
        //        }

        [Test]
        public void TestgiftCardAuthReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<giftCardAuthReversalResponse xmlns=""http://www.litle.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>1</litleTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</giftCardAuthReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(giftCardAuthReversalResponse));
            StringReader reader = new StringReader(xml);
            giftCardAuthReversalResponse giftCardAuthReversalResponse = (giftCardAuthReversalResponse)serializer.Deserialize(reader);
            Assert.AreEqual("theId", giftCardAuthReversalResponse.id);
            Assert.AreEqual("theCustomerId", giftCardAuthReversalResponse.customerId);
            Assert.AreEqual("theReportGroup", giftCardAuthReversalResponse.reportGroup);
            Assert.AreEqual(1, giftCardAuthReversalResponse.litleTxnId);
            Assert.AreEqual("000", giftCardAuthReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), giftCardAuthReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), giftCardAuthReversalResponse.postDate);
            Assert.AreEqual("Foo", giftCardAuthReversalResponse.message);
            Assert.AreEqual("5", giftCardAuthReversalResponse.giftCardResponse.availableBalance);
        }

        [Test]
        public void TestDepositReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<depositReversalResponse xmlns=""http://www.litle.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>1</litleTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</depositReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(depositReversalResponse));
            StringReader reader = new StringReader(xml);
            depositReversalResponse depositReversalResponse = (depositReversalResponse)serializer.Deserialize(reader);
            Assert.AreEqual("theId", depositReversalResponse.id);
            Assert.AreEqual("theCustomerId", depositReversalResponse.customerId);
            Assert.AreEqual("theReportGroup", depositReversalResponse.reportGroup);
            Assert.AreEqual(1, depositReversalResponse.litleTxnId);
            Assert.AreEqual("000", depositReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), depositReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), depositReversalResponse.postDate);
            Assert.AreEqual("Foo", depositReversalResponse.message);
            Assert.AreEqual("5", depositReversalResponse.giftCardResponse.availableBalance);
        }

        [Test]
        public void TestActivateReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<activateReversalResponse xmlns=""http://www.litle.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>1</litleTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</activateReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(activateReversalResponse));
            StringReader reader = new StringReader(xml);
            activateReversalResponse activateReversalResponse = (activateReversalResponse)serializer.Deserialize(reader);
            Assert.AreEqual("theId", activateReversalResponse.id);
            Assert.AreEqual("theCustomerId", activateReversalResponse.customerId);
            Assert.AreEqual("theReportGroup", activateReversalResponse.reportGroup);
            Assert.AreEqual(1, activateReversalResponse.litleTxnId);
            Assert.AreEqual("000", activateReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), activateReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), activateReversalResponse.postDate);
            Assert.AreEqual("Foo", activateReversalResponse.message);
            Assert.AreEqual("5", activateReversalResponse.giftCardResponse.availableBalance);
        }

        [Test]
        public void TestDeactivateReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<deactivateReversalResponse xmlns=""http://www.litle.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>1</litleTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</deactivateReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(deactivateReversalResponse));
            StringReader reader = new StringReader(xml);
            deactivateReversalResponse deactivateReversalResponse = (deactivateReversalResponse)serializer.Deserialize(reader);
            Assert.AreEqual("theId", deactivateReversalResponse.id);
            Assert.AreEqual("theCustomerId", deactivateReversalResponse.customerId);
            Assert.AreEqual("theReportGroup", deactivateReversalResponse.reportGroup);
            Assert.AreEqual(1, deactivateReversalResponse.litleTxnId);
            Assert.AreEqual("000", deactivateReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), deactivateReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), deactivateReversalResponse.postDate);
            Assert.AreEqual("Foo", deactivateReversalResponse.message);
            Assert.AreEqual("5", deactivateReversalResponse.giftCardResponse.availableBalance);
        }

        [Test]
        public void TestLoadReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<loadReversalResponse xmlns=""http://www.litle.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>1</litleTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</loadReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(loadReversalResponse));
            StringReader reader = new StringReader(xml);
            loadReversalResponse loadReversalResponse = (loadReversalResponse)serializer.Deserialize(reader);
            Assert.AreEqual("theId", loadReversalResponse.id);
            Assert.AreEqual("theCustomerId", loadReversalResponse.customerId);
            Assert.AreEqual("theReportGroup", loadReversalResponse.reportGroup);
            Assert.AreEqual(1, loadReversalResponse.litleTxnId);
            Assert.AreEqual("000", loadReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), loadReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), loadReversalResponse.postDate);
            Assert.AreEqual("Foo", loadReversalResponse.message);
            Assert.AreEqual("5", loadReversalResponse.giftCardResponse.availableBalance);
        }

        [Test]
        public void TestUnloadReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<unloadReversalResponse xmlns=""http://www.litle.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>1</litleTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</unloadReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(unloadReversalResponse));
            StringReader reader = new StringReader(xml);
            unloadReversalResponse unloadReversalResponse = (unloadReversalResponse)serializer.Deserialize(reader);
            Assert.AreEqual("theId", unloadReversalResponse.id);
            Assert.AreEqual("theCustomerId", unloadReversalResponse.customerId);
            Assert.AreEqual("theReportGroup", unloadReversalResponse.reportGroup);
            Assert.AreEqual(1, unloadReversalResponse.litleTxnId);
            Assert.AreEqual("000", unloadReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), unloadReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), unloadReversalResponse.postDate);
            Assert.AreEqual("Foo", unloadReversalResponse.message);
            Assert.AreEqual("5", unloadReversalResponse.giftCardResponse.availableBalance);
        }

        [Test]
        public void TestActivateResponseCanContainVirtualGiftCardResponse()
        {
            String xml = @"
<activateResponse reportGroup=""A"" id=""3"" customerId=""4"" duplicate=""true"" xmlns=""http://www.litle.com/schema"">
<litleTxnId>1</litleTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<message>Approved</message>
<virtualGiftCardResponse>
<accountNumber>123</accountNumber>
</virtualGiftCardResponse>
</activateResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(activateResponse));
            StringReader reader = new StringReader(xml);
            activateResponse activateResponse = (activateResponse)serializer.Deserialize(reader);

            Assert.AreEqual("123",activateResponse.virtualGiftCardResponse.accountNumber);
        }

        [Test]
        public void TestVirtualGiftCardResponse()
        {
            String xml = @"
<activateResponse reportGroup=""A"" id=""3"" customerId=""4"" duplicate=""true"" xmlns=""http://www.litle.com/schema"">
<litleTxnId>1</litleTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<message>Approved</message>
<virtualGiftCardResponse>
<accountNumber>123</accountNumber>
<cardValidationNum>abc</cardValidationNum>
</virtualGiftCardResponse>
</activateResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(activateResponse));
            StringReader reader = new StringReader(xml);
            activateResponse activateResponse = (activateResponse)serializer.Deserialize(reader);

            Assert.AreEqual("123", activateResponse.virtualGiftCardResponse.accountNumber);
            Assert.AreEqual("abc", activateResponse.virtualGiftCardResponse.cardValidationNum);
        }

        [Test]
        public void TestAccountUpdaterResponse()
        {
            String xml = @"
<authorizationResponse xmlns=""http://www.litle.com/schema"">
<accountUpdater>
<extendedCardResponse>
<message>TheMessage</message>
<code>TheCode</code>
</extendedCardResponse>
<newCardInfo>
<type>VI</type>
<number>4100000000000000</number>
<expDate>1000</expDate>
</newCardInfo>
<originalCardInfo>
<type>MC</type>
<number>5300000000000000</number>
<expDate>1100</expDate>
</originalCardInfo>
</accountUpdater>
</authorizationResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(authorizationResponse));
            StringReader reader = new StringReader(xml);
            authorizationResponse authorizationResponse = (authorizationResponse)serializer.Deserialize(reader);
            Assert.AreEqual("TheMessage", authorizationResponse.accountUpdater.extendedCardResponse.message);
            Assert.AreEqual("TheCode", authorizationResponse.accountUpdater.extendedCardResponse.code);
            Assert.AreEqual(methodOfPaymentTypeEnum.VI, authorizationResponse.accountUpdater.newCardInfo.type);
            Assert.AreEqual("4100000000000000", authorizationResponse.accountUpdater.newCardInfo.number);
            Assert.AreEqual("1000", authorizationResponse.accountUpdater.newCardInfo.expDate);
            Assert.AreEqual(methodOfPaymentTypeEnum.MC, authorizationResponse.accountUpdater.originalCardInfo.type);
            Assert.AreEqual("5300000000000000", authorizationResponse.accountUpdater.originalCardInfo.number);
            Assert.AreEqual("1100", authorizationResponse.accountUpdater.originalCardInfo.expDate);
        }
    }
}

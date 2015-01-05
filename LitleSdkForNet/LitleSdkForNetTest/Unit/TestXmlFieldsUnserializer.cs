using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;


namespace Litle.Sdk.Test.Unit
{
    [TestClass]
    class TestXmlFieldsUnserializer
    {

        [TestInitialize]
        public void SetUpLitle()
        {
        }

        [TestMethod]
        public void TestAuthorizationResponseContainsGiftCardResponse()
        {
            String xml = "<authorizationResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></authorizationResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(authorizationResponse));
            StringReader reader = new StringReader(xml);
            authorizationResponse authorizationResponse = (authorizationResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(authorizationResponse.giftCardResponse);
        }

        [TestMethod]
        public void TestAuthReversalResponseContainsGiftCardResponse()
        {
            String xml = "<authReversalResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></authReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(authReversalResponse));
            StringReader reader = new StringReader(xml);
            authReversalResponse authReversalResponse = (authReversalResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(authReversalResponse.giftCardResponse);
        }

        [TestMethod]
        public void TestCaptureResponseContainsGiftCardResponse()
        {
            String xml = "<captureResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></captureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureResponse));
            StringReader reader = new StringReader(xml);
            captureResponse captureResponse = (captureResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(captureResponse.giftCardResponse);
        }

        [TestMethod]
        public void TestCaptureResponseContainsFraudResult()
        {
            String xml = "<captureResponse xmlns=\"http://www.litle.com/schema\"><fraudResult></fraudResult></captureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureResponse));
            StringReader reader = new StringReader(xml);
            captureResponse captureResponse = (captureResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(captureResponse.fraudResult);
        }

        [TestMethod]
        public void TestForceCaptureResponseContainsGiftCardResponse()
        {
            String xml = "<forceCaptureResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></forceCaptureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(forceCaptureResponse));
            StringReader reader = new StringReader(xml);
            forceCaptureResponse forceCaptureResponse = (forceCaptureResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(forceCaptureResponse.giftCardResponse);
        }

        [TestMethod]
        public void TestForceCaptureResponseContainsFraudResult()
        {
            String xml = "<forceCaptureResponse xmlns=\"http://www.litle.com/schema\"><fraudResult></fraudResult></forceCaptureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(forceCaptureResponse));
            StringReader reader = new StringReader(xml);
            forceCaptureResponse forceCaptureResponse = (forceCaptureResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(forceCaptureResponse.fraudResult);
        }

        [TestMethod]
        public void TestCaptureGivenAuthResponseContainsGiftCardResponse()
        {
            String xml = "<captureGivenAuthResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></captureGivenAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureGivenAuthResponse));
            StringReader reader = new StringReader(xml);
            captureGivenAuthResponse captureGivenAuthResponse = (captureGivenAuthResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(captureGivenAuthResponse.giftCardResponse);
        }

        [TestMethod]
        public void TestCaptureGivenAuthResponseContainsFraudResult()
        {
            String xml = "<captureGivenAuthResponse xmlns=\"http://www.litle.com/schema\"><fraudResult></fraudResult></captureGivenAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureGivenAuthResponse));
            StringReader reader = new StringReader(xml);
            captureGivenAuthResponse captureGivenAuthResponse = (captureGivenAuthResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(captureGivenAuthResponse.fraudResult);
        }

        [TestMethod]
        public void TestSaleResponseContainsGiftCardResponse()
        {
            String xml = "<saleResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></saleResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(saleResponse));
            StringReader reader = new StringReader(xml);
            saleResponse saleResponse = (saleResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(saleResponse.giftCardResponse);
        }

        [TestMethod]
        public void TestCreditResponseContainsGiftCardResponse()
        {
            String xml = "<creditResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></creditResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(creditResponse));
            StringReader reader = new StringReader(xml);
            creditResponse creditResponse = (creditResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(creditResponse.giftCardResponse);
        }

        [TestMethod]
        public void TestCreditResponseContainsFraudResult()
        {
            String xml = "<creditResponse xmlns=\"http://www.litle.com/schema\"><fraudResult></fraudResult></creditResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(creditResponse));
            StringReader reader = new StringReader(xml);
            creditResponse creditResponse = (creditResponse)serializer.Deserialize(reader);

            Assert.IsNotNull(creditResponse.fraudResult);
        }

        [TestMethod]
        public void TestActivateResponse()
        {
            String xml = "<activateResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" duplicate=\"true\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>1</litleTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></activateResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(activateResponse));
            StringReader reader = new StringReader(xml);
            activateResponse activateResponse = (activateResponse)serializer.Deserialize(reader);

            Assert.AreEqual("A", activateResponse.reportGroup);
            Assert.AreEqual("3", activateResponse.id);
            Assert.AreEqual("4", activateResponse.customerId);
            Assert.IsTrue(activateResponse.duplicate);
            Assert.AreEqual("1", activateResponse.litleTxnId);
            Assert.AreEqual("2", activateResponse.orderId);
            Assert.AreEqual("000", activateResponse.response);
            Assert.AreEqual(new DateTime(2013,9,5,14,23,45), activateResponse.responseTime);
            Assert.AreEqual(new DateTime(2013,9,5), activateResponse.postDate);
            Assert.AreEqual("Approved", activateResponse.message);
            Assert.IsNotNull(activateResponse.fraudResult);
            Assert.IsNotNull(activateResponse.giftCardResponse);
        }

        [TestMethod]
        public void TestLoadResponse()
        {
            String xml = "<loadResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" duplicate=\"true\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>1</litleTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></loadResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(loadResponse));
            StringReader reader = new StringReader(xml);
            loadResponse loadResponse = (loadResponse)serializer.Deserialize(reader);

            Assert.AreEqual("A", loadResponse.reportGroup);
            Assert.AreEqual("3", loadResponse.id);
            Assert.AreEqual("4", loadResponse.customerId);
            Assert.IsTrue(loadResponse.duplicate);
            Assert.AreEqual("1", loadResponse.litleTxnId);
            Assert.AreEqual("2", loadResponse.orderId);
            Assert.AreEqual("000", loadResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), loadResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), loadResponse.postDate);
            Assert.AreEqual("Approved", loadResponse.message);
            Assert.IsNotNull(loadResponse.fraudResult);
            Assert.IsNotNull(loadResponse.giftCardResponse);
        }

        [TestMethod]
        public void TestUnloadResponse()
        {
            String xml = "<unloadResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" duplicate=\"true\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>1</litleTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></unloadResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(unloadResponse));
            StringReader reader = new StringReader(xml);
            unloadResponse unloadResponse = (unloadResponse)serializer.Deserialize(reader);

            Assert.AreEqual("A", unloadResponse.reportGroup);
            Assert.AreEqual("3", unloadResponse.id);
            Assert.AreEqual("4", unloadResponse.customerId);
            Assert.IsTrue(unloadResponse.duplicate);
            Assert.AreEqual("1", unloadResponse.litleTxnId);
            Assert.AreEqual("2", unloadResponse.orderId);
            Assert.AreEqual("000", unloadResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), unloadResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), unloadResponse.postDate);
            Assert.AreEqual("Approved", unloadResponse.message);
            Assert.IsNotNull(unloadResponse.fraudResult);
            Assert.IsNotNull(unloadResponse.giftCardResponse);
        }

        [TestMethod]
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

        [TestMethod]
        public void TestBalanceInquiryResponse()
        {
            String xml = "<balanceInquiryResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>1</litleTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></balanceInquiryResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(balanceInquiryResponse));
            StringReader reader = new StringReader(xml);
            balanceInquiryResponse balanceInquiryResponse = (balanceInquiryResponse)serializer.Deserialize(reader);

            Assert.AreEqual("A", balanceInquiryResponse.reportGroup);
            Assert.AreEqual("3", balanceInquiryResponse.id);
            Assert.AreEqual("4", balanceInquiryResponse.customerId);
            Assert.AreEqual("1", balanceInquiryResponse.litleTxnId);
            Assert.AreEqual("2", balanceInquiryResponse.orderId);
            Assert.AreEqual("000", balanceInquiryResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), balanceInquiryResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), balanceInquiryResponse.postDate);
            Assert.AreEqual("Approved", balanceInquiryResponse.message);
            Assert.IsNotNull(balanceInquiryResponse.fraudResult);
            Assert.IsNotNull(balanceInquiryResponse.giftCardResponse);
        }

        [TestMethod]
        public void TestDeactivateResponse()
        {
            String xml = "<deactivateResponse reportGroup=\"A\" id=\"3\" customerId=\"4\" xmlns=\"http://www.litle.com/schema\"><litleTxnId>1</litleTxnId><orderId>2</orderId><response>000</response><responseTime>2013-09-05T14:23:45</responseTime><postDate>2013-09-05</postDate><message>Approved</message><fraudResult></fraudResult><giftCardResponse></giftCardResponse></deactivateResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(deactivateResponse));
            StringReader reader = new StringReader(xml);
            deactivateResponse deactivateResponse = (deactivateResponse)serializer.Deserialize(reader);

            Assert.AreEqual("A", deactivateResponse.reportGroup);
            Assert.AreEqual("3", deactivateResponse.id);
            Assert.AreEqual("4", deactivateResponse.customerId);
            Assert.AreEqual("1", deactivateResponse.litleTxnId);
            Assert.AreEqual("2", deactivateResponse.orderId);
            Assert.AreEqual("000", deactivateResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), deactivateResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), deactivateResponse.postDate);
            Assert.AreEqual("Approved", deactivateResponse.message);
            Assert.IsNotNull(deactivateResponse.fraudResult);
            Assert.IsNotNull(deactivateResponse.giftCardResponse);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void TestEnhancedAuthResponseCanContainVirtualAccountNumber()
        {
            String xml = @"
<enhancedAuthResponse xmlns=""http://www.litle.com/schema"">
<virtualAccountNumber>123456</virtualAccountNumber>
</enhancedAuthResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(enhancedAuthResponse));
            StringReader reader = new StringReader(xml);
            enhancedAuthResponse enhancedAuthResponse = (enhancedAuthResponse)serializer.Deserialize(reader);
            Assert.AreEqual("123456", enhancedAuthResponse.virtualAccountNumber);
        }

        [TestMethod]
        public void TestAuthReversalResponseCanContainGiftCardResponse()
        {
            String xml = @"
<authReversalResponse xmlns=""http://www.litle.com/schema"" id=""theId"" customerId=""theCustomerId"" reportGroup=""theReportGroup"">
<litleTxnId>1</litleTxnId>
<orderId>2</orderId>
<response>000</response>
<responseTime>2013-09-05T14:23:45</responseTime>
<postDate>2013-09-05</postDate>
<message>Foo</message>
<giftCardResponse>
<availableBalance>5</availableBalance>
</giftCardResponse>
</authReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(authReversalResponse));
            StringReader reader = new StringReader(xml);
            authReversalResponse authReversalResponse = (authReversalResponse)serializer.Deserialize(reader);
            Assert.AreEqual("theId", authReversalResponse.id);
            Assert.AreEqual("theCustomerId", authReversalResponse.customerId);
            Assert.AreEqual("theReportGroup", authReversalResponse.reportGroup);
            Assert.AreEqual(1, authReversalResponse.litleTxnId);
            Assert.AreEqual("2", authReversalResponse.orderId);
            Assert.AreEqual("000", authReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), authReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), authReversalResponse.postDate);
            Assert.AreEqual("Foo", authReversalResponse.message);
            Assert.AreEqual("5", authReversalResponse.giftCardResponse.availableBalance);
        }

        [TestMethod]
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
            Assert.AreEqual("1", depositReversalResponse.litleTxnId);
            Assert.AreEqual("2", depositReversalResponse.orderId);
            Assert.AreEqual("000", depositReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), depositReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), depositReversalResponse.postDate);
            Assert.AreEqual("Foo", depositReversalResponse.message);
            Assert.AreEqual("5", depositReversalResponse.giftCardResponse.availableBalance);
        }

        [TestMethod]
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
            Assert.AreEqual("1", activateReversalResponse.litleTxnId);
            Assert.AreEqual("2", activateReversalResponse.orderId);
            Assert.AreEqual("000", activateReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), activateReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), activateReversalResponse.postDate);
            Assert.AreEqual("Foo", activateReversalResponse.message);
            Assert.AreEqual("5", activateReversalResponse.giftCardResponse.availableBalance);
        }

        [TestMethod]
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
            Assert.AreEqual("1", deactivateReversalResponse.litleTxnId);
            Assert.AreEqual("2", deactivateReversalResponse.orderId);
            Assert.AreEqual("000", deactivateReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), deactivateReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), deactivateReversalResponse.postDate);
            Assert.AreEqual("Foo", deactivateReversalResponse.message);
            Assert.AreEqual("5", deactivateReversalResponse.giftCardResponse.availableBalance);
        }

        [TestMethod]
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
            Assert.AreEqual("1", loadReversalResponse.litleTxnId);
            Assert.AreEqual("2", loadReversalResponse.orderId);
            Assert.AreEqual("000", loadReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), loadReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), loadReversalResponse.postDate);
            Assert.AreEqual("Foo", loadReversalResponse.message);
            Assert.AreEqual("5", loadReversalResponse.giftCardResponse.availableBalance);
        }

        [TestMethod]
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
            Assert.AreEqual("1", unloadReversalResponse.litleTxnId);
            Assert.AreEqual("2", unloadReversalResponse.orderId);
            Assert.AreEqual("000", unloadReversalResponse.response);
            Assert.AreEqual(new DateTime(2013, 9, 5, 14, 23, 45), unloadReversalResponse.responseTime);
            Assert.AreEqual(new DateTime(2013, 9, 5), unloadReversalResponse.postDate);
            Assert.AreEqual("Foo", unloadReversalResponse.message);
            Assert.AreEqual("5", unloadReversalResponse.giftCardResponse.availableBalance);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

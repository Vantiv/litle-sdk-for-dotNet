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

        [Test]
        public void TestAuthReversalResponseContainsGiftCardResponse()
        {
            String xml = "<authReversalResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></authReversalResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(authReversalResponse));
            StringReader reader = new StringReader(xml);
            authReversalResponse authReversalResponse = (authReversalResponse)serializer.Deserialize(reader);

            Assert.NotNull(authReversalResponse.giftCardResponse);
        }

        [Test]
        public void TestCaptureResponseContainsGiftCardResponse()
        {
            String xml = "<captureResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></captureResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(captureResponse));
            StringReader reader = new StringReader(xml);
            captureResponse captureResponse = (captureResponse)serializer.Deserialize(reader);

            Assert.NotNull(captureResponse.giftCardResponse);
        }

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

        [Test]
        public void TestCreditResponseContainsGiftCardResponse()
        {
            String xml = "<creditResponse xmlns=\"http://www.litle.com/schema\"><giftCardResponse></giftCardResponse></creditResponse>";
            XmlSerializer serializer = new XmlSerializer(typeof(creditResponse));
            StringReader reader = new StringReader(xml);
            creditResponse creditResponse = (creditResponse)serializer.Deserialize(reader);

            Assert.NotNull(creditResponse.giftCardResponse);
        }

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
            Assert.IsTrue(loadResponse.duplicate);
            Assert.AreEqual("1", loadResponse.litleTxnId);
            Assert.AreEqual("2", loadResponse.orderId);
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
            Assert.AreEqual("3", unloadResponse.id);
            Assert.AreEqual("4", unloadResponse.customerId);
            Assert.IsTrue(unloadResponse.duplicate);
            Assert.AreEqual("1", unloadResponse.litleTxnId);
            Assert.AreEqual("2", unloadResponse.orderId);
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
            Assert.AreEqual("1", balanceInquiryResponse.litleTxnId);
            Assert.AreEqual("2", balanceInquiryResponse.orderId);
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
            Assert.AreEqual("1", deactivateResponse.litleTxnId);
            Assert.AreEqual("2", deactivateResponse.orderId);
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

    }
}

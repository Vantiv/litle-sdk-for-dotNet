﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestClass]
    public class TestForceCapture
    {
        
        private LitleOnline litle;

        [TestInitialize]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [TestMethod]
        public void TestSurchargeAmount()
        {
            forceCapture capture = new forceCapture();
            capture.amount = 2;
            capture.surchargeAmount = 1;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><forceCaptureResponse><litleTxnId>123</litleTxnId></forceCaptureResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.ForceCapture(capture);
        }


        [TestMethod]
        public void TestSurchargeAmount_Optional()
        {
            forceCapture capture = new forceCapture();
            capture.amount = 2;
            capture.orderSource = orderSourceType.ecommerce;
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<orderSource>ecommerce</orderSource>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><forceCaptureResponse><litleTxnId>123</litleTxnId></forceCaptureResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.ForceCapture(capture);
        }

        [TestMethod]
        public void TestDebtRepayment_True()
        {
            forceCapture forceCapture = new forceCapture();
            forceCapture.merchantData = new merchantDataType();
            forceCapture.debtRepayment = true;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n<debtRepayment>true</debtRepayment>\r\n</forceCapture>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><forceCaptureResponse><litleTxnId>123</litleTxnId></forceCaptureResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.ForceCapture(forceCapture);
        }

        [TestMethod]
        public void TestDebtRepayment_False()
        {
            forceCapture forceCapture = new forceCapture();
            forceCapture.merchantData = new merchantDataType();
            forceCapture.debtRepayment = false;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n<debtRepayment>false</debtRepayment>\r\n</forceCapture>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><forceCaptureResponse><litleTxnId>123</litleTxnId></forceCaptureResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.ForceCapture(forceCapture);
        }

        [TestMethod]
        public void TestDebtRepayment_Optional()
        {
            forceCapture forceCapture = new forceCapture();
            forceCapture.merchantData = new merchantDataType();

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*</merchantData>\r\n</forceCapture>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.19' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><forceCaptureResponse><litleTxnId>123</litleTxnId></forceCaptureResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.ForceCapture(forceCapture);
        }



    }
}

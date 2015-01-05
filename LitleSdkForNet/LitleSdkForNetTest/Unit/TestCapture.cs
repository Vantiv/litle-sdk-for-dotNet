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
    public class TestCapture
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
            capture capture = new capture();
            capture.litleTxnId = 3;
            capture.amount = 2;
            capture.surchargeAmount = 1;
            capture.payPalNotes = "note";
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<surchargeAmount>1</surchargeAmount>\r\n<payPalNotes>note</payPalNotes>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureResponse><litleTxnId>123</litleTxnId></captureResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Capture(capture);
        }

        [TestMethod]
        public void TestSurchargeAmount_Optional()
        {
            capture capture = new capture();
            capture.litleTxnId = 3;
            capture.amount = 2;
            capture.payPalNotes = "note";
            capture.reportGroup = "Planets";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<amount>2</amount>\r\n<payPalNotes>note</payPalNotes>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><captureResponse><litleTxnId>123</litleTxnId></captureResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.Capture(capture);
        }

    }
}

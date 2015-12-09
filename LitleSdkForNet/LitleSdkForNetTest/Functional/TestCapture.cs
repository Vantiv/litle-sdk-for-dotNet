using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    class TestCapture
    {
        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "500");
            config.Add("merchantId", "101");
            config.Add("password", "TESTCASE");
            config.Add("printxml", "true");
            config.Add("proxyHost", Properties.Settings.Default.proxyHost);
            config.Add("proxyPort", Properties.Settings.Default.proxyPort);
            config.Add("logFile", Properties.Settings.Default.logFile);
            config.Add("neuterAccountNums", "true");
            litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleCapture()
        {
            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            captureResponse response = litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleCaptureWithPartial()
        {
            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.partial = true;
            capture.payPalNotes = "Notes";

            captureResponse response = litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void complexCapture()
        {
            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";
            enhancedData enhanceddata = new enhancedData();
            enhanceddata.customerReference = "Litle";
            enhanceddata.salesTax = 50;
            enhanceddata.deliveryType = enhancedDataDeliveryType.TBD;
            capture.enhancedData = enhanceddata;
            capture.payPalOrderComplete = true;
            captureResponse response = litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureWithSpecial()
        {
            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "<'&\">";

            captureResponse response = litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

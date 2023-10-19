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
        [Test]
        public void SimpleCaptureWithforeignRetailerIndicatorEnum()
        {
            capture capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "<'&\">";
            capture.foreignRetailerIndicator = foreignRetailerIndicatorEnum.F;
            captureResponse response = litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

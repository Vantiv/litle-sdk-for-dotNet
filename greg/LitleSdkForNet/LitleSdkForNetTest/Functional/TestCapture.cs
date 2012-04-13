using System;
using System.Collections.Generic;
using System.Linq;
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
            litle = new LitleOnline();
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
            
    }
}

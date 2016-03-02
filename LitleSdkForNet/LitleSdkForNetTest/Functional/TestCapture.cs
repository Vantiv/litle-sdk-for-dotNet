using System.Collections.Generic;
using System.Text;
using Litle.Sdk.Properties;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCapture
    {
        private LitleOnline litle;
        private IDictionary<string, StringBuilder> _memoryCache;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _memoryCache = new Dictionary<string, StringBuilder>();
            var config = new Dictionary<string, string>();
            config.Add("url", "https://www.testlitle.com/sandbox/communicator/online");
            config.Add("reportGroup", "Default Report Group");
            config.Add("username", "DOTNET");
            config.Add("version", "8.13");
            config.Add("timeout", "5000");
            config.Add("merchantId", "101");
            config.Add("password", "TESTCASE");
            config.Add("printxml", "true");
            config.Add("proxyHost", Settings.Default.proxyHost);
            config.Add("proxyPort", Settings.Default.proxyPort);
            config.Add("logFile", Settings.Default.logFile);
            config.Add("neuterAccountNums", "true");
            litle = new LitleOnline(_memoryCache, config);
        }

        [Test]
        public void SimpleCapture()
        {
            var capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";

            var response = litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleCaptureWithPartial()
        {
            var capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.partial = true;
            capture.payPalNotes = "Notes";

            var response = litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void complexCapture()
        {
            var capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "Notes";
            var enhanceddata = new enhancedData();
            enhanceddata.customerReference = "Litle";
            enhanceddata.salesTax = 50;
            enhanceddata.deliveryType = enhancedDataDeliveryType.TBD;
            capture.enhancedData = enhanceddata;
            capture.payPalOrderComplete = true;
            var response = litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureWithSpecial()
        {
            var capture = new capture();
            capture.litleTxnId = 123456000;
            capture.amount = 106;
            capture.payPalNotes = "<'&\">";

            var response = litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

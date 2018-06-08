using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCapture
    {
        private LitleOnline _litle;
        private Dictionary<string, string> _config;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _config = new Dictionary<string, string>
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

            _litle = new LitleOnline(_config);
        }

        [Test]
        public void SimpleCapture()
        {
            var capture = new capture
            {
                id = "1",
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes",
                pin = "1234"
            };

            var response = _litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void simpleCaptureWithPartial()
        {
            var capture = new capture
            {
                id = "1",
                litleTxnId = 123456000,
                amount = 106,
                partial = true,
                payPalNotes = "Notes"
            };


            var response = _litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void complexCapture()
        {
            var capture = new capture
            {
                id = "1",
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes",
                pin = "1234",
                enhancedData = new enhancedData
                {
                    customerReference = "Litle",
                    salesTax = 50,
                    deliveryType = enhancedDataDeliveryType.TBD
                },
                payPalOrderComplete = true,
                customBilling = new customBilling
                {
                    phone = "51312345678",
                    city = "Lowell",
                    url = "test.com",
                    descriptor = "Nothing",
                }
            };

            var response = _litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureWithSpecial()
        {
            var capture = new capture
            {
                id = "1",
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "<'&\">"
            };
            
            var response = _litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }
    }
}

using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestCapture
    {
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            var config = new Dictionary<string, string>
            {
                {"url", "https://www.testvantivcnp.com/sandbox/communicator/online"},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "8.13"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };
            _litle = new LitleOnline(config);
        }

        [Test]
        public void SimpleCapture()
        {
            var capture = new capture
            {
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes"
            };

            var response = _litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCaptureWithPartial()
        {
            var capture = new capture
            {
                litleTxnId = 123456000,
                amount = 106,
                partial = true,
                payPalNotes = "Notes"
            };

            var response = _litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void ComplexCapture()
        {
            var capture = new capture
            {
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes",
                payPalOrderComplete = true,
                enhancedData = new enhancedData
                {
                    customerReference = "Litle",
                    salesTax = 50,
                    deliveryType = enhancedDataDeliveryType.TBD
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
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "<'&\">"
            };

            var response = _litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCapture_withPin()
        {
            var capture = new capture
            {
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes",
                pin = "1234"
            };

            var response = _litle.Capture(capture);
            Assert.AreEqual("Approved", response.message);
        }

        [Test]
        public void SimpleCapture_withPin_negative()
        {
            var capture = new capture
            {
                litleTxnId = 123456000,
                amount = 106,
                payPalNotes = "Notes",
                pin = "1"
            };

            try
            {
                _litle.Capture(capture);
                Assert.Fail("Exception expected!");
            }
            catch (LitleOnlineException e)
            {
                Assert.True(e.Message.StartsWith("Error validating xml data against the"));
            }
            
        }
    }
}

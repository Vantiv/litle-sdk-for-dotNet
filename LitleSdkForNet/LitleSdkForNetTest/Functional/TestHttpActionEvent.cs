using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestHttpActionEvent
    {
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            var config = new Dictionary<string, string>
            {
                {"url", "https://www.testlitle.com/sandbox/communicator/online"},
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
        public void TestHttpEvents()
        {
            var requestCount = 0;
            var responseCount = 0;
            var httpActionCount = 0;

            _litle.HttpAction += (sender, args) =>
            {
                var eventArgs = (Communications.HttpActionEventArgs)args;
                httpActionCount++;
                if (eventArgs.RequestType == Communications.RequestType.Request)
                {
                    requestCount++;
                }
                else if (eventArgs.RequestType == Communications.RequestType.Response)
                {
                    responseCount++;
                }
            };

            var capture = new capture
            {
                litleTxnId = 123456000,
                amount = 106,
                id = "1"
            };

            _litle.Capture(capture);
            Assert.AreEqual(httpActionCount, 2);
            Assert.AreEqual(requestCount, 1);
            Assert.AreEqual(responseCount, 1);
        }
    }
}

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
            int requestCount = 0;
            int responseCount = 0;
            int httpActionCount = 0;

            _litle.HttpAction += (sender, args) =>
            {
                var eventArgs = (HttpActionEventArgs)args;
                httpActionCount++;
                if (eventArgs.RequestType == RequestType.Request)
                {
                    requestCount++;
                }
                else if (eventArgs.RequestType == RequestType.Response)
                {
                    responseCount++;
                }
            };

            var capture = new capture
            {
                litleTxnId = 123456000,
                amount = 106
            };

            _litle.Capture(capture);
            Assert.AreEqual(httpActionCount, 2);
            Assert.AreEqual(requestCount, 1);
            Assert.AreEqual(responseCount, 1);
        }
    }
}

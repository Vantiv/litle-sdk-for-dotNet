using System.Collections.Generic;
using NUnit.Framework;

namespace Litle.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestQueryTransaction
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
        public void SimpleQueryTransaction()
        {
            var query = new queryTransaction
            {
                id = "myId",
                reportGroup = "myReportGroup",
                origId = "Deposit1",
                origActionType = actionTypeEnum.D,
                origLitleTxnId = 54321
            };

            var response = _litle.queryTransaction(query);
            var queryResponse = (queryTransactionResponse)response;

            Assert.NotNull(queryResponse);
            Assert.AreEqual("150", queryResponse.response);
            Assert.AreEqual("Original transaction found", queryResponse.message);
            Assert.AreEqual("000", ((captureResponse)queryResponse.results_max10[0]).response);

        }

        [Test]
        public void SimpleQueryTransaction_MultipleResponses()
        {
            var query = new queryTransaction
            {
                id = "myId",
                reportGroup = "myReportGroup",
                origId = "Auth2",
                origActionType = actionTypeEnum.A,
                origLitleTxnId = 54321
            };

            var response = _litle.queryTransaction(query);
            var queryResponse = (queryTransactionResponse)response;

            Assert.NotNull(queryResponse);
            Assert.AreEqual("150", queryResponse.response);
            Assert.AreEqual("Original transaction found", queryResponse.message);
            Assert.AreEqual("2", queryResponse.matchCount);
        }

        [Test]
        public void testQueryTransactionUnavailableResponse()
        {
            var query = new queryTransaction
            {
                id = "myId",
                reportGroup = "myReportGroup",
                origId = "Auth",
                origActionType = actionTypeEnum.A,
                origLitleTxnId = 54321
            };

            var response = _litle.queryTransaction(query);
            var queryResponse = (queryTransactionUnavailableResponse)response;

            Assert.AreEqual("152", queryResponse.response);
            Assert.AreEqual("Original transaction found but response not yet available", queryResponse.message);
        }

        [Test]
        public void testQueryTransactionNotFoundResponse()
        {
            var query = new queryTransaction
            {
                id = "myId",
                reportGroup = "myReportGroup",
                origId = "Auth0",
                origActionType = actionTypeEnum.A,
                origLitleTxnId = 54321
            };

            var response = _litle.queryTransaction(query);
            var queryResponse = (queryTransactionResponse)response;

            Assert.AreEqual("151", queryResponse.response);
            Assert.AreEqual("Original transaction not found", queryResponse.message);
        }
    }
}

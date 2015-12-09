using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestQueryTransactionRequest
    {

        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void TestSimple()
        {
            queryTransaction query = new queryTransaction();
            query.id = "myId";
            query.reportGroup = "myReportGroup";
            query.origId = "12345";
            query.origActionType = actionTypeEnum.D;
            query.origLitleTxnId = 54321;

            string result = query.Serialize();
            Assert.AreEqual("\r\n<queryTransaction id=\"myId\" reportGroup=\"myReportGroup\">\r\n<origId>12345</origId>\r\n<origActionType>D</origActionType>\r\n<origLitleTxnId>54321</origLitleTxnId>\r\n</queryTransaction>", result);
            
        }

        [Test]
        public void TestQueryTransactionResponse()
        {
            queryTransaction query = new queryTransaction();
            query.id = "myId";
            query.reportGroup = "myReportGroup";
            query.origId = "12345";
            query.origActionType = actionTypeEnum.D;
            query.origLitleTxnId = 54321;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<queryTransaction.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='10.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><queryTransactionResponse id='FindAuth' reportGroup='Mer5PM1' customerId='1'><response>000</response><responseTime>2015-12-03T10:30:02</responseTime><message>Original transaction found</message><results_max10><authorizationResponse id='1' reportGroup='defaultReportGroup'><litleTxnId>756027696701750</litleTxnId><orderId>GenericOrderId</orderId><response>000</response><responseTime>2015-04-14T12:04:59</responseTime><postDate>2015-04-14</postDate><message>Approved</message><authCode>055858</authCode></authorizationResponse><authorizationResponse id='1' reportGroup='defaultReportGroup'><litleTxnId>756027696701751</litleTxnId><orderId>GenericOrderId</orderId><response>000</response><responseTime>2015-04-14T12:04:59</responseTime><postDate>2015-04-14</postDate><message>Approved</message><authCode>055858</authCode></authorizationResponse><captureResponse><response>000</response><message>Deposit approved</message></captureResponse></results_max10></queryTransactionResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            transactionTypeWithReportGroup response = (transactionTypeWithReportGroup)litle.queryTransaction(query);
            queryTransactionResponse queryTransactionResponse = (queryTransactionResponse)response;

            Assert.NotNull(queryTransactionResponse);
            Assert.AreEqual("000", queryTransactionResponse.response);
            Assert.AreEqual(3, queryTransactionResponse.results_max10.Count);
            Assert.AreEqual("Original transaction found", queryTransactionResponse.message);
            Assert.AreEqual("000", ((authorizationResponse)queryTransactionResponse.results_max10[0]).response);
            Assert.AreEqual("Approved", ((authorizationResponse)queryTransactionResponse.results_max10[0]).message);
            Assert.AreEqual(756027696701750, ((authorizationResponse)queryTransactionResponse.results_max10[0]).litleTxnId);

            Assert.AreEqual("000", ((authorizationResponse)queryTransactionResponse.results_max10[1]).response);
            Assert.AreEqual("Approved", ((authorizationResponse)queryTransactionResponse.results_max10[1]).message);
            Assert.AreEqual(756027696701751, ((authorizationResponse)queryTransactionResponse.results_max10[1]).litleTxnId);

            Assert.AreEqual("000", ((authorizationResponse)queryTransactionResponse.results_max10[1]).response);
            Assert.AreEqual("Approved", ((authorizationResponse)queryTransactionResponse.results_max10[1]).message);
            Assert.AreEqual(756027696701751, ((authorizationResponse)queryTransactionResponse.results_max10[1]).litleTxnId);

            Assert.AreEqual("000", ((captureResponse)queryTransactionResponse.results_max10[2]).response);
            Assert.AreEqual("Deposit approved", ((captureResponse)queryTransactionResponse.results_max10[2]).message);

        }

        [Test]
        public void TestQueryTransactionUnavailableResponse()
        {
            queryTransaction query = new queryTransaction();
            query.id = "myId";
            query.reportGroup = "myReportGroup";
            query.origId = "12345";
            query.origActionType = actionTypeEnum.D;
            query.origLitleTxnId = 54321;

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<queryTransaction.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='10.10' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><queryTransactionUnavailableResponse id='FindAuth' reportGroup='Mer5PM1' customerId='1'><response>152</response><responseTime>2015-12-03T14:45:31</responseTime><message>Original transaction found but response not yet available</message></queryTransactionUnavailableResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            transactionTypeWithReportGroup response = (transactionTypeWithReportGroup)litle.queryTransaction(query);
            queryTransactionUnavailableResponse queryTransactionResponse = (queryTransactionUnavailableResponse)response;

            Assert.NotNull(queryTransactionResponse);
            Assert.AreEqual("152", queryTransactionResponse.response);
        }
    }
}

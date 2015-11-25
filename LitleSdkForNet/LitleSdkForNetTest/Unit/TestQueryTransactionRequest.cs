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
    }
}

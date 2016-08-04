using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Text.RegularExpressions;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    class TestAdvancedFraudCheck
    {

        private LitleOnline litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [Test]
        public void TestNoCustomAttributes()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><litleTxnId>742802348034313000</litleTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></litleOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = litle.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("pass", fraudCheckResponse.advancedFraudResults.deviceReviewStatus);
        }

        [Test]
        public void TestCustomAttribute1()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";

            var mock = new Mock<Communications>();
            
            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><litleTxnId>742802348034313000</litleTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></litleOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = litle.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual(42, fraudCheckResponse.advancedFraudResults.deviceReputationScore);
        }

        [Test]
        public void TestCustomAttribute2()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";
            advancedFraudCheck.customAttribute2 = "def";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><litleTxnId>742802348034313000</litleTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></litleOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = litle.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("triggered_rule_default", fraudCheckResponse.advancedFraudResults.triggeredRule);
        }

        [Test]
        public void TestCustomAttribute3()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";
            advancedFraudCheck.customAttribute2 = "def";
            advancedFraudCheck.customAttribute3 = "ghi";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n<customAttribute3>ghi</customAttribute3>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><litleTxnId>742802348034313000</litleTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></litleOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = litle.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("Approved", fraudCheckResponse.message);
        }

        [Test]
        public void TestCustomAttribute4()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";
            advancedFraudCheck.customAttribute2 = "def";
            advancedFraudCheck.customAttribute3 = "ghi";
            advancedFraudCheck.customAttribute4 = "jkl";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n<customAttribute3>ghi</customAttribute3>\r\n<customAttribute4>jkl</customAttribute4>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><litleTxnId>742802348034313000</litleTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></litleOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = litle.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual("000", fraudCheckResponse.response);
        }

        [Test]
        public void TestCustomAttribute5()
        {
            fraudCheck fraudCheck = new fraudCheck();
            advancedFraudChecksType advancedFraudCheck = new advancedFraudChecksType();
            fraudCheck.advancedFraudChecks = advancedFraudCheck;
            advancedFraudCheck.threatMetrixSessionId = "123";
            advancedFraudCheck.customAttribute1 = "abc";
            advancedFraudCheck.customAttribute2 = "def";
            advancedFraudCheck.customAttribute3 = "ghi";
            advancedFraudCheck.customAttribute4 = "jkl";
            advancedFraudCheck.customAttribute5 = "mno";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex("..*<threatMetrixSessionId>123</threatMetrixSessionId>\r\n<customAttribute1>abc</customAttribute1>\r\n<customAttribute2>def</customAttribute2>\r\n<customAttribute3>ghi</customAttribute3>\r\n<customAttribute4>jkl</customAttribute4>\r\n<customAttribute5>mno</customAttribute5>\r\n.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='10.1' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><fraudCheckResponse id='127' reportGroup='Planets' customerId=''><litleTxnId>742802348034313000</litleTxnId><response>000</response><message>Approved</message><advancedFraudResults><deviceReviewStatus>pass</deviceReviewStatus><deviceReputationScore>42</deviceReputationScore><triggeredRule>triggered_rule_default</triggeredRule></advancedFraudResults></fraudCheckResponse></litleOnlineResponse >");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            fraudCheckResponse fraudCheckResponse = litle.FraudCheck(fraudCheck);

            Assert.NotNull(fraudCheckResponse);
            Assert.AreEqual(742802348034313000, fraudCheckResponse.litleTxnId);
        }

    }
}

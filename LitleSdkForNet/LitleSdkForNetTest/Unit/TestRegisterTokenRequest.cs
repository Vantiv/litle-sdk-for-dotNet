using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestRegisterTokenRequest
    {
        private LitleOnline litle;
        private IDictionary<string, StringBuilder> _memoryStreams;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _memoryStreams = new Dictionary<string, StringBuilder>();
            litle = new LitleOnline(_memoryStreams);
        }

        [Test]
        public void TestSimpleRequest()
        {
            var register = new registerTokenRequestType();
            register.orderId = "12344";
            register.accountNumber = "4100000000000001";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<registerTokenRequest.*<accountNumber>4100000000000001</accountNumber>.*</registerTokenRequest>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><registerTokenResponse><litleTxnId>4</litleTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></registerTokenResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.RegisterToken(register);
        }

        [Test]
        public void TestCanContainCardValidationNum()
        {
            var register = new registerTokenRequestType();
            register.orderId = "12344";
            register.accountNumber = "4100000000000001";
            register.cardValidationNum = "123";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<registerTokenRequest.*<accountNumber>4100000000000001</accountNumber>.*<cardValidationNum>123</cardValidationNum>.*</registerTokenRequest>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><registerTokenResponse><litleTxnId>4</litleTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></registerTokenResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.RegisterToken(register);
        }

        [Test]
        public void TestSimpleRequestWithApplepay()
        {
            var register = new registerTokenRequestType();
            register.orderId = "12344";
            var applepay = new applepayType();
            var applepayHeaderType = new applepayHeaderType();
            applepayHeaderType.applicationData = "454657413164";
            applepayHeaderType.ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            applepayHeaderType.publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            applepayHeaderType.transactionId = "1234";
            applepay.header = applepayHeaderType;
            applepay.data = "user";
            applepay.signature = "sign";
            applepay.version = "1";
            register.applepay = applepay;

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<registerTokenRequest.*<applepay>.*?<data>user</data>.*?</applepay>.*?</registerTokenRequest>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><registerTokenResponse><litleTxnId>4</litleTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></registerTokenResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.RegisterToken(register);
        }
    }
}

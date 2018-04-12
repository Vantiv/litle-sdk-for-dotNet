using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestRegisterTokenRequest
    {
        
        private LitleOnline _litle;

        [TestFixtureSetUp]
        public void SetUpLitle()
        {
            _litle = new LitleOnline();
        }

        [Test]
        public void TestSimpleRequest()
        {
            var register = new registerTokenRequestType
            {
                orderId = "12344",
                accountNumber = "4100000000000001"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<registerTokenRequest.*<accountNumber>4100000000000001</accountNumber>.*</registerTokenRequest>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><registerTokenResponse><litleTxnId>4</litleTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></registerTokenResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.RegisterToken(register);
        }

        [Test]
        public void TestCanContainCardValidationNum()
        {
            var register = new registerTokenRequestType
            {
                orderId = "12344",
                accountNumber = "4100000000000001",
                cardValidationNum = "123"
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<registerTokenRequest.*<accountNumber>4100000000000001</accountNumber>.*<cardValidationNum>123</cardValidationNum>.*</registerTokenRequest>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><registerTokenResponse><litleTxnId>4</litleTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></registerTokenResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.RegisterToken(register);
        }

        [Test]
        public void TestSimpleRequestWithApplepay()
        {
            var register = new registerTokenRequestType
            {
                orderId = "12344",
                applepay = new applepayType
                {
                    data = "user",
                    signature = "sign",
                    version = "1",
                    header = new applepayHeaderType
                    {
                        applicationData = "454657413164",
                        ephemeralPublicKey = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        publicKeyHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                        transactionId = "1234"
                    }
                }
            };

            var mock = new Mock<Communications>();

            mock.Setup(communications => communications.HttpPost(It.IsRegex(".*<registerTokenRequest.*<applepay>.*?<data>user</data>.*?</applepay>.*?</registerTokenRequest>.*", RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns("<litleOnlineResponse version='9.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><registerTokenResponse><litleTxnId>4</litleTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></registerTokenResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            _litle.setCommunication(mockedCommunication);
            _litle.RegisterToken(register);
        }

    }
}

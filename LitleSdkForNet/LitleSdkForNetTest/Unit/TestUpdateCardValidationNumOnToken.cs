using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestUpdateCardValidationNumOnToken
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
            var update = new updateCardValidationNumOnToken();
            update.orderId = "12344";
            update.litleToken = "1111222233334444";
            update.cardValidationNum = "321";
            update.id = "123";
            update.reportGroup = "Default Report Group";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<updateCardValidationNumOnToken id=\"123\" reportGroup=\"Default Report Group\".*<orderId>12344</orderId>.*<litleToken>1111222233334444</litleToken>.*<cardValidationNum>321</cardValidationNum>.*</updateCardValidationNumOnToken>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><updateCardValidationNumOnTokenResponse><litleTxnId>4</litleTxnId><orderId>12344</orderId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></updateCardValidationNumOnTokenResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.UpdateCardValidationNumOnToken(update);
        }

        [Test]
        public void TestOrderIdIsOptional()
        {
            var update = new updateCardValidationNumOnToken();
            update.orderId = null;
            update.litleToken = "1111222233334444";
            update.cardValidationNum = "321";
            update.id = "123";
            update.reportGroup = "Default Report Group";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<updateCardValidationNumOnToken id=\"123\" reportGroup=\"Default Report Group\".*<litleToken>1111222233334444</litleToken>.*<cardValidationNum>321</cardValidationNum>.*</updateCardValidationNumOnToken>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                //mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns(
                    "<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><updateCardValidationNumOnTokenResponse><litleTxnId>4</litleTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></updateCardValidationNumOnTokenResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            var response = litle.UpdateCardValidationNumOnToken(update);
            Assert.IsNotNull(response);
            Assert.IsNull(response.orderId);
        }
    }
}

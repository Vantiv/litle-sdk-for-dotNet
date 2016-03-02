using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;

namespace Litle.Sdk.Test.Unit
{
    [TestFixture]
    internal class TestEcheckRedeposit
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
        public void TestMerchantData()
        {
            var echeckRedeposit = new echeckRedeposit();
            echeckRedeposit.litleTxnId = 1;
            echeckRedeposit.merchantData = new merchantDataType();
            echeckRedeposit.merchantData.campaign = "camp";
            echeckRedeposit.merchantData.affiliate = "affil";
            echeckRedeposit.merchantData.merchantGroupingId = "mgi";

            var mock = new Mock<Communications>(_memoryStreams);

            mock.Setup(
                Communications =>
                    Communications.HttpPost(
                        It.IsRegex(
                            ".*<echeckRedeposit.*<litleTxnId>1</litleTxnId>.*<merchantData>.*<campaign>camp</campaign>.*<affiliate>affil</affiliate>.*<merchantGroupingId>mgi</merchantGroupingId>.*</merchantData>.*",
                            RegexOptions.Singleline), It.IsAny<Dictionary<string, string>>()))
                .Returns(
                    "<litleOnlineResponse version='8.13' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckRedepositResponse><litleTxnId>123</litleTxnId></echeckRedepositResponse></litleOnlineResponse>");

            var mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.EcheckRedeposit(echeckRedeposit);
        }
    }
}

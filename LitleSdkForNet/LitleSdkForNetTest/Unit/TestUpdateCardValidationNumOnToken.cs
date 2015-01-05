using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litle.Sdk;
using Moq;
using System.Text.RegularExpressions;


namespace Litle.Sdk.Test.Unit
{
    [TestClass]
    class TestUpdateCardValidationNumOnToken
    {
        
        private LitleOnline litle;

        [TestInitialize]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [TestMethod]
        public void TestSimpleRequest()
        {
            updateCardValidationNumOnToken update = new updateCardValidationNumOnToken();
            update.orderId = "12344";
            update.litleToken = "1111222233334444";
            update.cardValidationNum = "321";
            update.id = "123";
            update.reportGroup = "Default Report Group";
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<updateCardValidationNumOnToken id=\"123\" reportGroup=\"Default Report Group\".*<orderId>12344</orderId>.*<litleToken>1111222233334444</litleToken>.*<cardValidationNum>321</cardValidationNum>.*</updateCardValidationNumOnToken>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><updateCardValidationNumOnTokenResponse><litleTxnId>4</litleTxnId><orderId>12344</orderId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></updateCardValidationNumOnTokenResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.UpdateCardValidationNumOnToken(update);
        }

        [TestMethod]
        public void TestOrderIdIsOptional()
        {
            updateCardValidationNumOnToken update = new updateCardValidationNumOnToken();
            update.orderId = null;
            update.litleToken = "1111222233334444";
            update.cardValidationNum = "321";
            update.id = "123";
            update.reportGroup = "Default Report Group";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<updateCardValidationNumOnToken id=\"123\" reportGroup=\"Default Report Group\".*<litleToken>1111222233334444</litleToken>.*<cardValidationNum>321</cardValidationNum>.*</updateCardValidationNumOnToken>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
            //mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><updateCardValidationNumOnTokenResponse><litleTxnId>4</litleTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></updateCardValidationNumOnTokenResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            updateCardValidationNumOnTokenResponse response = litle.UpdateCardValidationNumOnToken(update);
            Assert.IsNotNull(response);
            Assert.IsNull(response.orderId);

        }

    }
}

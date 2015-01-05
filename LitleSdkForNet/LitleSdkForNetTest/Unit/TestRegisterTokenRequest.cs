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
    class TestRegisterTokenRequest
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
            registerTokenRequestType register = new registerTokenRequestType();
            register.orderId = "12344";
            register.accountNumber = "4100000000000001";
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<registerTokenRequest.*<accountNumber>4100000000000001</accountNumber>.*</registerTokenRequest>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><registerTokenResponse><litleTxnId>4</litleTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></registerTokenResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.RegisterToken(register);
        }

        [TestMethod]
        public void TestCanContainCardValidationNum()
        {
            registerTokenRequestType register = new registerTokenRequestType();
            register.orderId = "12344";
            register.accountNumber = "4100000000000001";
            register.cardValidationNum = "123";

            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<registerTokenRequest.*<accountNumber>4100000000000001</accountNumber>.*<cardValidationNum>123</cardValidationNum>.*</registerTokenRequest>.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.14' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><registerTokenResponse><litleTxnId>4</litleTxnId><response>801</response><message>Token Successfully Registered</message><responseTime>2012-10-10T10:17:03</responseTime></registerTokenResponse></litleOnlineResponse>");

            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.RegisterToken(register);
        }

    }
}

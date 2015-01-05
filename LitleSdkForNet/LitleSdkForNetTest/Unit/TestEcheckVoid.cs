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
    class TestEcheckVoid
    {
        
        private LitleOnline litle;

        [TestInitialize]
        public void SetUpLitle()
        {
            litle = new LitleOnline();
        }

        [TestMethod]
        public void TestFraudFilterOverride()
        {
            echeckVoid echeckVoid = new echeckVoid();
            echeckVoid.litleTxnId = 123456789;
           
            var mock = new Mock<Communications>();

            mock.Setup(Communications => Communications.HttpPost(It.IsRegex(".*<echeckVoid.*<litleTxnId>123456789.*", RegexOptions.Singleline), It.IsAny<Dictionary<String, String>>()))
                .Returns("<litleOnlineResponse version='8.13' response='0' message='Valid Format' xmlns='http://www.litle.com/schema'><echeckVoidResponse><litleTxnId>123</litleTxnId></echeckVoidResponse></litleOnlineResponse>");
     
            Communications mockedCommunication = mock.Object;
            litle.setCommunication(mockedCommunication);
            litle.EcheckVoid(echeckVoid);
        }            
    }
}

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Text;
using System.Xml.XPath;
using System.Collections.Generic;

/*
 * This class calls the methods and classes to create all four sample transactions one at a time and send them to the server 
 * Also will display the request and the response onto the console
 */

public class testPayment
{
    static void Main()
    {

        List<litleOnlineRequest> onlineTxnList = new List<litleOnlineRequest>();
        litleOnlineRequest onlineReq = createTransaction.createSaleObject();// test sale
        litleOnlineRequest onlineReq1 = createTransaction.createAuthObject();// test Authorization
        litleOnlineRequest onlineReq2 = createTransaction.createCreditObject();//test refund
        litleOnlineRequest onlineReq3 = createTransaction.createTokenObject();//test token request
        onlineTxnList.Add(onlineReq);
        onlineTxnList.Add(onlineReq1);
        onlineTxnList.Add(onlineReq2);
        onlineTxnList.Add(onlineReq3);
            // Iterate through all of the sample transactions 
            foreach (litleOnlineRequest req in onlineTxnList) {
                sendPaymentToLitle(req);
            }
            Console.Read();
    }
    //sends the payment to Litle and writes the request & response onto the console. 
    private static void sendPaymentToLitle(litleOnlineRequest onlineReq)
    {
        String xmlOut = SerializeAndDeserializeXML.SerializeObject(onlineReq);// create data from Object
        Console.WriteLine(xmlOut);//write the post to console

        String resp = communications.HttpPost("http://l-gdake-t5500:8081/sandbox/communicator/online", xmlOut);//send data to Litle Servers
        Console.WriteLine(" ");
        Console.WriteLine("--- WebClient result ---");
        Console.WriteLine(resp);//write the response to console
        Console.WriteLine("**********************************************************************");

// litleOnlineResponse litleResponse = SerializeAndDeserializeXML.DeserializeObject(resp);// create objects from response data
    }
     
}




/*
 *   
 * XmlDocument response = new XmlDocument();
 * response.LoadXml(resp);//loads resp string into xml document response.xml
 * response.Save("C:\\Program Files\\Microsoft Visual Studio 10.0\\VC\\response.xml");
 * 
 */
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Text;
using System.Xml.XPath;

/**
 * Simple example code to create XML from objects and objects from response XML
 */

public class SerializeAndDeserializeXML
{

    public static String SerializeObject(litleOnlineRequest req)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(litleOnlineRequest));
        MemoryStream ms = new MemoryStream();
        serializer.Serialize(ms, req);
        return Encoding.UTF8.GetString(ms.GetBuffer());//return string is UTF8 encoded. 
    }// serialize the xml

    public static litleOnlineResponse DeserializeObject(string response)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(litleOnlineResponse));
        StringReader reader = new StringReader(response);
        litleOnlineResponse i = (litleOnlineResponse)serializer.Deserialize(reader);
        return i;

    }// deserialize the object

}
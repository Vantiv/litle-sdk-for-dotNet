using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Litle.Sdk
{
    public class litleXmlSerializer
    {
        virtual public String SerializeObject(litleOnlineRequest req)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(litleOnlineRequest));
            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, req);
            return Encoding.UTF8.GetString(ms.GetBuffer());//return string is UTF8 encoded.
        }// serialize the xml

        virtual public String SerializeObjectToFile(litleOnlineRequest req)
        {

            return "filename";
        }

        virtual public litleResponse DeserializeObject(string response)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(litleResponse));
            StringReader reader = new StringReader(response);
            litleResponse i = (litleResponse)serializer.Deserialize(reader);
            return i;

        }// deserialize the object

        virtual public litleResponse DeserializeObjectFromFile(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(litleResponse));
            XmlTextReader reader = new XmlTextReader(filePath);
            litleResponse i = (litleResponse)serializer.Deserialize(reader);
            return i;

        }// deserialize the object
    }
}

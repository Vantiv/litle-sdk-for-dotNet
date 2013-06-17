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

        virtual public litleResponse DeserializeObjectFromFile(string filePath)
        {
            litleResponse i;
            i = new litleResponse(filePath);
            return i;
        }// deserialize the object
    }
}

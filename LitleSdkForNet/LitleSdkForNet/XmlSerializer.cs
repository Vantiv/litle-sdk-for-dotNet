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
            try
            {
                serializer.Serialize(ms, req);
            }
            catch (XmlException e)
            {
                throw new LitleOnlineException("Error in sending request to Litle!", e);
            }
            return Encoding.UTF8.GetString(ms.GetBuffer());//return string is UTF8 encoded.
        }// serialize the xml

        virtual public litleResponse DeserializeObjectFromFile(Communications communications, string filePath)
        {
            litleResponse i;
            try
            {
                var stream = communications[filePath];
                var value = stream.ToString();
                var bytes = Encoding.UTF8.GetBytes(value);
                using (var memoryStream = new MemoryStream(bytes))
                {
                    i = new litleResponse(memoryStream);
                }
            }
            catch (XmlException e)
            {
                throw new LitleOnlineException("Error in recieving response from Litle!", e);
            }
            return i;
        }// deserialize the object
    }
}

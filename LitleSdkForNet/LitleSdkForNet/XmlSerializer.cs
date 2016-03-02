using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Litle.Sdk
{
    public class litleXmlSerializer
    {
        public virtual string SerializeObject(litleOnlineRequest req)
        {
            var serializer = new XmlSerializer(typeof (litleOnlineRequest));
            var ms = new MemoryStream();
            try
            {
                serializer.Serialize(ms, req);
            }
            catch (XmlException e)
            {
                throw new LitleOnlineException("Error in sending request to Litle!", e);
            }
            return Encoding.UTF8.GetString(ms.GetBuffer()); //return string is UTF8 encoded.
        } // serialize the xml
        
        public virtual litleResponse DeserializeObjectFromString(string value)
        {
            litleResponse i;
            try
            {
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
        } // deserialize the object
    }
}

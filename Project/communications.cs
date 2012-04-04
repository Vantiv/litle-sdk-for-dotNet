using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Text;
using System.Xml.XPath;

public class communications
{
    /**
     * 
     * This method does a simple Http post to the URI indicated
     * This URI should be pointing at the Litle test system, a host and port will be provided by the Litle Implementations team
     * we'll need to configure access for your organization since the Litle test system is protected by firewalls
     * 
     * Also note that communications to the Litle system will utilize HTTPS for secure transmission of data
     * 
     * Error handling is omitted for this simple example 
     * However communication errors are something that should be handled according to your needs
     */
    public static string HttpPost(string URI, string Parameters)//method to post and URI to post to 
    {

        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters); // get raw bytes to be sent 
        System.Net.WebRequest req = createRequest(URI, bytes);
        System.IO.Stream os = req.GetRequestStream();
        try
        {
            // submit http request
            os.Write(bytes, 0, bytes.Length);

            // read response
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null)
            {
                return null;
            }
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }
        finally
        {
            os.Close();
        }
    }

    //Simple method to create the http Post request
    private static System.Net.WebRequest createRequest(string URI, byte[] bytes)
    {
        System.Net.WebRequest req = System.Net.WebRequest.Create(URI);

        req.ContentType = "text/xml";
        req.Method = "POST";
        req.ContentLength = bytes.Length;
        return req;
    }
}
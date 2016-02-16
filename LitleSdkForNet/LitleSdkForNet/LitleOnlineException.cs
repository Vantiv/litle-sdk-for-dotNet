using System;

namespace Litle.Sdk
{
    public class LitleOnlineException : Exception
    {
        public LitleOnlineException(string message) : base(message)
        {
        }

        public LitleOnlineException(string message, Exception e) : base(message, e)
        {
        }
    }
}

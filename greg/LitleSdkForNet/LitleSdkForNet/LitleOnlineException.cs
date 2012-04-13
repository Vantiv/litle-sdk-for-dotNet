using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Litle.Sdk
{
    public class LitleOnlineException : Exception
    {
        public LitleOnlineException(string message) : base(message)
        {
            
        }
    }
}

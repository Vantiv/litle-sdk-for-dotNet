using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitleXSDGenerated;

namespace LitleSdkForNet
{
    public class LitleOnlineException : Exception
    {
        public LitleOnlineException(string message) : base(message)
        {
            
        }
    }
}

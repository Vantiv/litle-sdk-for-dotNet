using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LitleSdkForNet
{
    public class LitleOnline
    {
        public LitleOnline()
        {
        }

        public String Authorize(String str)
        {
            if (str.Equals("a"))
            {
                return "first";
            }
            else
            {
                return "second";
            }
        }
    }
}

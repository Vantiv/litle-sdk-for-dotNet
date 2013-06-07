using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace LitleSdkForNetTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Litle.Sdk.Test.Certification.TestCert6Batch test = new Litle.Sdk.Test.Certification.TestCert6Batch();

            test.setUp();
            test.Test1Auth();


            string[] my_args = { Assembly.GetExecutingAssembly().Location };

            int returnCode = NUnit.ConsoleRunner.Runner.Main(my_args);

            if (returnCode != 0)
                Console.Beep();
        }
    }
}

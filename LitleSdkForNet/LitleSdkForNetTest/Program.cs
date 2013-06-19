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
            Litle.Sdk.Test.Functional.TestBatchStream test = new Litle.Sdk.Test.Functional.TestBatchStream();

            test.setUp();
            test.setUpBeforeTest();
            test.SimpleBatch();

            string[] my_args = { Assembly.GetExecutingAssembly().Location };

            int returnCode = NUnit.ConsoleRunner.Runner.Main(my_args);

            if (returnCode != 0)
                Console.Beep();
        }
    }
}

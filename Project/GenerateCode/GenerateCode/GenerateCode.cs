using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.IO;
using Xsd2Code.Library;
using Xsd2Code.Library.Helpers;

namespace GenerateCode
{
    public class GenerateCode
    {
        private const string CodeGenerationNamespace = "Xsd2Code.TestUnit";

        public static void Main()
        {
            GeneratorFacade xsdGen = new GeneratorFacade(GetGeneratorParams("C:\\msysgit\\litle-sdk-for-dotNet\\Project\\GenerateCode\\GenerateCode\\xsd\\litleOnline_v8.10.xsd"));
            Result<string> result = xsdGen.Generate();

            xsdGen = new GeneratorFacade(GetGeneratorParams("C:\\msysgit\\litle-sdk-for-dotNet\\Project\\GenerateCode\\GenerateCode\\xsd\\litleTransaction_v8.10.xsd"));
            result = xsdGen.Generate();

            xsdGen = new GeneratorFacade(GetGeneratorParams("C:\\msysgit\\litle-sdk-for-dotNet\\Project\\GenerateCode\\GenerateCode\\xsd\\litleCommon_v8.10.xsd"));
            result = xsdGen.Generate();
        }

        private static GeneratorParams GetGeneratorParams(string inputFilePath)
        {
            var generatorParams = new GeneratorParams
                       {
                           InputFilePath = inputFilePath,
                           NameSpace = CodeGenerationNamespace,
                           TargetFramework = TargetFramework.Net20,
                           CollectionObjectType = CollectionType.ObservableCollection,
                           EnableDataBinding = true,
                           GenerateDataContracts = true,
                           GenerateCloneMethod = true,
                           OutputFilePath = GetOutputFilePath(inputFilePath)
                       };
            generatorParams.Miscellaneous.HidePrivateFieldInIde = true;
            generatorParams.Miscellaneous.DisableDebug = true;
            generatorParams.Serialization.Enabled = true;
            return generatorParams;
        }

        static private string GetOutputFilePath(string inputFilePath)
        {
            return Path.ChangeExtension(inputFilePath, ".TestGenerated.cs");
        }

    }
}

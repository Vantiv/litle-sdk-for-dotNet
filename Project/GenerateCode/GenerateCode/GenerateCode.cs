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
            ModifyXSDs mdfxsdObj = new ModifyXSDs("8.11");
            string[] fileArrayToGenerateFrom = mdfxsdObj.getXSDFileList();
            String pathToPass = "";
            System.Threading.Thread.Sleep(3000);
            // calls to generate the code
            foreach (string fileName in fileArrayToGenerateFrom)
            {
                pathToPass = System.IO.Path.GetFullPath(fileName);
                if (System.IO.File.Exists(fileName))
                {
                    pathToPass = System.IO.Path.GetFullPath(fileName);
                }
                GeneratorFacade xsdGen = new GeneratorFacade(GetGeneratorParams(pathToPass));
                Result<string> result = xsdGen.Generate();
            }
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

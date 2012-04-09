using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.IO;
using Xsd2Code.Library;
using Xsd2Code.Library.Helpers;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace GenerateCode
{
    public class GenerateCode
    {
        private const string CodeGenerationNamespace = "LitleXSDGenerated";

        private static string generatedCodeDir = "";

        public static string versionToGenerate = "8.10";

        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                versionToGenerate = args[0];
            }
            ModifyXSDs mdfxsdObj = new ModifyXSDs(versionToGenerate);
            string[] fileArrayToGenerateFrom = mdfxsdObj.getXSDFileList();
            String pathToPass = "";
            System.Threading.Thread.Sleep(3000);
            // calls to generate the code
            foreach (string fileName in fileArrayToGenerateFrom)
            {
                pathToPass = System.IO.Path.GetFullPath(fileName);
               if (pathToPass.IndexOf("litleOnline") != -1)
                {
                    GeneratorFacade xsdGen = new GeneratorFacade(GetGeneratorParams(pathToPass));
                    Result<string> result = xsdGen.Generate();
                    // delete the modified xsd file.
                    File.Delete(fileName);
                }
            }

            BuildGeneratedCode();
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

        private static string GetOutputFilePath(string inputFilePath)
        {
            String directoryForFile = Path.GetDirectoryName(Path.GetDirectoryName(inputFilePath));
            generatedCodeDir = directoryForFile;
            String valToReturn = "";

            if (inputFilePath.IndexOf("litleCommon") != -1)
            {
                valToReturn = directoryForFile + "\\generated\\litleCommon.cs";
            }
            else if (inputFilePath.IndexOf("litleOnline") != -1)
            {
                valToReturn = directoryForFile + "\\generated\\litleOnline.cs";
            }
            else if (inputFilePath.IndexOf("litleTransaction") != -1)
            {
                valToReturn = directoryForFile + "\\generated\\litleTransaction.cs";
            }

            return valToReturn;
        }

        public static void BuildGeneratedCode()
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters cp = new CompilerParameters();
            cp.OutputAssembly = generatedCodeDir + "\\generated\\litleXSDGenerated.dll";
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Xml.dll");

            CompilerResults result = provider.CompileAssemblyFromFile(cp, (generatedCodeDir + "\\generated\\litleOnline.cs"));
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace GenerateCode
{
    class ModifyXSDs
    {
        private String xsdVersionNumber = "";
        private String pathToXSDFolder = "xsd\\";

        /*
         * Constructor
         * */
        public ModifyXSDs(String in_versionNumber = "8.10")
        {
            this.xsdVersionNumber = in_versionNumber;
        }

        /*
        * Constructor
        * */
        public ModifyXSDs(String in_versionNumber, String in_pathToXSDFolder)
        {
            this.xsdVersionNumber = in_versionNumber;
            this.pathToXSDFolder = in_pathToXSDFolder;
        }

        /**
         * This method scans the xsd folder, and returns all the xsd files that match the xsdVersionNumber.
         * */
        public string[] getXSDFileList()
        {
            String filesToSearchFor = "*" + xsdVersionNumber + "*.xsd";
            string[] fileArrayToReturn = Directory.GetFiles(pathToXSDFolder, filesToSearchFor, SearchOption.AllDirectories);

            int i = 0;
            foreach (string file in fileArrayToReturn)
            {
                fileArrayToReturn[i++] = modifyXSDFileForGenerationgCode(file);
            }

            return fileArrayToReturn;
        }

        public String modifyXSDFileForGenerationgCode(String filepath)
        {
            String pathToReturn = "";
            pathToReturn = filepath.Replace(".xsd", "_toGenerate.xsd");
            Process.Start("modifyXSD.vbs", (filepath + " " + pathToReturn));
            return pathToReturn;
        }
    }
}

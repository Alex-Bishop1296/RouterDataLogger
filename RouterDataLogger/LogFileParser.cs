// The purpose of this class is to take a directory passed in and find the fields of each file in the subdirectories of the folder and path for passing entries to the database
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace RouterDataLogger
{
    class LogFileParser
    {
        /// <summary>
        /// A DirectoryInfo object containing the path to the Logs folder
        /// </summary>
        private DirectoryInfo logsFolder;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="directoryInfo">A DirectoryInfo object containing the path to the Logs folder</param>
        public LogFileParser(DirectoryInfo directoryInfo)
        {
            logsFolder = directoryInfo;
            ParseFiles();
        }
        
        private void ParseFiles()
        {
            int count = 0;

            //Start a search of the Logs folder, only looking at files ending in "WL5GInterference.xml"
            foreach (FileInfo file in logsFolder.GetFiles("*WL5GInterference.xml", SearchOption.AllDirectories))
            {
                string filepath = file.FullName;
                XDocument xmlDoc = XDocument.Load(filepath);
                //If any none in the XML tree contains the element "Invalid Parameter Name"
                if (xmlDoc.Descendants("FaultString").Any())
                {
                    //File does not contain a status element, skip it
                    continue;
                }
                Console.WriteLine(file.FullName);
                count++;
                //Retrieve the Status Code of the Router Log
                //C: \Users\Alex\source\repos\RouterDataLogger\RouterDataLogger\Logs\D8B6B7EF8EC5\20210401\140305001 - InternetGatewayDevice.X_COMTREND_COM_AppCfg.WL5GInterference.xml
                //< Value xsi: type = "xsd:string" xmlns: xsi = "http://www.w3.org/2001/XMLSchema-instance" > Acceptable </ Value >
                XElement statusCode = xmlDoc.Descendants("ParameterValueStruct").Descendants("Value").First();
                Console.WriteLine(statusCode);
                Console.WriteLine("Status code is: {0}", (string)statusCode);

                //Get the Serial code from the filepath using Regex, assuming the filepath contains Logs folder in problem described architecture 
                Regex findSerialCode = new Regex(@"(?<=Logs\\)(.*?)(?=\\)");
                Match foundSerialCode = findSerialCode.Match(filepath);
                if(foundSerialCode.Success) 
                {
                    string serialCode = foundSerialCode.Value;
                    Console.WriteLine("Serial code is: {0}", serialCode);
                }
                else
                {
                    Console.WriteLine("Failed to find serial code for filepath: {0}", filepath);
                }
            }
            Console.WriteLine("{0} files found", count);
        }

    }
}

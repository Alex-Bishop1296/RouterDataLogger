// The purpose of this class is to take a directory passed in and find the fields of each file in the subdirectories of the folder and path for passing entries to the database.
// This class relies on the given file structure for the problem, the "Logs" folder and it's subfolders. If the structure of that folder organization was changed, this class would break
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;

namespace RouterDataLogger
{
    class LogFileParser
    {
        /// <summary>
        /// A DirectoryInfo object containing the path to the Logs folder
        /// </summary>
        private DirectoryInfo logsFolder;
        /// <summary>
        /// A DBcontext object for our database to access the routers log table
        /// </summary>
        private RoutersContext db;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="directoryInfo">A DirectoryInfo object containing the path to the Logs folder</param>
        public LogFileParser(DirectoryInfo directoryInfo)
        {
            db = new RoutersContext();
            logsFolder = directoryInfo;
            DeleteDatabaseAndRecreate();
            ParseFiles();
        }
        
        /// <summary>
        /// Main method of the class, takes the DirectoryInfo object and starts to parse it as if it were the Logs folder, placing the appropriate 
        /// </summary>
        private void ParseFiles()
        {
            int count = 0;  // Debuging, used to keep track of file count
            // Start a search of the Logs folder, only looking at files ending in "WL5GInterference.xml"
            foreach (FileInfo file in logsFolder.GetFiles("*WL5GInterference.xml", SearchOption.AllDirectories))
            {
                string filepath = file.FullName;
                XDocument xmlDoc = XDocument.Load(filepath);
                // If any nodes in the XML tree contains the element "FaultString"
                if (xmlDoc.Descendants("FaultString").Any())
                {
                    //File does not contain a status element, skip it
                    continue;
                }
                Console.WriteLine(file.FullName);
                // Retrieve the Status Code of the Router Log
                XElement statusCodeElement = xmlDoc.Descendants("ParameterValueStruct").Descendants("Value").First();
                string statusCode = (string)statusCodeElement;
                Console.WriteLine("Status code is: {0}", statusCode);

                //Retrieve the Serial Code and Timestamp of the Router Log
                string serialCode = ParseSerialCode(filepath);
                DateTime timeStamp = ParseTimestamp(file, serialCode);
                CreateDatabaseEntry(statusCode, serialCode, timeStamp);
                count++;
            }
            Console.WriteLine("{0} files were parsed", count);
        }

        private string ParseSerialCode(string filepath)
        {
            // Get the Serial code from the filepath using Regex, assuming the filepath contains Logs folder in problem described architecture 
            Regex findSerialCode = new Regex(@"(?<=Logs\\)(.*?)(?=\\)");
            Match foundSerialCode = findSerialCode.Match(filepath);
            if (foundSerialCode.Success)
            {
                Console.WriteLine("Serial code is: {0}", foundSerialCode.Value);
            }
            else
            {
                Console.WriteLine("Failed to find serial code for filepath: {0}", filepath);
            }
            return foundSerialCode.Value;
        }

        private DateTime ParseTimestamp(FileInfo file, string serialCode)
        {
            // Get the Timestamp using the filepath and the numbers at the start of the filename, filepath has YYYYMMDD, filename has HHMMSSfff with fff representing milliseconds
            Regex findYearsMonthsDays = new Regex(@"(?<=" + serialCode + @"\\)(.*?)(?=\\)");
            Match foundYearsMonthDays = findYearsMonthsDays.Match(file.FullName);
            string logdate = foundYearsMonthDays.Value;
            string logtime = file.Name.Substring(0, file.Name.IndexOf('-'));
            CultureInfo provider = new CultureInfo("en-US");

            // Convert the timestamps into a format that will be accepted by DateTime
            DateTime timeStamp = DateTime.ParseExact(logdate + logtime, "yyyyMMddHHmmssfff", provider, DateTimeStyles.AssumeLocal);
            Console.WriteLine("Timestamp of entry is: {0}", timeStamp);
            return timeStamp;
        }

        /// <summary>
        /// Adds a Router Log to our database using the given inputs
        /// </summary>
        /// <param name="statusCode">The Status of the router log to add</param>
        /// <param name="serialCode">The Serial of the router log to add</param>
        /// <param name="timeStamp">The Timestamp of the router log to add</param>
        private void CreateDatabaseEntry(string statusCode, string serialCode, DateTime timeStamp)
        {
            RouterLog addedLog = new RouterLog()
            {
                Serial = serialCode,
                Status = statusCode,
                Timestamp = timeStamp
            };
            db.RouterLogs.Add(addedLog);
            db.SaveChanges();
        }

        /// <summary>
        /// Deletes the database and recreates it
        /// </summary>
        private void DeleteDatabaseAndRecreate()
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}

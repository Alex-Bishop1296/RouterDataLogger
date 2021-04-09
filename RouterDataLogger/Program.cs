using System;
using System.IO;


namespace RouterDataLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dev note: Replace path to Logs folder here, THE FOLDER MUST BE NAMED "Logs"
            DirectoryInfo logFiles = new DirectoryInfo(@"C:\Users\Alex\source\repos\RouterDataLogger\RouterDataLogger\Logs");
            try
            {
                if (logFiles.Exists)
                {
                    Console.WriteLine("The filepath {0} exists", logFiles);
                    LogFileParser logFileParser = new LogFileParser(logFiles);
                }
                if (!logFiles.Exists)
                {
                    Console.WriteLine("The filepath {0} does not exist", logFiles);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            //Prevent Console app from exiting before I can read it
            Console.ReadLine();
        }
    }
}

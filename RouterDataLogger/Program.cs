// Program.cs for the RouterDataLogger ASP.NET Core Console application
using System;
using System.IO;


namespace RouterDataLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            string response;                // Used to get user input
            string filePathCheck;           // Used to get user input filepath
            bool validResponse = false;     // Used to validate user input
            // File path directly written in to use
            // Dev note: Replace path to Logs folder here, THE FOLDER MUST BE NAMED "Logs"
            DirectoryInfo logFiles = new DirectoryInfo(@"C:\Users\Alex\source\repos\RouterDataLogger\RouterDataLogger\Logs");
            //Greet user and explain program
            Console.WriteLine("Welcome. This application can be used to take router Logs located in the 'Logs' folder and place the information in the path and WL5GInterference.xml status information." +
                                "\r\n--The filepath has been set in the Program.cs, but can be set manually by entering \"set path\" to the next prompt, enter any other command to move on with default path.");
            filePathCheck = Console.ReadLine();
            if(filePathCheck=="set path")
            {
                Console.WriteLine("--Enter file path:");
                logFiles = new DirectoryInfo(Console.ReadLine());
            }
            Console.WriteLine("--WARNING:: If you execute this program it will delete the database containing log files if it exists and make a new one, is that okay? Y/N");
            while (!validResponse)
            {
                response = Console.ReadLine();
                if (response == "y" | response == "Y" | response == "yes" | response == "Yes")
                {
                    validResponse = true;
                    //Add functionality to drop database before creation step
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
                }
                else if (response == "n" | response == "N" | response == "no" | response == "No")
                {
                    validResponse = true;
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("--Invalid response, please enter y or n.");
                }
            }
        }
    }
}

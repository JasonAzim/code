using System;
using System.Diagnostics;


namespace reverse
{
    public class FileHelper
    {
        public string CurrentDirectory { get; set; }

        public string CurrentSolutionFileName { get; set; }

        public string SolutionFilePath { get; set; }

        public FileHelper()
        {
        }

        public FileHelper(string currentDirectory, string currentSolutionFileName)
        {
            SolutionFilePath = CurrentDirectory + @"\" + CurrentSolutionFileName;
        }

        public string[] GetProjectNames()
        {
            // if File Exists, read it and parse for project names
            if (!File.Exists(SolutionFilePath))
            {
                // Create a file to write to. 
                string[] readText = File.ReadAllLines(path);

                foreach (string s in readText)
                {
                    //Console.WriteLine(s);
                    Debug.WriteLine(line);

                }
            }
        }
    }
}
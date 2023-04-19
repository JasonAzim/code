using System;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace reverse
{
    public class DirectoryHelper
    {
        public int Max { get; set; }

        public int ErrorCount { get; set; }
        public string ErrorMessage { get; set; }
        public string FileExtension { get; set; }
        public List<string> FileList { get; set; }

        private string output = string.Empty;
        private string last = string.Empty;

        private int counter = 0;

        public bool? DebugMode { get; set; }

        public DirectoryHelper()
        {
            DebugMode = null;

            FileList = new List<string>();

            ErrorCount = 0;
            ErrorMessage = string.Empty;

            FileExtension = "*.*";
            counter = 0;
            Max = 0;
        }

        public void PrintLine(string output)
        {
            if (DebugMode.HasValue)
            {
                if (DebugMode.Value == true)
                {
                    Debug.WriteLine(output);
                }
                else
                {
                    Console.WriteLine(output); 
                }
            }
            else
            {
            }
        }

        public void BuildFileList(string StartingDirectory, string fileExtension)
        {
            counter = 0;
            Max = 0;
            FileList = new List<string>();

            FileExtension = fileExtension;
            FileSearch(StartingDirectory);
        }

        public void SearchSolutionFiles(string StartingDirectory)
        {
            counter = 0;
            Max = 0;
            FileList = new List<string>();

            FileExtension = "*.sln";
            DirSearch(StartingDirectory); 
        }

        public void BuildFileTypeList(string CurrentDirectory)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(CurrentDirectory))
                {
                    foreach (string filename in Directory.GetFiles(dir, FileExtension))
                    {
                        counter = counter + 1;
                        output = Path.GetFileName(filename);

                        if (output.IndexOf(".") > 0)
                        {
                            last = output.Substring(output.LastIndexOf('.') + 1);

                            if (FileList.Contains(last))
                            {

                            }
                            else
                            {

                                FileList.Add(last);
                                PrintLine(filename);
                            }
                        }
                    }

                    BuildFileTypeList(dir);
                }
            }
            catch (Exception exp)
            {
                ErrorCount = ErrorCount + 1;
                PrintLine(exp.Message);
            }
        }

        public void DirSearch(string CurrentDirectory)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(CurrentDirectory))
                {
                    foreach (string filename in Directory.GetFiles(dir, FileExtension))
                    {
                        counter = counter + 1;
                        output = filename + @"," + Path.GetFileName(filename);
                        PrintLine(output);
                        FileList.Add(output);
                    }

                    DirSearch(dir);
                }
            }
            catch (Exception exp)
            {
                ErrorCount = ErrorCount + 1;
                PrintLine(exp.Message);
            }
        }

        public void FileSearch(string CurrentDirectory)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(CurrentDirectory))
                {
                    foreach (string filename in Directory.GetFiles(dir, FileExtension))
                    {
                        counter = counter + 1;
                        output = filename;
                        PrintLine(output);
                        FileList.Add(output);
                    }

                    //if (counter > Max) break;
                    FileSearch(dir);
                }
            }
            catch (Exception exp)
            {
                ErrorCount = ErrorCount + 1;
                PrintLine(exp.Message);
            }
        }

        public void PrintErrorInfo()
        {
            PrintLine("Error count = " + ErrorCount); 
        }
    }
}
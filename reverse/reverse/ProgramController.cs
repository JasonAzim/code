using System;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic; 


namespace reverse
{
    public class ProgramController
    {
        public string OutputDirectory { get; set; }
        public string SolutionFileListReportName { get; set; }
        public string FileTypeListReportName { get; set; }

        public bool? DebugMode { get; set; }

        public ProgramController()
        {
            OutputDirectory = @"C:\work\reverse\output";
            SolutionFileListReportName = @"SolutionFiles.csv";
            FileTypeListReportName = @"FileTypes.csv";
        }

        public void OutputText(string FileName, List<string> InputList)
        {
            StreamWriter OutputFile;
            OutputFile = File.CreateText(FileName);

            foreach (string line in InputList)
            {
                OutputFile.WriteLine(line);
            }

            OutputFile.Close();
        }

        public void AppendText(string FileName, List<string> InputList)
        {
            StreamWriter OutputFile;
            OutputFile = File.AppendText(FileName);

            foreach (string line in InputList)
            {
                OutputFile.WriteLine(line);
            }

            OutputFile.Close();
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

        public void FindTextInFile(string StartingDirectoryName, string FileExtension, string SearchPattern)
        {
            DirectoryHelper DirSearch = new DirectoryHelper();

            DirSearch.DebugMode = false;
            DirSearch.Max = 20;

            DirSearch.BuildFileList(StartingDirectoryName, FileExtension);

            SolutionInfo SI = new SolutionInfo();
            
            FileHelper FileSearch = new FileHelper();

            FileSearch.DebugMode = false;

            foreach (string filename in DirSearch.FileList)
            {
                FileSearch.ParseCodeFileNew(SI, SearchPattern, filename);
            }
        }

        public void GetUniqueFileTypeList(string StartingDirectoryName)
        {
            DirectoryHelper helper = new DirectoryHelper();

            helper.DebugMode = true;

            helper.BuildFileTypeList(StartingDirectoryName);
 
            string SolutionFileList = OutputDirectory + @"\" + FileTypeListReportName;

            OutputText(SolutionFileList, helper.FileList);
        }

        public void CreateSolutionFilesList(string StartingDirectoryName)
        {
            DirectoryHelper helper = new DirectoryHelper();

            helper.DebugMode = true;

            helper.SearchSolutionFiles(StartingDirectoryName);

            string SolutionFileList = OutputDirectory + @"\" + SolutionFileListReportName;

            OutputText(SolutionFileList, helper.FileList);
        }

        public string GetLastPart(string input)
        {
            string[] parts = input.Split(new Char[] { '\\' });

            string result = string.Empty;

            for (int i = 0; i < parts.Length - 1; i++)
            {
                if (i == 0)
                {
                    result = parts[i];
                }
                else
                {
                    result = result + @"\" + parts[i];
                }
            }

            return result;
        }

        public void ProcessSolutionFiles()
        {
            string SolutionFileList = OutputDirectory + @"\" + SolutionFileListReportName;

            SolutionInfo SI = new SolutionInfo();
            FileHelper helper = new FileHelper();

            helper.DebugMode = false;

            string[] readText = File.ReadAllLines(SolutionFileList);

            string ProjectFolder = string.Empty;
            string ProjectFileName = string.Empty;

            foreach (string line in readText)
            {
                string[] parts = line.Split(new Char[] { ',' });

                ProjectFileName = parts[0];
                SI.ProjectList = helper.GetProjectNames(ProjectFileName);

                if (SI.ProjectList.Count == 0)
                {
                    PrintLine(ProjectFileName + ",None"); 
                }
                else
                {
                    //Debugger.Break();

                    DebugMode = false;
                    ProjectFolder = GetLastPart(ProjectFileName);
                    SI.SolutionFilePath = ProjectFileName;
                    SI.BaseDirectoryName = ProjectFolder;

                    //Debug.WriteLine("Print Project Include List");
                    //helper.LoadProjectIncludeListNew(SI);

                    //Debug.WriteLine("Print Project Reference List");
                    //helper.LoadProjectReferenceListNew(SI);

                    DebugMode = null;
                    Debug.WriteLine("Print Project File List");
                    helper.LoadProjectFileListNew(SI);

                    DebugMode = false;
                    Debug.WriteLine("Parse files");
                    helper.ParseCodeFileNew(SI, "^using");
                }
            }            
        }

        public void ProcessSolutionFile(string CurrentDirectory, string CurrentSolutionFileName)
        {
            SolutionInfo SI = new SolutionInfo(CurrentDirectory, CurrentSolutionFileName);

            FileHelper helper = new FileHelper();

            Debug.WriteLine("Print Solution Project Names");
            helper.DebugMode = this.DebugMode;
            SI.ProjectList = helper.GetProjectNames(SI.SolutionFilePath);

            
            Debug.WriteLine("Print Project Reference List");
            helper.LoadProjectReferenceList(SI);

            Debug.WriteLine("Print Project Include List");
            helper.LoadProjectIncludeList(SI);

            Debug.WriteLine("Print Project File List");
            helper.LoadProjectFileList(SI);

            Debug.WriteLine("Parse files");
            helper.ParseCodeFile(SI);
            

            helper.PrintErrorInfo();
        }

        public void ListSolutionFiles(string StartingDirectoryName)
        {
            DirectoryHelper helper = new DirectoryHelper();

            helper.SearchSolutionFiles(StartingDirectoryName);
        }

        public void Run1()
        {
            string CurrentDirectory = @"C:\work\tfsdev\iFolder\RealEC.iFolder";
            string CurrentSolutionFileName = @"RealEC.iFolder.sln";

            SolutionInfo SI = new SolutionInfo(CurrentDirectory, CurrentSolutionFileName);

            FileHelper helper = new FileHelper();

            Debug.WriteLine("Print Solution Project Names");
            SI.ProjectList = helper.GetProjectNames(SI.SolutionFilePath);

            Debug.WriteLine("Print Project Reference List");
            helper.LoadProjectReferenceList(SI);

            Debug.WriteLine("Print Project Include List");
            helper.LoadProjectIncludeList(SI);

            Debug.WriteLine("Print Project File List");
            helper.LoadProjectFileList(SI);

            Debug.WriteLine("Parse files");
            helper.ParseCodeFile(SI);

            helper.PrintErrorInfo();
        }

        public void Run2()
        {
            string CurrentDirectory = @"C:\work\tfs\SystemEngine\iFolder\Main";
            string CurrentSolutionFileName = @"QI.sln";

            SolutionInfo SI = new SolutionInfo(CurrentDirectory, CurrentSolutionFileName);

            FileHelper helper = new FileHelper();

            Debug.WriteLine("Print Solution Project Names");
            SI.ProjectList = helper.GetProjectNames(SI.SolutionFilePath);

            Debug.WriteLine("Print Project Reference List");
            helper.LoadProjectReferenceList(SI);

            Debug.WriteLine("Print Project Include List");
            helper.LoadProjectIncludeList(SI);

            Debug.WriteLine("Print Project File List");
            helper.LoadProjectFileList(SI);

            Debug.WriteLine("Parse files");
            helper.ParseCodeFile(SI);

            helper.PrintErrorInfo();
        }

    }
}
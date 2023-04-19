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
    public class FileHelper
    {
        public int ErrorCount { get; set; }
        public string ErrorMessage { get; set; }

        public bool? DebugMode { get; set; }

        public FileHelper()
        {
            ErrorCount = 0;
            ErrorMessage = string.Empty;
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

        public static string RemoveSemiColon(string input)
        {
            return input.Replace(";", "");
        }

        public static string RemoveBracketSmallLeft(string input)
        {
            return input.Replace("(", "");
        }

        public static string RemoveBracketSmallRight(string input)
        {
            return input.Replace(")", "");
        }

        public static string RemoveBracketMediumLeft(string input)
        {
            return input.Replace("{", "");
        }

        public static string RemoveBracketMediumRight(string input)
        {
            return input.Replace("}", "");
        }

        public static string RemoveQuotes(string input)
        {
            return input.Replace("\"", "");
        }

        public static string GetProjectGuid(string input)
        {
            string result = string.Empty;

            string[] parts = input.Split(new Char[] { '=' });

            if (parts.Length > 0)
            {
                result = parts[0].Replace("Project", "");
                result = RemoveBracketSmallLeft(result);
                result = RemoveBracketSmallRight(result);
                result = RemoveQuotes(result);
                result = RemoveBracketMediumLeft(result);
                result = RemoveBracketMediumRight(result);
            }

            return result.Trim();
        }

        public static string GetProjectName(string input)
        {
            string result = string.Empty;

            string[] parts = input.Split(new Char[] { '=' });

            if (parts.Length > 0)
            {
                result = RemoveQuotes(parts[1]);
            }

            return result.Trim();
        }

        public static string GetProjectFileName(string input)
        {
            string result = string.Empty;

            result = RemoveQuotes(input);

            return result.Trim();
        }

        public static string GetProjectReferenceGuid(string input)
        {
            string result = string.Empty;

            result = RemoveQuotes(input);
            result = RemoveBracketMediumLeft(result);
            result = RemoveBracketMediumRight(result);

            return result.Trim();
        }

        public static ProjectInfo GetProjectInfo(string input)
        {
            ProjectInfo data = new ProjectInfo();

            // Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "RealEC.iFolder.Data", "RealEC.iFolder.Data\RealEC.iFolder.Data.csproj", "{CCD79094-614A-4B6A-BAB7-C58BF89A8706}"

            string[] parts = input.Split(new Char[] { ',' });

            if (parts.Length == 3)
            {
                data.ProjectGuid = GetProjectGuid(parts[0]);
                data.ProjectName = GetProjectName(parts[0]);
                data.ProjectFileName = GetProjectFileName(parts[1]);
                data.ProjectReferenceGuid = GetProjectReferenceGuid(parts[2]);
            }
            else
            {

            }

            return data;
        }

        public static string GetIncludeText(string input)
        {
            string result = string.Empty;

            string[] parts = input.Split(new Char[] { '=' });

            if (parts.Length > 0)
            {
                result = parts[1].Replace("xmlns", "");
                result = RemoveQuotes(result);
            }
            else
            {

            }

            return result.Trim();
        }

        public static string GetDLLName(string input)
        {
            string result = string.Empty;

            result = input.Replace("using", "");
            result = RemoveSemiColon(result);

            return result.Trim();
        }

        public void WriteFileText(string FileName, bool? Option)
        {
            string[] readText = File.ReadAllLines(FileName);

            foreach (string line in readText)
            {
                if (Option.HasValue)
                {
                    if (Option == true)
                    {
                        Console.WriteLine(line);
                    }
                    else
                    {
                        Debug.WriteLine(line);
                    }
                }
                else
                {
                    
                }
            }
        }

        public List<ProjectInfo> GetProjectNames(string SolutionFilePath)
        {
            // "Microsoft Visual Studio Solution File, Format Version 11.00"
            string pattern1 = "Version";
            string Version = string.Empty;

            string pattern2 = "^Project";

            List<ProjectInfo> result = new List<ProjectInfo>();
            ProjectInfo data = new ProjectInfo("none");

            // if File Exists, read it and parse for project names
            if (File.Exists(SolutionFilePath))
            {
                string[] readText = File.ReadAllLines(SolutionFilePath);

                foreach (string line in readText)
                {

                    if (Regex.IsMatch(line, pattern1, RegexOptions.IgnoreCase))
                    {
                        if (line == "Microsoft Visual Studio Solution File, Format Version 11.00")
                        {
                            Version = "11.00";
                        }
                        
                    }
                    else if (Regex.IsMatch(line, pattern2, RegexOptions.IgnoreCase))
                    {
                        // Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "RealEC.iFolder.Data", "RealEC.iFolder.Data\RealEC.iFolder.Data.csproj", "{CCD79094-614A-4B6A-BAB7-C58BF89A8706}"
                        data = GetProjectInfo(line);

                        // Only CSharp and VB Projects are supported for now
                        if (data.ProjectFileName.IndexOf("csproj") > 0)
                        {
                            PrintLine(data.ProjectFileName);
                            result.Add(data);
                        }
                        else if (data.ProjectFileName.IndexOf("vbproj") > 0)
                        {
                            PrintLine(data.ProjectFileName);
                            result.Add(data);
                        }
                    }
                    else
                    {
                        //Debug.WriteLine();
                    }
                }
            }

            return result;
        }

        public void LoadProjectIncludeListNew(SolutionInfo solution)
        {
            List<string> ResultList = new List<string>();

            string ProjectFilePath = string.Empty;

            for (int i = 0; i < solution.ProjectList.Count; i++)
            {
                ProjectFilePath = solution.BaseDirectoryName + @"\" + solution.ProjectList[i].ProjectFileName;

                if (File.Exists(ProjectFilePath))
                {
                    ResultList = new List<string>();

                    XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

                    XDocument ProjectDefinition = XDocument.Load(ProjectFilePath);

                    //var nodes = ProjectDefinition.Descendants(ns + "Project").Descendants(ns + "ItemGroup");
                    var nodes = ProjectDefinition.Descendants(ns + "Project").Descendants(ns + "ItemGroup").Descendants(ns + "Reference");

                    string ProjectInclude = string.Empty;
                    string output = string.Empty;

                    foreach (var n in nodes)
                    {
                        ProjectInclude = GetIncludeText(n.ToString());
                        //ResultList.Add(ProjectInclude);
                        output = solution.SolutionFilePath + "," + ProjectFilePath + "," + ProjectInclude;
                        ResultList.Add(output);
                        //PrintLine(output); 
                        Debug.WriteLine(output);
                    }

                    solution.ProjectList[i].ProjectIncludeList = ResultList;
                }
                else
                {
                    ErrorCount = ErrorCount + 1;
                }
            }

        }

        public void LoadProjectReferenceListNew(SolutionInfo solution)
        {
            List<string> ResultList = null;

            string ProjectFilePath = string.Empty;

            for (int i = 0; i < solution.ProjectList.Count; i++)
            {
                ProjectFilePath = solution.BaseDirectoryName + @"\" + solution.ProjectList[i].ProjectFileName;

                if (File.Exists(ProjectFilePath))
                {
                    ResultList = new List<string>();

                    XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

                    XDocument ProjectDefinition = XDocument.Load(ProjectFilePath);

                    var nodes = ProjectDefinition.Descendants(ns + "Project").Descendants(ns + "ItemGroup").Descendants(ns + "ProjectReference");

                    string ProjectInclude = string.Empty;
                    string output = string.Empty;

                    foreach (var n in nodes)
                    {
                        ProjectInclude = GetIncludeText(n.ToString());
                        //ResultList.Add(ProjectInclude);
                        output = ProjectFilePath + "," + ProjectInclude;
                        output = solution.SolutionFilePath + "," + ProjectFilePath + "," + ProjectInclude;
                        ResultList.Add(output);

                        Debug.WriteLine(output);
                    }

                    solution.ProjectList[i].ProjectReferenceList = ResultList;
                }
                else
                {
                    ErrorCount = ErrorCount + 1;
                }
            }

        }

        public void LoadProjectFileListNew(SolutionInfo solution)
        {
            List<string> ResultList = null;

            string ProjectFilePath = string.Empty;

            for (int i = 0; i < solution.ProjectList.Count; i++)
            {
                ProjectFilePath = solution.BaseDirectoryName + @"\" + solution.ProjectList[i].ProjectFileName;

                if (File.Exists(ProjectFilePath))
                {
                    ResultList = new List<string>();

                    XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

                    XDocument ProjectDefinition = XDocument.Load(ProjectFilePath);

                    var nodes = ProjectDefinition.Descendants(ns + "Project").Descendants(ns + "ItemGroup").Descendants(ns + "Compile");

                    string ProjectInclude = string.Empty;
                    string output = string.Empty;

                    foreach (var n in nodes)
                    {
                        ProjectInclude = GetIncludeText(n.ToString());
                        ResultList.Add(ProjectInclude);
                        //output = ProjectFilePath + "," + ProjectInclude;
                        output = solution.SolutionFilePath + "," + ProjectFilePath + "," + ProjectInclude;
                        PrintLine(output);
                    }

                    solution.ProjectList[i].ProjectFileList = ResultList;
                }
                else
                {
                    ErrorCount = ErrorCount + 1;
                }
            }

        }

        public void ParseCodeFileNew(SolutionInfo solution, string SearchPattern)
        {
            string ProjectFolder = string.Empty;
            string CurrentFileName = string.Empty;
            string output = string.Empty;
            string DLLName = string.Empty;

            // loop all projects
            for (int i = 0; i < solution.ProjectList.Count; i++)
            {
                ProjectFolder = solution.BaseDirectoryName + @"\" + solution.ProjectList[i].ProjectName;

                for (int j = 0; j < solution.ProjectList[i].ProjectFileList.Count; j++)
                {
                    // C:\work\tfsdev\iFolder\RealEC.iFolder\RealEC.iFolder.Data\Constants.cs

                    CurrentFileName = ProjectFolder + @"\" + solution.ProjectList[i].ProjectFileList[j];

                    if (File.Exists(CurrentFileName))
                    {
                        string[] readText = File.ReadAllLines(CurrentFileName);

                        foreach (string line in readText)
                        {
                            if (Regex.IsMatch(line, SearchPattern, RegexOptions.IgnoreCase))
                            {
                                DLLName = GetDLLName(line);
                                output = ProjectFolder + @"," + solution.ProjectList[i].ProjectFileList[j] + @"," + DLLName;
                                PrintLine(output);
                            }
                        }
                    }
                    else
                    {
                        ErrorCount = ErrorCount + 1;
                    }
                }
            }
        }

        public void ParseCodeFileNew(SolutionInfo solution, string SearchPattern, string FileName)
        {
            string ProjectFolder = string.Empty;
            string output = string.Empty;
            string DLLName = string.Empty;

            if (File.Exists(FileName))
            {
                string[] readText = File.ReadAllLines(FileName);

                foreach (string line in readText)
                {
                    if (Regex.IsMatch(line, SearchPattern, RegexOptions.IgnoreCase))
                    {
                        output = line;
                        PrintLine(output);
                    }
                }
            }
            else
            {
                ErrorCount = ErrorCount + 1;
            }
        }

        public void LoadProjectReferenceList(SolutionInfo solution)
        {
            List<string> ResultList = null;

            string ProjectFilePath = string.Empty;

            for (int i = 0; i < solution.ProjectList.Count; i++)
            {
                ProjectFilePath = solution.BaseDirectoryName + @"\" + solution.ProjectList[i].ProjectFileName;

                if (File.Exists(ProjectFilePath))
                {
                    ResultList = new List<string>();

                    XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

                    XDocument ProjectDefinition = XDocument.Load(ProjectFilePath);

                    var nodes = ProjectDefinition.Descendants(ns + "Project").Descendants(ns + "ItemGroup").Descendants(ns + "ProjectReference");

                    string ProjectInclude = string.Empty;
                    string output = string.Empty;

                    foreach (var n in nodes)
                    {
                        ProjectInclude = GetIncludeText(n.ToString());
                        ResultList.Add(ProjectInclude);
                        output = ProjectFilePath + "," + ProjectInclude;
                        Debug.WriteLine(output);
                    }

                    solution.ProjectList[i].ProjectReferenceList = ResultList;
                }
                else
                {
                    ErrorCount = ErrorCount + 1;
                }
            }

        }

        public void LoadProjectIncludeList(SolutionInfo solution)
        {
            List<string> ResultList = new List<string>();

            string ProjectFilePath = string.Empty;

            for (int i = 0; i < solution.ProjectList.Count; i++)
            {
                ProjectFilePath = solution.BaseDirectoryName + @"\" + solution.ProjectList[i].ProjectFileName;

                if (File.Exists(ProjectFilePath))
                {
                    ResultList = new List<string>();

                    XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

                    XDocument ProjectDefinition = XDocument.Load(ProjectFilePath);

                    //var nodes = ProjectDefinition.Descendants(ns + "Project").Descendants(ns + "ItemGroup");
                    var nodes = ProjectDefinition.Descendants(ns + "Project").Descendants(ns + "ItemGroup").Descendants(ns + "Reference");

                    string ProjectInclude = string.Empty;
                    string output = string.Empty;

                    foreach (var n in nodes)
                    {
                        ProjectInclude = GetIncludeText(n.ToString());
                        ResultList.Add(ProjectInclude);
                        output = ProjectFilePath + "," + ProjectInclude;
                        Debug.WriteLine(output);
                    }

                    solution.ProjectList[i].ProjectIncludeList = ResultList;
                }
                else
                {
                    ErrorCount = ErrorCount + 1;
                }
            }

        }

        public void LoadProjectFileList(SolutionInfo solution)
        {
            List<string> ResultList = null;

            string ProjectFilePath = string.Empty;

            for (int i = 0; i < solution.ProjectList.Count; i++)
            {
                ProjectFilePath = solution.BaseDirectoryName + @"\" + solution.ProjectList[i].ProjectFileName;

                if (File.Exists(ProjectFilePath))
                {
                    ResultList = new List<string>();

                    XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";

                    XDocument ProjectDefinition = XDocument.Load(ProjectFilePath);

                    var nodes = ProjectDefinition.Descendants(ns + "Project").Descendants(ns + "ItemGroup").Descendants(ns + "Compile");

                    string ProjectInclude = string.Empty;
                    string output = string.Empty;

                    foreach (var n in nodes)
                    {
                        ProjectInclude = GetIncludeText(n.ToString());
                        ResultList.Add(ProjectInclude);
                        output = ProjectFilePath + "," + ProjectInclude;
                        Debug.WriteLine(output);
                    }

                    solution.ProjectList[i].ProjectFileList = ResultList;
                }
                else
                {
                    ErrorCount = ErrorCount + 1;
                }
            }

        }

        public void ParseCodeFile(SolutionInfo solution)
        {
            string pattern1 = "^using";

            string ProjectFolder = string.Empty;
            string CurrentFileName = string.Empty;
            string output = string.Empty;
            string DLLName = string.Empty;

            // loop all projects
            for (int i = 0; i < solution.ProjectList.Count; i++)
            {
                ProjectFolder = solution.BaseDirectoryName + @"\" + solution.ProjectList[i].ProjectName;

                for (int j = 0; j < solution.ProjectList[i].ProjectFileList.Count; j++)
                {
                    // C:\work\tfsdev\iFolder\RealEC.iFolder\RealEC.iFolder.Data\Constants.cs

                    CurrentFileName = ProjectFolder + @"\" + solution.ProjectList[i].ProjectFileList[j];

                    if (File.Exists(CurrentFileName))
                    {
                        string[] readText = File.ReadAllLines(CurrentFileName);

                        foreach (string line in readText)
                        {
                            if (Regex.IsMatch(line, pattern1, RegexOptions.IgnoreCase))
                            {
                                DLLName = GetDLLName(line);
                                output = ProjectFolder + @"," + solution.ProjectList[i].ProjectFileList[j] + @"," + DLLName;
                                Debug.WriteLine(output);
                            }
                        }
                    }
                    else
                    {
                        ErrorCount = ErrorCount + 1;
                    }
                }
            }
        }

        public void PrintErrorInfo()
        {
            Debug.WriteLine("Error count = " + ErrorCount); 
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic; 


namespace reverse
{
    public class SolutionInfo
    {
        // Directory where solution is located
        public string BaseDirectoryName { get; set; }

        // Name of solution file
        public string SolutionFileName { get; set; }

        public string SolutionFilePath { get; set; }

        public List<ProjectInfo> ProjectList { get; set; }

        public SolutionInfo()
        {
        }

        public SolutionInfo(string baseDirectoryName, string solutionFileName)
        {
            BaseDirectoryName = baseDirectoryName;
            SolutionFileName = solutionFileName;

            SolutionFilePath = BaseDirectoryName + @"\" + SolutionFileName;
        }

    }
}
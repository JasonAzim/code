using System;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic; 


namespace reverse
{
    public class ProjectInfo
    {
        public string ProjectGuid { get; set; }
        public string ProjectName { get; set; }
        public string ProjectReferenceGuid { get; set; }

        // Directory where project is located
        public string BaseDirectoryName { get; set; }

        // Name of project file
        public string ProjectFileName { get; set; }

        public string ProjectFilePath { get; set; }

        // Data in the ProjectReference tag for msbuild, used for project dependency identification
        public List<string> ProjectReferenceList { get; set; }

        // Data in the Reference tag for msbuild, used for project dependency identification
        public List<string> ProjectIncludeList { get; set; }

        // Data in the Compile tag for msbuild, used for File Names that are compiled
        public List<string> ProjectFileList { get; set; }

        public ProjectInfo()
        {
            ProjectReferenceList = new List<string>();
            ProjectIncludeList = new List<string>();
            ProjectFileList = new List<string>();
        }

        public ProjectInfo(string projectName)
        {
            ProjectName = projectName;

            ProjectReferenceList = new List<string>();
            ProjectIncludeList = new List<string>();
            ProjectFileList = new List<string>();
        }

        public ProjectInfo(string BaseDirectoryName, string ProjectFileName)
        {
            ProjectFilePath = BaseDirectoryName + @"\" + ProjectFileName;

            ProjectReferenceList = new List<string>();
            ProjectIncludeList = new List<string>();
            ProjectFileList = new List<string>();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;

namespace Common.Utility
{
    class Catalogue
    {
        // constants
        const string FILE_FILTER = "*.*";
        const string DIRECTORY_FILTER = "*";
        const string REPORT_DELIMITER = ",";

        private bool _errorOccurred = false;

        public bool ErrorOccurred
        {
            get
            {
                return _errorOccurred;
            }
            set
            {
                _errorOccurred = value;
            }
        }

        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get
            {
                return _errorMessage; 
            }
            set
            {
                _errorMessage = value;
            }
        }

        // Default file Filter is set to all files
        private string _fileFilter = FILE_FILTER;

        public string FileFilter
        {
            get
            {
                return _fileFilter;
            }
            set
            {
                _fileFilter = value;
            }
        }

        private string _directoryFilter = DIRECTORY_FILTER;

        public string DirectoryFilter
        {
            get
            {
                return _directoryFilter;
            }
            set
            {
                _directoryFilter = value;
            }
        }

        public Catalogue()
        {
            // default constructor
            CatalogueFileList = new List<CatalogueFile>();
            FileList = new List<string>();
            ColumnList = new List<string>();
        }

        ~Catalogue()
        {
            // default destructor
        }

        public List<string> FileList = null;

        public List<string> ColumnList = null;

        public List<CatalogueFile> CatalogueFileList = null;

        public void LoadFileList(string path)
        {

            try
            {
                foreach (string dir in Directory.GetDirectories(path))
                {
                    foreach (string fileName in Directory.GetFiles(dir, _fileFilter))
                    {
                        FileList.Add(fileName); 
                    }
                    LoadFileList(dir);
                }
            }
            catch (System.Exception e)
            {
                _errorOccurred = true;
                _errorMessage = e.Message;
            }            
        }

        public void LoadCatalogueFileListOld(string path)
        {
            string OutputDirectory = Settings.ReadConfigValue("CatalogueDirectory");
            OutputDirectory += "\\";

            CatalogueFile CF = null;
            FileInfo FI = null;

            try
            {
                foreach (string dir in Directory.GetDirectories(path, _directoryFilter))
                {
                    foreach (string fileName in Directory.GetFiles(dir, _fileFilter))
                    {
                        CF = new CatalogueFile();
                        CF.Path = fileName;
                        CF.SetName();
                        CF.SetApplicationName(OutputDirectory);

                        FI = new FileInfo(fileName);

                        CF.Size = FI.Length;
                        CatalogueFileList.Add(CF);
                    }

                    LoadCatalogueFileList(dir);
                }
            }
            catch (Exception e)
            {
                _errorOccurred = true;
                _errorMessage = e.Message;
            }
        }

        public void LoadCatalogueFileList(string path)
        {
            string OutputDirectory = Settings.ReadConfigValue("CatalogueDirectory");
            OutputDirectory += "\\";


            CatalogueFile CF = null;
            FileInfo FI = null;

            try
            {
                foreach (string dir in Directory.GetDirectories(path, _directoryFilter))
                {
                    foreach (string fileName in Directory.GetFiles(dir, _fileFilter))
                    {
                        CF = new CatalogueFile();

                        CF.Path = fileName;
                        FI = new FileInfo(fileName);
                        

                        for (int i = 0; i < ColumnList.Count; i++)
                        {
                            if (ColumnList[i] == "Name")
                            {
                                CF.SetName();
                            }
                            else if (ColumnList[i] == "ApplicationName")
                            {
                                CF.SetApplicationName(OutputDirectory);
                            }
                            else if (ColumnList[i] == "Size")
                            {
                                CF.Size = FI.Length;
                            }
                            else if (ColumnList[i] == "Extension")
                            {
                                CF.Extension = FI.Extension;
                            }
                        }
                        
                        CatalogueFileList.Add(CF);
                    }

                    LoadCatalogueFileList(dir);
                }
            }
            catch (Exception e)
            {
                _errorOccurred = true;
                _errorMessage = e.Message;
            }
        }

        public DataTable GetCatalogue()
        {
            DataTable Results = new DataTable("tblCatalogue");

            for (int j = 0; j < ColumnList.Count; j++)
            {
                DataColumn DC = new DataColumn(ColumnList[j], Type.GetType("System.String"));
                Results.Columns.Add(DC);
            }

            try
            {
                string OutputDirectory = Settings.ReadConfigValue("OutputDirectory");

                string OutputReport = OutputDirectory + @"\Report.csv";

                if (File.Exists(OutputReport))
                {
                    // if file already exists then delete it
                    File.Delete(OutputReport);
                }

                DataRow CurrentRow = null;

                CatalogueFile OutputFile = null;

                for (int i = 0; i < CatalogueFileList.Count; i++)
                {
                    OutputFile = CatalogueFileList[i];
                    CurrentRow = Results.NewRow();

                        for (int j = 0; j < ColumnList.Count; j++)
                        {
                        if (ColumnList[j] == "Name")
                        {
                            CurrentRow["Name"] = OutputFile.Name;
                        }
                        else if (ColumnList[j] == "ApplicationName")
                        {
                            CurrentRow["ApplicationName"] = OutputFile.ApplicationName;
                        }
                        else if (ColumnList[j] == "Size")
                        {
                            CurrentRow["Size"] = OutputFile.Size.ToString();
                        }
                        else if (ColumnList[j] == "Path")
                        {
                            CurrentRow["Path"] = OutputFile.Path;
                        }
                        else if (ColumnList[j] == "Extension")
                        {
                            CurrentRow["Extension"] = OutputFile.Extension;
                        }
                    }

                    Results.Rows.Add(CurrentRow); 
                }

            }
            catch (Exception e)
            {
                _errorOccurred = true;
                _errorMessage = e.Message;
            }

            return Results;
        }

        public void ExportCatalogueFromView(DataView vwExport)
        {
            try
            {
                string OutputDirectory = Settings.ReadConfigValue("OutputDirectory");

                string OutputReport = OutputDirectory + @"\Report.csv";

                if (File.Exists(OutputReport))
                {
                    // if file already exists then delete it
                    File.Delete(OutputReport);
                }

                StreamWriter sw = new StreamWriter(OutputReport, false);
                // First we will write the headers.

                //DataTable dt = m_dsProducts.Tables[0];
                int iColCount = vwExport.Table.Columns.Count;

                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write(vwExport.Table.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }

                sw.Write(sw.NewLine);
                // Now write all the rows.

                foreach (DataRowView rowView in vwExport)
                {
                    for (int i = 0; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(rowView[i]))
                        {
                            sw.Write(rowView[i].ToString());
                        }

                        if (i < iColCount - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                _errorOccurred = true;
                _errorMessage = e.Message;
            }

        }

        public void CopyCatalogue(DataTable dt)
        {
            try
            {
                string OutputDirectory = Settings.ReadConfigValue("OutputDirectory");
                string CopyLocation = OutputDirectory + "\\COPY";
                Directory.CreateDirectory(CopyLocation);
  
                string SourcePath = string.Empty;
                string FileSize = string.Empty;
                string FullFileName = string.Empty;
                string FileName = string.Empty;
                string FileExtension = string.Empty;
                string DestinationPath = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    if (dt.Columns.Contains("Size"))
                    {
                        SourcePath = row["Path"].ToString();
                        FullFileName = row["Name"].ToString();
                        string[] Parts = FullFileName.Split('.');
                        for (int i = 0; i < Parts.Length; i++)
                        {
                            if (i == 0)
                            {
                                FileName = Parts[i];
                            }
                            else if (i == Parts.Length - 1)
                            {
                                FileExtension = Parts[i];
                            }
                            else
                            {
                                FileName = FileName + "." + Parts[i];
                            }
                        }

                        FileSize = row["Size"].ToString();
                        DestinationPath = CopyLocation + "\\" + FullFileName;

                        if (File.Exists(DestinationPath))
                        {
                            DestinationPath = CopyLocation + "\\" + FileName + "_" + FileSize + "." + FileExtension;
                            if (File.Exists(DestinationPath))
                            {
                                // Name + Size is duplicate. Already copied. Ignore.
                            }
                            else
                            {
                                File.Copy(SourcePath, DestinationPath);
                            }
                        }
                        else
                        {
                            File.Copy(SourcePath, DestinationPath);
                        }
                    }
                    else
                    {
                        SourcePath = row["Path"].ToString();
                        FullFileName = row["Name"].ToString();
                        DestinationPath = CopyLocation + "\\" + FullFileName;

                        if (File.Exists(DestinationPath))
                        {    
                            // Copy unique files only
                        }
                        else
                        {
                            File.Copy(SourcePath, DestinationPath);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _errorOccurred = true;
                _errorMessage = e.Message;
            }

        }

        public void ExportCatalogue(DataTable dt)
        {
            try
            {
                string OutputDirectory = Settings.ReadConfigValue("OutputDirectory");

                string OutputReport = OutputDirectory + @"\Report.csv";

                if (File.Exists(OutputReport))
                {
                    // if file already exists then delete it
                    File.Delete(OutputReport);
                }

                StreamWriter sw = new StreamWriter(OutputReport, false);
                // First we will write the headers.

                int iColCount = dt.Columns.Count;

                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }

                sw.Write(sw.NewLine);
                // Now write all the rows.

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(row[i]))
                        {
                            sw.Write(row[i].ToString());
                        }

                        if (i < iColCount - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                _errorOccurred = true;
                _errorMessage = e.Message;
            }

        }

        public void ExportCatalogue()
        {
            try
            {
                string OutputDirectory = Settings.ReadConfigValue("OutputDirectory");

                string OutputReport = OutputDirectory + @"\Report.csv";

                if (File.Exists(OutputReport))
                {
                    // if file already exists then delete it
                    File.Delete(OutputReport); 
                }

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(OutputReport);

                // output header
                sw.Write("Application Name");
                sw.Write(REPORT_DELIMITER);
                sw.Write("File Name");
                sw.Write(REPORT_DELIMITER);
                sw.Write("File Size");
                sw.Write(REPORT_DELIMITER);
                sw.WriteLine("Path");

                CatalogueFile OutputFile = null;

                for (int i = 0; i < CatalogueFileList.Count; i++)
                {
                    OutputFile = CatalogueFileList[i];
                    sw.Write(OutputFile.ApplicationName);
                    sw.Write(REPORT_DELIMITER);
                    sw.Write(OutputFile.Name);
                    sw.Write(REPORT_DELIMITER);
                    sw.Write(OutputFile.Size.ToString());
                    sw.Write(REPORT_DELIMITER);
                    sw.WriteLine(OutputFile.Path);
                }

                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                _errorOccurred = true;
                _errorMessage = e.Message;
            }
        }

    }
}

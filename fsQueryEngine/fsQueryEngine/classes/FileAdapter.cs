using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;

namespace Common.Utility
{
    class FileAdapter
    {

        private string _catalogDirectory = string.Empty;

        public string catalogDirectory
        {
            get
            {
                return _catalogDirectory;
            }
            set
            {
                _catalogDirectory = value;
            }
        }

        private string _outputDirectory = string.Empty;

        public string OutputDirectory
        {
            get
            {
                return _outputDirectory;
            }
            set
            {
                _outputDirectory = value;
            }
        }

        private string _query = string.Empty;

        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
            }
        }

        public FileAdapter()
        {
            // default constructor
            _catalogDirectory = Settings.ReadConfigValue("CatalogueDirectory");
            _outputDirectory = Settings.ReadConfigValue("OutputDirectory");
        }

        public FileAdapter(string CatalogDirectory,string OutputDirectory)
        {
            // default constructor
        }

        public DataTable ExecuteQuery(string Query,bool Export,bool Copy)
        {
            Parser CatalogueParser = new Parser(Query);
            CatalogueParser.Intilialize();
            return Execute(CatalogueParser,Export,Copy);
        }

        public void CleanUpOutputDirectory()
        {
            if (Directory.Exists(_outputDirectory))
            {
                // Directory.Delete(_outputDirectory, true);
                Directory.CreateDirectory(_outputDirectory);
            }
            else
            {
                Directory.CreateDirectory(_outputDirectory);
            }
        }

        public DataTable Execute(Parser P,bool Export,bool Copy)
        {
            DataTable OriginalTable = null;
            DataTable ModifiedTable = null;

            CleanUpOutputDirectory();

            Catalogue catalog = new Catalogue();

            // set default filter values
            catalog.FileFilter = "*.*";
            catalog.DirectoryFilter = "*";

            // Override the default parameters from Parser Engine
            IDictionaryEnumerator en = P.GlobalHashTable.GetEnumerator();
            while (en.MoveNext())
            {
                if (en.Key.ToString() == "FILE_FILTER")
                {
                    catalog.FileFilter = en.Value.ToString();
                }
                else if (en.Key.ToString() == "DIR_FILTER")
                {
                    catalog.DirectoryFilter = en.Value.ToString();
                }
            }

            string[] Columns = P.ColumnNames.Split(',');
            foreach (string fileProperty in Columns)
            {
                catalog.ColumnList.Add(fileProperty);
            }

            catalog.LoadCatalogueFileList(_catalogDirectory);

            if (P.Command == "select")
            {
                if (P.Modifier == "distinct")
                {
                    OriginalTable = catalog.GetCatalogue();
                    DataView CatalogueView = new DataView(OriginalTable);
                    ModifiedTable = CatalogueView.ToTable(true, Columns);

                    if (Export)
                    {
                        catalog.ExportCatalogue(ModifiedTable);
                    }
                }
                else
                {
                    OriginalTable = catalog.GetCatalogue();
                    DataView CatalogueView = new DataView(OriginalTable);
                    ModifiedTable = CatalogueView.ToTable(true, Columns);
                    if (Export)
                    {
                        catalog.ExportCatalogue(ModifiedTable);
                    }
                }

                if (Copy)
                {
                    DataView CopyView = new DataView(OriginalTable);
                    CopyView.Sort = "Size";
                    ModifiedTable = CopyView.ToTable(true, Columns);
                    catalog.CopyCatalogue(ModifiedTable);
                }
            }
            else
            {
            }

            return ModifiedTable;
        }

        public void Run()
        {
            CleanUpOutputDirectory();
            Catalogue catalog = new Catalogue();
            catalog.FileFilter = "*.dll";
            catalog.DirectoryFilter = "*";

            catalog.LoadCatalogueFileList(_catalogDirectory);
            catalog.ExportCatalogue();
        }

        public void Run(string Operator,string Modifier,string ColumnNames)
        {
            CleanUpOutputDirectory();

            Catalogue catalog = new Catalogue();
            catalog.FileFilter = "*.dll";
            catalog.DirectoryFilter = "*";

            string[] Columns = ColumnNames.Split(',');
            foreach (string fileProperty in Columns)
            {
                catalog.ColumnList.Add(fileProperty);
            }

            catalog.LoadCatalogueFileList(_catalogDirectory);
            catalog.ExportCatalogue();
        }

    }
}

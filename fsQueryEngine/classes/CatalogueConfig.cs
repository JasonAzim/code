using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows.Forms.Design;
using System.Drawing.Design;



namespace Common.Utility
{
    class CatalogueConfig
    {
        string _catalogueDirectory;
        string _OutputDirectory;
        string _OutputReport;
        int    _MaxFileCount;

        [CategoryAttribute("Catalogue Directory"), DescriptionAttribute("Catalogue Directory"),EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string CatalogueDirectory
        {
            get 
            { 
                return _catalogueDirectory; 
            }
            set 
            {
                _catalogueDirectory = value;
            }
        }

        [CategoryAttribute("Output Directory"), DescriptionAttribute("Output Directory"),EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string OutputDirectory
        {
            get 
            { 
                return _OutputDirectory; 
            }
            set 
            {
                _OutputDirectory = value;
            }
        }

        [CategoryAttribute("Output Report"), DescriptionAttribute("Output Report"),EditorAttribute(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string OutputReport
        {
            get 
            { 
                return _OutputReport; 
            }
            set 
            {
                _OutputReport = value;
            }
        }

        [CategoryAttribute("Maximum File Count"), DescriptionAttribute("Maximum File Count")]
        public int MaxFileCount
        {
            get 
            { 
                return _MaxFileCount; 
            }
            set
            { 
                _MaxFileCount = value; 
            }
        }
    }
}

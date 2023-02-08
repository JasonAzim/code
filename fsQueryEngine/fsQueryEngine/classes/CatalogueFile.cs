using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Common.Utility
{
    class CatalogueFile
    {

        private string _path = string.Empty;

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        private string _name = string.Empty;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private string _applicationName = string.Empty;

        public string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        private string _extension = string.Empty;

        public string Extension
        {
            get
            {
                return _extension;
            }
            set
            {
                _extension = value;
            }
        }

        private long _size = 0;

        public long Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        private Hashtable _Attributes = null;

        public CatalogueFile()
        {
            // default constructor
            this._Attributes = new Hashtable();
        }

        ~CatalogueFile()
        {
            // default destructor
        }

        public void SetAttribute(string key, string value)
        {
            if (this._Attributes.ContainsKey(key))
            {
                this._Attributes[key] = value;
            }
            else
            {
                this._Attributes.Add(key, value);
            }
        }

        public string GetAttribute(string key, string value)
        {
            return (string) this._Attributes[key];
        }

        public void PrintAttributes()
        {
            ICollection ALLKeys;
            string AttributeList = string.Empty;

            if (_Attributes.Count == 0)
            {
                // return "The hashtable is empty";
            }
            else
            {
                ALLKeys = this._Attributes.Keys;

                foreach (object Key in ALLKeys)
                {
                    // under construction
                    AttributeList = Key.ToString();
                }
            }
        }

        public void SetName()
        {
            string[] Parts = _path.Split('\\');

            this._name = Parts[Parts.Length - 1];
        }

        public void SetApplicationName(string OutputDirectory)
        {
            string FilePath = this._path;

            FilePath = FilePath.Replace(OutputDirectory, "");

            string[] Parts = FilePath.Split('\\');

            this._applicationName = Parts[0];
        }
    }
}

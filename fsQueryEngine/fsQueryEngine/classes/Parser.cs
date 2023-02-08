using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections; 

namespace Common.Utility
{
    class Parser
    {
        string[] ReservedWords = {"select","from","distinct","where"};

        string[] Globals = {"FILE_FILTER","DIR_FILTER"};

        public Hashtable GlobalHashTable = null;

        private string _command = string.Empty;

        public string Command
        {
            get
            {
                return _command;
            }
            set
            {
                _command = value;
            }
        }

        private string _columnNames = string.Empty;

        public string ColumnNames
        {
            get
            {
                return _columnNames;
            }
            set
            {
                _columnNames = value;
            }
        }

        private string _objectName = string.Empty;

        public string ObjectName
        {
            get
            {
                return _objectName;
            }
            set
            {
                _objectName = value;
            }
        }

        private string _modifier = string.Empty;

        public string Modifier
        {
            get
            {
                return _modifier;
            }
            set
            {
                _modifier = value;
            }
        }

        private string _where = string.Empty;

        public string Where
        {
            get
            {
                return _where;
            }
            set
            {
                _where = value;
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

        private string _processQuery = string.Empty;

        public string ProcessQuery
        {
            get
            {
                return _processQuery;
            }
            set
            {
                _processQuery = value;
            }
        }

        public Parser(string query)
        {
            // default constructor
            _query = query;
            _processQuery = query;
            GlobalHashTable = new Hashtable();
        }

        private string RemoveTrailingAndLeadingSpaces(string Line)
        {
            Line = Line.TrimEnd();
            Line = Line.TrimStart();
            return Line;
        }

        private string[] WordSplit(string Input, string word)
        {
            string Line = Input.Replace(word, "$");
            return Line.Split('$');
        }

        public void Intilialize()
        {
            _processQuery = _query;

            string[] Words = _processQuery.Split(' ');

            if (Words[0].ToLower() == "select")
            {
                _command = "select";
                _processQuery = ProcessQuery.Replace("select", "");

                if (_processQuery.IndexOf("distinct") > 0)
                {
                    this._modifier = "distinct";
                    _processQuery = _processQuery.Replace("distinct", "");
                }

                if (_processQuery.IndexOf("where") > 0)
                {
                    string[] Words2 = WordSplit(_processQuery, "where");
                    _processQuery = RemoveTrailingAndLeadingSpaces(Words2[0]);
                    this._where = Words2[1]; 

                    string[] Words3 = WordSplit(_where, "&");

                    for (int i = 0; i < Words3.Length; i++)
                    {           
                        string[] WhereClause = Words3[i].Split('=');
                        string Key = RemoveTrailingAndLeadingSpaces(WhereClause[0]);
                        string Value = RemoveTrailingAndLeadingSpaces(WhereClause[1]);
                        Value = Value.Replace("'", "");
                        GlobalHashTable.Add(Key, Value);
                    }
                }

                string LineWithoutFrom = _processQuery.Replace("from", "$");
                string[] Words4 = LineWithoutFrom.Split('$');

                this._columnNames = RemoveTrailingAndLeadingSpaces(Words4[0]);

                _objectName = RemoveTrailingAndLeadingSpaces(Words4[1]);

                if (_objectName == "[CurrentCatalogue]")
                {
                    _objectName = Settings.ReadConfigValue("CatalogueDirectory");
                }
            }
            else
            {
            }
            _processQuery = _query;
        }
    }
}

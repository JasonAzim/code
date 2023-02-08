using System;

namespace Common.Utility
{
    class ThreadMessage
    {        
        private string _query;

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

        private string _source;

        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        public ThreadMessage(string query,string source)
        {
            _query = query;
            _source = source;
        }

    }
}
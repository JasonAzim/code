using System;
using System.Configuration;
using System.Web;
using System.IO;
using System.Collections;

namespace AppLibrary.Data
{
    [Serializable()]
    public class ObjectState
    {
        private int _RecordID = 0;

        public int RecordID
        {
            get
            {
                return _RecordID;
            }
            set
            {
                _RecordID = value;
            }
        }

        private DateTime _dateCreated; 

        public DateTime DateCreated
        {
            get
            {
                return _dateCreated;
            }
            set
            {
                _dateCreated = value;
            }
        }

        private string _createdBy = string.Empty;

        public string CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }

        private DateTime _lastUpdated;

        public DateTime LastUpdated
        {
            get
            {
                return _lastUpdated;
            }
            set
            {
                _lastUpdated = value;
            }
        }

        private string _lastUpdateBy = string.Empty;

        public string LastUpdateBy
        {
            get
            {
                return _lastUpdateBy;
            }
            set
            {
                _lastUpdateBy = value;
            }
        }

        private DateTime _reportDate;

        public DateTime ReportDate
        {
            get
            {
                return _reportDate;
            }
            set
            {
                _reportDate = value;
            }
        }

        private bool _ISValid = true;

        public bool ISValid
        {
            get
            {
                return _ISValid;
            }
            set
            {
                _ISValid = value;
            }
        }

        private bool _MaskException = false;

        public bool MaskException
        {
            get
            {
                return _MaskException;
            }
            set
            {
                _MaskException = value;
            }
        }

        private bool _ErrorOccurred = false;

        public bool ErrorOccurred
        {
            get
            {
                return _ErrorOccurred;
            }
            set
            {
                _ErrorOccurred = value;
            }
        }

        // ErrorType 0 means general unspecified, default error.
        // ErrorType 1 means Logical Error occurred and a business rule was violated.
        // ErrorType 2 means Datatabase Error occurred. Error while performing a database operation.
        // ErrorType 3 means UI Error occurred. Error while displaying the data. Data or display UI is not valid. Violation of data validation rules.
        // ErrorType 4 means Dependency Error occurred. An object that is needed for this operation was not present.
        private int _ErrorType = 0;
                
        public int ErrorType
        {
            get
            {
                return _ErrorType;
            }
            set
            {
                _ErrorType = value;
            }
        }
        
        private string _ErrorMessage = string.Empty;

        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                _ErrorMessage = value;
            }
        }

        private string _DisplayMessage = string.Empty;

        public string DisplayMessage
        {
            get
            {
                return _DisplayMessage;
            }
            set
            {
                _DisplayMessage = value;
            }
        }

        private string _Query = string.Empty;

        public string Query
        {
            get
            {
                return _Query;
            }
            set
            {
                _Query = value;
            }
        }
    }
}
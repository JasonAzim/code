using System;


namespace AppLibrary.Model
{
    [Serializable()]
    public class UserInfo
    {
        private string _userID = string.Empty;

        public string UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

        private string _loginID = string.Empty;

        public string LoginID
        {
            get
            {
                return _loginID;
            }
            set
            {
                _loginID = value;
            }
        }

        private string _password = string.Empty;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private string _domain = string.Empty;

        public string Domain
        {
            get
            {
                return _domain;
            }
            set
            {
                _domain = value;
            }
        }

        private string _Email = string.Empty;

        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }

        private string _firstName = string.Empty;

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        private string _middleName = string.Empty;

        public string MiddleName
        {
            get
            {
                return _middleName;
            }
            set
            {
                _middleName = value;
            }
        }

        private string _lastName = string.Empty;

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        private string _Permission = string.Empty;

        public string Permission
        {
            get
            {
                return _Permission;
            }
            set
            {
                _Permission = value;
            }
        }

        private string _Title = string.Empty;

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        public UserInfo()
        {
            // Empty Constructor
        }

        public UserInfo(string userID,int clientID, string loginID, string password, string domain, string email, string firstName, string middleName, string lastName)
        {
            // Full Constructor
            this._userID = userID;
            this.Permission = "View";
            this._loginID = loginID;
            this._password = password;
            this._domain = domain;
            this._firstName = firstName;
            this._middleName = middleName;
            this._lastName = lastName;
        }

    }
}

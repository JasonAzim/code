using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PharmacyAdmin.Helpers
{
    public class StateHelper    
    {
        // These properties are not subscribed to and do not use an event handler
        public Dictionary<string, object> Items { get; set; }

        public string LoginAuditProperty { get; set; } = "Initial value from StateContainer";
       
        public event Action OnChange;
            
        public void SetLoginAuditProperty(string value)
        {
            LoginAuditProperty = value;       
            NotifyStateChanged();
        }

        public StateHelper()
        {
            Items = new Dictionary<string, object>();
        }

        #region Utility Functions
        public string GetPropertyOrDefault(string key, string DefaultValue)
        {
            string result = null;

            if (Items != null && Items.ContainsKey(key))
            {
                result = Items[key]?.ToString();
                if (string.IsNullOrEmpty(result)) { result = DefaultValue; }
            }
            return result;
        }

        public static string GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
        #endregion

        #region Security
        public string IsAuthenticated()
        {
            bool Authenticated = Items.TryGetValue("UserIdentityName", out object identity);
            return identity?.ToString();
        }

        public bool HasPageAccess(string PageName, string UserList)
        {
            bool result = false;

            if (Items != null && Items.ContainsKey("UserIdentityName"))
            {
                if (PageName == "FedExSearch")
                {
                    string UserName = Items["UserIdentityName"]?.ToString();
                    if (string.IsNullOrEmpty(UserName))
                    {
                        return false;
                    }
                    else
                    {

                    }
                }
                string result2 = Items["UserIdentityName"]?.ToString();
                //if (string.IsNullOrEmpty(result)) {
            }
            return result;
        }
        #endregion

        private void NotifyStateChanged() => OnChange?.Invoke();

        #region  Form Input Validation
        // Todo consider refactoring into its own class called ValidationHelper

        public static bool ContainsAlphabets(string Text)
        {
            return Regex.IsMatch(Text, @"^[a-zA-Z]+$");
        }

        public static bool IsCSV(string Text)
        {
            //return Regex.IsMatch(Text, @"^\d*[,]\d*$");
            //return Regex.IsMatch(Text, @"^\d{1,9}(?:,\s\d{1,9}){4}$");
            //return Regex.IsMatch(Text, @"^[0-9,]+$");
            return Regex.IsMatch(Text, @"^[,]$");
        }

        public static bool hasSpecialChar(string input, string specialChar = @",")
        {
            //specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        public static bool IsNumberCSV(string Text)
        {
            string[] numbers = Text.Split(",");
            foreach (string number in numbers)
            {
                if (!IsNumber(number.Trim())) return false;
            }
            return true;
        }

        public static bool IsNumber(string Text)
        {
            int number;
            if (int.TryParse(Text.Trim(), out number))
            {
               return true; 
            }
            return false;
        }

        public static bool IsRxNumber(string Text, int DatabaseUID)
        {
            int number;
            if (int.TryParse(Text, out number))
            {
                if ((DatabaseUID == 1) && (Text.Length == 6))
                {
                    return true;
                }
                else if ((DatabaseUID == 2) && (Text.Length == 7))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsRxNumberCSV(string Text, int DatabaseUID)
        {
            string[] numbers = Text.Split(",");
            foreach (string number in numbers)
            {
                if (!IsRxNumber(number.Trim(), DatabaseUID))  return false; 
            }
            return true;
        }

        public static bool IsTrackingNumber(string Text)
        {
            long number;
            if (Int64.TryParse(Text, out number))
            {
                if (Text.Length == 12)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsTrackingNumberCSV(string Text)
        {
            string[] numbers = Text.Split(",");
            foreach (string number in numbers)
            {
                if (!IsTrackingNumber(number.Trim())) return false;
            }
            return true;
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Global.Utility
{
    public sealed class SettingsHelper
    {
        public static string _ServerType = "Production";  //  Other values are "Staging" and "Development"
        private static SettingsHelper instance = null;
        private static readonly object padlock = new object();
        public static readonly string DBNameImproveCode = "improve_code";

        SettingsHelper()
        {
        }

        public static SettingsHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SettingsHelper();
                    }
                    return instance;
                }
            }
        }

        public static string ServerType()
        {
            return _ServerType;
        }

        public static string SQLServerDB(string DatabaseName = "ISHUB")
        {
            if (DatabaseName == DBNameImproveCode)
            {
                return @"";
            }

            return @"";
        }

        public static string ServerUrl()
        {
            return @"http://www.imrpovecode.com";
        }

        public static string SmtpServer()
        {
            return "improvecode.com";
        }

        public static string EmailFromAddress()
        {
            return "me@improvecode.com";
        }
    }
}
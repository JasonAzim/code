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

        // If the database name is set to an emtpy string or null then it will default to the local sql server
        public static string SQLServerDB(string DatabaseName = "improve_code")
        {
            if (DatabaseName == DBNameImproveCode)
            {
                return @"Data Source=192.185.6.39;Initial Catalog=improve_code;User ID=impro_sa;Password=Terminator$1";
            }

            // Return the locally installed database if nothing is specified
            return @"Data Source=localhost;Initial Catalog=improve_code;User ID=impro_sa;Password=Terminator$1";
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
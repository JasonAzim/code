using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace PharmacyAdmin.Data
{
    public sealed class SettingsHelper
    {
        public static string _ServerType = "Production";
        private static SettingsHelper instance = null;
        private static readonly object padlock = new object();

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
            //return "Production";
            //return "Staging";
            return "Development";
        }

        public static string SQLServerDB(string DatabaseName = null)
        {
            if (DatabaseName == "Data Warehouse")
            {
                return "Data Source=alpha-sql01\\CPRPlusDB;Initial Catalog=\"Data Warehouse\";Integrated Security=True";
            }
           
            return "Data Source=alpha-sql01\\CPRPlusDB;Initial Catalog=\"UTILIZATION_REVIEWS\";Integrated Security=True";
        }

        public static string ServerUrl()
        {
            return @"http://www.aprilrx.com";
        }

        public static string SmtpServer()
        {
            return "aprilrx.com";
        }

        public static string EmailFromAddress()
        {
            return "me@aprilrx.com";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections; 
using System.Xml;
using System.Reflection;
using System.Configuration;

namespace Common.Utility
{
    class Settings
    {
        public XmlDocument ConfigXmlDocument = new XmlDocument();

        public CatalogueConfig AppConfig = new CatalogueConfig();
        
        private string _appConfigPath;

        public string AppConfigPath
        {
            get
            {
                return _appConfigPath;
            }
            set
            {
                _appConfigPath = value;
            }
        }

        public static string ReadConfigValue(string Key)
        {
            AppSettingsReader ConfigReader = new AppSettingsReader();
            string Result = (string)ConfigReader.GetValue(Key, typeof(System.String));
            return Result;
        }

        public static void WriteConfigValue(string Key, string KeyValue)
        {
            // This code needs a reference to system.configuration DLL for it to compile
            Configuration  config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings[Key].Value = KeyValue;
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");    
        }

        public Settings()
        {
            _appConfigPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\App.config";

            if (File.Exists(_appConfigPath))
            {
                _appConfigPath = Assembly.GetExecutingAssembly().Location + ".config";
            }

            LoadConfig();
        }

        public void LoadConfig()
        {
            ConfigXmlDocument.Load(_appConfigPath);

            AppConfig.CatalogueDirectory = getAppConfigValue("CatalogueDirectory");
            AppConfig.OutputDirectory = getAppConfigValue("OutputDirectory");
            AppConfig.OutputReport = getAppConfigValue("OutputReport");
            AppConfig.MaxFileCount = int.Parse(getAppConfigValue("MaxFileCount"));
        }

        private void setAppConfigValue(string keyName, string keyValue)
        {
            XmlNode currentNode;

            currentNode = ConfigXmlDocument.SelectSingleNode("//appSettings/add[@key='" + keyName + "']");

            if (currentNode != null)
            {
                currentNode.Attributes.GetNamedItem("value").InnerText = keyValue;
            }
        }

        private string getAppConfigValue(string keyName)
        {
            XmlNode currentNode;

            currentNode = ConfigXmlDocument.SelectSingleNode("//appSettings/add[@key='" + keyName + "']");

            if (currentNode == null) return "";

            return currentNode.Attributes.GetNamedItem("value").InnerText;
        }

        public void SaveConfig()
        {

            setAppConfigValue("CatalogueDirectory", AppConfig.CatalogueDirectory);
            setAppConfigValue("OutputDirectory", AppConfig.OutputDirectory);
            setAppConfigValue("OutputReport", AppConfig.OutputReport);
            setAppConfigValue("MaxFileCount", AppConfig.MaxFileCount.ToString());
            ConfigXmlDocument.Save(_appConfigPath);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections;
using System.Configuration;
using System.Diagnostics;

namespace Common.Utility
{
    class ConfigurationManipulator
    {
        public static void Test()
        {
            DisplayAppSettings();

            // Show how to use OpenExeConfiguration() and RefreshSection.
            UpdateAppSettings();

            // Show how to use AppSettings.
            DisplayAppSettings();

            /*
            // Show how to use OpenExeConfiguration(string).
            DisplayAppSettingsRawXml();

            // Show how to use GetSection.
            DisplayAppSettingsSectionRawXml();

            // Show how to use ConnectionStrings.
            DisplayConnectionStrings();

            // Show how to use OpenMappedMachineConfiguration.
            DisplayMappedMachineConfigurationFileSections();

            // Show how to use OpenMappedExeConfiguration.
            DisplayMappedExeConfigurationFileSections();

            // Show how to use OpenMachineConfiguration.
            DisplayMachineConfigSections();
            */
        }

        // Show how to use AppSettings.
        static void DisplayAppSettings()
        {
            // Get the AppSettings collection.
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            string[] keys = appSettings.AllKeys;

            Debug.WriteLine("");
            Debug.WriteLine("Application appSettings:");

            // Loop to get key/value pairs.
            for (int i = 0; i < appSettings.Count - 1; i++)
            {
                string Output = String.Format("#{0} Name: {1} Value: {2}", i, keys[i], appSettings[i]);
                Debug.WriteLine(Output); 

            }
        }

        // Show how to use ConnectionStrings.
        // This function assumes that at least one connection string
        // has been defined.
        static void DisplayConnectionStrings()
        {
            // Get the ConnectionStrings collection.
            ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
            if (connections.Count != 0)
            {
                Debug.WriteLine("Connection strings:");
                // Loop to get the collection elements.
                foreach (ConnectionStringSettings connection in connections)
                {
                    string name = connection.Name;
                    string provider = connection.ProviderName;
                    string connectionString = connection.ConnectionString;
                    Debug.WriteLine("Name: {0}", name);
                    Debug.WriteLine("Connection string: {0}", connectionString);
                    Debug.WriteLine("Provider: {0}", provider);
                }
            }
            else
            {
                Debug.WriteLine("No connection string is defined.");
            }
        }

        // Show how to use OpenMachineConfiguration.
        static void DisplayMachineConfigSections()
        {
            // Get the machine.config file.
            Configuration machineConfig = ConfigurationManager.OpenMachineConfiguration();

            ConfigurationSectionCollection sections = machineConfig.Sections;
            Debug.WriteLine("");
            Debug.WriteLine("Sections in machine.config:");

            // Loop to get the sections machine.config.
            foreach (ConfigurationSection section in sections)
            {
                string name = section.SectionInformation.Name;
                Debug.WriteLine("Name: {0}", name);
            }
        }

        // Show how to use OpenExeConfiguration(ConfigurationUserLevel) 
        // and RefreshSection. 
        // This function creates a configuration file for the application, if one
        // does not exist.
        static void UpdateAppSettings()
        {
            // Get the configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Add an entry to appSettings.
            int appStgCnt = ConfigurationManager.AppSettings.Count;
            string newKey = "NewKey" + appStgCnt.ToString();

            string newValue = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            config.AppSettings.Settings.Add(newKey, newValue);

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of the changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }

        // Show how to use OpenExeConfiguration(string).
        static void DisplayAppSettingsRawXml()
        {
            // Get the application path.
            string exePath = System.IO.Path.Combine(Environment.CurrentDirectory, "ConfigurationManager.exe");

            // Get the configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);

            // Get the AppSetins section.
            AppSettingsSection appSettingSection = config.AppSettings;

            // Display raw xml.
            Debug.WriteLine(appSettingSection.SectionInformation.GetRawXml());
        }

        // Show how to use GetSection.
        static void DisplayAppSettingsSectionRawXml()
        {
            // Get the configuration file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Get the AppSetins section.
            AppSettingsSection appSettingSection = (AppSettingsSection)config.GetSection("appSettings");

            // Display raw xml.
            Debug.WriteLine("appSettings XML:");
            Debug.WriteLine(appSettingSection.SectionInformation.GetRawXml());
        }

        // Show how to use OpenMappedMachineConfiguration.
        static void DisplayMappedMachineConfigurationFileSections()
        {
            // Get the machine.config file.
            Configuration machineConfig = ConfigurationManager.OpenMachineConfiguration();

            // Map to the machine configuration file.
            ConfigurationFileMap configFile = new ConfigurationFileMap(machineConfig.FilePath);
            Configuration config = ConfigurationManager.OpenMappedMachineConfiguration(configFile);

            // Display the configuration file sections.
            ConfigurationSectionCollection sections = config.Sections;

            Debug.WriteLine("");
            Debug.WriteLine("Sections in machine.config:");

            foreach (ConfigurationSection section in sections)
            {
                string name = section.SectionInformation.Name;
                Debug.WriteLine("Name: {0}", name);
            }
        }

        // Show how to use OpenMappedExeConfiguration.
        static void DisplayMappedExeConfigurationFileSections()
        {
            // Get the application configuration file path.
            string exeFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "ConfigurationManager.exe.config");

            // Map to the application configuration file.
            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
            configFile.ExeConfigFilename = exeFilePath;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);

            // Display the configuration file sections.
            ConfigurationSectionCollection sections = config.Sections;

            Debug.WriteLine("");
            Debug.WriteLine("Sections in machine.config:");

            foreach (ConfigurationSection section in sections)
            {
                string name = section.SectionInformation.Name;
                Debug.WriteLine("Name: {0}", name);
            }
        }
    }
}


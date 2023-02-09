using System;

namespace Global
{
    public class SettingDatabase
    {
        public string ISHUBConnection { get; set; }
        public string CPRSQLConnection { get; set; }
        public string ALPHAScriptSqlConnection { get; set; }
        public string PioneerRXSqlConnection { get; set; }
    }

    public class SettingFedEx
    {
        public string protocol { get; set; }
        public string endpoint { get; set; }
        public string key { get; set; }
        public string password { get; set; }
        public string parentkey { get; set; }
        public string parentpassword { get; set; }
        public string accountnumber { get; set; }
        public string meternumber { get; set; }
        public string ShipmentAccountNumber { get; set; }
        public string SecureSpodAccount { get; set; }
        public string LanguageCode { get; set; }
        public string LocaleCode { get; set; }
        public string SPODWebServiceClientSettingsFile { get; set; }

        public SettingFedEx()
        {
        }
    }

    public class Settings
    {

        // miscellanous settings which do not belong to any section. The app config file has sections
        public string Mode { get; set; }
        public string DocumentStorageMode { get; set; }
        public string PageAllowedUsers { get; set; }
        public string DocumentDownloadMethod { get; set; }
        public int DocumentDownloadMethodInt
        {
            get => int.Parse(DocumentDownloadMethod);
        }

        public string DocumentDownloadPath { get; set; }
        public string DocumentDownloadFolderCPRPlus { get; set; }
        public string DocumentDownloadFolderPioneerRX { get; set; }

        public string CPRPlusDocumentDownloadStartDate { get; set; }
        public DateTime CPRPlusDocumentDownloadStartDatePart
        {
            get => DateTime.Parse(CPRPlusDocumentDownloadStartDate);
        }

        public string PioneerRXDocumentDownloadStartDate { get; set; }
        public DateTime PioneerRXDocumentDownloadStartDatePart
        {
            get => DateTime.Parse(PioneerRXDocumentDownloadStartDate);
        }

        public string DocumentMaxBatchCount { get; set; }
        public int DocumentMaxBatchCountInt
        {
            get => int.Parse(DocumentMaxBatchCount);
        }

        public string TransactionWaitTime { get; set; }

        public int TransactionWaitTimeInt
        {
            get => int.Parse(TransactionWaitTime);
        }
        public string StorageYearStart { get; set; }

        public SettingDatabase Database { get; set; }
        public SettingFedEx FedEx { get; set; }

        public Settings()
        {
            Database = new SettingDatabase();
            FedEx = new SettingFedEx();
        }
    }
}

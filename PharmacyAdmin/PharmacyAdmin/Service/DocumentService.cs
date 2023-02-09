using System;
using System.IO;
using System.Threading.Tasks;
using Global;
using PharmacyAdmin.Model;
using PharmacyAdmin.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace PharmacyAdmin.Service
{
    public class DocumentService
    {
        private readonly IConfiguration _config;
        private Settings _appSettings;
        public ObjectState State { get; set; }
        public static readonly string DocumentDownloadServerFolder = @"e:\document\FedexPOD\";

        private Settings GetAppSettings()
        {
            Settings AppSettings = new Settings();
            AppSettings.Mode = _config.GetSection("AppSettings:Mode").Value;
            AppSettings.DocumentStorageMode = _config.GetSection("AppSettings:DocumentStorageMode").Value;
            AppSettings.DocumentDownloadPath = _config.GetSection("AppSettings:DocumentDownloadPath").Value;
            AppSettings.DocumentDownloadFolderCPRPlus = _config.GetSection("AppSettings:DocumentDownloadFolderCPRPlus").Value;
            AppSettings.DocumentDownloadFolderPioneerRX = _config.GetSection("AppSettings:DocumentDownloadFolderPioneerRX").Value;
            AppSettings.CPRPlusDocumentDownloadStartDate = _config.GetSection("AppSettings:CPRPlusDocumentDownloadStartDate").Value;
            AppSettings.PioneerRXDocumentDownloadStartDate = _config.GetSection("AppSettings:PioneerRXDocumentDownloadStartDate").Value;

            return AppSettings;
        }

        public DocumentService(IConfiguration config)
        {
            _config = config;
            _appSettings = GetAppSettings();
            State = new ObjectState();
        }

        public void Dispose()
        {
        }

        public void HandleException(Exception ex, string DisplayMessage)
        {
            State.ErrorOccurred = true;
            State.DisplayMessage = DisplayMessage;

            State.ErrorMessage = ex.Message;

            if (State.MaskException == true)
            {
            }
            else
            {
            }
        }

        public string GetFileMonthFolderPath(string DocumentDownloadPath, int Year, int Month)
        {
            return Path.Combine(DocumentDownloadPath, Year.ToString() + "-" + Month.ToString("00"));
        }

        public string DocumentDownloadPath(string DatabaseUID, string TrackingNumber, string ReferenceNumber, string Date, string DocumentDownloadShare)
        {
            string DocumentDownloadPath = DocumentDownloadShare;

            if (DatabaseUID == "1")
            {
                DocumentDownloadPath += @"/cprplus";
            }
            else
            {

                DocumentDownloadPath += @"/pioneerrx";
            }

            DateTime DocumentDownloadDate = DateTime.Parse(Date);
            DocumentDownloadPath = GetFileMonthFolderPath(DocumentDownloadPath, DocumentDownloadDate.Year, DocumentDownloadDate.Month);

            string FileName = string.Empty;
            if (string.IsNullOrEmpty(ReferenceNumber))
            {
                FileName = string.Format("{0}_SPOD_PDF.pdf", TrackingNumber);
            }
            else
            {
                FileName = string.Format("{0}_{1}_SPOD_PDF.pdf", TrackingNumber, ReferenceNumber);
            }

            return Path.Combine(DocumentDownloadPath, FileName);
        }

        public string GetDocumentDownloadServerPath(string DownloadPath, string appSettingsDocumentDownloadPath)
        {
            string DocumentDownloadServerPath = string.Empty;
            
            if (_appSettings.Mode == "Test")
            {
                DocumentDownloadServerPath = DownloadPath.Replace(DocumentDownloadServerFolder, @"C:\temp\FedexPOD\");
            }
            else if (_appSettings.DocumentStorageMode == "Test")
            {
                DocumentDownloadServerPath = DownloadPath;  // e:\document\cprplus\2019-04\484778227263_SPOD_PDF.pdf
            }
            else
            {
                DocumentDownloadServerPath = DownloadPath.Replace(DocumentDownloadServerFolder, appSettingsDocumentDownloadPath);
            }

            System.Diagnostics.Debug.WriteLine(string.Format("Document Download Path = {0}", DocumentDownloadServerPath));
            return DocumentDownloadServerPath;
        }

        public string GetDocumentDownloadServerPath(string DownloadPath)
        {
            return GetDocumentDownloadServerPath(DownloadPath, _appSettings.DocumentDownloadPath);
        }

        public Task<byte[]> TestDocumentSearchAsync()
        {
            var WebServerUri = Path.Combine(@"C:\temp\FedEx\CPRPlus\2021-10", "285070558905_SPOD_PDF.pdf");
            byte[] fileBytes = File.ReadAllBytes(WebServerUri);
            return Task.FromResult(fileBytes);
        }

        //public Task<File> SearchAsync(string DatabaseUID, string TrackingNumber, string ReferenceNumber, string Date)
        //{
        //    string WebServerUri = DocumentDownloadPath(DatabaseUID, TrackingNumber, ReferenceNumber, Date, @"k:\document");
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(WebServerUri);
        //    File result = new System.IO.File(fileBytes, "application/force-download", "file1.xlsx");
        //    return Task.FromResult(System.IO.File(fileBytes, "application/force-download", "file1.xlsx"));
        //}

        public Task<byte[]> ReadFileAsync(string DatabaseUID, string TrackingNumber, string ReferenceNumber, string Date)
        {
            //Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            string WebServerUri;
            WebServerUri = @"C:\temp\FedEx\CPRPlus\2021-10\285070558905_SPOD_PDF.pdf";
            //WebServerUri = DocumentDownloadPath(DatabaseUID, TrackingNumber, ReferenceNumber, Date, @"k:\document");
            return ReadFileAsync(WebServerUri);
        }

        public Task<byte[]> ReadFileAsync(string WebServerUri)
        {
            byte[] fileBytes = null;

            try
            {
                fileBytes = File.ReadAllBytes(WebServerUri);
            }
            catch (Exception ex)
            {
                HandleException(ex, Constants.ERROR_DOCUMENT_SERVICE_READ_FILE_TO_BYTES);
            }
            return Task.FromResult(fileBytes);
        }

        // Another name for this function is read a file into a Byte Array
        public DocumentModel ReadFileContent(string downloadPath)
        {
            DocumentModel document = new DocumentModel() { DownloadPath = downloadPath };

            using (FileStream stream = new FileStream(document.DownloadPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                try
                {
                    using (BinaryReader binaryReader = new BinaryReader(stream))
                    {
                        long byteLength = new FileInfo(document.DownloadPath).Length;
                        document.Content = binaryReader.ReadBytes((Int32)byteLength);
                        binaryReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex, Constants.ERROR_DOCUMENT_SERVICE_READ_FILE_TO_BYTES);
                }
                finally
                {
                    stream.Close();
                    // fs.Dispose(); This will be called by the using statement so no need to call it manually
                }
            }

            return document;
        }

        public async Task<MemoryStream> ReadFileToMemory(string downloadPath)
        {
            var memory = new MemoryStream();

            try
            {
                using (FileStream stream = new FileStream(downloadPath, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        await stream.CopyToAsync(memory);
                        memory.Position = 0;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex, Constants.ERROR_DOCUMENT_SERVICE_READ_FILE_TO_MEMORY);
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, Constants.ERROR_DOCUMENT_SERVICE_READ_FILE_TO_MEMORY);
            }

            return memory;
        }

        public async void LogDocumentAccess(Guid DocumentID, DateTime AccessDate, int AccessSource, int AccessType, string UserID, string Database)
        {
            SqlService ISHUBService = new SqlService(_config);
            int DatabaseUID;
            if (!int.TryParse(Database, out DatabaseUID))
            {
                DatabaseUID = SQLUtility.GetISHUBDataSourceID(Database);
            }

            var result = await ISHUBService.DocumentAuditInsert(DocumentID, AccessDate, AccessSource, AccessType, UserID, DatabaseUID);
            this.State = ISHUBService.State;
        }

        public async Task<List<DocumentModel>> ReadDocument(string TrackingNumber, string Database)
        {
            SqlService ISHUBService = new SqlService(_config);
            int DatabaseUID;
            if (!int.TryParse(Database, out DatabaseUID))
            {
                DatabaseUID = SQLUtility.GetISHUBDataSourceID(Database);
            }

            var entities = await ISHUBService.DocumentRead(TrackingNumber, DatabaseUID);
            if ((entities != null) && (entities.Count > 0))
            {
                entities[0].PackageIdentifier = TrackingNumber;
            }
            this.State = ISHUBService.State;
            return entities;
        }
    }
}
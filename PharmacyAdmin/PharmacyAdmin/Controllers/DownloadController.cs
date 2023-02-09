using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PharmacyAdmin.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Global;
using PharmacyAdmin.Data;
using PharmacyAdmin.Model;

namespace PharmacyAdmin
{

    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : Controller
    {
        private IConfiguration _config;
        private Settings _appSettings;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private DocumentService _DocumentManagementService;

        private Settings GetAppSettings()
        {
            Settings AppSettings = new Settings();
            AppSettings.Mode = _config.GetSection("AppSettings:Mode").Value;
            AppSettings.DocumentStorageMode = _config.GetSection("AppSettings:DocumentStorageMode").Value;
            AppSettings.DocumentDownloadPath = _config.GetSection("AppSettings:DocumentDownloadPath").Value;
            AppSettings.DocumentDownloadFolderCPRPlus = _config.GetSection("AppSettings:DocumentDownloadFolderCPRPlus").Value;
            AppSettings.DocumentDownloadFolderPioneerRX = _config.GetSection("AppSettings:DocumentDownloadFolderPioneerRX").Value;

            return AppSettings;
        }

        public DownloadController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _config = config;
            _appSettings = GetAppSettings();
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet, DisableRequestSizeLimit]
        public async Task<IActionResult> Download(string UserID, string Database,string DOCID, string RXNO, string TrackingNO, string REFNO, string DownloadDate, string ShipDate)
        {
            _DocumentManagementService = new DocumentService(_config);
            
            if (_appSettings.Mode == "Test")
            {
                return TestReadFileFromLocalFolder(UserID, Database, RXNO, "285070558905", REFNO, DownloadDate).Result;
            }

            int DatabaseUID;
            if (!int.TryParse(Database, out DatabaseUID))
            {
                DatabaseUID = SQLUtility.GetISHUBDataSourceID(Database);
            }

            string DocumentDownloadName = "204.pdf";
            string DocumentDownloadPath = Path.Combine(_appSettings.DocumentDownloadPath, DocumentDownloadName);

            Task<List<DocumentModel>> task = _DocumentManagementService.ReadDocument(TrackingNO, Database);

            if (_DocumentManagementService.State.ErrorOccurred)
            {
                return NotFound("HTTP Code 204 - The file is not present in the Database (Document Catalog).");
            }

            if ((task.Result == null) || (task.Result.Count == 0)) return NotFound("Could not find document in the database."); // HTTP 404 

            for (int i = 0; i < task.Result.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(task.Result[i].DocumentGUID);
                _DocumentManagementService.LogDocumentAccess(task.Result[i].DocumentGUID, DateTime.Now, 1, 1, UserID, Database);
                if (DatabaseUID == 1)
                {
                    // To do implement any database specific logic here
                }

                if (!string.IsNullOrEmpty(task.Result[i].DownloadPath))
                {
                    DocumentDownloadPath = _DocumentManagementService.GetDocumentDownloadServerPath(task.Result[i].DownloadPath);
                }

                DocumentDownloadName = string.Format(@"{0}_SPOD_PDF.pdf", TrackingNO);
                if (!string.IsNullOrEmpty(ShipDate)) DocumentDownloadName = ShipDate + "_" + DocumentDownloadName;
                if (!string.IsNullOrEmpty(RXNO)) DocumentDownloadName = RXNO + "_" + DocumentDownloadName;
            }

            if (_DocumentManagementService.State.ErrorOccurred) return NotFound("The server sucessfully processed the request but the file is not available.");

            var memory = await _DocumentManagementService.ReadFileToMemory(DocumentDownloadPath);

            if (memory.Length == 0) return DownloadPDFHTTPError204(_appSettings.DocumentDownloadPath).Result;
            
            return File(memory, "application/octet-stream", DocumentDownloadName);
        }

        private async Task<FileStreamResult> DownloadPDFHTTPError204(string AppSettingsDocumentDownloadPath)
        {
            string DocumentDownloadName = "204.pdf";
            string DocumentDownloadPath = Path.Combine(AppSettingsDocumentDownloadPath, DocumentDownloadName);

            var memory = await _DocumentManagementService.ReadFileToMemory(DocumentDownloadPath);
            return File(memory, "application/octet-stream", DocumentDownloadName);
        }

        private async Task<FileStreamResult> TestReadFileFromLocalFolder(string UserID, string Database, string RXNO, string TrackingNO, string REFNO, string Date)
        {
            string DocumentDownloadPath = string.Format(@"C:\temp\FedexPOD\cprplus\2021-10\{0}_SPOD_PDF.pdf",TrackingNO);
            string DocumentDownloadName = string.Format(@"{0}_SPOD_PDF.pdf",TrackingNO);

            Guid DocGuid = Guid.Parse("F3BAA641-6914-4EE5-8FCD-0C57C1F43A65");
            _DocumentManagementService.LogDocumentAccess(DocGuid, DateTime.Now, 1, 1, UserID, Database);

            var memory = await _DocumentManagementService.ReadFileToMemory(DocumentDownloadPath);
            return File(memory, "application/octet-stream", DocumentDownloadName);
        }

        // Unit Tests
        [HttpGet("Test/{testId}"), DisableRequestSizeLimit]
        public async Task<IActionResult> TestDownload()
        {
            var memory = new MemoryStream();
            await using (var stream = new FileStream(@"C:\temp\FedexPOD\cprplus\2021-10\285070558905_SPOD_PDF.pdf", FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            //set correct content type here
            return File(memory, "application/octet-stream", "285070558905_SPOD_PDF.pdf");
        }
    }
}

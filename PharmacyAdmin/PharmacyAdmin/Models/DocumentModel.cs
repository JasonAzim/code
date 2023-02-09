using System;
using Global;

namespace PharmacyAdmin.Model
{
    public class DocumentModel
    {
        public int DatabaseUID { get; set; }
        public int DocumentID { get; set; }


        public string Code { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime? DownloadDate { get; set; }
        public int? DocumentCount { get; set; }
        public string TrackStatus { get; set; }
        public string TrackCode { get; set; }
        public string OtherIdentifierType { get; set; }
        public string OtherIdentifier { get; set; }
        public string ImageAvailability { get; set; }
        public string TrackEvents { get; set; }
        public Guid? RxID { get; set; }
        public Guid RxTransactionID { get; set; }
        public int? TICKNO { get; set; }
        public DateTime? TOUCHDATE { get; set; }
        // Database Fields
        public string RXNumber { get; set; }
        public string DocumentUID { get; set; }
        public Guid DocumentGUID { get; set; }

        public string INVOICENO { get; set; }
        public string MRN { get; set; }
        public int DELFLAG { get; set; }

        // Storage Fields
        public string Name { get; set; }
        public string DownloadPath { get; set; }
        public int DownloadMethod { get; set; }
        public string DownloadCode { get; set; }
        public string ErrorType { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ReportedDate { get; set; }
        public int UserID { get; set; }

        // custom fields
        public bool? Success { get; set; }
        public bool? SuccessFileSave { get; set; }

        public DateTime DispenseDate { get; set; }

        // SPOD Fields
        public string DocumentRequestFunctionName { get; set; } = "SPODRequestReturnPDF";
        public string PackageIdentifier { get; set; }
        public DownloadDocumentType DownloadDocumentType { get; set; }
        public byte[] Content { get; set; }

        // Document Tracking Fields
        public string ShipDateRangeBegin { get; set; }
        public string ShipDateRangeEnd { get; set; }

        public string TrackDetailStatusDescription { get; set; }
        public string TrackDetailStatusCode { get; set; }
        public string TrackDetailAvailableImages { get; set; }

        public string TrackDetailNotes { get; set; }
        public string TrackEventNotes { get; set; }

        public string CustomerTransactionId()
        {
            return DocumentRequestFunctionName + "_" + PackageIdentifier + "_" + DispenseDate.ToShortDateString();
        }

        public NotificationModel Notification { get; set; }

        public DocumentModel()
        {
            Notification = new NotificationModel();
            DownloadDocumentType = DownloadDocumentType.PDF;
        }
    }
}

using System;

namespace PharmacyAdmin.Model
{
    public class FedExPioneerRXDispenseModel
    {
        public int RecordID { get; set; }

        public Guid? RxID { get; set; }
        public int? RxNumber { get; set; }
        public Guid? PharmacistID { get; set; }
        public Guid? PatientID { get; set; }
        public Guid? PrescriberID { get; set; }
        public Guid? PrescribedItemID { get; set; }
	    public DateTime? DateWritten { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public Decimal? Quantity { get; set; }
        public DateTime? RxStatusChangedOn { get; set; }
        public string LegacyNumber { get; set; }
        public Guid? RxTransactionID { get; set; }
        public DateTime? DateFilled { get; set; }
        public Guid? PrimaryPatientPayMethodID { get; set; }
        public Guid? DispensedItemID { get; set; }
        public Decimal? DispensedQuantity { get; set; }
        public int? DaysSupply { get; set; }
        public Guid? PricingMethodID { get; set; }
        public int? RefillNumber { get; set; }
        public Guid? RxTransactionStatusTypeID { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Guid? ShipmentID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string SaleReceiptString { get; set; }
        public string TrackingNumber { get; set; }
        public string ShipperName { get; set; }
        public DateTime? PostingDate { get; set; }
        public int? TransactionStatusID { get; set; }
        public Guid? SignatureRawID { get; set; }
        public int?  CardTicketNumber { get; set; }
        public Guid? SignatureTypeID { get; set; }
        public int? IsDelivery { get; set; }
        public int? DeliveryStatusTypeID { get; set; }

        // public Decimal GrossProfitAmount { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public int? PatientSerialNumber { get; set; }
        public string PatientFullName { get; set; }
        public string DispensedItemNDC { get; set; }
        public string DispensedItemName { get; set; }
        public string DispensedItemNdcFormatted { get; set; }
        public int? DocumentDownloaded { get; set; }

        // From Pharmacy.Document
        public Guid? DocumentID { get; set; }
        public string RXNumber { get; set; }
        public DateTime? TOUCHDATE { get; set; }
        public DateTime? CREATEDON { get; set; }
        public string Code { get; set; }
        public string TrackCode { get; set; }
        public string TrackStatus { get; set; }
        public string ServiceInfo { get; set; }
        public string OtherIdentifier { get; set; }
        public string OtherIdentifierType { get; set; }
        public DateTime? DownloadDate { get; set; }
        public string DownloadPath { get; set; }
        public int DownloadMethod { get; set; }
        public string DownloadCode { get; set; }
        public int? DocumentCount { get; set; }
        public string TrackEvents { get; set; }
        public string ImageAvailability { get; set; }

        // Custom
        public bool RecordSelected { get; set; }
        public string UpdatedBy { get; set; } = "PharmacyAdmin";

        public static FedExPioneerRXDispenseModel Construct(string ShipmentTrackingNumber)
        {
            FedExPioneerRXDispenseModel entity = new FedExPioneerRXDispenseModel()
            {
                TrackingNumber = ShipmentTrackingNumber
            };

            //entity.Id = 0;
            //entity.IsLoadedFromDB = false;
            return entity;
        }

        public static FedExPioneerRXDispenseModel Construct(int rxNumber, DateTime DateFilled, string TrackingNumber)
        {
            FedExPioneerRXDispenseModel entity = new FedExPioneerRXDispenseModel()
            {
                RxNumber = rxNumber,
                DateFilled = DateFilled,
                TrackingNumber = TrackingNumber
            };

            //entity.Id = 0;
            //entity.IsLoadedFromDB = false;
            return entity;
        }

        public FedExPioneerRXDispenseModel()
        {
            //RecordID = dispense_prescription_sys_id.Value;
        }

        public string GetDocumentNote()
        {
            string note = string.Empty;
            if ((DocumentDownloaded.HasValue) && (DocumentDownloaded.Value == 0))
            {
                note = "Before May 2020";  // Document was before Mar 1, 2020 so we are not able to download those.
            }
            else if (!string.IsNullOrEmpty(Code) || (Code == "0"))
            {
                if (Code == "0")
                {
                    note = "";
                }
                else if (Code == "5508")
                {
                    note = "Invalid Tracking Number";
                }
                else if (Code == "5510")
                {
                    note = "Tracking Number Not Found";
                }
                else if (Code == "5534")
                {
                    //note = "Ground Track Failed."; // Error on FedEx side
                    note = "Wait 5 days. Document not available"; // Error on FedEx side
                }
                else if (Code == "5606")
                {
                    note = "Invalid Tracking Number";
                }
                else if (Code == "9006")
                {
                    note = "FedEx Service Unavailable";  // Unable to invoke method: <method_name>. Service is currently unavailable.
                }
                else
                {
                    note = "Refer to Error#" + Code + "-" + TrackCode;
                }
            }

            return note;
        }

    }
}

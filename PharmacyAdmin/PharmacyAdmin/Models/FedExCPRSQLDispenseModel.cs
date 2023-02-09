using System;

namespace PharmacyAdmin.Model
{
    public class FedExCPRSQLDispenseModel
    {
        public int RecordID { get; set; }
        public int RecordType { get; set; } = 0;
        public string CssClassName { get; set; }

        // Dispense
        public int? ncpdp_dispense_sys_id { get; set; } // LABLOGNO
        public int? dispense_prescription_sys_id { get; set; }
        public string rx_number { get; set; }
        public DateTime? dispense_date_timestamp { get; set; }
        public string refillnum { get; set; }

        // Patient
        public string mrn { get; set; }
        public string patient_first_name { get; set; }
        public string patient_last_name { get; set; }
        public string patient_full_name { get; set; }

        // Drug
        public string ndc { get; set; }
        public string drug_name { get; set; }

        // Ticket
        public int? ticket_original_ticket_sys_id { get; set; }
        public int? ticket_created_user_sys_id { get; set; }
        public string ticket_service_type { get; set; }
        public string ticket_shipping_method { get; set; }
        public string ticket_type { get; set; }
        public int? ticket_sys_id { get; set; } // TICKNO
        public string ticket_shipping_tracking_number { get; set; }
        public DateTime? ticket_confirmation_date { get; set; }
        
        public int? DocumentDownloaded { get; set; }

        // Additional Ticket Fields in vw_cube_ticket
        public int? ticket_number { get; set; }
        public string ticket_item_name { get; set; }

        public string ticket_stage { get; set; }
        public string ticket_invoice_number { get; set; }
        public string patient_mrn { get; set; }
        public string ticket_payor { get; set; }
        public string ticket_payor_type { get; set; }
        public string ticket_payor_pcn { get; set; }
        public string ticket_payor_binno { get; set; }
        public string inventory_ndc { get; set; }

        public string patient_team { get; set; }
        public string ticket_creator { get; set; }
        public string ticket_shipping_service_type { get; set; }
        public string ticket_shipping_package_cost { get; set; }
        public string ticket_ship_to_address { get; set; }
        public string ticket_ship_to_city { get; set; }
        public string ticket_ship_to_state { get; set; }
        public string ticket_ship_to_zip { get; set; }
        public string ticket_ship_to_phone { get; set; }

        // Redelivery and Partial Delivery Fields
        public string DELIVINS { get; set; }
        public int? DELIVINS_TICKNO { get; set; }  
        public int? DELIVINS_BILLNO { get; set; }

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
        public string DownloadPath { get; set; }
        public int DownloadMethod { get; set; }
        public string DownloadCode { get; set; }
        public int? DocumentCount { get; set; }
        public string TrackEvents { get; set; }

        public DateTime? DownloadDate { get; set; }
        public string ImageAvailability { get; set; }

        // Custom
        public string Note { get; set; }
        public bool RecordSelected { get; set; }
        public string UpdatedBy { get; set; } = "PharmacyAdmin";

        public FedExCPRSQLDispenseModel()
        {
            //RecordID = dispense_prescription_sys_id.Value;
        }

        public string GetDocumentNote()
        {
            if (!string.IsNullOrEmpty(Note)) return Note;

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

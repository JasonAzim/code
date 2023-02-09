using System;

namespace PharmacyAdmin.Model
{
    public class DispenseModel
    {
        public string RxTransactionID { get; set; }
        public string RxNumber { get; set; }
        public DateTime? DateFilled { get; set; }

        public int RefillNumber { get; set; }
        public int PatientSerialNumber { get; set; }
        public string DispensedItemNDC { get; set; }
        public string DispensedItemName { get; set; }
        public decimal? TotalPriceSubmitted { get; set; }
        public decimal? TotalPricePaid { get; set; }
        public decimal? PatientPaidAmount { get; set; }
        public decimal? AcquisitionCost { get; set; }
        public decimal? GrossProfit { get; set; }

        public string ticket_shipping_tracking_number { get; set; }
        // Custom Fields
        public string DatabaseName { get; set; }
        public bool RecordSelected { get; set; }
        public string UpdatedBy { get; set; } = "PharmacyAdmin"; // [UpatedBy]

        public DispenseModel()
        {
        }
    }
}
